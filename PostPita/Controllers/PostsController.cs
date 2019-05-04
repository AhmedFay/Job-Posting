using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PostPita.Data;
using PostPita.Models;

namespace PostPita.Controllers
{
    [Authorize(Roles = "Company")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPostRep _postRep;

        public PostsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IPostRep postRep)
        {
            _context = context;
            _userManager = userManager;
            _postRep = postRep;

        }

        public async Task<IActionResult> Index()
        {
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.CompanyUsers.SingleOrDefaultAsync(u => u.User == appUser);
            //ViewBag.CompanyName = user.CoName;
            var posts = await _context.Posts.Where(p => p.Company == user).ToListAsync();
            posts.Reverse();
            return View(posts);
        }

        public async Task<IActionResult> AllApplicant()
        {
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.CompanyUsers.SingleOrDefaultAsync(u => u.User == appUser);

            var applicants = await _postRep.GetAllApplicantAsync(user);
            applicants.Reverse();
            return View(applicants);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            var app = await _postRep.GetApplicant(post);
            var t = new Tuple<Post, List<Applicant>>(post, app);

            return View(t);
        }

        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
                return View();
            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return View();
            }

            return View(new PostVM(post));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostVM postVm)
        {
            if (ModelState.IsValid)
            {
                var usera = await _userManager.GetUserAsync(User);
                var user = await _context.CompanyUsers.SingleOrDefaultAsync(u => usera == u.User);
                _context.Posts.Add(new Post(postVm, user) { PostStatus = PostStatus.Wait, PostingTime = DateTime.Now });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postVm);
        }

        public async Task<IActionResult> Hide(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user.CompanyUser != post.Company)
                return RedirectToAction("AccessDenied", "Account");

            if (post.PostStatus != PostStatus.Approved)
            {
                return BadRequest();
            }
            post.PostStatus = PostStatus.Hidden;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Show(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user.CompanyUser != post.Company)
                return RedirectToAction("AccessDenied", "Account");

            if (post.PostStatus != PostStatus.Hidden)
            {
                return BadRequest();
            }
            post.PostStatus = PostStatus.Approved;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Review(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user.CompanyUser != post.Company)
                return RedirectToAction("AccessDenied", "Account");

            if (post.PostStatus != PostStatus.Rejected)
            {
                return BadRequest();
            }
            post.PostStatus = PostStatus.Review;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.Include(a => a.Applicants).SingleOrDefaultAsync(m => m.Id == id);

            _context.Applicants.RemoveRange(post.Applicants);
            _context.Posts.Remove(post);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

        public async Task<IActionResult> addInBlackList(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants.SingleOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
            {
                return NotFound();
            }

            applicant.isInBlackList = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            //if (post.PostStatus != PostStatus.Approved)
            //{
            //    return BadRequest();
            //}

            var user = await _userManager.GetUserAsync(User);
            if (user.CompanyUser != post.Company)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            return View(new PostVM(post));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, [FromForm]PostVM postVm)
        {
            var user = await _userManager.GetUserAsync(User);
            var post = await _context.Posts.Include(p => p.Modified).SingleOrDefaultAsync(m => m.Id == id);
            if (user.CompanyUser != post.Company)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            //if (post.PostStatus != PostStatus.Approved)
            //{
            //    return BadRequest();
            //}

            if (ModelState.IsValid)
            {
                if (post.Modified == null)
                {
                    _context.PostsModified.Add(new PostModified(postVm, post));
                }
                else
                {
                    var p2 = await _context.PostsModified.SingleOrDefaultAsync(a => a == post.Modified);

                    _context.PostsModified.Remove(p2);
                    _context.SaveChanges();
                    _context.PostsModified.Add(new PostModified(postVm, post));

                }

                post.PostStatus = PostStatus.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postVm);
        }


    }
}
