using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class PizzashopContext : DbContext
{
    public PizzashopContext()
    {
    }

    public PizzashopContext(DbContextOptions<PizzashopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Customertable> Customertables { get; set; }

    public virtual DbSet<Favouriteitem> Favouriteitems { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Mainmodifier> Mainmodifiers { get; set; }

    public virtual DbSet<Modifier> Modifiers { get; set; }

    public virtual DbSet<Modifiersgroup> Modifiersgroups { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderitem> Orderitems { get; set; }

    public virtual DbSet<Ordermodifier> Ordermodifiers { get; set; }

    public virtual DbSet<Ordertable> Ordertables { get; set; }

    public virtual DbSet<Ordertaxis> Ordertaxes { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<Tax> Taxes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Waitinglist> Waitinglists { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseNpgsql("Host=localhost; Database=Pizzashop; Username=postgres;     password=Tatva@123 ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("orderstatus", new[] { "Pending", "Completed", "InProgress", "Running" })
            .HasPostgresEnum("paymentmode", new[] { "Completed", "Failed" })
            .HasPostgresEnum("paymentstatus", new[] { "UPI", "Card", "Cash", "Pending" })
            .HasPostgresEnum("role", new[] { "Chef", "AccountManager", "SuperAdmin" })
            .HasPostgresEnum("tablestatus", new[] { "Available", "Occupied" });


        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Decription)
                .HasMaxLength(100)
                .HasColumnName("decription");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("city_pkey");

            entity.ToTable("city");

            entity.Property(e => e.CityId)
                .ValueGeneratedNever()
                .HasColumnName("city_id");
            entity.Property(e => e.Cityname)
                .HasMaxLength(20)
                .HasColumnName("cityname");
            entity.Property(e => e.StateId).HasColumnName("state_id");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("city_state_id_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("country_pkey");

            entity.ToTable("country");

            entity.Property(e => e.CountryId)
                .ValueGeneratedNever()
                .HasColumnName("country_id");
            entity.Property(e => e.Countryname)
                .HasMaxLength(20)
                .HasColumnName("countryname");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.HasIndex(e => new { e.Email, e.Phone }, "customer_email_phone_key").IsUnique();

            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .HasColumnName("email");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.NoOfPerson).HasColumnName("no_of_person");
            entity.Property(e => e.Phone)
                .HasMaxLength(16)
                .HasColumnName("phone");
            entity.Property(e => e.TableId).HasColumnName("table_id");

            entity.HasOne(d => d.Table).WithMany(p => p.Customers)
                .HasForeignKey(d => d.TableId)
                .HasConstraintName("customer_table_id_fkey");
        });

        modelBuilder.Entity<Customertable>(entity =>
        {
            entity.HasKey(e => e.Customertableid).HasName("customertable_pkey");

            entity.ToTable("customertable");

            entity.Property(e => e.Customertableid).HasColumnName("customertableid");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.TableId).HasColumnName("table_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Customertables)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("customertable_customerid_fkey");

            entity.HasOne(d => d.Table).WithMany(p => p.Customertables)
                .HasForeignKey(d => d.TableId)
                .HasConstraintName("customertable_table_id_fkey");
        });

        modelBuilder.Entity<Favouriteitem>(entity =>
        {
            entity.HasKey(e => e.Favouriteid).HasName("favouriteitem_pkey");

            entity.ToTable("favouriteitem");

            entity.Property(e => e.Favouriteid).HasColumnName("favouriteid");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Item).WithMany(p => p.Favouriteitems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("favouriteitem_item_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Favouriteitems)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("favouriteitem_user_id_fkey");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("items_pkey");

            entity.ToTable("items");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Additionaltax)
                .HasPrecision(10, 2)
                .HasColumnName("additionaltax");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Code)
                .HasMaxLength(7)
                .HasColumnName("code");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Decription)
                .HasMaxLength(100)
                .HasColumnName("decription");
            entity.Property(e => e.Defaulttax).HasColumnName("defaulttax");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Isavailable)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("isavailable");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.ModifierId).HasColumnName("modifier_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasColumnName("type");
            entity.Property(e => e.Unit).HasColumnName("unit");

            entity.HasOne(d => d.Category).WithMany(p => p.Items)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("items_category_id_fkey");

            entity.HasOne(d => d.Modifier).WithMany(p => p.Items)
                .HasForeignKey(d => d.ModifierId)
                .HasConstraintName("items_modifier_id_fkey");
        });

        modelBuilder.Entity<Mainmodifier>(entity =>
        {
            entity.HasKey(e => e.MainmodifierId).HasName("mainmodifiers_pkey");

            entity.ToTable("mainmodifiers");

            entity.Property(e => e.MainmodifierId).HasColumnName("mainmodifier_id");
            entity.Property(e => e.ModifierId).HasColumnName("modifier_id");
            entity.Property(e => e.ModifiergroupId).HasColumnName("modifiergroup_id");

            entity.HasOne(d => d.Modifier).WithMany(p => p.Mainmodifiers)
                .HasForeignKey(d => d.ModifierId)
                .HasConstraintName("mainmodifiers_modifier_id_fkey");

            entity.HasOne(d => d.Modifiergroup).WithMany(p => p.Mainmodifiers)
                .HasForeignKey(d => d.ModifiergroupId)
                .HasConstraintName("mainmodifiers_modifiergroup_id_fkey");
        });

        modelBuilder.Entity<Modifier>(entity =>
        {
            entity.HasKey(e => e.ModifierId).HasName("modifiers_pkey");

            entity.ToTable("modifiers");

            entity.Property(e => e.ModifierId).HasColumnName("modifier_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Decription)
                .HasMaxLength(100)
                .HasColumnName("decription");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.ModifiergroupId).HasColumnName("modifiergroup_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Unit).HasColumnName("unit");

            entity.HasOne(d => d.Modifiergroup).WithMany(p => p.Modifiers)
                .HasForeignKey(d => d.ModifiergroupId)
                .HasConstraintName("modifiers_modifiergroup_id_fkey");
        });

        modelBuilder.Entity<Modifiersgroup>(entity =>
        {
            entity.HasKey(e => e.ModifiergroupId).HasName("modifiersgroup_pkey");

            entity.ToTable("modifiersgroup");

            entity.Property(e => e.ModifiergroupId)
                .ValueGeneratedNever()
                .HasColumnName("modifiergroup_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Decription)
                .HasMaxLength(100)
                .HasColumnName("decription");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");

            entity.HasOne(d => d.Category).WithMany(p => p.Modifiersgroups)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("modifiersgroup_category_id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Comment)
                .HasMaxLength(20)
                .HasColumnName("comment");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Orderdate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("orderdate");
            entity.Property(e => e.Paymentmode).HasColumnName("paymentmode");
            entity.Property(e => e.Reviewid).HasColumnName("reviewid");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.Totalamount)
                .HasPrecision(10, 2)
                .HasColumnName("totalamount");
            entity.Property(e => e.Status)
                   .HasColumnType("text")
                   .HasColumnName("status");
            entity.Property(e => e.Paymentmode)
                        .HasColumnType("text")
                        .HasColumnName("paymentmode");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("orders_customerid_fkey");

            entity.HasOne(d => d.Review).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Reviewid)
                .HasConstraintName("orders_reviewid_fkey");

            entity.HasOne(d => d.Table).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TableId)
                .HasConstraintName("orders_table_id_fkey");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.Orderitemid).HasName("orderitems_pkey");

            entity.ToTable("orderitems");

            entity.Property(e => e.Orderitemid).HasColumnName("orderitemid");
            entity.Property(e => e.Instructions)
                .HasMaxLength(20)
                .HasColumnName("instructions");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Readyitems).HasColumnName("readyitems");

            entity.HasOne(d => d.Item).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("orderitems_item_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("orderitems_order_id_fkey");
        });

        modelBuilder.Entity<Ordermodifier>(entity =>
        {
            entity.HasKey(e => e.Ordermodifierid).HasName("ordermodifiers_pkey");

            entity.ToTable("ordermodifiers");

            entity.Property(e => e.Ordermodifierid).HasColumnName("ordermodifierid");
            entity.Property(e => e.ModifierId).HasColumnName("modifier_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.HasOne(d => d.Modifier).WithMany(p => p.Ordermodifiers)
                .HasForeignKey(d => d.ModifierId)
                .HasConstraintName("ordermodifiers_modifier_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Ordermodifiers)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("ordermodifiers_order_id_fkey");
        });

        modelBuilder.Entity<Ordertable>(entity =>
        {
            entity.HasKey(e => e.Ordertableid).HasName("ordertable_pkey");

            entity.ToTable("ordertable");

            entity.Property(e => e.Ordertableid).HasColumnName("ordertableid");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Ordertables)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("ordertable_order_id_fkey");

            entity.HasOne(d => d.Table).WithMany(p => p.Ordertables)
                .HasForeignKey(d => d.TableId)
                .HasConstraintName("ordertable_table_id_fkey");
        });

        modelBuilder.Entity<Ordertaxis>(entity =>
        {
            entity.HasKey(e => e.Ordertaxid).HasName("ordertaxes_pkey");

            entity.ToTable("ordertaxes");

            entity.Property(e => e.Ordertaxid).HasColumnName("ordertaxid");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.TaxId).HasColumnName("tax_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Ordertaxes)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("ordertaxes_order_id_fkey");

            entity.HasOne(d => d.Tax).WithMany(p => p.Ordertaxes)
                .HasForeignKey(d => d.TaxId)
                .HasConstraintName("ordertaxes_tax_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("payment_pkey");

            entity.ToTable("payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(5, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Paymentmode)
                    .HasColumnType("text")
                    .HasColumnName("paymentMode");
            entity.Property(e => e.Paymentstatus)
                        .HasColumnType("text")
                        .HasColumnName("paymentStatus");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Paymentmode).HasColumnName("paymentmode");
            entity.Property(e => e.Paymentstatus).HasColumnName("paymentstatus");

            entity.HasOne(d => d.Customer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("payment_customerid_fkey");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PageName).HasName("permissions_pkey");

            entity.ToTable("permissions");

            entity.Property(e => e.PageName)
                .HasMaxLength(15)
                .HasColumnName("page_name");
            entity.Property(e => e.Candelete).HasColumnName("candelete");
            entity.Property(e => e.Canedit).HasColumnName("canedit");
            entity.Property(e => e.Canview)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("canview");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("permissions_role_id_fkey");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Reviewid).HasName("review_pkey");

            entity.ToTable("review");

            entity.Property(e => e.Reviewid).HasColumnName("reviewid");
            entity.Property(e => e.Comments).HasColumnName("comments");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Reviewfor)
                .HasMaxLength(20)
                .HasColumnName("reviewfor");
            entity.Property(e => e.Stars).HasColumnName("stars");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("review_customerid_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("role_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Profileimage).HasColumnName("profileimage");
            entity.Property(e => e.Role1).HasColumnName("role");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.SectionId).HasName("section_pkey");

            entity.ToTable("section");

            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Decription)
                .HasMaxLength(100)
                .HasColumnName("decription");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("states_pkey");

            entity.ToTable("states");

            entity.Property(e => e.StateId)
                .ValueGeneratedNever()
                .HasColumnName("state_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Statename)
                .HasMaxLength(20)
                .HasColumnName("statename");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("states_country_id_fkey");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("tables_pkey");

            entity.ToTable("tables");

            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Section).WithMany(p => p.Tables)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("tables_section_id_fkey");
        });

        modelBuilder.Entity<Tax>(entity =>
        {
            entity.HasKey(e => e.TaxId).HasName("tax_pkey");

            entity.ToTable("tax");

            entity.Property(e => e.TaxId).HasColumnName("tax_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Defaulttax).HasColumnName("defaulttax");
            entity.Property(e => e.Isenabled).HasColumnName("isenabled");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasColumnName("type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => new { e.Email, e.Phone }, "users_email_phone_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("first_name");
            entity.Property(e => e.Isactive)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("isactive");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .HasColumnName("last_name");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Password)
                .HasMaxLength(16)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(16)
                .HasColumnName("phone");
            entity.Property(e => e.Role).HasColumnName("role").HasColumnType("text");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasColumnName("username");
            entity.Property(e => e.Zipcode).HasColumnName("zipcode");

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("users_city_id_fkey");

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("users_country_id_fkey");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("users_role_id_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.Users)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("users_state_id_fkey");
        });

        modelBuilder.Entity<Waitinglist>(entity =>
        {
            entity.HasKey(e => e.Waitingid).HasName("waitinglist_pkey");

            entity.ToTable("waitinglist");

            entity.Property(e => e.Waitingid).HasColumnName("waitingid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Modifiedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.Waitingtime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("waitingtime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Waitinglists)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("waitinglist_customerid_fkey");

            entity.HasOne(d => d.Section).WithMany(p => p.Waitinglists)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("waitinglist_section_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
