using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ballazza.Models
{
    public class MailModel
    {
        [Required(ErrorMessage = "this column cannot be empty")]
        [DataType(DataType.Text)]
        public string To { get; set; }
        
        [Required(ErrorMessage = "this column cannot be empty")]
        [DataType(DataType.Text)]
        public string Subject { get; set; }
        
        [Required(ErrorMessage = "this column cannot be empty")]
        [DataType(DataType.Text)]
        public string Body { get; set; }
    }
}