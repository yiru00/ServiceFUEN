using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;
using System.Diagnostics.Metrics;
using System.Net;

namespace ServiceFUEN.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("AllowAny")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {

        private readonly ProjectFUENContext _context;

        public ShoppingCartController(ProjectFUENContext context)
        {

            _context = context;

        }

        [HttpGet("GetProducts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts()
        {
            ReturnVM rtn = new ReturnVM();
            try
            {
                var product = _context.Products.ToList();

                rtn.Code = (int)RetunCode.呼叫成功;
                rtn.Messsage = "成功取得商品";
                rtn.Data = product;

                return Ok(rtn);
            }
            catch (Exception ex)
            {
                rtn.Code = (int)RetunCode.呼叫失敗;
                rtn.Messsage = "其他錯誤";
                return BadRequest(rtn);
            }

        }

        [HttpGet("GetUserCart")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserCart(int userId)
        {
            ReturnVM rtn = new ReturnVM();
            try
            {
                var product = _context.Products.ToList();

                // 查詢此id所有購物車資料
                var shoppingCart = _context.ShoppingCarts
                    .Where(a => a.MemberId == userId)
                    .Include(a => a.Product)
                    .Select(a => new CartProduct
                    {
                        Id = a.ProductId,
                        Qty = a.Number,
                        Name = a.Product.Name,
                    })
                    .ToList();


                rtn.Code = (int)RetunCode.呼叫成功;
                rtn.Messsage = "成功取得商品";
                rtn.Data = shoppingCart;

                return Ok(rtn);
            }
            catch (Exception ex)
            {
                rtn.Code = (int)RetunCode.呼叫失敗;
                rtn.Messsage = "其他錯誤";
                return BadRequest(rtn);
            }

        }


        /// <summary>
        /// 接收購物車內容、算錢、存DB
        /// </summary>
        /// <param name="shoppingCartVM"></param>
        [HttpPost("SaveShoppingCart")]
        [AllowAnonymous]
        public async Task<IActionResult> SaveShoppingCart([Bind(nameof(ShoppingCartVM))] ShoppingCartVM shoppingCartVM)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                ReturnVM rtn = new ReturnVM();
                try
                {
                    // 商品(明細檔)
                    List<OrderItem> orderItemList = new List<OrderItem>();

                    // 是否有此會員
                    Member? member = _context.Members.Where(a => a.Id == shoppingCartVM.MemberId).FirstOrDefault();

                    if (member == null)
                    {
                        rtn.Code = (int)RetunCode.呼叫失敗;
                        rtn.Messsage = "無此會員";
                        return BadRequest(rtn);
                    }


				if (shoppingCartVM.CartProducts == null || shoppingCartVM.CartProducts.Length==0)
				{
					return null;

				}

                    if (shoppingCartVM.CartProducts == null || shoppingCartVM.CartProducts.Length == 0)
                    {
                        rtn.Code = (int)RetunCode.呼叫失敗;
                        rtn.Messsage = "購物車無商品";
                        return BadRequest(rtn);
                    }



                    // 存主檔
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        MemberId = member.Id,
                        // 因為Azure伺服器在美國 所以要以美國時間 +8hr
                        OrderDate = DateTime.UtcNow.AddHours(08),
                        Address = member.Address,
                        State = shoppingCartVM.State,
                    };

                    _context.OrderDetails.Add(orderDetail);
                    _context.SaveChanges();

                    // 抓已儲存主檔id
                    int orderDetailID = _context.OrderDetails
                        .Where(a =>
                            a.MemberId == orderDetail.MemberId &&
                            a.OrderDate == orderDetail.OrderDate &&
                            a.State == orderDetail.State
                        )
                        .Select(a => a.Id)
                        .FirstOrDefault();

                    if (orderDetailID == 0)
                    {
                        rtn.Code = (int)RetunCode.呼叫失敗;
                        rtn.Messsage = "未成功儲存ID";
                        return BadRequest(rtn);
                    }

                    foreach (var item in shoppingCartVM.CartProducts)
                    {
                        var product = _context.Products.Where(a => a.Id == item.Id).FirstOrDefault();

                        if (product == null)
                        {
                            rtn.Code = (int)RetunCode.呼叫失敗;
                            rtn.Messsage = "購物車商品不存在";
                            return BadRequest(rtn);
                        }

                        // 找到商品加入訂單明細檔
                        orderItemList.Add(new OrderItem
                        {
                            OrderId = orderDetailID,
                            ProductId = product.Id,
                            ProductName = product.Name,
                            ProductPrice = product.Price,
                            ProductNumber = item.Qty
                        });

                    }

                    _context.OrderItems.AddRange(orderItemList);
                    _context.SaveChanges();


                    // 刪購物車資料
                    var useCart = _context.ShoppingCarts.Where(a => a.MemberId == member.Id).ToList();

                    if (useCart.Count > 0)
                    {
                        _context.ShoppingCarts.RemoveRange(useCart);

                    }



                    transaction.Commit();

                    rtn.Code = (int)RetunCode.呼叫成功;
                    rtn.Messsage = "儲存成功";

                    return Ok(rtn);
                }
                catch (Exception ex)
                {
                    rtn.Code = (int)RetunCode.呼叫失敗;
                    rtn.Messsage = "其他錯誤";
                    transaction.Rollback();
                    return BadRequest(rtn);
                }
            }
        }


        [HttpPost("AddToCart/{memberID}")]
        public async Task<IActionResult> AddToCart([Bind(nameof(CartProduct))] CartProduct cartProduct, int memberID)
        {

            ReturnVM rtn = new ReturnVM();

            using (var transaction = _context.Database.BeginTransaction())
            {

                try
                {
                    if (cartProduct.Qty < 1)
                    {
                        rtn.Code = (int)RetunCode.呼叫失敗;
                        rtn.Messsage = "商品數量不得為0";
                        return BadRequest(rtn);
                    }

                    // 是否有此會員
                    Member? member = _context.Members.Where(a => a.Id == memberID).FirstOrDefault();

                    if (member == null)
                    {
                        rtn.Code = (int)RetunCode.呼叫失敗;
                        rtn.Messsage = "無此會員";
                        return BadRequest(rtn);
                    }

                    // 是否有此產品
                    Product? product = _context.Products.Where(a => a.Id == cartProduct.Id).FirstOrDefault();

                    if (product == null)
                    {
                        rtn.Code = (int)RetunCode.呼叫失敗;
                        rtn.Messsage = "無此商品";
                        return BadRequest(rtn);
                    }


                    // 查詢購物車table是否有此商品
                    ShoppingCart? shoppingCart = _context.ShoppingCarts
                        .Where(a => a.MemberId == member.Id && a.ProductId == cartProduct.Id)
                        .FirstOrDefault();

                    if (shoppingCart == null)
                    {
                        // 沒有寫一筆
                        _context.ShoppingCarts.Add(new ShoppingCart()
                        {
                            MemberId = member.Id,
                            ProductId = product.Id,
                            Number = cartProduct.Qty,
                        });
                    }
                    else
                    {
                        shoppingCart.MemberId = member.Id;
                        shoppingCart.ProductId = product.Id;
                        shoppingCart.Number = shoppingCart.Number += cartProduct.Qty;
                        _context.ShoppingCarts.Update(shoppingCart);
                    }

                    _context.SaveChanges();

                    // 查詢此id所有購物車資料
                    var cartUpdated = _context.ShoppingCarts
                        .Where(a => a.MemberId == member.Id)
                        .Include(a => a.Product)
                        .Select(a => new CartProduct
                        {
                            Id = a.ProductId,
                            Qty = a.Number,
                            Name = a.Product.Name,
                        })
                        .ToList();

                    rtn.Data = cartUpdated;
                    transaction.Commit();



                    rtn.Code = (int)RetunCode.呼叫成功;
                    rtn.Messsage = "加入成功";
                    return Ok(rtn);
                }
                catch (Exception ex)
                {
                    rtn.Code = (int)RetunCode.呼叫失敗;
                    rtn.Messsage = "其他錯誤";
                    transaction.Rollback();
                    return BadRequest(rtn);
                }

            }

        }


    }


}
