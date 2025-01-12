using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositries
{
    public interface ITagRepositry
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag> GetAsync(Guid id);

        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);
        Task<Tag> AddAsync(Tag tag);
    }
}
