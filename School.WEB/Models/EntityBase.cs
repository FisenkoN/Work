using System.ComponentModel.DataAnnotations;

namespace School.WEB.Models
{
    public abstract class EntityBase
    {
        [Key] 
        public int Id { get; set; }
    }
}