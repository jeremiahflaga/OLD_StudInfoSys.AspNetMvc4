using StudInfoSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudInfoSys.Helpers
{
    public static class StudInfoSysHelper
    {
        public static IEnumerable<SelectListItem> GenderToSelectList()
        {
            var genderItems = EnumHelpers.ToSelectList(typeof(Gender)).ToList();
            genderItems.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            return genderItems;
        }

        public static IEnumerable<SelectListItem> StudntStatusToSelectList()
        {
            var genderItems = EnumHelpers.ToSelectList(typeof(StudentStatus)).ToList();
            genderItems.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            return genderItems;
        }
    }
}