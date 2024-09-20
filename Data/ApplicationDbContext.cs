using BetaLogistics.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace BetaLogistics.Data
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions option) : base(option) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<LocationUpdate> LocationUpdates { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Vehicles
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { VehicleId = 1, LicensePlate = "BWC823", Model = "Truck" },
                new Vehicle { VehicleId = 2, LicensePlate = "XYZ789", Model = "Van" },
                new Vehicle { VehicleId = 3, LicensePlate = "DES439", Model = "Hummer Bus" }
            );

            
            modelBuilder.Entity<Customer>().HasData(
             new Customer { CustomerId = 1, Name = "Wunmi Martins", PhoneNumber = "09043214290", Address = "MAxwell,Calabar"},
             new Customer { CustomerId = 2, Name = "Goke Quadri", PhoneNumber = "080213434370", Address = "Iyanapaja,Lagos" },
             new Customer { CustomerId = 3, Name = "Madu Chinedu", PhoneNumber = "08125467893", Address = "Ikeja, Lagos" }
         );
            modelBuilder.Entity<Driver>().HasData(
             new Driver { DriverId = 1, Name = "Adefemi Adedayo", LicenseNumber = "D15445", VehicleId = 3 },
             new Driver { DriverId = 2, Name = "Azeez Babalola", LicenseNumber = "D67090", VehicleId = 2 },
             new Driver { DriverId = 3, Name = "Uche Phillips", LicenseNumber = "B47630", VehicleId = 1 }
         );

            modelBuilder.Entity<Shipment>().HasData(
                  new Shipment
                  {
                      ShipmentId = 1,
                      TrackingNumber = "TRK123456",
                      Origin = "Lagos",
                      Destination = "Port Harcourt",
                      Status = "In Transit",
                      EstimatedDelivery = DateTime.Now.AddDays(3),
                      VehicleId = 1,
                      CustomerId = 1,
                  },
                  new Shipment
                  {
                      ShipmentId = 2,
                      TrackingNumber = "TRK654321",
                      Origin = "Calabar",
                      Destination = "Lagos",
                      Status = "Delivered",
                      EstimatedDelivery = DateTime.Now.AddDays(-2),
                      VehicleId = 2,
                      CustomerId = 2
                  },
                   new Shipment
                   {
                       ShipmentId = 3,
                       TrackingNumber = "TRK654321",
                       Origin = "Lagos",
                       Destination = "Abuja",
                       Status = "In Transit",
                       EstimatedDelivery = DateTime.Now.AddDays(2),
                       VehicleId = 3,
                       CustomerId = 3,
                   }
              );

        }
    }
}
