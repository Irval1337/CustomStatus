using System;
using System.Collections.Generic;

namespace CustomStatus
{
    public class DateItem
    {
        public string Name { get; set; }

        public string FormatText { get; set; }

        public DateTime Date { get; set; }

        public bool Repeat { get; set; }
    }

    public class DataSettings
    {
        public List<DateItem> Dates { get; set; }

        public int UseProxy { get; set; }

        public List<string> Proxies { get; set; }
    }
}
