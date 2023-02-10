using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudBlazorUICRUDApp.Shared.AuthModels
{
    public class UserDetails
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
    }
}
