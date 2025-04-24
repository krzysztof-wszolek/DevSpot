using DevSpot.Constants;
using Microsoft.AspNetCore.Identity;

namespace DevSpot.Data
{
    public class RoleSeeder
    {

        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {

            var roleMenager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (! await roleMenager.RoleExistsAsync(Roles.Admin))
            {
                await roleMenager.CreateAsync(new IdentityRole(Roles.Admin));

            }

            if (!await roleMenager.RoleExistsAsync(Roles.JobSeeker))
            {
                await roleMenager.CreateAsync(new IdentityRole(Roles.JobSeeker));

            }

            if (!await roleMenager.RoleExistsAsync(Roles.Employer))
            {
                await roleMenager.CreateAsync(new IdentityRole(Roles.Employer));

            }

        }
    }
}
