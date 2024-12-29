using week9.Base;

namespace week9.Model
{
    public class User
    {
        public Guid UserId { get; set; }    

        public string UserName { get; set; }    

        public string Password { get; set; }

        public Currency Currency { get; set; }
    }
}
