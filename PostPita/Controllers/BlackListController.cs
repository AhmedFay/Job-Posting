using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PostPita.Data;
using PostPita.Models;

namespace PostPita.Controllers
{
    [Authorize(Roles = "Company")]
    public class BlackListController : Controller
    {
        private readonly IPostRep _postRep;

        public BlackListController(IPostRep postRep)
        {
            _postRep = postRep;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _postRep.GetBlackListAsync());
        }
    }
}