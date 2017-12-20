using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Data;

namespace NewsSite.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private ApplicationDbContext context;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
            //AddAllUsers();
        }

        [HttpGet, Route("view/OpenNews")]
        public IActionResult ViewOpenNews()
        {
            return Ok();
        }

        //[HttpGet, Route("view/AddAll")]
        //public async Task<IActionResult> AddAllUsers()
        //{
            //    await roleManager.CreateAsync(new IdentityRole("Admin"));
            //    await roleManager.CreateAsync(new IdentityRole("Publisher"));
            //    await roleManager.CreateAsync(new IdentityRole("Subscriber"));

            //string[] users = { "adam@mail.com", "", "Admin", "peter@email.com", "", "Publisher", "susan@email.com", "48", "Subscriber", "viktor@email.com", "15", "Subscriber", "xerxes@mail.com", "", "" };

            //for (int i = 0; i < users.Length; i += 3)
            //{
                //ApplicationUser newUser = new ApplicationUser();
                //if (!users[i + 1].Equals(""))
                //{
                //    newUser.Age = int.Parse(users[i + 1]);
                //}
                //newUser.Email = users[i];
                //newUser.UserName = users[i];

                //var resultNewUser = await userManager.CreateAsync(newUser);

                //if (resultNewUser.Succeeded && users[i + 2] != "")
                //{
                //    await userManager.AddToRoleAsync(newUser, users[i + 2]);
                //}

                //        var newUser = await userManager.FindByEmailAsync(users[i]);

                //        if ((await userManager.GetRolesAsync(newUser)).Contains("Admin")
                //            || (await userManager.GetRolesAsync(newUser)).Contains("Publisher")
                //            || (newUser.Age != null && newUser.Age >= 20))
                //        {
                //            await userManager.AddClaimAsync(newUser, new Claim("MinimumAge", "true"));
                //        }

        //        var newUser = await userManager.FindByEmailAsync(users[i]);

        //        if ((await userManager.GetRolesAsync(newUser)).Contains("Admin"))
        //        {
        //            await userManager.AddClaimAsync(newUser, new Claim("Publish", "all"));

        //        }
        //        if (users[i] == "peter@email.com")
        //        {
        //            await userManager.AddClaimAsync(newUser, new Claim("Publish", "sports"));
        //            await userManager.AddClaimAsync(newUser, new Claim("Publish", "economy"));
        //        }
        //    }

        //    return Ok(userManager.Users);
        //}

        [HttpGet, Route("claims")]
        public async Task<IActionResult> ViewThem()
        {
            var user = await userManager.FindByEmailAsync("adam@mail.com");
            return Ok(await userManager.GetClaimsAsync(user));
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            await signInManager.SignInAsync(user, true);

            //var result = await signInManager.CreateUserPrincipalAsync(user);
            //var info = result.Identity.Name;

            return Ok(user.UserName + " signed in");
        }

        // TODO: Skapa en Policy i Startup.cs och avkommentera sedan nedan
        // Controllern behöver inte innehålla någon mer kod

        [HttpGet, Route("open")]
        public IActionResult Open()
        {
            return Ok("Open");
        }

        [Authorize(Policy = "HiddenNews")]
        [HttpGet, Route("hiddennews")]
        public IActionResult HiddenNews()
        {
            return Ok("Hidden");
        }

        [Authorize(Policy = "AgeRequirement")]
        [Authorize(Policy = "HiddenNews")]
        [HttpGet, Route("age")]
        public IActionResult Age()
        {
            return Ok("Age");
        }

        [Authorize(Policy = "SportsRequirement")]
        [HttpGet, Route("sport")]
        public IActionResult Sport()
        {
            return Ok("Sport");
        }

        [Authorize(Policy = "CultureRequirement")]
        [HttpGet, Route("culture")]
        public IActionResult Culture()
        {
            return Ok("Culture");
        }

        [HttpGet, Route("recoverusers")]
        public async Task<IActionResult> RecoverUsers()
        {

            //await roleManager.CreateAsync(new IdentityRole("Admin"));
            //await roleManager.CreateAsync(new IdentityRole("Publisher"));
            //await roleManager.CreateAsync(new IdentityRole("Subscriber"));

            context.RemoveRange(userManager.Users.ToList());
            context.SaveChanges();

            string[] users = { "adam@mail.com", "", "Admin", "Adam" ,"peter@email.com", "", "Publisher", "Peter" , "susan@email.com", "48", "Subscriber", "Susan", "viktor@email.com", "15", "Subscriber", "Viktor", "xerxes@mail.com", "", "", "Xerxes" };

            for (int i = 0; i < users.Length; i += 4)
            {
                ApplicationUser newUser = new ApplicationUser();
                if (!users[i + 1].Equals(""))
                {
                    newUser.Age = int.Parse(users[i + 1]);
                }
                newUser.Email = users[i];
                newUser.UserName = users[i+3];

                var resultNewUser = await userManager.CreateAsync(newUser);

                if (resultNewUser.Succeeded && users[i + 2] != "")
                {
                    await userManager.AddToRoleAsync(newUser, users[i + 2]);
                }

                //var newUser = await userManager.FindByEmailAsync(users[i]);

                if ((await userManager.GetRolesAsync(newUser)).Contains("Admin")
                    || (await userManager.GetRolesAsync(newUser)).Contains("Publisher")
                    || (newUser.Age != null && newUser.Age >= 20))
                {
                    await userManager.AddClaimAsync(newUser, new Claim("MinimumAge", "true"));
                }

                //var newUser = await userManager.FindByEmailAsync(users[i]);

                if ((await userManager.GetRolesAsync(newUser)).Contains("Admin"))
                {
                    await userManager.AddClaimAsync(newUser, new Claim("Publish", "all"));

                }
                if (users[i] == "peter@email.com")
                {
                    await userManager.AddClaimAsync(newUser, new Claim("Publish", "sports"));
                    await userManager.AddClaimAsync(newUser, new Claim("Publish", "economy"));
                }
            }
            return Ok(userManager.Users);
        }

        [HttpGet, Route("getallusers")]
        public IActionResult GetAllUsers()
        {
            return Ok(userManager.Users.ToList());
        }

        [HttpGet, Route("getalluserswithclaims")]
        public async Task<IActionResult> GetAllUsersWithClaims()
        {
            var returnList = new List<ReturnModel>();
            foreach (var user in userManager.Users)
            {
                var claimsToThisUser = await userManager.GetClaimsAsync(user);

                var returnModel = new ReturnModel()
                {
                    Claims = claimsToThisUser,
                    User = user
                };
                returnList.Add(returnModel);
            }
            return Ok(returnList);
        }
    }
}

