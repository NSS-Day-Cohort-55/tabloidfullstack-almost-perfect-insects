using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tabloid.Repositories;
using Tabloid.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tabloid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private IPostRepository _postRepository;
        private IUserProfileRepository _userProfileRepository;

        public PostController(IPostRepository postRepository, IUserProfileRepository userProfileRepository)
        {
            _postRepository = postRepository;
            _userProfileRepository = userProfileRepository;
        }

        // GET: api/<PostController>
        [HttpGet]
        public List<Post> Get()
        {
            return _postRepository.GetAllPosts();
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public Post Get(int id)
        {
            return _postRepository.GetById(id);
        }

        // POST api/<PostController>
        [HttpPost]
        public IActionResult Post(Post post)
        {
            post.UserProfileId = GetCurrentUserProfile().Id;
            post.IsApproved = true;
            _postRepository.AddPost(post);
            return CreatedAtAction("Get", new { id = post.Id }, post);
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }
            _postRepository.Update(post);
            return NoContent();
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}
