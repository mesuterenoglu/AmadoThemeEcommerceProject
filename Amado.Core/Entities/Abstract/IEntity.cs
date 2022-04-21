

using System;
using System.ComponentModel.DataAnnotations;

namespace Amado.Core.Entities.Abstract
{
    public interface IEntity
    {
        public bool IsActive { get; set; }
        public bool IsModified { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
    }
}
