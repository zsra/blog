using Blog.Core.Entities;
using Blog.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Infrastructure.DataAccess
{
    public class BlogDbContextSeed
    {

        public static async Task SeedAsync(BlogDbContext context,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                if (!await context.Users.AnyAsync())
                {


                    await context.Users.AddAsync(new User {
                        FirstName = "admin",
                        LastName = "admin",
                        AgeGroup = AgeGroup.DEFAULT,
                        ApplicationUserId = ApplicationDbContextSeed.ID,
                        Username = "admin",
                    });
                    await context.SaveChangesAsync();
                }

#if DEBUG
                if (await context.Users.CountAsync() == 1)
                {
                    await AddTestValues(context);
                }
#endif
            }
            catch (Exception e)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<BlogDbContext>();
                    log.LogError(e.Message);
                    await SeedAsync(context, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private async static Task AddTestValues(BlogDbContext context)
        {
            User adult = new()
            {
                AgeGroup = AgeGroup.ADULT,
                ApplicationUserId = ApplicationDbContextSeed.ADULT_ID,
                FirstName = "f_adult",
                LastName = "l_adult",
                Username = "adult",
                Posts = new List<Post>
                {
                    new Post
                    {
                        Body = "Lorum ipsum",
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                Name = "Sport",
                                SerializedAgeGroups = (new AgeGroup[] { AgeGroup.ADULT }).ToJson(),
                            }
                        },
                        Title = "test",
                        Comments = new List<Comment>
                        {
                            new Comment
                            {
                                CreatedAt = DateTime.Now,
                                Body = "test comment",
                                Comments = new List<Comment>
                                {
                                    new Comment
                                    {
                                        CreatedAt = DateTime.Now,
                                        Body = "test comment 2",
                                    }
                                }
                            }
                        }
                    },
                    new Post
                    {
                        Body = "Lorum ipsum 2",
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                Name = "Public",
                                SerializedAgeGroups = (new AgeGroup[] { AgeGroup.ADULT, AgeGroup.ELDER }).ToJson(),
                            }
                        }
                    },
                }
            };
            await context.Users.AddAsync(adult);

            User young = new()
            {
                AgeGroup = AgeGroup.YOUNG,
                ApplicationUserId = ApplicationDbContextSeed.YOUNG_ID,
                FirstName = "f_young",
                LastName = "l_young",
                Username = "young",
                Posts = new List<Post>
                {
                    new Post
                    {
                        Body = "Lorum ipsum",
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                Name = "Game",
                                SerializedAgeGroups = (new AgeGroup[] { AgeGroup.YOUNG }).ToJson(),
                            }
                        }
                    },
                    new Post
                    {
                        Body = "Lorum ipsum 2",
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                Name = "School",
                                SerializedAgeGroups = (new AgeGroup[] { AgeGroup.ADULT, AgeGroup.YOUNG }).ToJson(),
                            }
                        }
                    },
                }
            };
            await context.Users.AddAsync(young);

            User elder = new()
            {
                AgeGroup = AgeGroup.ELDER,
                ApplicationUserId = ApplicationDbContextSeed.ELDER_ID,
                FirstName = "f_elder",
                LastName = "l_elder",
                Username = "elder",
                Posts = new List<Post>
                {
                    new Post
                    {
                        Body = "Lorum ipsum",
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                Name = "Church",
                                SerializedAgeGroups = (new AgeGroup[] { AgeGroup.ELDER }).ToJson(),
                            }
                        }
                    },
                    new Post
                    {
                        Body = "Lorum ipsum 2",
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                Name = "Politics",
                                SerializedAgeGroups = (new AgeGroup[] { AgeGroup.ADULT, AgeGroup.ELDER }).ToJson(),
                            }
                        }
                    },
                }
            };
            await context.Users.AddAsync(elder);

            await context.SaveChangesAsync();
        }
    }
}
