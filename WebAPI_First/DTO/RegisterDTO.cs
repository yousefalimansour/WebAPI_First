using System.ComponentModel.DataAnnotations;

namespace WebAPI_First.DTO
{
    public class RegisterDTO
    {
        public string UserName {  get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
