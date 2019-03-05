using System.Collections.Generic;

namespace API.Responses
{
    public class ResponseUserModel
    {
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }


        public ResponseUserModel()
        {
            
        }
    }
}
