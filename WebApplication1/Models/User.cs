using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public enum Role
    {
        User, Tech, Admin, SuperAdmin
    }
    public class User
    {
        public int UserID { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string FullAddress { get; set; }
        [Required]
        [StringLength(100)]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegisterDate { get; set; }
        public Role? Role { get; set; }
        public virtual ICollection<RentalInfo> RentalInfos { get; set; }
    }
}