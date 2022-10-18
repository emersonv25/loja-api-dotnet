using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_loja.Models.Object
{
    public class ViewUserLogin
    {
        public ViewUser User { get; set; }
        public string Token { get; set; }
    }
    public class ViewUser
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
