using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitityLayer.Entities
{
    public class AppUser
    {
        public int AppUserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int AppRoleId { get; set; }
        public AppRole AppRole { get; set; }
        public string OAuthProvider { get; set; }
        public string OAuthId { get; set; }
    }
}
