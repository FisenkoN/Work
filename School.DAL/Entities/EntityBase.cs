using System;
using System.ComponentModel.DataAnnotations;

namespace School.DAL.Entities
{
    public abstract class EntityBase
    {
        [Key] 
        public int Id { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime LastUpdatedTime { get; set; }
    }
}