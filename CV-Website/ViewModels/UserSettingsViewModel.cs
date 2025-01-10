using CV_Website.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_Website.ViewModels
{
    public class UserSettingsViewModel
        {
            public int Id { get; set; }

           
            public string Name { get; set; }

            public string Address { get; set; }

            [EmailAddress(ErrorMessage = "Ange en giltig e-postadress.")]
            public string UserName { get; set; }

            public string PhoneNumber { get; set; }
            public bool Private { get; set; }

            public string Email { get; set; }
        }
}
