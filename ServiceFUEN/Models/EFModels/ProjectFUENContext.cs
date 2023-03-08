using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ServiceFUEN.Models.EFModels;

public partial class ProjectFUENContext : DbContext
{
    public ProjectFUENContext()
    {
    }

    public ProjectFUENContext(DbContextOptions<ProjectFUENContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<ActivityCategory> ActivityCategories { get; set; }

    public virtual DbSet<ActivityCollection> ActivityCollections { get; set; }

    public virtual DbSet<ActivityMember> ActivityMembers { get; set; }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<AlbumItem> AlbumItems { get; set; }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentReport> CommentReports { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<FollowInfo> FollowInfos { get; set; }

    public virtual DbSet<IndiscriminateReport> IndiscriminateReports { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OthersCollection> OthersCollections { get; set; }

    public virtual DbSet<OwnCollection> OwnCollections { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<PhotoReport> PhotoReports { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductPhoto> ProductPhotos { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<View> Views { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=ProjectFUEN;Trusted_Connection=False;Trust Server Certificate=true;User ID=ProjectLogIn;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activiti__3214EC07D71BECC3");

            entity.Property(e => e.ActivityName).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.CoverImage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateOfCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deadline).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.GatheringTime).HasColumnType("datetime");
            entity.Property(e => e.Recommendation).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Activities)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Activitie__Categ__34C8D9D1");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Activities)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Activitie__Istru__33D4B598");
        });

        modelBuilder.Entity<ActivityCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activity__3214EC071381514F");

            entity.HasIndex(e => e.DisplayOrder, "UQ__Activity__FB8517E6D7F2F089").IsUnique();

            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<ActivityCollection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activity__3214EC0782A7E881");

            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Activity).WithMany(p => p.ActivityCollections)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK__ActivityC__Activ__38996AB5");

            entity.HasOne(d => d.User).WithMany(p => p.ActivityCollections)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ActivityC__UserI__7A672E12");
        });

        modelBuilder.Entity<ActivityMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activity__3214EC076C12D4AC");

            entity.Property(e => e.DateJoined)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Activity).WithMany(p => p.ActivityMembers)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK__ActivityM__Activ__3D5E1FD2");

            entity.HasOne(d => d.Member).WithMany(p => p.ActivityMembers)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__ActivityM__Membe__7C4F7684");
        });

        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Albums__3214EC0722E27D62");

            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Member).WithMany(p => p.Albums)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__Albums__MemberId__7F2BE32F");
        });

        modelBuilder.Entity<AlbumItem>(entity =>
        {
            entity.HasKey(e => new { e.AlbumId, e.PhotoId }).HasName("PK__AlbumIte__A5AFC5691C30A08B");

            entity.Property(e => e.AddTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Album).WithMany(p => p.AlbumItems)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK__AlbumItem__Album__7D439ABD");

            entity.HasOne(d => d.Photo).WithMany(p => p.AlbumItems)
                .HasForeignKey(d => d.PhotoId)
                .HasConstraintName("FK__AlbumItem__Photo__7E37BEF6");
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Answers__3214EC07EDCABB5B");

            entity.Property(e => e.Content).HasMaxLength(300);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Answers__Questio__46E78A0C");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brands__3214EC0779D2979A");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07C32ECD63");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comments__3214EC0749449392");

            entity.Property(e => e.CommentTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Content).HasMaxLength(300);

            entity.HasOne(d => d.Member).WithMany(p => p.Comments)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__Comments__Member__6442E2C9");

            entity.HasOne(d => d.Photo).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PhotoId)
                .HasConstraintName("FK__Comments__PhotoI__65370702");
        });

        modelBuilder.Entity<CommentReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CommentR__3214EC0747F3DF76");

            entity.Property(e => e.ReportTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentReports)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FK__CommentRe__Comme__18B6AB08");

            entity.HasOne(d => d.ReporterNavigation).WithMany(p => p.CommentReports)
                .HasForeignKey(d => d.Reporter)
                .HasConstraintName("FK__CommentRe__Repor__02084FDA");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coupons__3214EC071D66F5CB");

            entity.Property(e => e.Code)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Discount).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Events__3214EC07FEDFFAE1");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.EventName).HasMaxLength(50);
            entity.Property(e => e.Photo)
                .HasMaxLength(32)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasMany(d => d.Products).WithMany(p => p.Events)
                .UsingEntity<Dictionary<string, object>>(
                    "EventItem",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__EventItem__Produ__05D8E0BE"),
                    l => l.HasOne<Event>().WithMany()
                        .HasForeignKey("EventId")
                        .HasConstraintName("FK__EventItem__Event__04E4BC85"),
                    j =>
                    {
                        j.HasKey("EventId", "ProductId").HasName("PK__EventIte__B204047C7B75EFC8");
                        j.ToTable("EventItems");
                    });
        });

        modelBuilder.Entity<FollowInfo>(entity =>
        {
            entity.HasKey(e => new { e.Follower, e.Following }).HasName("PK__FollowIn__512B98D21B71FC3E");

            entity.Property(e => e.FollowTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.FollowerNavigation).WithMany(p => p.FollowInfoFollowerNavigations)
                .HasForeignKey(d => d.Follower)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FollowInf__Follo__08B54D69");

            entity.HasOne(d => d.FollowingNavigation).WithMany(p => p.FollowInfoFollowingNavigations)
                .HasForeignKey(d => d.Following)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FollowInf__Follo__09A971A2");
        });

        modelBuilder.Entity<IndiscriminateReport>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Indiscri__0CF04B18E8BBAFC8");

            entity.Property(e => e.MemberId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Instruct__3214EC072DEB9982");

            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.InstructorName).HasMaxLength(50);
            entity.Property(e => e.ResumePhoto)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Members__3214EC079107B4D8");

            entity.HasIndex(e => e.EmailAccount, "UQ__Members__005407CD4FE3E788").IsUnique();

            entity.Property(e => e.About).HasMaxLength(500);
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.BirthOfDate).HasColumnType("date");
            entity.Property(e => e.ConfirmCode)
                .HasMaxLength(32)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.EmailAccount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EncryptedPassword)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Mobile)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NickName).HasMaxLength(50);
            entity.Property(e => e.PhotoSticker)
                .HasMaxLength(32)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.RealName).HasMaxLength(50);

            entity.HasMany(d => d.Coupons).WithMany(p => p.Members)
                .UsingEntity<Dictionary<string, object>>(
                    "UsedCoupon",
                    r => r.HasOne<Coupon>().WithMany()
                        .HasForeignKey("CouponId")
                        .HasConstraintName("FK__UsedCoupo__Coupo__1CBC4616"),
                    l => l.HasOne<Member>().WithMany()
                        .HasForeignKey("MemberId")
                        .HasConstraintName("FK__UsedCoupo__Membe__1DB06A4F"),
                    j =>
                    {
                        j.HasKey("MemberId", "CouponId").HasName("PK__UsedCoup__BF74E4032892A5E6");
                        j.ToTable("UsedCoupons");
                    });

            entity.HasMany(d => d.Products).WithMany(p => p.Members)
                .UsingEntity<Dictionary<string, object>>(
                    "Favorite",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__Favorites__Produ__07C12930"),
                    l => l.HasOne<Member>().WithMany()
                        .HasForeignKey("MemberId")
                        .HasConstraintName("FK__Favorites__Membe__06CD04F7"),
                    j =>
                    {
                        j.HasKey("MemberId", "ProductId").HasName("PK__Favorite__C7B0877483289A35");
                        j.ToTable("Favorites");
                    });
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC074F92AF7E");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentId)
                .HasMaxLength(50)
                .HasColumnName("paymentID");
            entity.Property(e => e.UsedCoupon).HasColumnName("usedCoupon");

            entity.HasOne(d => d.Member).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__OrderDeta__Membe__0A9D95DB");

            entity.HasOne(d => d.UsedCouponNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.UsedCoupon)
                .HasConstraintName("FK_OrderDetails_Coupons");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK__OrderIte__08D097A3BAD6A03A");

            entity.Property(e => e.ProductName).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderItem__Order__0B91BA14");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderItem__Produ__0C85DE4D");
        });

        modelBuilder.Entity<OthersCollection>(entity =>
        {
            entity.HasKey(e => new { e.MemberId, e.PhotoId }).HasName("PK__OthersCo__3EEB304637DC9079");

            entity.Property(e => e.AddTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.OthersCollections)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__OthersCol__Membe__0D7A0286");

            entity.HasOne(d => d.Photo).WithMany(p => p.OthersCollections)
                .HasForeignKey(d => d.PhotoId)
                .HasConstraintName("FK__OthersCol__Photo__0E6E26BF");
        });

        modelBuilder.Entity<OwnCollection>(entity =>
        {
            entity.HasKey(e => new { e.MemberId, e.PhotoId }).HasName("PK__OwnColle__3EEB3046EBD81066");

            entity.Property(e => e.AddTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.OwnCollections)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__OwnCollec__Membe__0F624AF8");

            entity.HasOne(d => d.Photo).WithMany(p => p.OwnCollections)
                .HasForeignKey(d => d.PhotoId)
                .HasConstraintName("FK__OwnCollec__Photo__10566F31");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Photos__3214EC07DDDA3C3A");

            entity.Property(e => e.Aperture)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Camera)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.Iso).HasColumnName("ISO");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Negative)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Pixel)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ShootingTime).HasColumnType("datetime");
            entity.Property(e => e.Shutter)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Source)
                .HasMaxLength(32)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UploadTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Photos)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Photos__Author__1332DBDC");
        });

        modelBuilder.Entity<PhotoReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhotoRep__3214EC0713229747");

            entity.Property(e => e.ReportTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Photo).WithMany(p => p.PhotoReports)
                .HasForeignKey(d => d.PhotoId)
                .HasConstraintName("FK__PhotoRepo__Photo__114A936A");

            entity.HasOne(d => d.ReporterNavigation).WithMany(p => p.PhotoReports)
                .HasForeignKey(d => d.Reporter)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__PhotoRepo__Repor__123EB7A3");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC079D9AFB71");

            entity.Property(e => e.ManufactorDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ProductSpec).HasMaxLength(500);
            entity.Property(e => e.ReleaseDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__BrandI__151B244E");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Catego__160F4887");
        });

        modelBuilder.Entity<ProductPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductP__3214EC0752351BB8");

            entity.Property(e => e.Source).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPhotos)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductPh__Produ__14270015");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC079D3C850B");

            entity.Property(e => e.Content).HasMaxLength(300);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Activity).WithMany(p => p.Questions)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK__Questions__Activ__4222D4EF");

            entity.HasOne(d => d.Member).WithMany(p => p.Questions)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__Questions__Membe__4316F928");
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => new { e.MemberId, e.ProductId }).HasName("PK__Shopping__C7B08774F54AED19");

            entity.HasOne(d => d.Member).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__ShoppingC__Membe__18EBB532");

            entity.HasOne(d => d.Product).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ShoppingC__Produ__19DFD96B");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tags__3214EC0771AF4F6F");

            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasMany(d => d.Photos).WithMany(p => p.Tags)
                .UsingEntity<Dictionary<string, object>>(
                    "TagItem",
                    r => r.HasOne<Photo>().WithMany()
                        .HasForeignKey("PhotoId")
                        .HasConstraintName("FK__TagItems__PhotoI__1AD3FDA4"),
                    l => l.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK__TagItems__TagId__1BC821DD"),
                    j =>
                    {
                        j.HasKey("TagId", "PhotoId").HasName("PK__TagItems__576782F2BE84ECDE");
                        j.ToTable("TagItems");
                    });
        });

        modelBuilder.Entity<View>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Views__3214EC07081BD2E4");

            entity.Property(e => e.ViewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.Views)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__Views__MemberId__1EA48E88");

            entity.HasOne(d => d.Photo).WithMany(p => p.Views)
                .HasForeignKey(d => d.PhotoId)
                .HasConstraintName("FK__Views__PhotoId__1F98B2C1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
