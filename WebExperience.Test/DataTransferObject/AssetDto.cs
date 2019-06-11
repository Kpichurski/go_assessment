using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebExperience.Test.DataTransferObject
{
    public class AssetDto
    {
        public string Email { get; set; }
        public string Country { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }

    }
}