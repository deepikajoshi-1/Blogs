using Azure;
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositries
{
    public class TagRepositry : ITagRepositry
    {
        private readonly BloggieDbContext bloggieDbContext;
        public TagRepositry(BloggieDbContext bloggieDbContext) 
        { 
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var tag = await bloggieDbContext.Tags.FindAsync(id);
            bloggieDbContext.Remove(tag);
            await bloggieDbContext.SaveChangesAsync();
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
             return await bloggieDbContext.Tags.ToListAsync();
        }

        public async Task<Tag> GetAsync(Guid id)
        {
            var tag= await bloggieDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
            return tag;
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag= await bloggieDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;

        }
    }
}
