using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostPita.Models
{
    public interface IPostRep
    {
        Task<List<Post>> GetAll();

        List<Post> GetApparent();
        Task<List<Post>> GetApparentAsync();

        Post Get(int? id);
        Task<Post> GetAsync(int? id);
        Task<List<Post>> GetUserPosts(ApplicationUser user);

        Task AddApplicant(ApplicantVM vm, Post post);
        Task<List<Applicant>> GetApplicant(Post p);

        Task<List<Applicant>> GetBlackListAsync();

        List<Post> SearchPosts(string s);
        Task<List<Post>> SearchPostsAsync(string s);





    }
}
