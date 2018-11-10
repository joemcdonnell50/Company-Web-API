using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Company_COREMvc.Models
{
    public class Company
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Exchange { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Ticker { get; set; }

        [RegularExpression(@"\b([A-Z]{2})((?![A-Z]{10}\b)[A-Z0-9]{10})\b")]
        [Required]
        [Remote(action: "VerifyISIN", controller: "Companies")]
        public string ISIN { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Website { get; set; }
    }
}
