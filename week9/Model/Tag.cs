namespace week9.Model
{
    public class Tag
    {
        public Guid Id { get; set; } = new Guid();

        public string TagName { get; set; } 

        public bool IsActive { get; set; }
    }
}
