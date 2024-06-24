using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyProject.Data;
using UdemyProject.Data.Models;
using UdemyProject.Dtos;

namespace UdemyProject.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        readonly AppDbContext _appDbContext;
        
            private readonly AppDbContext _context;
            public PostController(IConfiguration config, AppDbContext context)
            {
                _context = context ;
            }
        // get all posts
        [HttpGet("Posts")]
        public IEnumerable<Post> GetPosts()
        {
            var posts = _context.Posts;
            if (posts == null) return Enumerable.Empty<Post>();
            return posts;
        }
        // get specific post by ID

        [HttpGet("PostSingle/{postId}")]
        public async Task<Post> GetPostSingle(int postId)
        {
            return await _context.Posts.FindAsync(postId);
        }

        [HttpGet("PostsByUser/{userId}")]
        public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
        {
            return await _context.Posts.Where(p => p.UserId == userId).ToListAsync();
        }

        [HttpGet("MyPosts")]
        public async Task<IEnumerable<Post>> GetMyPosts()
        {
            var userId = int.Parse(this.User.FindFirst("userId")?.Value);
            return await _context.Posts.Where(p => p.UserId == userId).ToListAsync();
        }

        [HttpGet("PostsBySearch/{searchParam}")]
        public async Task<IEnumerable<Post>> PostsBySearch(string searchParam)
        {
            return await _context.Posts
                .Where(p => EF.Functions.Like(p.PostTitle, $"%{searchParam}%") || EF.Functions.Like(p.PostContent, $"%{searchParam}%"))
                .ToListAsync();
        }

        [HttpPost("Post")]
        public async Task<IActionResult> AddPost(PostToAddDto postToAdd)
        {
            var userId = int.Parse(this.User.FindFirst("userId")?.Value);
            var post = new Post
            {
                UserId = userId,
                PostTitle = postToAdd.PostTitle,
                PostContent = postToAdd.PostContent,
                PostCreated = DateTime.Now,
                PostUpdated = DateTime.Now
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("Post")]
        public async Task<IActionResult> EditPost(PostToEditDto postToEdit)
        {
            var post = await _context.Posts.FindAsync(postToEdit.PostId);
            if (post == null || post.UserId != int.Parse(this.User.FindFirst("userId")?.Value))
            {
                return BadRequest("Post not found or you are not authorized to edit this post.");
            }

            post.PostTitle = postToEdit.PostTitle;
            post.PostContent = postToEdit.PostContent;
            post.PostUpdated = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("delete-Post/{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post == null || post.UserId != int.Parse(this.User.FindFirst("userId")?.Value))
            {
                return BadRequest("Post not found or you are not authorized to delete this post.");
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
