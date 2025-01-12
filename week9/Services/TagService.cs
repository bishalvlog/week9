using week9.Abstraction;
using week9.Model;
using week9.Model.Dto;
using week9.Model.Exception;
using week9.Services.Interface;

namespace week9.Services
{
    public class TagService :UserBase<Tag>, ITag
    {
        private List<Tag> _tags;

        public TagService() : base("Tag.json")
        {
            _tags = LoadItems();
        }

        public void ActiveDeactive(Guid Id, bool isActive)
        {
            var success = UpdateItem(t => t.Id == Id,t => t.IsActive = isActive);
        }

        public async Task AddTag(CreateTagDto tag)
        {
             var exists = _tags.FirstOrDefault<Tag>(t =>t.TagName == tag.TagName);

                if (exists != null)
                {
                    throw new NotFoundException("the flowong tag is already exits");
                }

                var tagModel = new Tag()
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    TagName = tag.TagName,
                };

                _tags.Add(tagModel);

                SaveItems(_tags);
        }

        public List<Tag> GetAllTag()
        {
            return _tags.ToList();
        }

        public Tag TagGetById(Guid Id)
        {
            try
            {
               
              return _tags.FirstOrDefault(t => t.Id == Id);
                
            }
            catch (Exception ex) 
            {
                throw new Exception("An error occurred while fetching the tag by ID.", ex);
            }
        }

        public async Task UpdateTag(UpdateTagDto tag)
        {
            UpdateItem(t => t.Id == tag.Id, t =>
            {
                t.TagName = tag.TagName;
            });
        }
    }
}
