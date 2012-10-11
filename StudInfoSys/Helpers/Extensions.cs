using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudInfoSys.Helpers
{
    //public static class Extensions
    //{
    //    public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
    //    {
    //        var values = from TEnum e in Enum.GetValues(typeof(TEnum))
    //                     select new { Id = e, Name = e.ToString() };

    //        return new SelectList(values, "Id", "Name", enumObj);
    //    }

    //}


    public static class EnumExtensions
    {
        /// <summary>
        /// from http://stackoverflow.com/questions/388483/how-do-you-create-a-dropdownlist-from-an-enum-in-asp-net-mvc
        /// by Zaid Masud
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> ToSelectList(this Enum enumValue)
        {
            return from Enum e in Enum.GetValues(enumValue.GetType())
                   select new SelectListItem
                   {
                       Selected = e.Equals(enumValue),
                       Text = e.ToDescription(),
                       Value = e.ToString()
                   };
        }

        public static string ToDescription(this Enum value)
        {
            var attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }

    

}