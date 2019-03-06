using API.Responses;
using BLL.Entities;

namespace API.Requests
{
    public class AuthorizeUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }


        public AuthorizeUserModel()
        {
            
        }


        public static explicit operator ResponseUserModel(AuthorizeUserModel model)
        {
            return new ResponseUserModel
            {
                Email = model.Email
            };
        }

        public static explicit operator User(AuthorizeUserModel userModel)
        {
            return new User {
                Email = userModel.Email,

            };
        }
    }
}
