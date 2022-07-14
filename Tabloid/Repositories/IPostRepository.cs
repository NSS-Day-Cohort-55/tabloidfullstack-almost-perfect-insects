using System.Collections.Generic;
using Tabloid.Models;

namespace Tabloid.Repositories
{
    public interface IPostRepository
    {
        List<Post> GetAllPosts();

        Post GetById(int id);
        void AddPost(Post post);
        void Update(Post post);
    }
}