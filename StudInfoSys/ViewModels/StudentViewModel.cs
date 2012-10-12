using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudInfoSys.Helpers;
using StudInfoSys.Models;

namespace StudInfoSys.ViewModels
{
    public class StudentViewModel
    {
        [HiddenInput]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "First Name", Order = 1000)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Last Name", Order = 2000)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth", Prompt="Month/Day/Year")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        //[Required]
        //[Range(1, 31)]
        //public int BirthDay { get; set; }

        //[Required]
        //[Range(1, 12)]
        //public Month BirthMonth { get; set; }

        //[Required]
        //[Range(1800, 9999)]
        //public int BirthYear { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Address { get; set; }

        [MaxLength(5000)]
        public byte[] Photo { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }

        public StudentStatus StudentStatus { get; set; }

        [Display(Name = "Name", Order = 100)]
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }

        #region For Dropdown 

        public IEnumerable<SelectListItem> GendersList
        {
            get { return StudInfoSysHelper.GenderToSelectList(); }
            set {}
        }

        public IEnumerable<SelectListItem> StudentStatusList
        {
            get { return StudInfoSysHelper.StudntStatusToSelectList(); }
            set {}
        }

        #endregion

        
    }
}