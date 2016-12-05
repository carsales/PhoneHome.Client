﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Models
{
    public class DataTag
    {
        public string Key { get; set; }

        public object Value { get; set; }

        public DataTag() { }

        public DataTag(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
