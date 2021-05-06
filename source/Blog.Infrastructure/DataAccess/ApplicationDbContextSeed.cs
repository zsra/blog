using Blog.Core.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Blog.Infrastructure.DataAccess
{
    public class ApplicationDbContextSeed
    {
        internal static readonly string ID = "ADMIN_APP_USER_ID";
        internal static readonly string ADULT_ID = "ADULT_APP_USER_ID";
        internal static readonly string YOUNG_ID = "YOUNG_APP_USER_ID";
        internal static readonly string ELDER_ID = "ELDER_APP_USER_ID";

        public static async Task SeedAsync(ApplicationDbContext context,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                if (!await context.Users.AnyAsync())
                {
                    await context.Roles.AddAsync(new IdentityRole { Name = Roles.ADMIN, NormalizedName = Roles.ADMIN });
                    await context.Roles.AddAsync(new IdentityRole { Name = Roles.STANDARD, NormalizedName = Roles.STANDARD });
                    await context.SaveChangesAsync();

                    var user = new ApplicationUser
                    {
                        Id = ID,
                        UserName = "admin",
                        NormalizedUserName = "admin",
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(user, "admin");
                    user.PasswordHash = hashed;

                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();

                    await context.UserRoles.AddAsync(new IdentityUserRole<string>()
                    {
                        RoleId = context.Roles.FirstOrDefaultAsync().Result.Id,
                        UserId = user.Id
                    });
                    await context.SaveChangesAsync();
                }

#if DEBUG
                if (await context.Users.CountAsync() == 1)
                {
                    await AddTestUsers(context);
                }
#endif
            }
            catch (Exception e)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<ApplicationDbContext>();
                    log.LogError(e.Message);
                    await SeedAsync(context, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private async static Task AddTestUsers( ApplicationDbContext context )
        {
            var adult = new ApplicationUser
            {
                Id = ADULT_ID,
                UserName = "adult",
                NormalizedUserName = "adult",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(adult, "adult");
            adult.PasswordHash = hashed;

            await context.Users.AddAsync(adult);

            var young = new ApplicationUser
            {
                Id = YOUNG_ID,
                UserName = "young",
                NormalizedUserName = "young",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            password = new PasswordHasher<ApplicationUser>();
            hashed = password.HashPassword(young, "young");
            young.PasswordHash = hashed;

            await context.Users.AddAsync(young);

            var elder = new ApplicationUser
            {
                Id = ELDER_ID,
                UserName = "elder",
                NormalizedUserName = "elder",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            password = new PasswordHasher<ApplicationUser>();
            hashed = password.HashPassword(elder, "elder");
            elder.PasswordHash = hashed;

            await context.Users.AddAsync(elder);

            await context.SaveChangesAsync();
        }
    }
}
