using BLL.Entities;

namespace API.Requests
{
    public class UserRegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }


        public UserRegisterModel()
        {
            
        }


        public static explicit operator User(UserRegisterModel model) => new User {Email = model.Email};
    }
}
