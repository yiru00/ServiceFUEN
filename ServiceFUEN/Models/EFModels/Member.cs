using System;
using System.Collections.Generic;

namespace ServiceFUEN.Models.EFModels;

public partial class Member
{
    public int Id { get; set; }

    public string EmailAccount { get; set; } = null!;

    public string EncryptedPassword { get; set; } = null!;

    public string? RealName { get; set; }

    public string? NickName { get; set; }

    public DateTime? BirthOfDate { get; set; }

    public string? Mobile { get; set; }

    public string? Address { get; set; }

    public string? PhotoSticker { get; set; }

    public string? About { get; set; }

    public string? ConfirmCode { get; set; }

    public bool IsConfirmed { get; set; }

    public bool IsInBlackList { get; set; }

    public virtual ICollection<ActivityCollection> ActivityCollections { get; } = new List<ActivityCollection>();

    public virtual ICollection<ActivityMember> ActivityMembers { get; } = new List<ActivityMember>();

    public virtual ICollection<Album> Albums { get; } = new List<Album>();

    public virtual ICollection<CommentReport> CommentReports { get; } = new List<CommentReport>();

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<FollowInfo> FollowInfoFollowerNavigations { get; } = new List<FollowInfo>();

    public virtual ICollection<FollowInfo> FollowInfoFollowingNavigations { get; } = new List<FollowInfo>();

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual ICollection<OthersCollection> OthersCollections { get; } = new List<OthersCollection>();

    public virtual ICollection<OwnCollection> OwnCollections { get; } = new List<OwnCollection>();

    public virtual ICollection<PhotoReport> PhotoReports { get; } = new List<PhotoReport>();

    public virtual ICollection<Photo> Photos { get; } = new List<Photo>();

    public virtual ICollection<Question> Questions { get; } = new List<Question>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; } = new List<ShoppingCart>();

    public virtual ICollection<View> Views { get; } = new List<View>();

    public virtual ICollection<Coupon> Coupons { get; } = new List<Coupon>();

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
