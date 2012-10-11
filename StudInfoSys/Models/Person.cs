using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StudInfoSys.Models
{
    public class Person
    {
        public virtual int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "First Name", Order=1000)]
        public virtual string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Last Name", Order=2000)]
        public virtual string LastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public virtual DateTime DateOfBirth { get; set; }

        

        [Required]
        [StringLength(500, MinimumLength = 1)]
        public virtual string Address { get; set; }

        [MaxLength(5000)]
        public virtual byte[] Photo { get; set; }

        [Required]
        public virtual Gender Gender { get; set; }

        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }

        [Display(Name = "Name", Order = 100)]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }
    }
}
