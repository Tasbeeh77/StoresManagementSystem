using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EFProject
{
    public partial class showAllStore_Result
    {
        public int Store_Id { get; set; }
        public string Store_Name { get; set; }
        public string Address { get; set; }
        public string Manager_Name { get; set; }
        public string Item_Name { get; set; }
        public int Item_Total_Count { get; set; }
        public System.DateTime Prod_Date { get; set; }
        public System.DateTime Expire_Date { get; set; }
    }
    public partial class EFEntity : DbContext
    {
        public EFEntity()
            : base("name=EFEntity")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Items_Movement> Items_Movement { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Permissions_Items> Permissions_Items { get; set; }
        public virtual DbSet<Store_Items> Store_Items { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual ObjectResult<showAllStore_Result> showAllStore(string storename)
        {
            var storenameParameter = storename != null ?
                new ObjectParameter("storename", storename) :
                new ObjectParameter("storename", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<showAllStore_Result>("showAllStore", storenameParameter);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.Cust_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.EMail)
                .IsUnicode(false);

            modelBuilder.Entity<Measurement>()
                .Property(e => e.measure_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Permission>()
                .Property(e => e.Perm_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Items_Movement)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.FromStore_Id);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Items_Movement1)
                .WithRequired(e => e.Store1)
                .HasForeignKey(e => e.ToStore_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Sup_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.EMail)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Website)
                .IsUnicode(false);
        }
    }
}
