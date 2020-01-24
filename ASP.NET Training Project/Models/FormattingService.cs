using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Training_Project.Models
{
    public class FormattingService
    {
        public string AsReadableDate(DateTime date)
        {
            return date.ToString("d");
        }
    }
}
