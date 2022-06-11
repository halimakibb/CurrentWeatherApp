using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class JsonModel
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }

        public string JsonData { get; set; }    
    }
}
