using System.ComponentModel.DataAnnotations;

namespace School.DAL.Entities
{
    public abstract class EntityBase
    {
        [Key] public int Id { get; set; }
    }
}