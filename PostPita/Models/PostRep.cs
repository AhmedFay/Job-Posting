using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostPita.Data;

namespace PostPita.Models
{
    public class PostRep : IPostRep
    {
        private readonly ApplicationDbContext _context;

        public PostRep(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAll()
        {
            return await _context.Posts.ToListAsync();
        }

        public List<Post> GetApparent()
        {
            var posts = _context.Posts
                .Where(p => p.DeadLine.CompareTo(DateTime.Now) > 0 && p.PostStatus == PostStatus.Approved)
                .Include(p => p.Company)
                .ToList();

            return posts;
        }
        public Task<List<Post>> GetApparentAsync()
        {
            var posts = _context.Posts
                .Where(p => p.DeadLine.CompareTo(DateTime.Now) > 0 && p.PostStatus == PostStatus.Approved)
                .Include(p => p.Company)
                .ToListAsync();

            return posts;
        }

        public Post Get(int? id)
        {
            return _context.Posts.SingleOrDefault(m => m.Id == id);
        }
        public Task<Post> GetAsync(int? id)
        {
            return _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<Post>> GetUserPosts(ApplicationUser user)
        {
            return await _context.Posts.Where(p => p.Company.UserId == user.Id).ToListAsync();
        }

        public Task<List<Applicant>> GetApplicant(Post p)
        {
            return _context.Applicants.Where(a => a.post == p).ToListAsync();
        }
        public async Task AddApplicant(ApplicantVM vm, Post post)
        {
            var ap = new Applicant(vm, post) { isAccepted = false };

            //var filePath = Path.GetTempFileName() + Path.GetExtension(vm.Cv.FileName);
            //if (vm.Cv.Length > 0)
            //{
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await vm.Cv.CopyToAsync(stream);
            //        ap.CvPath = filePath;
            //    }
            //}

            _context.Applicants.Add(ap);
            await _context.SaveChangesAsync();
        }
        public Task<List<Applicant>> GetAllApplicantAsync(CompanyUser user)
        {
            return _context.Applicants.Where(a => a.post.Company == user).ToListAsync();
        }

        public Task<List<Applicant>> GetBlackListAsync()
        {
            return _context.Applicants.Where(a => a.isInBlackList).ToListAsync();
        }

        public List<Post> SearchPosts(string s)
        {
            s = s.ToLower();

            var posts = GetApparent().Where(
                p => p.Title.ToLower().Contains(s) ||
                     p.Description.ToLower().Contains(s) ||
                     p.RequireSkills.ToLower().Contains(s) ||
                     p.Company.CoName.ToLower().Contains(s) ||
                     p.Salary.ToString().Equals(s) ||
                     p.HourType.ToString().ToLower().Contains(s) ||
                     p.State.ToString().ToLower().Contains(s) ||
                     p.City.ToLower().Contains(s)).ToList();

            return posts;
        }
        public Task<List<Post>> SearchPostsAsync(string s)
        {
            throw new NotImplementedException();
        }





    }
}
