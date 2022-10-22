using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "es requerido")]
        [EmailAddress(ErrorMessage = "no válido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
