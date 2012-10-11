using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudInfoSys.Helpers
{
    public class EnumHelpers
    {
        public static IEnumerable<SelectListItem> ToSelectList(Type enumType)
        {
            var values = (from Enum e in Enum.GetValues(enumType)
                          select new SelectListItem
                          {
                              // Selected = e.Equals(enumValue),
                              Text = ToDescription(e),
                              Value = e.ToString()
                          }).ToList();
            values.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            return values;
        }

        public static string ToDescription(Enum value)
        {
            var attributes =
                (DescriptionAttribute[])
                value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}