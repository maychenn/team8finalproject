using team8finalproject.Models;
using team8finalproject.DAL;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace team8finalproject.Seeding
{
    public static class SeedCustomers
    {
        public static async Task SeedAllCustomers(IServiceProvider serviceProvider)
        {
            AppDbContext _db = serviceProvider.GetRequiredService<AppDbContext>();
            UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            AppUser newUser1 = _db.Users.FirstOrDefault(u => u.Email == "cbaker@freezer.co.uk");

            if (newUser1 == null)
            {
                newUser1 = new AppUser();
                newUser1.Email = "cbaker@freezer.co.uk";
                newUser1.FirstName = "Christopher";
                newUser1.LastName = "Baker";
                newUser1.MiddleInitial = "L";
                newUser1.StreetAddress = "1245 Lake Austin Blvd.";
                newUser1.City = "Austin";
                newUser1.State = "TX";
                newUser1.ZipCode = "78733";
                newUser1.PhoneNumber = "5125571146";
                newUser1.Birthdate = new DateTime(1991, 2, 7);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser1, "gazing");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser1 = _db.Users.FirstOrDefault(u => u.UserName == "cbaker@freezer.co.uk");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser1, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser1, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser2 = _db.Users.FirstOrDefault(u => u.Email == "mb@aool.com");

            if (newUser2 == null)
            {
                newUser2 = new AppUser();
                newUser2.Email = "mb@aool.com";
                newUser2.FirstName = "Michelle";
                newUser2.LastName = "Banks";
                newUser2.MiddleInitial = "1300";
                newUser2.StreetAddress = "Tall Pine Lane";
                newUser2.City = "San Antonio";
                newUser2.State = "TX";
                newUser2.ZipCode = "78261";
                newUser2.PhoneNumber = "2102678873";
                newUser2.Birthdate = new DateTime(1990, 6, 23);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser2, "banquet");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser2 = _db.Users.FirstOrDefault(u => u.UserName == "mb@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser2, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser2, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser3 = _db.Users.FirstOrDefault(u => u.Email == "fd@aool.com");

            if (newUser3 == null)
            {
                newUser3 = new AppUser();
                newUser3.Email = "fd@aool.com";
                newUser3.FirstName = "Franco";
                newUser3.LastName = "Broccolo";
                newUser3.MiddleInitial = "V";
                newUser3.StreetAddress = "62 Browning Rd";
                newUser3.City = "Houston";
                newUser3.State = "TX";
                newUser3.ZipCode = "77019";
                newUser3.PhoneNumber = "8175659699";
                newUser3.Birthdate = new DateTime(1986, 5, 6);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser3, "666666");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser3 = _db.Users.FirstOrDefault(u => u.UserName == "fd@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser3, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser3, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser4 = _db.Users.FirstOrDefault(u => u.Email == "wendy@ggmail.com");

            if (newUser4 == null)
            {
                newUser4 = new AppUser();
                newUser4.Email = "wendy@ggmail.com";
                newUser4.FirstName = "Wendy";
                newUser4.LastName = "Chang";
                newUser4.MiddleInitial = "L";
                newUser4.StreetAddress = "202 Bellmont Hall";
                newUser4.City = "Austin";
                newUser4.State = "TX";
                newUser4.ZipCode = "78713";
                newUser4.PhoneNumber = "5125943222";
                newUser4.Birthdate = new DateTime(1964, 12, 21);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser4, "clover");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser4 = _db.Users.FirstOrDefault(u => u.UserName == "wendy@ggmail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser4, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser4, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser5 = _db.Users.FirstOrDefault(u => u.Email == "limchou@yaho.com");

            if (newUser5 == null)
            {
                newUser5 = new AppUser();
                newUser5.Email = "limchou@yaho.com";
                newUser5.FirstName = "Lim";
                newUser5.LastName = "Chou";
                newUser5.MiddleInitial = "";
                newUser5.StreetAddress = "1600 Teresa Lane";
                newUser5.City = "San Antonio";
                newUser5.State = "TX";
                newUser5.ZipCode = "78266";
                newUser5.PhoneNumber = "2107724599";
                newUser5.Birthdate = new DateTime(1950, 6, 14);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser5, "austin");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser5 = _db.Users.FirstOrDefault(u => u.UserName == "limchou@yaho.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser5, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser5, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser6 = _db.Users.FirstOrDefault(u => u.Email == "Dixon@aaol.com");

            if (newUser6 == null)
            {
                newUser6 = new AppUser();
                newUser6.Email = "Dixon@aaol.com";
                newUser6.FirstName = "Shan";
                newUser6.LastName = "Dixon";
                newUser6.MiddleInitial = "D";
                newUser6.StreetAddress = "234 Holston Circle";
                newUser6.City = "Dallas";
                newUser6.State = "TX";
                newUser6.ZipCode = "75208";
                newUser6.PhoneNumber = "2142643255";
                newUser6.Birthdate = new DateTime(1930, 5, 9);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser6, "mailbox");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser6 = _db.Users.FirstOrDefault(u => u.UserName == "Dixon@aaol.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser6, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser6, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser7 = _db.Users.FirstOrDefault(u => u.Email == "louann@ggmail.com");

            if (newUser7 == null)
            {
                newUser7 = new AppUser();
                newUser7.Email = "louann@ggmail.com";
                newUser7.FirstName = "Lou Ann";
                newUser7.LastName = "Feeley";
                newUser7.MiddleInitial = "K";
                newUser7.StreetAddress = "600 S 8th Street W";
                newUser7.City = "Houston";
                newUser7.State = "TX";
                newUser7.ZipCode = "77010";
                newUser7.PhoneNumber = "8172556749";
                newUser7.Birthdate = new DateTime(1930, 2, 24);


               

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser7, "aggies");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser7 = _db.Users.FirstOrDefault(u => u.UserName == "louann@ggmail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser7, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser7, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser8 = _db.Users.FirstOrDefault(u => u.Email == "tfreeley@minntonka.ci.state.mn.us");

            if (newUser8 == null)
            {
                newUser8 = new AppUser();
                newUser8.Email = "tfreeley@minntonka.ci.state.mn.us";
                newUser8.FirstName = "Tesa";
                newUser8.LastName = "Freeley";
                newUser8.MiddleInitial = "P";
                newUser8.StreetAddress = "4448 Fairview Ave.";
                newUser8.City = "Houston";
                newUser8.State = "TX";
                newUser8.ZipCode = "77009";
                newUser8.PhoneNumber = "8173255687";
                newUser8.Birthdate = new DateTime(1935, 9, 1);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser8, "raiders");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser8 = _db.Users.FirstOrDefault(u => u.UserName == "tfreeley@minntonka.ci.state.mn.us");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser8, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser8, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser9 = _db.Users.FirstOrDefault(u => u.Email == "mgar@aool.com");

            if (newUser9 == null)
            {
                newUser9 = new AppUser();
                newUser9.Email = "mgar@aool.com";
                newUser9.FirstName = "Margaret";
                newUser9.LastName = "Garcia";
                newUser9.MiddleInitial = "L";
                newUser9.StreetAddress = "594 Longview";
                newUser9.City = "Houston";
                newUser9.State = "TX";
                newUser9.ZipCode = "77003";
                newUser9.PhoneNumber = "8176593544";
                newUser9.Birthdate = new DateTime(1990, 7, 3);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser9, "mustangs");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser9 = _db.Users.FirstOrDefault(u => u.UserName == "mgar@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser9, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser9, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser10 = _db.Users.FirstOrDefault(u => u.Email == "chaley@thud.com");

            if (newUser10 == null)
            {
                newUser10 = new AppUser();
                newUser10.Email = "chaley@thud.com";
                newUser10.FirstName = "Charles";
                newUser10.LastName = "Haley";
                newUser10.MiddleInitial = "E";
                newUser10.StreetAddress = "One Cowboy Pkwy";
                newUser10.City = "Dallas";
                newUser10.State = "TX";
                newUser10.ZipCode = "75261";
                newUser10.PhoneNumber = "2148475583";
                newUser10.Birthdate = new DateTime(1985, 9, 17);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser10, "region");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser10 = _db.Users.FirstOrDefault(u => u.UserName == "chaley@thud.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser10, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser10, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser11 = _db.Users.FirstOrDefault(u => u.Email == "jeff@ggmail.com");

            if (newUser11 == null)
            {
                newUser11 = new AppUser();
                newUser11.Email = "jeff@ggmail.com";
                newUser11.FirstName = "Jeffrey";
                newUser11.LastName = "Hampton";
                newUser11.MiddleInitial = "T";
                newUser11.StreetAddress = "337 38th St.";
                newUser11.City = "Austin";
                newUser11.State = "TX";
                newUser11.ZipCode = "78705";
                newUser11.PhoneNumber = "5126978613";
                newUser11.Birthdate = new DateTime(1995, 1, 23);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser11, "hungry");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser11 = _db.Users.FirstOrDefault(u => u.UserName == "jeff@ggmail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser11, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser11, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser12 = _db.Users.FirstOrDefault(u => u.Email == "wjhearniii@umch.edu");

            if (newUser12 == null)
            {
                newUser12 = new AppUser();
                newUser12.Email = "wjhearniii@umch.edu";
                newUser12.FirstName = "John";
                newUser12.LastName = "Hearn";
                newUser12.MiddleInitial = "B";
                newUser12.StreetAddress = "4225 North First";
                newUser12.City = "Dallas";
                newUser12.State = "TX";
                newUser12.ZipCode = "75237";
                newUser12.PhoneNumber = "2148965621";
                newUser12.Birthdate = new DateTime(1994, 1, 8);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser12, "logicon");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser12 = _db.Users.FirstOrDefault(u => u.UserName == "wjhearniii@umch.edu");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser12, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser12, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser13 = _db.Users.FirstOrDefault(u => u.Email == "hicks43@ggmail.com");

            if (newUser13 == null)
            {
                newUser13 = new AppUser();
                newUser13.Email = "hicks43@ggmail.com";
                newUser13.FirstName = "Anthony";
                newUser13.LastName = "Hicks";
                newUser13.MiddleInitial = "J";
                newUser13.StreetAddress = "32 NE Garden Ln.; Ste 910";
                newUser13.City = "San Antonio";
                newUser13.State = "TX";
                newUser13.ZipCode = "78239";
                newUser13.PhoneNumber = "2105788965";
                newUser13.Birthdate = new DateTime(1990, 10, 6);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser13, "doofus");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser13 = _db.Users.FirstOrDefault(u => u.UserName == "hicks43@ggmail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser13, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser13, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser14 = _db.Users.FirstOrDefault(u => u.Email == "bradsingram@mall.utexas.edu");

            if (newUser14 == null)
            {
                newUser14 = new AppUser();
                newUser14.Email = "bradsingram@mall.utexas.edu";
                newUser14.FirstName = "Brad";
                newUser14.LastName = "Ingram";
                newUser14.MiddleInitial = "S";
                newUser14.StreetAddress = "6548 La Pasada Ct.";
                newUser14.City = "Austin";
                newUser14.State = "TX";
                newUser14.ZipCode = "78736";
                newUser14.PhoneNumber = "5124678821";
                newUser14.Birthdate = new DateTime(1984, 4, 12);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser14, "mother");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser14 = _db.Users.FirstOrDefault(u => u.UserName == "bradsingram@mall.utexas.edu");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser14, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser14, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser15 = _db.Users.FirstOrDefault(u => u.Email == "mother.Ingram@aool.com");

            if (newUser15 == null)
            {
                newUser15 = new AppUser();
                newUser15.Email = "mother.Ingram@aool.com";
                newUser15.FirstName = "Todd";
                newUser15.LastName = "Jacobs";
                newUser15.MiddleInitial = "L";
                newUser15.StreetAddress = "4564 Elm St.";
                newUser15.City = "Austin";
                newUser15.State = "TX";
                newUser15.ZipCode = "78731";
                newUser15.PhoneNumber = "5124653365";
                newUser15.Birthdate = new DateTime(1983, 4, 4);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser15, "whimsical");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser15 = _db.Users.FirstOrDefault(u => u.UserName == "mother.Ingram@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser15, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser15, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser16 = _db.Users.FirstOrDefault(u => u.Email == "victoria@aool.com");

            if (newUser16 == null)
            {
                newUser16 = new AppUser();
                newUser16.Email = "victoria@aool.com";
                newUser16.FirstName = "Victoria";
                newUser16.LastName = "Lawrence";
                newUser16.MiddleInitial = "M";
                newUser16.StreetAddress = "6639 Butterfly Ln.";
                newUser16.City = "Austin";
                newUser16.State = "TX";
                newUser16.ZipCode = "78761";
                newUser16.PhoneNumber = "5129457399";
                newUser16.Birthdate = new DateTime(1961, 2, 3);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser16, "nothing");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser16 = _db.Users.FirstOrDefault(u => u.UserName == "victoria@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser16, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser16, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser17 = _db.Users.FirstOrDefault(u => u.Email == "lineback@flush.net");

            if (newUser17 == null)
            {
                newUser17 = new AppUser();
                newUser17.Email = "lineback@flush.net";
                newUser17.FirstName = "Erik";
                newUser17.LastName = "Lineback";
                newUser17.MiddleInitial = "W";
                newUser17.StreetAddress = "1300 Netherland St";
                newUser17.City = "San Antonio";
                newUser17.State = "TX";
                newUser17.ZipCode = "78293";
                newUser17.PhoneNumber = "2102449976";
                newUser17.Birthdate = new DateTime(1946, 9, 3);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser17, "GoodFellow");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser17 = _db.Users.FirstOrDefault(u => u.UserName == "lineback@flush.net");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser17, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser17, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser18 = _db.Users.FirstOrDefault(u => u.Email == "elowe@netscrape.net");

            if (newUser18 == null)
            {
                newUser18 = new AppUser();
                newUser18.Email = "elowe@netscrape.net";
                newUser18.FirstName = "Ernest";
                newUser18.LastName = "Lowe";
                newUser18.MiddleInitial = "S";
                newUser18.StreetAddress = "3201 Pine Drive";
                newUser18.City = "San Antonio";
                newUser18.State = "TX";
                newUser18.ZipCode = "78279";
                newUser18.PhoneNumber = "2105344627";
                newUser18.Birthdate = new DateTime(1992, 2, 7);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser18, "impede");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser18 = _db.Users.FirstOrDefault(u => u.UserName == "elowe@netscrape.net");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser18, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser18, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser19 = _db.Users.FirstOrDefault(u => u.Email == "luce_chuck@ggmail.com");

            if (newUser19 == null)
            {
                newUser19 = new AppUser();
                newUser19.Email = "luce_chuck@ggmail.com";
                newUser19.FirstName = "Chuck";
                newUser19.LastName = "Luce";
                newUser19.MiddleInitial = "B";
                newUser19.StreetAddress = "2345 Rolling Clouds";
                newUser19.City = "San Antonio";
                newUser19.State = "TX";
                newUser19.ZipCode = "78268";
                newUser19.PhoneNumber = "2106983548";
                newUser19.Birthdate = new DateTime(1942, 10, 25);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser19, "LuceyDacey");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser19 = _db.Users.FirstOrDefault(u => u.UserName == "luce_chuck@ggmail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser19, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser19, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser20 = _db.Users.FirstOrDefault(u => u.Email == "mackcloud@pimpdaddy.com");

            if (newUser20 == null)
            {
                newUser20 = new AppUser();
                newUser20.Email = "mackcloud@pimpdaddy.com";
                newUser20.FirstName = "Jennifer";
                newUser20.LastName = "MacLeod";
                newUser20.MiddleInitial = "D";
                newUser20.StreetAddress = "2504 Far West Blvd.";
                newUser20.City = "Austin";
                newUser20.State = "TX";
                newUser20.ZipCode = "78731";
                newUser20.PhoneNumber = "5124748138";
                newUser20.Birthdate = new DateTime(1965, 8, 6);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser20, "cloudyday");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser20 = _db.Users.FirstOrDefault(u => u.UserName == "mackcloud@pimpdaddy.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser20, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser20, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser21 = _db.Users.FirstOrDefault(u => u.Email == "liz@ggmail.com");

            if (newUser21 == null)
            {
                newUser21 = new AppUser();
                newUser21.Email = "liz@ggmail.com";
                newUser21.FirstName = "Elizabeth";
                newUser21.LastName = "Markham";
                newUser21.MiddleInitial = "P";
                newUser21.StreetAddress = "7861 Chevy Chase";
                newUser21.City = "Austin";
                newUser21.State = "TX";
                newUser21.ZipCode = "78732";
                newUser21.PhoneNumber = "5124579845";
                newUser21.Birthdate = new DateTime(1959, 4, 13);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser21, "emarkbark");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser21 = _db.Users.FirstOrDefault(u => u.UserName == "liz@ggmail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser21, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser21, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser22 = _db.Users.FirstOrDefault(u => u.Email == "mclarence@aool.com");

            if (newUser22 == null)
            {
                newUser22 = new AppUser();
                newUser22.Email = "mclarence@aool.com";
                newUser22.FirstName = "Clarence";
                newUser22.LastName = "Martin";
                newUser22.MiddleInitial = "A";
                newUser22.StreetAddress = "87 Alcedo St.";
                newUser22.City = "Houston";
                newUser22.State = "TX";
                newUser22.ZipCode = "77045";
                newUser22.PhoneNumber = "8174955201";
                newUser22.Birthdate = new DateTime(1990, 1, 6);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser22, "smartinmartin");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser22 = _db.Users.FirstOrDefault(u => u.UserName == "mclarence@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser22, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser22, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser23 = _db.Users.FirstOrDefault(u => u.Email == "smartinmartin.Martin@aool.com");

            if (newUser23 == null)
            {
                newUser23 = new AppUser();
                newUser23.Email = "smartinmartin.Martin@aool.com";
                newUser23.FirstName = "Gregory";
                newUser23.LastName = "Martinez";
                newUser23.MiddleInitial = "R";
                newUser23.StreetAddress = "8295 Sunset Blvd.";
                newUser23.City = "Houston";
                newUser23.State = "TX";
                newUser23.ZipCode = "77030";
                newUser23.PhoneNumber = "8178746718";
                newUser23.Birthdate = new DateTime(1987, 10, 9);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser23, "looter");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser23 = _db.Users.FirstOrDefault(u => u.UserName == "smartinmartin.Martin@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser23, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser23, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser24 = _db.Users.FirstOrDefault(u => u.Email == "cmiller@mapster.com");

            if (newUser24 == null)
            {
                newUser24 = new AppUser();
                newUser24.Email = "cmiller@mapster.com";
                newUser24.FirstName = "Charles";
                newUser24.LastName = "Miller";
                newUser24.MiddleInitial = "R";
                newUser24.StreetAddress = "8962 Main St.";
                newUser24.City = "Houston";
                newUser24.State = "TX";
                newUser24.ZipCode = "77031";
                newUser24.PhoneNumber = "8177458615";
                newUser24.Birthdate = new DateTime(1984, 7, 21);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser24, "chucky33");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser24 = _db.Users.FirstOrDefault(u => u.UserName == "cmiller@mapster.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser24, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser24, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser25 = _db.Users.FirstOrDefault(u => u.Email == "nelson.Kelly@aool.com");

            if (newUser25 == null)
            {
                newUser25 = new AppUser();
                newUser25.Email = "nelson.Kelly@aool.com";
                newUser25.FirstName = "Kelly";
                newUser25.LastName = "Nelson";
                newUser25.MiddleInitial = "T";
                newUser25.StreetAddress = "2601 Red River";
                newUser25.City = "Austin";
                newUser25.State = "TX";
                newUser25.ZipCode = "78703";
                newUser25.PhoneNumber = "5122926966";
                newUser25.Birthdate = new DateTime(1956, 7, 4);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser25, "orange");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser25 = _db.Users.FirstOrDefault(u => u.UserName == "nelson.Kelly@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser25, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser25, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser26 = _db.Users.FirstOrDefault(u => u.Email == "jojoe@ggmail.com");

            if (newUser26 == null)
            {
                newUser26 = new AppUser();
                newUser26.Email = "jojoe@ggmail.com";
                newUser26.FirstName = "Joe";
                newUser26.LastName = "Nguyen";
                newUser26.MiddleInitial = "C";
                newUser26.StreetAddress = "1249 4th SW St.";
                newUser26.City = "Dallas";
                newUser26.State = "TX";
                newUser26.ZipCode = "75238";
                newUser26.PhoneNumber = "2143125897";
                newUser26.Birthdate = new DateTime(1963, 1, 29);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser26, "victorious");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser26 = _db.Users.FirstOrDefault(u => u.UserName == "jojoe@ggmail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser26, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser26, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser27 = _db.Users.FirstOrDefault(u => u.Email == "orielly@foxnets.com");

            if (newUser27 == null)
            {
                newUser27 = new AppUser();
                newUser27.Email = "orielly@foxnets.com";
                newUser27.FirstName = "Bill";
                newUser27.LastName = "O'Reilly";
                newUser27.MiddleInitial = "T";
                newUser27.StreetAddress = "8800 Gringo Drive";
                newUser27.City = "San Antonio";
                newUser27.State = "TX";
                newUser27.ZipCode = "78260";
                newUser27.PhoneNumber = "2103450925";
                newUser27.Birthdate = new DateTime(1983, 1, 7);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser27, "billyboy");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser27 = _db.Users.FirstOrDefault(u => u.UserName == "orielly@foxnets.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser27, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser27, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser28 = _db.Users.FirstOrDefault(u => u.Email == "or@aool.com");

            if (newUser28 == null)
            {
                newUser28 = new AppUser();
                newUser28.Email = "or@aool.com";
                newUser28.FirstName = "Anka";
                newUser28.LastName = "Radkovich";
                newUser28.MiddleInitial = "L";
                newUser28.StreetAddress = "1300 Elliott Pl";
                newUser28.City = "Dallas";
                newUser28.State = "TX";
                newUser28.ZipCode = "75260";
                newUser28.PhoneNumber = "2142345566";
                newUser28.Birthdate = new DateTime(1980, 3, 31);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser28, "radicalone");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser28 = _db.Users.FirstOrDefault(u => u.UserName == "or@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser28, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser28, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser29 = _db.Users.FirstOrDefault(u => u.Email == "megrhodes@freezing.co.uk");

            if (newUser29 == null)
            {
                newUser29 = new AppUser();
                newUser29.Email = "megrhodes@freezing.co.uk";
                newUser29.FirstName = "Megan";
                newUser29.LastName = "Rhodes";
                newUser29.MiddleInitial = "C";
                newUser29.StreetAddress = "4587 Enfield Rd.";
                newUser29.City = "Austin";
                newUser29.State = "TX";
                newUser29.ZipCode = "78707";
                newUser29.PhoneNumber = "5123744746";
                newUser29.Birthdate = new DateTime(1944, 8, 12);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser29, "gohorns");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser29 = _db.Users.FirstOrDefault(u => u.UserName == "megrhodes@freezing.co.uk");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser29, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser29, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser30 = _db.Users.FirstOrDefault(u => u.Email == "erynrice@aool.com");

            if (newUser30 == null)
            {
                newUser30 = new AppUser();
                newUser30.Email = "erynrice@aool.com";
                newUser30.FirstName = "Eryn";
                newUser30.LastName = "Rice";
                newUser30.MiddleInitial = "M";
                newUser30.StreetAddress = "3405 Rio Grande";
                newUser30.City = "Austin";
                newUser30.State = "TX";
                newUser30.ZipCode = "78705";
                newUser30.PhoneNumber = "5123876657";
                newUser30.Birthdate = new DateTime(1934, 8, 2);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser30, "iloveme");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser30 = _db.Users.FirstOrDefault(u => u.UserName == "erynrice@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser30, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser30, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser31 = _db.Users.FirstOrDefault(u => u.Email == "jorge@hootmail.com");

            if (newUser31 == null)
            {
                newUser31 = new AppUser();
                newUser31.Email = "jorge@hootmail.com";
                newUser31.FirstName = "Jorge";
                newUser31.LastName = "Rodriguez";
                newUser31.MiddleInitial = "";
                newUser31.StreetAddress = "6788 Cotter Street";
                newUser31.City = "Houston";
                newUser31.State = "TX";
                newUser31.ZipCode = "77057";
                newUser31.PhoneNumber = "8178904374";
                newUser31.Birthdate = new DateTime(1989, 8, 11);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser31, "greedy");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser31 = _db.Users.FirstOrDefault(u => u.UserName == "jorge@hootmail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser31, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser31, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser32 = _db.Users.FirstOrDefault(u => u.Email == "ra@aoo.com");

            if (newUser32 == null)
            {
                newUser32 = new AppUser();
                newUser32.Email = "ra@aoo.com";
                newUser32.FirstName = "Allen";
                newUser32.LastName = "Rogers";
                newUser32.MiddleInitial = "B";
                newUser32.StreetAddress = "4965 Oak Hill";
                newUser32.City = "Austin";
                newUser32.State = "TX";
                newUser32.ZipCode = "78732";
                newUser32.PhoneNumber = "5128752943";
                newUser32.Birthdate = new DateTime(1967, 8, 27);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser32, "familiar");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser32 = _db.Users.FirstOrDefault(u => u.UserName == "ra@aoo.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser32, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser32, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser33 = _db.Users.FirstOrDefault(u => u.Email == "st-jean@home.com");

            if (newUser33 == null)
            {
                newUser33 = new AppUser();
                newUser33.Email = "st-jean@home.com";
                newUser33.FirstName = "Olivier";
                newUser33.LastName = "Saint-Jean";
                newUser33.MiddleInitial = "M";
                newUser33.StreetAddress = "255 Toncray Dr.";
                newUser33.City = "San Antonio";
                newUser33.State = "TX";
                newUser33.ZipCode = "78292";
                newUser33.PhoneNumber = "2104145678";
                newUser33.Birthdate = new DateTime(1950, 7, 8);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser33, "historical");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser33 = _db.Users.FirstOrDefault(u => u.UserName == "st-jean@home.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser33, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser33, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser34 = _db.Users.FirstOrDefault(u => u.Email == "ss34@ggmail.com");

            if (newUser34 == null)
            {
                newUser34 = new AppUser();
                newUser34.Email = "ss34@ggmail.com";
                newUser34.FirstName = "Sarah";
                newUser34.LastName = "Saunders";
                newUser34.MiddleInitial = "J";
                newUser34.StreetAddress = "332 Avenue C";
                newUser34.City = "Austin";
                newUser34.State = "TX";
                newUser34.ZipCode = "78705";
                newUser34.PhoneNumber = "5123497810";
                newUser34.Birthdate = new DateTime(1977, 10, 29);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser34, "guiltless");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser34 = _db.Users.FirstOrDefault(u => u.UserName == "ss34@ggmail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser34, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser34, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser35 = _db.Users.FirstOrDefault(u => u.Email == "willsheff@email.com");

            if (newUser35 == null)
            {
                newUser35 = new AppUser();
                newUser35.Email = "willsheff@email.com";
                newUser35.FirstName = "William";
                newUser35.LastName = "Sewell";
                newUser35.MiddleInitial = "T";
                newUser35.StreetAddress = "2365 51st St.";
                newUser35.City = "Austin";
                newUser35.State = "TX";
                newUser35.ZipCode = "78709";
                newUser35.PhoneNumber = "5124510084";
                newUser35.Birthdate = new DateTime(1941, 4, 21);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser35, "frequent");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser35 = _db.Users.FirstOrDefault(u => u.UserName == "willsheff@email.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser35, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser35, "Customer");
            }

            //save changes
            _db.SaveChanges();
            AppUser newUser36 = _db.Users.FirstOrDefault(u => u.Email == "sheff44@ggmail.com");

            if (newUser36 == null)
            {
                newUser36 = new AppUser();
                newUser36.Email = "sheff44@ggmail.com";
                newUser36.FirstName = "Martin";
                newUser36.LastName = "Sheffield";
                newUser36.MiddleInitial = "J";
                newUser36.StreetAddress = "3886 Avenue A";
                newUser36.City = "Austin";
                newUser36.State = "TX";
                newUser36.ZipCode = "78705";
                newUser36.PhoneNumber = "5125479167";
                newUser36.Birthdate = new DateTime(1937, 11, 10);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser36, "history");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser36 = _db.Users.FirstOrDefault(u => u.UserName == "sheff44@ggmail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser36, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser36, "Customer");
            }

            //save changes
            _db.SaveChanges();
            AppUser newUser37 = _db.Users.FirstOrDefault(u => u.Email == "johnsmith187@aool.com");

            if (newUser37 == null)
            {
                newUser37 = new AppUser();
                newUser37.Email = "johnsmith187@aool.com";
                newUser37.FirstName = "John";
                newUser37.LastName = "Smith";
                newUser37.MiddleInitial = "A";
                newUser37.StreetAddress = "23 Hidden Forge Dr.";
                newUser37.City = "San Antonio";
                newUser37.State = "TX";
                newUser37.ZipCode = "78280";
                newUser37.PhoneNumber = "2108321888";
                newUser37.Birthdate = new DateTime(1954, 10, 26);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser37, "squirrel");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser37 = _db.Users.FirstOrDefault(u => u.UserName == "johnsmith187@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser37, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser37, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser38 = _db.Users.FirstOrDefault(u => u.Email == "dustroud@mail.com");

            if (newUser38 == null)
            {
                newUser38 = new AppUser();
                newUser38.Email = "dustroud@mail.com";
                newUser38.FirstName = "Dustin";
                newUser38.LastName = "Stroud";
                newUser38.MiddleInitial = "P";
                newUser38.StreetAddress = "1212 Rita Rd";
                newUser38.City = "Dallas";
                newUser38.State = "TX";
                newUser38.ZipCode = "75221";
                newUser38.PhoneNumber = "2142346667";
                newUser38.Birthdate = new DateTime(1932, 9, 1);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser38, "snakes");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser38 = _db.Users.FirstOrDefault(u => u.UserName == "dustroud@mail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser38, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser38, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser39 = _db.Users.FirstOrDefault(u => u.Email == "ericstuart@aool.com");

            if (newUser39 == null)
            {
                newUser39 = new AppUser();
                newUser39.Email = "ericstuart@aool.com";
                newUser39.FirstName = "Eric";
                newUser39.LastName = "Stuart";
                newUser39.MiddleInitial = "D";
                newUser39.StreetAddress = "5576 Toro Ring";
                newUser39.City = "Austin";
                newUser39.State = "TX";
                newUser39.ZipCode = "78746";
                newUser39.PhoneNumber = "5128178335";
                newUser39.Birthdate = new DateTime(1930, 12, 28);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser39, "landus");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser39 = _db.Users.FirstOrDefault(u => u.UserName == "ericstuart@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser39, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser39, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser40 = _db.Users.FirstOrDefault(u => u.Email == "peterstump@hootmail.com");

            if (newUser40 == null)
            {
                newUser40 = new AppUser();
                newUser40.Email = "peterstump@hootmail.com";
                newUser40.FirstName = "Peter";
                newUser40.LastName = "Stump";
                newUser40.MiddleInitial = "L";
                newUser40.StreetAddress = "1300 Kellen Circle";
                newUser40.City = "Houston";
                newUser40.State = "TX";
                newUser40.ZipCode = "77018";
                newUser40.PhoneNumber = "8174560903";
                newUser40.Birthdate = new DateTime(1989, 8, 13);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser40, "rhythm");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser40 = _db.Users.FirstOrDefault(u => u.UserName == "peterstump@hootmail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser40, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser40, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser41 = _db.Users.FirstOrDefault(u => u.Email == "tanner@ggmail.com");

            if (newUser41 == null)
            {
                newUser41 = new AppUser();
                newUser41.Email = "tanner@ggmail.com";
                newUser41.FirstName = "Jeremy";
                newUser41.LastName = "Tanner";
                newUser41.MiddleInitial = "S";
                newUser41.StreetAddress = "4347 Almstead";
                newUser41.City = "Houston";
                newUser41.State = "TX";
                newUser41.ZipCode = "77044";
                newUser41.PhoneNumber = "8174590929";
                newUser41.Birthdate = new DateTime(1982, 5, 21);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser41, "kindly");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser41 = _db.Users.FirstOrDefault(u => u.UserName == "tanner@ggmail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser41, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser41, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser42 = _db.Users.FirstOrDefault(u => u.Email == "TayTaylor@aool.com");

            if (newUser42 == null)
            {
                newUser42 = new AppUser();
                newUser42.Email = "TayTaylor@aool.com";
                newUser42.FirstName = "Rachel";
                newUser42.LastName = "Taylor";
                newUser42.MiddleInitial = "K";
                newUser42.StreetAddress = "345 Longview Dr.";
                newUser42.City = "Austin";
                newUser42.State = "TX";
                newUser42.ZipCode = "78705";
                newUser42.PhoneNumber = "5124512631";
                newUser42.Birthdate = new DateTime(1975, 7, 27);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser42, "arched");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser42 = _db.Users.FirstOrDefault(u => u.UserName == "TayTaylor@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser42, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser42, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser43 = _db.Users.FirstOrDefault(u => u.Email == "taylordjay@aool.com");

            if (newUser43 == null)
            {
                newUser43 = new AppUser();
                newUser43.Email = "taylordjay@aool.com";
                newUser43.FirstName = "Allison";
                newUser43.LastName = "Taylor";
                newUser43.MiddleInitial = "R";
                newUser43.StreetAddress = "467 Nueces St.";
                newUser43.City = "Austin";
                newUser43.State = "TX";
                newUser43.ZipCode = "78705";
                newUser43.PhoneNumber = "5124748452";
                newUser43.Birthdate = new DateTime(1960, 1, 8);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser43, "instrument");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser43 = _db.Users.FirstOrDefault(u => u.UserName == "taylordjay@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser43, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser43, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser44 = _db.Users.FirstOrDefault(u => u.Email == "teefrank@hootmail.com");

            if (newUser44 == null)
            {
                newUser44 = new AppUser();
                newUser44.Email = "teefrank@hootmail.com";
                newUser44.FirstName = "Frank";
                newUser44.LastName = "Tee";
                newUser44.MiddleInitial = "J";
                newUser44.StreetAddress = "5590 Lavell Dr";
                newUser44.City = "Houston";
                newUser44.State = "TX";
                newUser44.ZipCode = "77004";
                newUser44.PhoneNumber = "8178765543";
                newUser44.Birthdate = new DateTime(1968, 4, 6);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser44, "median");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser44 = _db.Users.FirstOrDefault(u => u.UserName == "teefrank@hootmail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser44, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser44, "Customer");
            }

            //save changes
            _db.SaveChanges();


            AppUser newUser45 = _db.Users.FirstOrDefault(u => u.Email == "tuck33@ggmail.com");

            if (newUser45 == null)
            {
                newUser45 = new AppUser();
                newUser45.Email = "tuck33@ggmail.com";
                newUser45.FirstName = "Clent";
                newUser45.LastName = "Tucker";
                newUser45.MiddleInitial = "J";
                newUser45.StreetAddress = "312 Main St.";
                newUser45.City = "Dallas";
                newUser45.State = "TX";
                newUser45.ZipCode = "75315";
                newUser45.PhoneNumber = "2148471154";
                newUser45.Birthdate = new DateTime(1978, 5, 19);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser45, "approval");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser45 = _db.Users.FirstOrDefault(u => u.UserName == "tuck33@ggmail.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser45, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser45, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser46 = _db.Users.FirstOrDefault(u => u.Email == "avelasco@yaho.com");

            if (newUser46 == null)
            {
                newUser46 = new AppUser();
                newUser46.Email = "avelasco@yaho.com";
                newUser46.FirstName = "Allen";
                newUser46.LastName = "Valasco";
                newUser46.MiddleInitial = "G";
                newUser46.StreetAddress = "679 W. 4th";
                newUser46.City = "Dallas";
                newUser46.State = "TX";
                newUser46.ZipCode = "75207";
                newUser46.PhoneNumber = "2143985638";
                newUser46.Birthdate = new DateTime(1963, 10, 6);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser46, "decorate");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser46 = _db.Users.FirstOrDefault(u => u.UserName == "avelasco@yaho.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser46, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser46, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser47 = _db.Users.FirstOrDefault(u => u.Email == "westj@pioneer.net");

            if (newUser47 == null)
            {
                newUser47 = new AppUser();
                newUser47.Email = "westj@pioneer.net";
                newUser47.FirstName = "Jake";
                newUser47.LastName = "West";
                newUser47.MiddleInitial = "T";
                newUser47.StreetAddress = "RR 3287";
                newUser47.City = "Dallas";
                newUser47.State = "TX";
                newUser47.ZipCode = "75323";
                newUser47.PhoneNumber = "2148475244";
                newUser47.Birthdate = new DateTime(1993, 10, 14);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser47, "grover");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser47 = _db.Users.FirstOrDefault(u => u.UserName == "westj@pioneer.net");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser47, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser47, "Customer");
            }

            //save changes
            _db.SaveChanges();


            AppUser newUser48 = _db.Users.FirstOrDefault(u => u.Email == "louielouie@aool.com");

            if (newUser48 == null)
            {
                newUser48 = new AppUser();
                newUser48.Email = "louielouie@aool.com";
                newUser48.FirstName = "Louis";
                newUser48.LastName = "Winthorpe";
                newUser48.MiddleInitial = "L";
                newUser48.StreetAddress = "2500 Padre Blvd";
                newUser48.City = "Dallas";
                newUser48.State = "TX";
                newUser48.ZipCode = "75220";
                newUser48.PhoneNumber = "2145650098";
                newUser48.Birthdate = new DateTime(1952, 5, 31);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser48, "sturdy");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser48 = _db.Users.FirstOrDefault(u => u.UserName == "louielouie@aool.com");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser48, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser48, "Customer");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser49 = _db.Users.FirstOrDefault(u => u.Email == "rwood@voyager.net");

            if (newUser49 == null)
            {
                newUser49 = new AppUser();
                newUser49.Email = "rwood@voyager.net";
                newUser49.FirstName = "Regean";
                newUser49.LastName = "Wood";
                newUser49.MiddleInitial = "B";
                newUser49.StreetAddress = "447 Westlake Dr.";
                newUser49.City = "Austin";
                newUser49.State = "TX";
                newUser49.ZipCode = "78746";
                newUser49.PhoneNumber = "5124545242";
                newUser49.Birthdate = new DateTime(1992, 4, 24);


                

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser49, "decorous");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser49 = _db.Users.FirstOrDefault(u => u.UserName == "rwood@voyager.net");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser49, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(newUser49, "Customer");
            }

            //save changes
            _db.SaveChanges();

        }
    }
}

