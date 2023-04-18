using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace freshMart.Models
{
    public class ProductViewModel
    {
        
        public int prouductId { get; set; }
        [Required]
        public string productName { get; set; }
        [Required]
        public string productImage { get; set; }
        [Required]
        public string productDescription { get; set; }
        [Required]
        public string productPrice { get; set; }


    }
}