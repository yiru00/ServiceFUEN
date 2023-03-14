import{a as n}from"./axios-aba6f0e0.js";import{_,f as g,u as m,b as v,o as c,c as d,d as t,t as l,G as u,F as f,e as I,p as S,g as y,i as b}from"./index-44f62c83.js";const P={mixins:[g],name:"DetailProduct",setup(){const e=m();return v(),{toSoppingCart:a=>{sessionStorage.setItem("productSelItem",JSON.stringify(a)),e.push("/ShoppingCart")}}},data(){return{detail:{},coverimg:"",picture:[],MId:0,PId:0,status:{},cartsSelect:[]}},created(){this.scrollToTop(),this.CallDetailProductsApi(),this.CallFavoritesStatus(),this.getStorageCart()},mounted(){this.GetMemberId()},methods:{async CallDetailProductsApi(){let e=this.$route.path.slice(9);this.PId=e,console.log(e),n.get(`https://localhost:7259/api/Product/DetailProducts?Id=${e}`).then(s=>{this.detail=s.data,this.picture=this.detail.source,this.coverimg=this.detail.source[0],this.picture=this.picture.slice(1),console.log(this.picture)}).catch(s=>{console.log(s)})},async GetMemberId(){this.MId=await this.fetchMemberId(),console.log(this.MId)},async CallProductFavorites(){await n.post(`https://localhost:7259/api/Favorites/ProductFavorites?memberId=${this.MId}&productId=${this.PId}`).then(e=>{console.log(e.data);let s=e.data;s.upshot&&(this.status.deleteId=s.deleteId,this.status.upshot=!0,this.showAlert(s.reply))}).catch(e=>{console.log(e)})},async CallUnFavorites(e){n.delete(`https://localhost:7259/api/favorites/unfavorites/${e}`).then(s=>{s&&(this.status.upshot=!1,this.status.deleteId=0,this.showAlert("取消收藏成功")),console.log(s)}).catch(s=>{console.log(s)})},async CallFavoritesStatus(){let e=await this.fetchMemberId();n.get(`https://localhost:7259/api/Favorites/FavoritesStatus?memberId=${e}&productId=${this.PId}`).then(s=>{this.status=s.data,console.log(this.status)}).catch(s=>{console.log(s)})},saveLocalStorage(e,s){localStorage.setItem(e,JSON.stringify(s))},getlocalStorage(e){this[e]=JSON.parse(localStorage.getItem(e))},getStorageCart(){this.getlocalStorage("cartsSelect"),this.cartsSelect||(this.cartsSelect=[])},buyDirectly(e){let s=this.cartsSelect.find(a=>a.Id==e.id);if(s){let a=this.cartsSelect.indexOf(s);this.cartsSelect[a].Qty++}else this.cartsSelect.push({Id:e.id,Qty:1,Name:e.name,Price:e.price,Cover:`https://localhost:7027/ProductImgFiles/${this.detail.source[0]}`});this.showAlert("加入成功"),this.saveLocalStorage("cartsSelect",this.cartsSelect)}}},i=e=>(S("data-v-391f50a8"),e=e(),y(),e),C={class:"container h-100 py-5"},k={class:"topProduct"},F={class:"ProName"},x={class:"ProName-P"},M={class:"Pro-lef d-flex"},w=["src"],D=i(()=>t("div",{class:"Pro-right"},"12加寬",-1)),N=i(()=>t("thead",null,[t("tr",null,[t("th",{class:"push-td"},[b("12345"),t("span",{class:"m-5 p-2"})])])],-1)),A={class:"cate-mt"},B=i(()=>t("td",null,"商品編號 :",-1)),O={class:"td-r"},T=i(()=>t("td",null,"商品類別 :",-1)),G={class:"td-r"},J=i(()=>t("td",null,"品牌 :",-1)),L={class:"td-r"},Q=i(()=>t("td",null,"庫存量 :",-1)),R={class:"td-r"},U=i(()=>t("td",null,"NTD :",-1)),V={class:"td-r"},j={class:"add-btn-buy"},E={class:"add-btn-like"},q={key:0,class:"fa-regular fa-star like-i","data-bs-toggle":"modal","data-bs-target":"#loginModal"},z=i(()=>t("div",{class:"TBborder"},[t("p",{class:"PBack"},"商品介紹")],-1)),H={class:"botproduct"},K=["src"],W={class:"spec-Out"},X={class:"spec-name"},Y={class:"Spec"};function Z(e,s,a,$,o,h){return c(),d("div",C,[t("div",k,[t("div",F,[t("p",x,l(o.detail.name),1)]),t("div",null,[t("div",M,[t("img",{class:"card-img",src:"https://localhost:7027/ProductImgFiles/"+o.coverimg,style:{width:"470px","object-fit":"cover"},alt:""},null,8,w),D,t("div",null,[t("table",null,[N,t("tbody",null,[t("tr",A,[B,t("td",O,l(o.detail.id),1)]),t("tr",null,[T,t("td",G,l(o.detail.categoryName),1)]),t("tr",null,[J,t("td",L,l(o.detail.brandName),1)]),t("tr",null,[Q,t("td",R,l(o.detail.inventory),1)]),t("tr",null,[U,t("td",V,l(o.detail.price),1)])])]),t("div",null,[t("button",{class:"add-btn",onClick:s[0]||(s[0]=u(r=>this.toSoppingCart(o.detail),["stop"]))}," 直接購買 ")]),t("div",null,[t("button",j,[t("i",{class:"fa-solid fa-cart-shopping buy-i",onClick:s[1]||(s[1]=u(r=>h.buyDirectly(o.detail),["stop"]))})]),t("button",E,[o.MId==0?(c(),d("i",q)):o.status.upshot?(c(),d("i",{key:2,onClick:s[3]||(s[3]=r=>h.CallUnFavorites(o.status.deleteId)),class:"fa-solid fa-star like-i"})):(c(),d("i",{key:1,onClick:s[2]||(s[2]=r=>h.CallProductFavorites()),class:"fa-regular fa-star like-i"}))])])])])])]),z,t("div",H,[(c(!0),d(f,null,I(o.picture,(r,p)=>(c(),d("div",{key:p},[t("img",{class:"card-img",src:"https://localhost:7027/ProductImgFiles/"+r,style:{width:"100%"},alt:""},null,8,K)]))),128)),t("div",W,[t("p",X,l(o.detail.name),1),t("p",Y," 商品介紹: "+l(o.detail.productSpec),1)])])])}const et=_(P,[["render",Z],["__scopeId","data-v-391f50a8"]]);export{et as default};
