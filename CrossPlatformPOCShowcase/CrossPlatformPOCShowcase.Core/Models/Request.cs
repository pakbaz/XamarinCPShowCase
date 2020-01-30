using System;
using System.Collections.Generic;
using System.Text;

namespace CrossPlatformPOCShowcase.Core.Models
{
    public class Request
    {
        public string id { get; set; }

        public string name { get; set; }
        public string headquarter { get; set; }
        public string teamLocation { get; set; }
        public string description { get; set; }
        public DateTimeOffset createdAt { get; set; }
        public DateTimeOffset updatedAt { get; set; }
        public byte[] version { get; set; }
        public bool deleted { get; set; }
        public bool complete { get; set; }
    }
}
