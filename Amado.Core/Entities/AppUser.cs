

using Amado.Core.Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Amado.Core.Entities
{
    public class AppUser : IdentityUser , IEntity
    {
        [Required(ErrorMessage = "İsim zorunludur!")]
        [MaxLength(100, ErrorMessage = "İsim 100 karakterden fazla olamaz!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim zorunludur!")]
        [MaxLength(200, ErrorMessage = "Soyisim 200 karakterden fazla olamaz!")]
        public string LastName { get; set; }

        private string _fullname;
        public string FullName
        {
            get
            {
                if (FirstName != null && LastName != null)
                {
                    _fullname = $"{FirstName} {LastName}";
                }
                else
                {
                    _fullname = "Name Surname";
                }
                return _fullname;
            }
        }

        public bool IsActive { get; set; }
        public bool IsModified { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
    }
}
