namespace week9.Model.Dto
{
    public class UpdateTagDto
    {
        public Guid Id { get; set; }

        public string TagName { get; set; }

        public bool IsActive { get; set; }
    }
}
