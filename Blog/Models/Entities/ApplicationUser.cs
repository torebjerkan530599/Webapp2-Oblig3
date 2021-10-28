using System;

namespace Blog.Models.Entities
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public DateTime? LastLoggedIn { get; set; }

        public bool? IsEnabled { get; set; }

        public bool? IsAdmin { get; set; }

        //public List<Blogg> Bloggs { get; set; } //abonnement

    }
}