using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Database.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }
    }
}
