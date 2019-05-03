using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ghumpo.model.Mobile;

namespace ghumpo.web.Core
{
    public static class WebCommon
    {
        public static SelectList ConvertEnumToSelectList(Enum enumValue)
        {
            var list = (
                from object item in Enum.GetValues(enumValue.GetType())
                select
                    new SelectListItem {Text = item.ToString().Replace("_", " "), Value = Convert.ToString((int) item)}
                ).ToList();
            return new SelectList(list.OrderBy(m => m.Value), "Value", "Text", null);
        }

        public static SelectList ConvertListToSelectList(List<BusinessGroupMobile> list)
        {
            list.Add(new BusinessGroupMobile {id = 0, name = "None"});
            var lists = from item in list select new SelectListItem {Text = item.name, Value = item.id.ToString()};
            var selectList = new SelectList(lists, "Value", "Text", null);
            return selectList;
        }
    }
}