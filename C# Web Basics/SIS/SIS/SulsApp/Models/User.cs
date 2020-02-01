using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SulsApp.Models
{
    public class User
    {
        public User()
        {
            this.Submissions = new HashSet<Submission>();
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        //...
        public string Password { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
