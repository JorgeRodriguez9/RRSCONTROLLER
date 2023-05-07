using System.Collections.Generic;
using System;
using RRSCONTROLLER.Models;
using Microsoft.EntityFrameworkCore;

namespace RRSCONTROLLER.DAL
{
    public class RSSCONTROLLERContext : DbContext
    {

        public RSSCONTROLLERContext(DbContextOptions<RSSCONTROLLERContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<USER>().HasIndex(u => u.Name_User).IsUnique();

            modelBuilder.Entity<NUTRITIONITS_INTS>()
             .HasOne(n => n.INSTITUTION)
             .WithMany(i => i.NUTRITIONITS_INTSs)
             .HasForeignKey(n => n.Id_Institution)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NUTRITIONITS_INTS>()
           .HasOne(n => n.USER)
           .WithMany()
           .HasForeignKey(n => n.Id_User)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SHIPMENT>()
            .HasOne(s => s.REQUEST)
            .WithMany()
            .HasForeignKey(s => s.Id_Request)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SECRETARY_INTS>()
           .HasOne(n => n.USER)
           .WithMany()
           .HasForeignKey(n => n.Id_User)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<USER>()
            .HasOne(u => u.SECRETARY_INTS)
            .WithOne(s => s.USER)
            .HasForeignKey<SECRETARY_INTS>(s => s.Id_User)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ADMIN_PAE>()
           .HasOne(u => u.USER)
           .WithOne(s => s.ADMIN_PAE)
           .HasForeignKey<ADMIN_PAE>(s => s.Id_User)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<USER>()
           .HasOne(u => u.NUTRITIONITS_INTS)
           .WithOne(s => s.USER)
           .HasForeignKey<NUTRITIONITS_INTS>(s => s.Id_User)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SECRETARY_INTS>()
            .HasOne(n => n.INSTITUTION)
            .WithMany(i => i.SECRETARY_INTSs)
            .HasForeignKey(n => n.Id_Institution)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FOOD_PRODUCT>()
            .HasOne(n => n.PRODUCT)
            .WithMany(i => i.FOOD_PRODUCTS)
            .HasForeignKey(n => n.Id_Product)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MENU_FOOD>()
           .HasOne(n => n.MENU)
           .WithMany(i => i.MENU_FOODS)
           .HasForeignKey(n => n.Id_Menu)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EVALUATION>()
          .HasOne(u => u.SHIPMENT)
          .WithOne(s => s.EVALUATION)
          .HasForeignKey<EVALUATION>(s => s.Id_Shipment)
          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<REQUEST_MENU>()
           .HasOne(n => n.REQUEST)
           .WithMany(i => i.REQUEST_MENUS)
           .HasForeignKey(n => n.Id_Request)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<REQUEST>()
            .HasOne(u => u.SHIPMENT)
            .WithOne(s => s.REQUEST)
            .HasForeignKey<SHIPMENT>(s => s.Id_Request)
            .OnDelete(DeleteBehavior.NoAction);

        }

        public DbSet<ADMIN_PAE> ADMIN_PAEs { get; set; }
        public DbSet<CATEGORY> CATEGORYS { get; set; }
        public DbSet<EVALUATION> EVALUATIONS { get; set; }
        public DbSet<FOOD> FOODS { get; set; }
        public DbSet<FOOD_PRODUCT> FOOD_PRODUCTS { get; set; }
        public DbSet<INSTITUTION> INSTITUTIONS { get; set; }
        public DbSet<MANAGER_PAE> MANAGER_PAEs { get; set; }
        public DbSet<MENU> MENUS { get; set; }
        public DbSet<MENU_FOOD> MENU_FOODS { get; set; }
        public DbSet<NUTRITIONITS_INTS> NUTRITIONITS_INTSs { get; set; }
        public DbSet<NUTRITIONITS_PAE> NUTRITIONITS_PAEs { get; set; }
        public DbSet<PRODUCT> PRODUCTS { get; set; }
        public DbSet<REQUEST> REQUESTS { get; set; }
        public DbSet<REQUEST_MENU> REQUEST_MENUS { get; set; }
        public DbSet<ROLE> ROLES { get; set; }
        public DbSet<SECRETARY_INTS> SECRETARY_INTSs { get; set; }
        public DbSet<SHIPMENT> SHIPMENTS { get; set; }
        public DbSet<SUPPLIER> SUPPLIERS { get; set; }
        public DbSet<UNIT> UNITS { get; set; }
        public DbSet<USER> USERS { get; set; }

        //ENUM CLASS TO AVOID HARDCODING
        public enum DiccionaryB
        {
            X,
            A,
            B,
            C,
            D,
            E
        }

    }
}