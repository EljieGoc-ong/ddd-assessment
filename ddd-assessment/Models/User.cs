using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ddd_assessment.Models
{
    public class User
    {
        [Required(ErrorMessage = "Please Enter UserName!!!")]
        [MinLength(4, ErrorMessage = "Please enter atleast 4 chars")]
        public string UserName { get; set; }
    }
}
