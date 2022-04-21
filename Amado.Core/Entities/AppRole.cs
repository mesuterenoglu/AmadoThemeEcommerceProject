

using Amado.Core.Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Amado.Core.Entities
{
    public class AppRole : IdentityRole<string> , IEntity
    {
        public bool IsActive { get; set; }
        public bool IsModified { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
    }
}
