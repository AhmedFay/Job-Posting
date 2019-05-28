using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostPita.Data;
using PostPita.Models;

namespace PostPita.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPostRep _postRep;

        public SearchController(IPostRep postRep)
        {
            _postRep = postRep;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            ViewBag.searchString = searchString;
            List<Post> posts;
            if (!String.IsNullOrEmpty(searchString))
            {
                posts = _postRep.SearchPosts(searchString);
                posts.Reverse();
            }
            else
            {
                posts = await _postRep.GetApparentAsync();
                posts.Reverse();
            }

            return View(posts);
        }

        [HttpPost]
        public async Task<IActionResult> PostModal(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRep.GetAsyncInclude(id);
            if (post == null)
            {
                return NotFound();
            }

            return PartialView("PostModal",post);
        }

        public async Task<IActionResult> Apply(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRep.GetAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply([FromRoute] int? id, [FromForm]ApplicantVM vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRep.GetAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            //validation for vm.Cv file in file type and size
            if (ModelState.IsValid)
            {
                await _postRep.AddApplicant(vm, post);
                return RedirectToAction("Applied");
            }

            return View(vm);
        }

        //[HttpPost]
        public IActionResult Applied(int? id)
        {
            ViewData["Msg"] = "Success submission.";
            return View();
        }

    }
}