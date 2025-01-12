using week9.Model;
using week9.Model.Dto;

namespace week9.Services.Interface
{
    public interface ITag
    {
        List<Tag> GetAllTag();

        Tag TagGetById(Guid Id);

        Task AddTag(CreateTagDto tag);

        void ActiveDeactive(Guid Id, bool isActive);

        Task UpdateTag(UpdateTagDto tag);
    }
}
