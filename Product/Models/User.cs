namespace Product.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class UserRegister : User
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
    }

}
