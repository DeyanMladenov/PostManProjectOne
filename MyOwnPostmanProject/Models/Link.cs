﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOwnPostmanProject.Models
{
    public class Link
    {
        [JsonProperty("rel", NullValueHandling = NullValueHandling.Ignore)]
        public string Rel { get; set; }

        [JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Href { get; set; }
    }
}
