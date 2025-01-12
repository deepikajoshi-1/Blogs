using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepositry tagRepositry;
        public AdminTagsController(ITagRepositry tagRepositry)
        {
            this.tagRepositry = tagRepositry;
                
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //since we have Tags entity in dbContext so we are creating a Tag object
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            await tagRepositry.AddAsync(tag);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
          var tags= await tagRepositry.GetAllAsync();
            return View(tags);
        }
        [HttpGet]

       
        public async Task<IActionResult> Edit(Guid id)
        {
            //interacting w dbcontext to retrieve the tags information for the given id
            var tag = await tagRepositry.GetAsync(id);
            if (tag != null)
            {
                EditForRequest editForRequest = new EditForRequest();
                editForRequest.Name = tag.Name;
                editForRequest.DisplayName = tag.DisplayName;
                return View(editForRequest);
            }
            return View(null);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditForRequest editForRequest)
        {
            var tag = new Tag
            {
                Name = editForRequest.Name,
                DisplayName = editForRequest.DisplayName,
                Id = editForRequest.Id
            };
            await tagRepositry.UpdateAsync(tag);
            
            return RedirectToAction("List");
        }

        
        public async Task<IActionResult> Delete(Guid id)
        {
           await tagRepositry.DeleteAsync(id);
            return RedirectToAction("List");
        }
    }
}
