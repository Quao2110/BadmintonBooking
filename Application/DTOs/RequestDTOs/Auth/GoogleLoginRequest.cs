using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RequestDTOs.Auth
{
    public class GoogleLoginRequest
    {
        [Required]
        public string IdToken { get; set; } = null!;
    }

}
