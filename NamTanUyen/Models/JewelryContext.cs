namespace NamTanUyen.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class JewelryContext : DbContext
    {
        public JewelryContext()
            : base("name=JewelryContext")
        {
        }
        
        public virtual DbSet<ProjectImage> ProjectImages { get; set; }
        public virtual DbSet<ViewCount> ViewCounts { get; set; }
        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<CommonInfo> CommonInfoes { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsCategory> NewsCategories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductColorImage> ProductColorImages { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductReview> ProductReviews { get; set; }
        public virtual DbSet<StaticPage> StaticPages { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }
        public virtual DbSet<ProductProductGroup> Product_ProductGroups { get; set; }
        public virtual DbSet<ProductProperty> Product_Properties { get; set; }
        public virtual DbSet<ProductObjectProperty> ProductObject_Property { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(e => e.TotalAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.SalePrice)
                .HasPrecision(19, 4);
        }

        public System.Data.Entity.DbSet<NamTanUyen.Models.Contact> Contacts { get; set; }

        public System.Data.Entity.DbSet<NamTanUyen.Models.ProductObject> ProductObjects { get; set; }

        public System.Data.Entity.DbSet<NamTanUyen.Models.Collection> Collections { get; set; }

        public System.Data.Entity.DbSet<NamTanUyen.Models.CollectionDetail> CollectionDetails { get; set; }



        public System.Data.Entity.DbSet<NamTanUyen.Models.Property> Properties { get; set; }

        public System.Data.Entity.DbSet<NamTanUyen.Models.PropertyDetail> PropertyDetails { get; set; }
    }
}
