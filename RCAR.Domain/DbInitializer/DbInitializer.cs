using Microsoft.AspNetCore.Identity;
using RCAR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCAR.Domain.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initializer()
        {
            if (!_roleManager.RoleExistsAsync("Administrator").GetAwaiter().GetResult())
                _roleManager.CreateAsync(new IdentityRole() { Name = "Administrator" }).GetAwaiter().GetResult();

            if (!_roleManager.RoleExistsAsync("Customer").GetAwaiter().GetResult())
                _roleManager.CreateAsync(new IdentityRole() { Name = "Customer" }).GetAwaiter().GetResult();

            if (_userManager.FindByEmailAsync("Administrator@poczta.pl").GetAwaiter().GetResult() == null)
            {
                var user = new User() { Email = "Administrator@poczta.pl", UserName = "Administrator@poczta.pl", EmailConfirmed = true };
                _userManager.CreateAsync(user, "Start123!").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, "Administrator").GetAwaiter().GetResult();
            }
        }
    }
}
