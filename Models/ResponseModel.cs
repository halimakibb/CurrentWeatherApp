using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ResponseModel<T>
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public List<T> Data { get; set; }
        public T DataSingular { get; set; }
    }
}
