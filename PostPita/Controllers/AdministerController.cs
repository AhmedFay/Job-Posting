using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostPita.Data;
using PostPita.Models;

namespace PostPita.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public AdministerController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Approver()
        {
            var t = await _dbContext.Posts.Include(p => p.Company).Where(p => p.PostStatus == PostStatus.Wait || p.PostStatus == PostStatus.Review).ToListAsync();
            t.Reverse();
            return View(t);
        }

        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _dbContext.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            post.PostStatus = PostStatus.Approved;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Approver");
        }

        public async Task<IActionResult> Reject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _dbContext.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            post.PostStatus = PostStatus.Rejected;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Approver");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _dbContext.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        public async Task<IActionResult> CreateRole()
        {
            await _roleManager.CreateAsync(new IdentityRole("Employee"));
            ViewData["Msg"] = "Create Role is success.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddToRole()
        {
            var user = await _userManager.FindByNameAsync("Ahmed");
            await _userManager.AddToRoleAsync(user, "Admin");
            ViewData["Msg"] = "insert to Role is success.";
            return RedirectToAction("Index");
        }

        public IActionResult ModificationApprover()
        {
            var b = _dbContext.PostsModified.Include(p => p.OldPost).ToList();
            return View(b);
        }

        public async Task<IActionResult> EditDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postModified = await _dbContext.PostsModified.Include(p => p.OldPost).SingleOrDefaultAsync(m => m.Id == id);
            if (postModified == null)
            {
                return NotFound();
            }

            return View(postModified);
        }

        public async Task<IActionResult> ApproveEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _dbContext.Posts.SingleOrDefaultAsync(m => m.Modified.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            var postModified = await _dbContext.PostsModified.SingleOrDefaultAsync(m => m.Id == id);
            post.Title = postModified.Title;
            post.Description = postModified.Description;
            post.RequireSkills = postModified.RequireSkills;
            post.ContractMonths = postModified.ContractMonths;
            post.HourType = postModified.HourType;
            post.Salary = postModified.Salary;
            post.DeadLine = postModified.DeadLine;

            post.PostStatus = PostStatus.Approved;
            _dbContext.PostsModified.Remove(postModified);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RejectEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _dbContext.Posts.SingleOrDefaultAsync(m => m.Modified.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            var postModified = await _dbContext.PostsModified.SingleOrDefaultAsync(m => m.Id == id);
            _dbContext.PostsModified.Remove(postModified);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CoManage()
        {
            var co = await _dbContext.CompanyUsers
                .Include(u => u.User)
                .Where(m => m.IsAccepted == false).ToListAsync();

            return View(co);
        }

        public async Task<IActionResult> AcceptCo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var u = await _dbContext.CompanyUsers
                .Include(companyUser => companyUser.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (u == null)
            {
                return NotFound();
            }

            var rls = await _userManager.GetRolesAsync(u.User);
            if (rls.Any(r => r.Equals("Company")))
            {
                return RedirectToAction("Index");
            }

            u.IsAccepted = true;
            var user = await _userManager.FindByIdAsync(u.UserId);
            var x = await _userManager.AddToRoleAsync(user, "Company");
            await _dbContext.SaveChangesAsync();

            if (x.Succeeded)
                ViewData["Msg"] = "insert to Company Role is success.";
            else
                ViewData["Msg"] = "insert to Company Role is failed.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SendNotification()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.Email);

                if (user == null)
                {
                    ModelState.AddModelError("Email", "The Email provided is incorrect");
                    return View(vm);
                }

                user.Notifications.Add(new Notification()
                {
                    Title = vm.Title,
                    Content = vm.Content,
                    User = user
                });

                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfigRoles()
        {
            string s = "";

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

                var user = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "Admin",
                    UserType = UserType.Admin
                };

                var result = await _userManager.CreateAsync(user, "1");

                s += "add Admin role, ";

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    s += "add admin user, ";
                }

            }

            if (!await _roleManager.RoleExistsAsync("Company"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Company"));
                s += "add Company role, ";
            }

            if (!await _roleManager.RoleExistsAsync("Employee"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Employee"));
                s += "add Employee role ";
            }

            ViewData["Msg"] = s + " done.";

            return View("Index");
        }

    }
}