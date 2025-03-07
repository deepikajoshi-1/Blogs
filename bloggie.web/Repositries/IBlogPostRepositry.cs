﻿using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositries
{
    public interface IBlogPostRepositry
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetAsync(Guid id);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid id);
        Task<BlogPost> AddAsync(BlogPost blogPost);
    }
}
