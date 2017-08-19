using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YavinWindowsClient
{
    static class FormatUtils
    {
        public static string ToMoney(decimal money)
        {
           return string.Format("{0:n}", money);
        }
    }
}
