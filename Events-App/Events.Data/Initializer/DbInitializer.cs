using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Events.Data.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Events.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(UserManager<Account> userManager, EventsDbContext context)
        {
            context.Database.EnsureCreated();

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
                await userManager.AddClaimsAsync(adminAccount, new[] { new Claim("Admins", "Administrator"), new Claim("Users", "User") });
                await userManager.CreateAsync(account, "123");
                await userManager.AddClaimAsync(account, new Claim("Users", "User"));

                var comments = new HashSet<Comment>
                {
                    new Comment { Author=account, Text = "This should be very cool event bro!" },
                    new Comment { Author=account, Text = "I really thing this should be very cool" },
                    new Comment { Author=account, Text = "This is fake comment from fake user"  },
                    new Comment { Author=account, Text = "Whole life waiting for this battle" },
                    new Comment { Author=account, Text = "I bet all the money that Mayweather will fall."  },
                    new Comment { Author=account, Text = "Why should we write this comments, can not just have no comments?" },
                    new Comment { Author=account, Text = "It's all about money, do not trust them"  },
                    new Comment { Author=account, Text = "Simple mate I admire the use of hero and playfulness!" },
                    new Comment { Author=account, Text = "This spaces has navigated right into my heart."},
                    new Comment { Author=account, Text = "My 19 year old son rates this work very fabulous." },
                    new Comment { Author=account, Text = "Let me take a nap... great illustration, anyway."},
                    new Comment { Author=adminAccount, Text = "My 25 year old son rates this shot very sublime dude" },
                    new Comment { Author=adminAccount, Text = "I want to learn this kind of lines! Teach me."},
                    new Comment { Author=adminAccount, Text = "Whoa." },
                    new Comment { Author=adminAccount, Text = "Such shot, many style, so cool"},
                    new Comment { Author=adminAccount, Text = "I shold get this event" },
                    new Comment { Author=adminAccount, Text = "This is sux why this event is dummy" },
                    new Comment { Author=adminAccount, Text = "Because is very stupid american guys" },
                    new Comment { Author=adminAccount, Text = "This should be very cool event bro!" },
                    new Comment { Author=adminAccount, Text = "I really want to coming get this event" },
                    new Comment { Author=adminAccount, Text = "This event has passed You can not go to a friend" },
                    new Comment { Author=adminAccount, Text = "Why this event is so sux? Explain me" },
                    new Comment { Author=adminAccount, Text = "Because is more fake than real event my friend :)" },
                    new Comment { Text = "Just anonymous comment, never mind" },
                    new Comment { Text = "Let me take a nap... great work, anyway." },
                    new Comment { Text = "I'd love to see a video of how it works." },
                    new Comment { Text = "I think I'm crying. It's that engaging." },
                    new Comment { Text = "Good, friend. I adore the use of typography and button!" },
                    new Comment { Text = "Mission accomplished. It's sublime." }
                };


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
                        Comments = comments.Take(2).ToArray()
                    },
                    new Event
                    {
                        Title = "Dead Men Tell No Tales’ World Premiere",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, ut labore et dolore magna aliqua.",
                        Author = adminAccount,
                        StartDate = DateTime.Now.AddMonths(4),
                        Location = "United Kingdom House, Winsley Street",
                        IsPublic = true,
                        Comments = comments.Skip(2).Take(1).ToArray()
                    },
                    new Event
                    {
                        Title = "Money Mayweather vs Connor McGregor",
                        Description = "That shall be most expensive battle in the history of the universe, don't forget to come.",
                        Author = account,
                        Location = "Nevada 582, Paradise, NV, US",
                        StartDate = DateTime.Now.AddMonths(5),
                        IsPublic = true,
                        Comments = comments.Skip(3).Take(2).ToArray()
                    },
                    new Event
                    {
                        Title = "Last upcoming event",
                        Description = "This is the last upcoming dummy event, just do not pay attention",
                        Author = adminAccount,
                        Location = "71 Pilgrim Avenue, MD 20815",
                        StartDate = DateTime.Now.AddMonths(6),
                        IsPublic = true,
                        Comments = comments.Skip(5).Take(1).ToArray()
                    },
                    new Event
                    {
                        Title = "First pass EV",
                        Description = "I can not think of a good description, so much I can",
                        Author = adminAccount,
                        Location = "4 Goldfield Rd.",
                        IsPublic = true,
                        Comments = comments.Skip(6).Take(1).ToArray()
                    },
                    new Event
                    {
                        Title = "ASP.NET Core Live demo",
                        Description = "Live demo that shows how to write your own custom authentication attribute.",
                        Author = adminAccount,
                        Location = "5B Gillygate, Pontefract WF8 1PH, UK",
                        IsPublic = true,
                        Comments = comments.Skip(7).Take(4).ToArray()
                    },
                    new Event
                    {
                        Title = "Such fun Event 12",
                        Description = "This colour palette has navigated right into my heart. It's magical not just slick!",
                        Author = adminAccount,
                        Location = "157-58 Fore St",
                        IsPublic = true,
                        Comments = comments.Skip(11).Take(4).ToArray()
                    },
                    new Event
                    {
                        Title = "Just passed event",
                        Description = "Dummy event for dummy people with dummy topics",
                        Author = adminAccount,
                        Duration = new TimeSpan(1, 1, 1),
                        Location = "NY 10163-4668, US",
                        IsPublic = true,
                        Comments = comments.Skip(15).Take(3).ToArray()
                    },
                    new Event
                    {
                        Title = "Lisp, Scheme and little Emacs",
                        Description = "Little emacs stuff, little lisp stuff and little fake event",
                        Author= account,
                        Duration = new TimeSpan(2,2,2),
                        Location = "Software university in bulgaria",
                        IsPublic = true,
                        Comments = comments.Skip(18).Take(3).ToArray()
                    },
                    new Event
                    {
                        Title = "Game Of Thrones",
                        Author = account,
                        Description = "John snow become programmer, the most exciting event ever isn't it?",
                        Location = "Winterfall",
                        IsPublic = true,
                        Comments = comments.Skip(21).Take(3).ToArray()
                    },
                    new Event
                    {
                        Title = "Last Passed",
                        Author = account,
                        Description = "Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur.",
                        Location = "19A Broad Gap, Bodicote, Banbury OX15 4DE, UK",
                        IsPublic = true,
                        Comments = comments.Skip(24).Take(5).ToArray()
                    }
                };

                await context.EventSet.AddRangeAsync(events);
                await context.SaveChangesAsync();
            }
        }
    }
}
