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

        public void ActiveDeactive(Guid Id)
        {
            var tag = _tags.FirstOrDefault(t => t.Id == Id);

            if(tag != null)
            {
                tag.IsActive = false;
            }
        }

        public async Task AddTag(CreateTagDto tag)
        {
            try
            {
                var exists = _tags.FirstOrDefault<Tag>(t =>t.TagName == tag.TagName);

                if (exists != null)
                {
                    throw new NotFoundException("the flowong tag is already exits");
                }

                var tagModel = new Tag()
                {
                    Id = new Guid(),
                    IsActive = true,
                    TagName = tag.TagName,
                };
                _tags.Add(tagModel);

                SaveItems(_tags);
            }
            catch(Exception ex)
            {
                throw new NotFoundException("some this is wrong");
            }
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
    }
}
