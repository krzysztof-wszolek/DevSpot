using DevSpot.Constants;
using Microsoft.AspNetCore.Identity;

namespace DevSpot.Data
{
    public class UserSeeder
    {

        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var userMenager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            await CreateUserWithRole(userMenager, "admin@devspot.com", "Admin123!", Roles.Admin);
            await CreateUserWithRole(userMenager, "jobseeker@devspot.com", "JobSeeker123!", Roles.JobSeeker);
            await CreateUserWithRole(userMenager, "employer@devspot.com", "Employer123!", Roles.Employer);

        }
      public static async Task CreateUserWithRole(
          UserManager<IdentityUser> userMenager,
          string email,
          string password,
          string role)
        {

            if (await userMenager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };

                var result = await userMenager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userMenager.AddToRoleAsync(user,role);
                }
                else



                {
                    throw new Exception($"Failed creating user with Email: {user.Email}. Erros: {string.Join(",", result.Errors)}");

                }

            }

        }
          
    }

          
}
