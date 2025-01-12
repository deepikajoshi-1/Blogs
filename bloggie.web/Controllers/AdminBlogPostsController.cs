using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{
    
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepositry tagRepositry;
        private readonly IBlogPostRepositry blogPostRepositry;

        public AdminBlogPostsController(ITagRepositry tagRepositry, IBlogPostRepositry blogPostRepositry)
        {
            this.tagRepositry = tagRepositry;
            this.blogPostRepositry = blogPostRepositry;
        }
        public async Task<IActionResult> Add()
        {
            var getTags= await tagRepositry.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = getTags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            var blogpost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
                ShortDescription = addBlogPostRequest.ShortDescription,
                PublishedDate = addBlogPostRequest.PublishedDate,
                UrlHandle = addBlogPostRequest.UrlHandle,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                Content = addBlogPostRequest.Content
            };
            var selectedTags = new List<Tag>();

            //map Tags from selected tags
            foreach(var selecteditem in addBlogPostRequest.SelectedTags)
            {
                var slectedTagId = Guid.Parse(selecteditem);
                var existingTag= await tagRepositry.GetAsync(slectedTagId);
                selectedTags.Add(existingTag);
            }
            blogpost.Tags = selectedTags;
            Console.WriteLine(selectedTags);
            await blogPostRepositry.AddAsync(blogpost);
            return RedirectToAction("Add");
        }

        public async Task<IActionResult> List()
        {
            var blogPost = await blogPostRepositry.GetAllAsync();
            
            return View(blogPost); 
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Retrieve the result from the repository 
            var blogPost = await blogPostRepositry.GetAsync(id);
            var tagsDomainModel = await tagRepositry.GetAllAsync();

            if (blogPost != null)
            {
                // map the domain model into the view model
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                Console.WriteLine(model.Tags);
                Console.WriteLine(model.SelectedTags);
                return View(model);

            }

            // pass data to view
            return View(null);
        }


    }
}
