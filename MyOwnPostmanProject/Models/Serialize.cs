using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOwnPostmanProject.Models
{
    public static class Serialize
    {
        public static string ToJson(this Household self) => JsonConvert.SerializeObject(self, MyOwnPostmanProject.Models.Converter.Settings);
        public static string ToJson(this Users self) => JsonConvert.SerializeObject(self, MyOwnPostmanProject.Models.Converter.Settings);
        public static string ToJson(this Book self) => JsonConvert.SerializeObject(self, MyOwnPostmanProject.Models.Converter.Settings);
        public static string ToJson(this Link self) => JsonConvert.SerializeObject(self, MyOwnPostmanProject.Models.Converter.Settings);

    }
}
