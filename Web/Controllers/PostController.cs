﻿using Application.Extensions.UserContext;
using Application.Services.PostService;
using Application.UseCases.Post;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserContextService _userContextService;

        public PostController(IPostService postService,
            IUserContextService userContextService)
        {
            _postService = postService;
            _userContextService = userContextService;
        }

        [HttpGet("posts")]
        public async Task<ActionResult<List<PostDto>>> GetPosts()
        {
            var employees = await _postService.GetPosts();

            if (employees == null)
                return NotFound();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPost(int id)
        {
            var post = await _postService.GetPost(id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpGet("{userId}/posts-by-user-id")]
        public async Task<ActionResult<List<PostDto>>> GetPostsByUserId(int userId)
        {
            var posts = await _postService.GetPostsByUserId(userId);

            if (posts == null)
                return NotFound();

            return Ok(posts);
        }

        //[HttpGet("{searchText}/search")]
        //[Authorize]
        //public async Task<ActionResult<List<EmployeeDto>>> SearchEmployees(string searchText)
        //{
        //    var result = await _employeeService.SearchEmployees(searchText);

        //    if (result == null)
        //        return NotFound();

        //    return Ok(result);
        //}

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostDto>> AddPost(AddPostDto addPostDto)
        {
            var result = await _postService.AddPost(addPostDto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<PostDto>> UpdatePost(UpdatePostDto updatePostDto, int id)
        {
            var result = await _postService.UpdatePost(updatePostDto, id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeletePost(int id)
        {
            var result = await _postService.DeletePost(id);

            if (result != true)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("{postId}/react")]
        [Authorize]
        public async Task<ActionResult> AddPostReaction(int postId, [FromBody] PostReactions reactionType)
        {
            await _postService.AddPostReaction(postId, reactionType, _userContextService.GetCurrentUserId());

            return Ok("Reaction added successfully");
        }

        [HttpGet("{postId}/reactions")]
        [Authorize]
        public async Task<ActionResult> GetPostReactionsByPostId(int postId)
        {
            var reactions = await _postService.GetPostReactionsByPostId(postId);

            return Ok(reactions);
        }
    }
}
