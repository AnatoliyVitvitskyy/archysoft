using System;
using System.Collections.Generic;
using System.Linq;
using Archysoft.D1.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Archysoft.D1.Data
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;

        public DatabaseInitializer(DataContext dataContext, UserManager<User> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }

        public void Initialize()
        {
            _dataContext.Database.Migrate();
            if (!_dataContext.Users.Any())
            {
                var users = new List<User>
                {
                    new User {
                        UserName = "admin",
                        Email = "admin@d1.archysoft.com",
                        EmailConfirmed = true,
                        Profile = new UserProfile {
                            FirstName = "John",
                            LastName = "Doe",
                            BirthDate = DateTime.Now.AddDays(-1)
                        }
                    },
                    new User {
                        UserName = "jane.doe",
                        Email = "jane.doe@d1.archysoft.com",
                        EmailConfirmed = true,
                        Profile = new UserProfile {
                            FirstName = "Jane",
                            LastName = "Doe",
                            BirthDate = DateTime.Now.AddDays(-2)
                        }
                    },
                    new User {
                        UserName = "john.smith",
                        Email = "john.smith@d1.archysoft.com",
                        EmailConfirmed = true,
                        Profile = new UserProfile {
                            FirstName = "John",
                            LastName = "Smith",
                            BirthDate = DateTime.Now.AddDays(-3)
                        }
                    }
                };

                foreach (var user in users) {
                    var result = _userManager.CreateAsync(user, "admin").Result;
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException();
                    }

                    var profile = new UserProfile
                    {
                        UserId = user.Id,
                        FirstName = user.Profile.FirstName,
                        LastName = user.Profile.LastName,
                        BirthDate = user.Profile.BirthDate
                    };

                    _dataContext.UserProfiles.Add(profile);
                }
                _dataContext.SaveChanges();
            }
        }
    }
}
