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
    public static class SeedUsers
    {
        public static async Task SeedAllUsers(IServiceProvider serviceProvider)
        {
            AppDbContext _db = serviceProvider.GetRequiredService<AppDbContext>();
            UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            AppUser newUser1 = _db.Users.FirstOrDefault(u => u.Email == "t.jacobs@longhornbank.net");

            //if admin hasn't been created, then add them

            if (newUser1 == null)
            {
                newUser1 = new AppUser();
                newUser1.Email = "t.jacobs@longhornbank.net";
                newUser1.FirstName = "Todd";
                newUser1.MiddleInitial = "L";
                newUser1.LastName = "Jacobs";
                newUser1.StreetAddress = "4564 ElmSt.";
                newUser1.City = "Houston";
                newUser1.State = "TX";
                newUser1.ZipCode = "77003";
                newUser1.PhoneNumber = "8176593544";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser1, "society");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser1 = _db.Users.FirstOrDefault(u => u.UserName == "t.jacobs@longhornbank.net");
            }

            //Add the new user we just created to the Admin role
            if (await _userManager.IsInRoleAsync(newUser1, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(newUser1, "Employee");
            }

            //save changes
            _db.SaveChanges();


            AppUser newUser2 = _db.Users.FirstOrDefault(u => u.Email == "e.rice@longhornbank.neet");


            //if admin hasn't been created, then add them

            if (newUser2 == null)
            {
                newUser2 = new AppUser();
                newUser2.Email = "e.rice@longhornbank.neet";
                newUser2.FirstName = "Eryn";
                newUser2.MiddleInitial = "M";
                newUser2.LastName = "Rice";
                newUser2.StreetAddress = "3405 Rio Grande";
                newUser2.City = "Dallas";
                newUser2.State = "TX";
                newUser2.ZipCode = "75261";
                newUser2.PhoneNumber = "2148475583";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser2, "ricearoni1");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser2 = _db.Users.FirstOrDefault(u => u.UserName == "e.rice@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser2, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(newUser2, "Employee");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser3 = _db.Users.FirstOrDefault(u => u.Email == "b.ingram@longhornbank.neet");


            //if admin hasn't been created, then add them

            if (newUser3 == null)
            {
                newUser3 = new AppUser();
                newUser3.Email = "b.ingram@longhornbank.neet";
                newUser3.FirstName = "Brad";
                newUser3.MiddleInitial = "S";
                newUser3.LastName = "Ingram";
                newUser3.StreetAddress = "6548 La Posada Ct.";
                newUser3.City = "Austin";
                newUser3.State = "TX";
                newUser3.ZipCode = "78705";
                newUser3.PhoneNumber = "126978613";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser3, "Ingram45");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser3 = _db.Users.FirstOrDefault(u => u.UserName == "b.ingram@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser3, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(newUser3, "Employee");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser4 = _db.Users.FirstOrDefault(u => u.Email == "a.taylor@longhornbank.neet");


            //if admin hasn't been created, then add them

            if (newUser4 == null)
            {
                newUser4 = new AppUser();
                newUser4.Email = "a.taylor@longhornbank.neet";
                newUser4.FirstName = "Allison";
                newUser4.MiddleInitial = "R";
                newUser4.LastName = "Taylor";
                newUser4.StreetAddress = "467 Nueces St.";
                newUser4.City = "Dallas";
                newUser4.State = "TX";
                newUser4.ZipCode = "75237";
                newUser4.PhoneNumber = "2148965621";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser4, "nostalgic");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser4 = _db.Users.FirstOrDefault(u => u.UserName == "a.taylor@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser4, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(newUser4, "Manager");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser5 = _db.Users.FirstOrDefault(u => u.Email == "g.martinez@longhornbank.neet");


            //if admin hasn't been created, then add them

            if (newUser5 == null)
            {
                newUser5 = new AppUser();
                newUser5.Email = "g.martinez@longhornbank.neet";
                newUser5.FirstName = "Gregory";
                newUser5.MiddleInitial = "R";
                newUser5.LastName = "Martinez";
                newUser5.StreetAddress = "8295 Sunset Blvd.";
                newUser5.City = "San Antonio";
                newUser5.State = "TX";
                newUser5.ZipCode = "78239";
                newUser5.PhoneNumber = "2105788965";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser5, "fungus");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser5 = _db.Users.FirstOrDefault(u => u.UserName == "g.martinez@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser5, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(newUser5, "Employee");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser6 = _db.Users.FirstOrDefault(u => u.Email == "m.sheffield@longhornbank.neet");


            //if admin hasn't been created, then add them

            if (newUser6 == null)
            {
                newUser6 = new AppUser();
                newUser6.Email = "m.sheffield@longhornbank.neet";
                newUser6.FirstName = "Martin";
                newUser6.MiddleInitial = "J";
                newUser6.LastName = "Shefield";
                newUser6.StreetAddress = "3886 Avenue A";
                newUser6.City = "Austin";
                newUser6.State = "TX";
                newUser6.ZipCode = "78763";
                newUser6.PhoneNumber = "5124678821";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser6, "longhorns");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser6 = _db.Users.FirstOrDefault(u => u.UserName == "g.martinez@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser6, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(newUser6, "Manager");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser7 = _db.Users.FirstOrDefault(u => u.Email == "j.macleod@longhornbank.neet");


            //if admin hasn't been created, then add them

            if (newUser7 == null)
            {
                newUser7 = new AppUser();
                newUser7.Email = "j.macleod@longhornbank.neet";
                newUser7.FirstName = "Jennifer";
                newUser7.MiddleInitial = "D";
                newUser7.LastName = "Macleod";
                newUser7.StreetAddress = "2504 Far West Blvd";
                newUser7.City = "Austin";
                newUser7.State = "TX";
                newUser7.ZipCode = "78731";
                newUser7.PhoneNumber = "5124653365";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser7, "smitty");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser7 = _db.Users.FirstOrDefault(u => u.UserName == "j.macleod@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser7, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(newUser7, "Manager");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser8 = _db.Users.FirstOrDefault(u => u.Email == "j.tanner@longhornbank.neet");


            //if admin hasn't been created, then add them

            if (newUser8 == null)
            {
                newUser8 = new AppUser();
                newUser8.Email = "j.tanner@longhornbank.neet";
                newUser8.FirstName = "Jeremy";
                newUser8.MiddleInitial = "S";
                newUser8.LastName = "Tanner";
                newUser8.StreetAddress = "4347 Almstead";
                newUser8.City = "Austin";
                newUser8.State = "TX";
                newUser8.ZipCode = "78761";
                newUser8.PhoneNumber = "5129457399";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser8, "tanman");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser8 = _db.Users.FirstOrDefault(u => u.UserName == "j.tanner@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser8, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(newUser8, "Employee");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser9 = _db.Users.FirstOrDefault(u => u.Email == "m.rhodes@longhornbank.neet");


            //if admin hasn't been created, then add them

            if (newUser9 == null)
            {
                newUser9 = new AppUser();
                newUser9.Email = "m.rhodes@longhornbank.neet";
                newUser9.FirstName = "Megan";
                newUser9.MiddleInitial = "C";
                newUser9.LastName = "Rhodes";
                newUser9.StreetAddress = "4587 Enfield Rd.";
                newUser9.City = "San Antonio";
                newUser9.State = "TX";
                newUser9.ZipCode = "78293";
                newUser9.PhoneNumber = "2102449976";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser9, "countryrhodes");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser9 = _db.Users.FirstOrDefault(u => u.UserName == "m.rhodes@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser9, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(newUser9, "Manager");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser10 = _db.Users.FirstOrDefault(u => u.Email == "e.stuart@longhornbank.neet");


            //if admin hasn't been created, then add them

            if (newUser10 == null)
            {
                newUser10 = new AppUser();
                newUser10.Email = "e.stuart@longhornbank.neet";
                newUser10.FirstName = "Eric";
                newUser10.MiddleInitial = "F";
                newUser10.LastName = "Stuart";
                newUser10.StreetAddress = "5576 Toro Ring";
                newUser10.City = "San Antonii";
                newUser10.State = "TX";
                newUser10.ZipCode = "78279";
                newUser10.PhoneNumber = "2105344627";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser10, "stewboy");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser10 = _db.Users.FirstOrDefault(u => u.UserName == "m.rhodes@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser10, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(newUser10, "Manager");
            }

            //save changes
            _db.SaveChanges();


            AppUser newUser11 = _db.Users.FirstOrDefault(u => u.Email == "l.chung@longhornbank.neet");



            //if admin hasn't been created, then add them

            if (newUser11 == null)
            {
                newUser11 = new AppUser();
                newUser11.Email = "l.chung@longhornbank.neet";
                newUser11.FirstName = "Lisa";
                newUser11.MiddleInitial = "N";
                newUser11.LastName = "Chung";
                newUser11.StreetAddress = "234 RR 12";
                newUser11.City = "San Antoni0";
                newUser11.State = "TX";
                newUser11.ZipCode = "78268";
                newUser11.PhoneNumber = "2106983548";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser11, "lisssa");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser11 = _db.Users.FirstOrDefault(u => u.UserName == "l.chung@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser11, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(newUser11, "Employee");
            }

            //save changes
            _db.SaveChanges();


            AppUser newUser12 = _db.Users.FirstOrDefault(u => u.Email == "l.swanson@longhornbank.neet");



            //if admin hasn't been created, then add them

            if (newUser12 == null)
            {
                newUser12 = new AppUser();
                newUser12.Email = "l.swanson@longhornbank.neet";
                newUser12.FirstName = "Leon";
                newUser12.MiddleInitial = "";
                newUser12.LastName = "Swanson";
                newUser12.StreetAddress = "245 River Rd";
                newUser12.City = "Austin";
                newUser12.State = "TX";
                newUser12.ZipCode = "78736";
                newUser12.PhoneNumber = "5124748138";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser12, "swansong");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser12 = _db.Users.FirstOrDefault(u => u.UserName == "l.swanson@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser12, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(newUser12, "Manager");
            }

            //save changes
            _db.SaveChanges();


            AppUser newUser13 = _db.Users.FirstOrDefault(u => u.Email == "w.loter@longhornbank.neet");



            //if admin hasn't been created, then add them

            if (newUser13 == null)
            {
                newUser13 = new AppUser();
                newUser13.Email = "w.loter@longhornbank.neet";
                newUser13.FirstName = "Wanda";
                newUser13.MiddleInitial = "k";
                newUser13.LastName = "Lotter";
                newUser13.StreetAddress = "3453 RR 3235";
                newUser13.City = "Austin";
                newUser13.State = "TX";
                newUser13.ZipCode = "78732";
                newUser13.PhoneNumber = "5124579845";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser13, "lottery");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser13 = _db.Users.FirstOrDefault(u => u.UserName == "w.loter@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser13, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(newUser13, "Employee");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser14 = _db.Users.FirstOrDefault(u => u.Email == "j.white@longhornbank.neet");



            //if admin hasn't been created, then add them

            if (newUser14 == null)
            {
                newUser14 = new AppUser();
                newUser14.Email = "j.white@longhornbank.neet";
                newUser14.FirstName = "Jason";
                newUser14.MiddleInitial = "M";
                newUser14.LastName = "White";
                newUser14.StreetAddress = "12 Valley View";
                newUser14.City = "Houston";
                newUser14.State = "TX";
                newUser14.ZipCode = "77045";
                newUser14.PhoneNumber = "8174955201";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser14, "evanescent");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser14 = _db.Users.FirstOrDefault(u => u.UserName == "j.white@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser14, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(newUser14, "Manager");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser15 = _db.Users.FirstOrDefault(u => u.Email == "w.montogomery@longhornbank.neet");



            //if admin hasn't been created, then add them

            if (newUser15 == null)
            {
                newUser15 = new AppUser();
                newUser15.Email = "w.montogomery@longhornbank.neet";
                newUser15.FirstName = "Wilda";
                newUser15.MiddleInitial = "K";
                newUser15.LastName = "Montgomery";
                newUser15.StreetAddress = "210 Blanco Dr";
                newUser15.City = "Houston";
                newUser15.State = "TX";
                newUser15.ZipCode = "77030";
                newUser15.PhoneNumber = "8178746718";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser15, "monty3");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser15 = _db.Users.FirstOrDefault(u => u.UserName == "w.montogomery@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser15, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(newUser15, "Manager");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser16 = _db.Users.FirstOrDefault(u => u.Email == "h.morales@longhornbank.neet");



            //if admin hasn't been created, then add them

            if (newUser16 == null)
            {
                newUser16 = new AppUser();
                newUser16.Email = "h.morales@longhornbank.neet";
                newUser16.FirstName = "Hector";
                newUser16.MiddleInitial = "M";
                newUser16.LastName = "Morales";
                newUser16.StreetAddress = "4501 RR 140";
                newUser16.City = "Houston";
                newUser16.State = "TX";
                newUser16.ZipCode = "77031";
                newUser16.PhoneNumber = "8177458615";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser16, "hecktour");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser16 = _db.Users.FirstOrDefault(u => u.UserName == "h.morales@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser16, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(newUser16, "Employee");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser17 = _db.Users.FirstOrDefault(u => u.Email == "m.rankin@longhornbank.neet");



            //if admin hasn't been created, then add them

            if (newUser17 == null)
            {
                newUser17 = new AppUser();
                newUser17.Email = "m.rankin@longhornbank.neet";
                newUser17.FirstName = "Mary";
                newUser17.MiddleInitial = "T";
                newUser17.LastName = "Rankin";
                newUser17.StreetAddress = "340 Second St";
                newUser17.City = "Austin";
                newUser17.State = "TX";
                newUser17.ZipCode = "78703";
                newUser17.PhoneNumber = "5122926966";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser17, "rankmary");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser17 = _db.Users.FirstOrDefault(u => u.UserName == "m.rankin@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser17, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(newUser17, "Employee");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser18 = _db.Users.FirstOrDefault(u => u.Email == "l.walker@longhornbank.neet");



            //if admin hasn't been created, then add them

            if (newUser18 == null)
            {
                newUser18 = new AppUser();
                newUser18.Email = "l.walker@longhornbank.neet";
                newUser18.FirstName = "Lary";
                newUser18.MiddleInitial = "G";
                newUser18.LastName = "Walker";
                newUser18.StreetAddress = "9 Bison Circle";
                newUser18.City = "Dallas";
                newUser18.State = "TX";
                newUser18.ZipCode = "75238";
                newUser18.PhoneNumber = "2143125897";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser18, "walkamile");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser18 = _db.Users.FirstOrDefault(u => u.UserName == "l.walker@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser18, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(newUser18, "Manager");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser19 = _db.Users.FirstOrDefault(u => u.Email == "g.chang@longhornbank.neet");



            //if admin hasn't been created, then add them

            if (newUser19 == null)
            {
                newUser19 = new AppUser();
                newUser19.Email = "g.chang@longhornbank.neet";
                newUser19.FirstName = "George";
                newUser19.MiddleInitial = "M";
                newUser19.LastName = "Chang";
                newUser19.StreetAddress = "9003 Joshua St";
                newUser19.City = "San Antonio";
                newUser19.State = "TX";
                newUser19.ZipCode = "78260";
                newUser19.PhoneNumber = "2103450925";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser19, "changalang");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser19 = _db.Users.FirstOrDefault(u => u.UserName == "g.chang@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser19, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(newUser19, "Manager");
            }

            //save changes
            _db.SaveChanges();

            AppUser newUser20 = _db.Users.FirstOrDefault(u => u.Email == "g.gonzalez@longhornbank.neet");



            //if admin hasn't been created, then add them

            if (newUser20 == null)
            {
                newUser20 = new AppUser();
                newUser20.Email = "g.gonzalez@longhornbank.neet";
                newUser20.FirstName = "Gwen";
                newUser20.MiddleInitial = "J";
                newUser20.LastName = "Gonzales";
                newUser20.StreetAddress = "9003 Joshua St";
                newUser20.City = "Dallas";
                newUser20.State = "TX";
                newUser20.ZipCode = "75260";
                newUser20.PhoneNumber = "2142345566";


                //TODO: Add additional fields that you created on the AppUser class

                //NOTE: This creates the user - "Abc123!" is the password for this user
                var result = await _userManager.CreateAsync(newUser20, "offbeat");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                newUser20 = _db.Users.FirstOrDefault(u => u.UserName == "g.gonzalez@longhornbank.neet");
            }
            if (await _userManager.IsInRoleAsync(newUser20, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(newUser20, "Employee");
            }

            //save changes
            _db.SaveChanges();


        }
    }
}
