using System;
using Domain.App;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.EF.AppDataInit
{
    public static class DataInit
    {
        public static void SeedIdentity(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var roleAdmin = new Role
            {
                Name = "Admin"
            };

            var resultAdmin = roleManager.CreateAsync(roleAdmin).Result;
            if (!resultAdmin.Succeeded)
            {
                foreach (var identityError in resultAdmin.Errors)
                {
                    Console.WriteLine("Cant create role! Error: " + identityError.Description);
                }
            }
            
            
            var user = new User
            {
                Email = "admin@gmail.com",
                UserName = "DionysusADMIN"
            };
            
            resultAdmin = userManager.CreateAsync(user, "Qwe!23").Result;
            if (!resultAdmin.Succeeded)
            {
                foreach (var identityError in resultAdmin.Errors)
                {
                    Console.WriteLine("Cant create user! Error: " + identityError.Description);
                }
            }

            resultAdmin = userManager.AddToRoleAsync(user, "Admin").Result;
            if (!resultAdmin.Succeeded)
            {
                foreach (var identityError in resultAdmin.Errors)
                {
                    Console.WriteLine("Cant add user to role! Error: " + identityError.Description);
                }
            }
        }
        
        // public static void SeedAppData(AppDbContext ctx)
        // {
        //     var ctypeSkype = new ContactType()
        //     {
        //         ContactTypeValue = "skype"
        //     };
        //     var ctypeEmail = new ContactType()
        //     {
        //         ContactTypeValue = "email"
        //     };
        //
        //     var p = new Person
        //     {
        //         FirstName = "Foo",
        //         LastName = "Bar",
        //         Contacts = new List<Contact>()
        //         {
        //             new() {ContactValue = "akaver", ContactType = ctypeSkype,},
        //             new Contact() {ContactValue = "akaver@itcollege.ee", ContactType = ctypeEmail,},
        //         }
        //     };
        //
        //
        //     ctx.Persons.Add(p);
        //
        //     ctx.SaveChanges();
        // }
    }
}
