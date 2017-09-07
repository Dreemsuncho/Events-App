using System.Linq;
using Microsoft.AspNetCore.Identity;

using Events.Data.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Events.Data
{
    public static class DbInitializer
    {
        public async static Task Initialize(UserManager<Account> userManager, EventsDbContext context)
        {
            await context.Database.MigrateAsync();

            if (!await context.AccountSet.AnyAsync())
            {
                var adminAccount = new Account
                {
                    FirstName = "Admin",
                    LastName = "Adminov",
                    UserName = "admin@admin.com",
                    EmailConfirmed = true
                };

                var account = new Account
                {
                    FirstName = "Elvis",
                    LastName = "Arabadjiyski",
                    UserName = "el@abv.bg",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminAccount, "admin");
                await userManager.CreateAsync(account, "123");

                var events = new[]
                {
                    new Event
                    {
                        Title = "Time for Thinking Algorithmic",
                        Description = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                        Author = account,
                        StartDate = DateTime.Now.AddMonths(1),
                        Duration = new TimeSpan(1 ,1 ,1),
                        Location = "Wooler NE71 6QS, UK",
                        IsPublic = true
                    },
                    new Event
                    {
                        Title = "Deep dive in JavaScript wold",
                        Description = "First class functions, callbacks, async functions, libraries IDE's, and ect. and ect.",
                        Author = account,
                        StartDate = DateTime.Now.AddMonths(1),
                        Duration = new TimeSpan(1 ,1 ,1),
                        Location = "London, United Kingdom",
                        IsPublic = true
                    },
                    new Event
                    {
                        Title = "ASP.NET Core Mvc",
                        Description = "Lets play little with asp.net core mvc, should be fun guys we welcome you.",
                        Author = adminAccount,
                        StartDate = DateTime.Now.AddMonths(2),
                        Duration = new TimeSpan(2, 2, 2),
                        Location = "California, United States",
                        IsPublic = true
                    },
                    new Event
                    {
                        Title = "AngularJS Fundamentals",
                        Description = "Getting started with AngularJS component base architecture, this event is bridge between AngularJS and Angular.",
                        Author= adminAccount,
                        StartDate = DateTime.Now.AddMonths(3),
                        Duration = new TimeSpan(3,3,3),
                        Location = "Sofiq Town - Arena Armeec",
                        IsPublic = true,
                        Comments = new HashSet<Comment>
                        {
                             new Comment{ Text = "This should be very cool event bro!", Author = account },
                             new Comment{ Text = "I really thing this should be very cool", Author = adminAccount }
                        }
                    },
                    new Event
                    {
                        Title = "Dead Men Tell No Tales’ World Premiere",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, ut labore et dolore magna aliqua.",
                        Author = adminAccount,
                        StartDate = DateTime.Now.AddMonths(4),
                        Location = "United Kingdom House, Winsley Street",
                        IsPublic = true,
                        Comments = new HashSet<Comment>
                        {
                            new Comment { Text = "This is fake comment from fake user" }
                        }
                    },
                    new Event
                    {
                        Title = "Money Mayweather vs Connor McGregor",
                        Description = "That shall be most expensive battle in the history of the universe, don't forget to come.",
                        Author = account,
                        Location = "Nevada 582, Paradise, NV, US",
                        StartDate = DateTime.Now.AddMonths(5),
                        IsPublic = true,
                        Comments = new HashSet<Comment>
                        {
                            new Comment { Text = "Whole life waiting for this battle", Author = account },
                            new Comment { Text = "I bet all the money that Mayweather will fall." }
                        }
                    },
                    new Event
                    {
                        Title = "Last upcoming event",
                        Description = "This is the last upcoming dummy event, just do not pay attention",
                        Author = adminAccount,
                        Location = "71 Pilgrim Avenue, MD 20815",
                        StartDate = DateTime.Now.AddMonths(6),
                        IsPublic = true,
                        Comments = new HashSet<Comment>
                        {
                            new Comment { Text = "Why should we write this comments, can not just have no comments?" }
                        }
                    },
                    new Event
                    {
                        Title = "First pass EV",
                        Description = "I can not think of a good description, so much I can",
                        Author = adminAccount,
                        Location = "4 Goldfield Rd.",
                        IsPublic = true,
                        Comments = new HashSet<Comment>
                        {
                            new Comment { Text = "It's all about money, do not trust them" }
                        }
                    },
                    new Event
                    {
                        Title = "ASP.NET Core Live demo",
                        Description = "Live demo that shows how to write your own custom authentication attribute.",
                        Author = adminAccount,
                        Location = "5B Gillygate, Pontefract WF8 1PH, UK",
                        IsPublic = true,
                        Comments = new HashSet<Comment>
                        {
                            new Comment { Text = "Simple mate I admire the use of hero and playfulness!", Author = account },
                            new Comment { Text = "This spaces has navigated right into my heart.", Author = adminAccount},
                            new Comment { Text = "My 19 year old son rates this work very fabulous.", Author = account },
                            new Comment { Text = "Let me take a nap... great illustration, anyway.", Author = adminAccount}
                        }
                    },
                    new Event
                    {
                        Title = "Such fun Event 12",
                        Description = "This colour palette has navigated right into my heart. It's magical not just slick!",
                        Author = adminAccount,
                        Location = "157-58 Fore St",
                        IsPublic = true,
                        Comments = new HashSet<Comment>
                        {
                            new Comment { Text = "My 25 year old son rates this shot very sublime dude", Author = account },
                            new Comment { Text = "I want to learn this kind of lines! Teach me.", Author = adminAccount},
                            new Comment { Text = "Whoa.", Author = account },
                            new Comment { Text = "Such shot, many style, so cool", Author = adminAccount}
                        }
                    },
                    new Event
                    {
                        Title = "Just passed event",
                        Description = "Dummy event for dummy people with dummy topics",
                        Author = adminAccount,
                        Duration = new TimeSpan(1, 1, 1),
                        Location = "NY 10163-4668, US",
                        IsPublic = true,
                        Comments = new HashSet<Comment>
                        {
                            new Comment { Text = "I shold get this event" },
                            new Comment { Text = "This is sux why this event is dummy" },
                            new Comment { Text = "Because is very stupid american guys" }
                        }
                    },
                    new Event
                    {
                        Title = "Lisp, Scheme and little Emacs",
                        Description = "Little emacs stuff, little lisp stuff and little fake event",
                        Author= account,
                        Duration = new TimeSpan(2,2,2),
                        Location = "Software university in bulgaria",
                        IsPublic = true,
                        Comments = new HashSet<Comment>
                        {
                             new Comment{ Text = "This should be very cool event bro!" },
                             new Comment{ Text = "I really want to coming get this event" },
                             new Comment{ Text = "This event has passed You can not go to a friend", Author = adminAccount }
                        }
                    },
                    new Event
                    {
                        Title = "Game Of Thrones",
                        Author = account,
                        Description = "John snow become programmer, the most exciting event ever isn't it?",
                        Location = "Winterfall",
                        IsPublic = true,
                        Comments = new HashSet<Comment>
                        {
                            new Comment { Text = "Why this event is so sux? Explain me", Author = adminAccount },
                            new Comment { Text = "Because is more fake than real event my friend :)", Author = account },
                            new Comment { Text = "Just anonymous comment, never mind" }
                        }
                    },
                    new Event
                    {
                        Title = "Last Passed",
                        Author = account,
                        Description = "Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur.",
                        Location = "19A Broad Gap, Bodicote, Banbury OX15 4DE, UK",
                        IsPublic = true,
                        Comments = new HashSet<Comment>
                        {
                            new Comment { Text = "Let me take a nap... great work, anyway.", Author = adminAccount },
                            new Comment { Text = "I'd love to see a video of how it works.", Author = account },
                            new Comment { Text = "I think I'm crying. It's that engaging." },
                            new Comment { Text = "Good, friend. I adore the use of typography and button!" },
                            new Comment { Text = "Mission accomplished. It's sublime." }
                        }
                    }
                };

                await context.EventSet.AddRangeAsync(events);
                await context.SaveChangesAsync();
            }
        }
    }
}
