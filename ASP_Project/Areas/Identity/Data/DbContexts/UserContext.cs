using ASP_Project.Areas.Identity.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ASP_Project.Areas.Identity.Data.DbContexts;

public class UserContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductVariation> ProductVariations { get; set; }
    public DbSet<Address> Addresses { get; set; }
    //public DbSet<Favorite> Favorites { get; set; }
    public DbSet<OnlinePayment> OnlinePayments { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<OrderStateHistory> OrderStateHistories { get; set; }
    public DbSet<ParentCategory> ParentCategories { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }

    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ParentCategory>(entity =>
        {
            entity.HasKey(pc => pc.Id);
            entity.Property(pc => pc.Id).ValueGeneratedOnAdd();
            entity.Property(pc => pc.Name).IsRequired().HasMaxLength(25);
            entity.HasIndex(pc => pc.Name).IsUnique();
        });
        
        builder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).ValueGeneratedOnAdd();
            entity.Property(c => c.Name).IsRequired().HasMaxLength(25);
            entity.HasIndex(c => c.Name).IsUnique();

            entity.HasOne(c => c.ParentCategory)
                .WithMany(pc => pc.Categories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        builder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
            entity.Property(p => p.Name).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Make).IsRequired().HasMaxLength(25);
            entity.Property(p => p.Gender).IsRequired().HasDefaultValue(Gender.Unisex);
            entity.Property(p => p.Fabric).HasMaxLength(100);
            entity.Property(p => p.Description).HasMaxLength(1000);
            
            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        builder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(pi => pi.Id);
            entity.Property(pi => pi.Id).ValueGeneratedOnAdd();
            entity.Property(pi => pi.Url).HasMaxLength(255);
            entity.Property(pi => pi.ImageData);
            
            entity.HasOne(pi => pi.ProductVariation)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductVariationId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        builder.Entity<ProductVariation>(entity =>
        {
            entity.HasKey(pv => pv.Id);
            entity.Property(pv => pv.Id).ValueGeneratedOnAdd();
            entity.Property(pv => pv.Name).IsRequired();
            entity.HasIndex(pv => pv.Name).IsUnique();
            entity.Property(pv => pv.Price).IsRequired();
            entity.Property(pv => pv.Size).IsRequired();
            entity.Property(pv => pv.Color).IsRequired();
            entity.Property(pv => pv.Discount).HasColumnType("smallint");

            entity.HasOne(pv => pv.Product)
                .WithMany(p => p.ProductVariations)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.FirstName).IsRequired();
            entity.Property(u => u.LastName).IsRequired();
            entity.Property(u => u.PhoneNumber);
            entity.HasIndex(u => u.PhoneNumber).IsUnique();
        });
        
        builder.Entity<Address>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).ValueGeneratedOnAdd();
            entity.Property(a => a.FirstName).IsRequired();
            entity.Property(a => a.LastName).IsRequired();
            entity.Property(a => a.PhoneNumber).IsRequired();
            entity.Property(a => a.Country).IsRequired();
            entity.Property(a => a.City).IsRequired();
            entity.Property(a => a.Region).IsRequired();
            entity.Property(a => a.StreetAddress).IsRequired();
            entity.Property(a => a.StreetAddressSecond);
            entity.Property(a => a.ZIP).IsRequired();
            
            entity.HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        builder.Entity<OnlinePayment>(entity =>
        {
            entity.HasKey(op => op.Id);
            entity.Property(op => op.Id).ValueGeneratedOnAdd();

            entity.Property(op => op.CardNumber).IsRequired().HasColumnType("nvarchar(16)");
            entity.Property(op => op.CardHolderName).IsRequired().HasMaxLength(255); 
            entity.Property(op => op.CardHolderSurname).IsRequired().HasMaxLength(255);
            entity.Property(op => op.ExpirationDate).IsRequired().HasMaxLength(5);
            entity.Property(op => op.CVV).IsRequired().HasMaxLength(3);

            entity.HasOne(op => op.User)
                .WithMany(u => u.OnlinePayments)
                .HasForeignKey(op => op.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // builder.Entity<Favorite>(entity =>
        // {
        //     entity.HasKey(f => f.UserId);
        //
        //     // entity.HasOne(f => f.User)
        //     //     .WithMany()
        //     //     .HasForeignKey(f => f.UserId);
        //
        //     entity.HasMany(f => f.Products)
        //         .WithOne()    //test
        //         .HasForeignKey(f => f.Id);
        // });

        builder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(sc => sc.UserId);
            entity.Property(sc => sc.TotalQuantity).IsRequired();
            entity.Property(sc => sc.TotalPrice).IsRequired();
            
            entity.HasOne(sc => sc.User)
                .WithOne(u => u.ShoppingCart)
                .HasForeignKey<ShoppingCart>(sc => sc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        builder.Entity<ShoppingCartItem>(entity =>
        {
            entity.HasKey(sci => sci.ProductVariationId);
            
            entity.Property(sci => sci.Quantity).IsRequired();
            entity.Property(sci => sci.TotalPrice).IsRequired();
            
            entity.HasOne(sci => sci.ProductVariation)
                .WithOne(pv => pv.ShoppingCartItem)
                .HasForeignKey<ShoppingCartItem>(sci => sci.ProductVariationId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(sci => sci.ShoppingCart)
                .WithMany(sc => sc.ShoppingCartItems)
                .HasForeignKey(sci => sci.ShoppingCartId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        builder.Entity<OrderStateHistory>(entity =>
        {
            entity.HasKey(osh => osh.Id);
            entity.Property(osh => osh.Id).ValueGeneratedOnAdd();
            entity.Property(osh => osh.State).IsRequired();
            entity.Property(osh => osh.DateChanged).IsRequired();
            
            entity.HasOne(osh => osh.OrderProduct)
                .WithMany(op => op.StateHistories)
                .HasForeignKey(osh => osh.OrderProductId);
        });
        
        builder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey(op => op.Id);
            entity.Property(op => op.Id).ValueGeneratedOnAdd();
            entity.Property(op => op.Quantity).IsRequired();
            entity.Property(op => op.TotalPrice).IsRequired();
            
            entity.HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            entity.HasOne(op => op.ShoppingCartItem)
                .WithOne() //.WithOne(sci => sci.OrderProduct)
                .HasForeignKey<OrderProduct>(sci => sci.ShoppingCartItemId);

            entity.HasMany(op => op.StateHistories)
                .WithOne(osh => osh.OrderProduct)
                .HasForeignKey(osh => osh.OrderProductId);
        });
        
        builder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Id).ValueGeneratedOnAdd();
            entity.Property(o => o.Date).IsRequired();
            entity.Property(o => o.TotalQuantity).IsRequired();
            entity.Property(o => o.TotalPrice).IsRequired();
            entity.Property(o => o.DeliveryState).IsRequired();
            
            entity.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);
        });
        
        base.OnModelCreating(builder);
        
    }
}
