using Clothing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Clothing.Infrastructure.Data
{
    public class ClothingDbContextInitilizer
    {
        private readonly ILogger<ClothingDbContextInitilizer> _logger;
        private readonly ClothingDBContext _context;

        public ClothingDbContextInitilizer(ILogger<ClothingDbContextInitilizer> logger, ClothingDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await SeedUsers();
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        private async Task SeedUsers()
        {
            var admin = new User()
            {

                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("pass@123"),
                IsActive = true,
                CreatedDate = DateTime.Now,


            };
        }
    }
}
