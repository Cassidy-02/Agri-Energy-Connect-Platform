using Agri_Energy_Connect_Platform.Models;
using Microsoft.AspNetCore.Identity;

namespace Agri_Energy_Connect_Platform.Data
{
    public class DbInitializer
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Farmer", "Employee" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var email = "farmer1@example.com";
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {

                //seed farmer  
                var testUser = new ApplicationUser
                {
                    UserName = "farmer1@example.com",
                    Email = "farmer1@example.com",
                    Role = "Farmer",
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(testUser, "Farmer123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(testUser, "Farmer");
                    // Add matching Farmer record using the same user ID
                    var farmer = new Farmer
                    {
                        Id = testUser.Id, // Match Identity User ID
                        FullName = "John Doe",
                        Email = email,
                        Password = "Farmer123!", // Optional if not using
                        Phone = "123-456-7890",
                        Address = "123 Farm Lane, Countryside",
                        Products = new List<Product>() // Initialize the collection
                    };

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        context.Farmers.Add(farmer);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}


           
    

