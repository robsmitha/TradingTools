using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TradingTools.Shared.Helpers
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription<TEnum>(this TEnum item) => item.GetType()
              .GetField(item.ToString())
              .GetCustomAttributes(typeof(DescriptionAttribute), false)
              .Cast<DescriptionAttribute>()
              .FirstOrDefault()?.Description ?? nameof(TEnum);
    }
}
