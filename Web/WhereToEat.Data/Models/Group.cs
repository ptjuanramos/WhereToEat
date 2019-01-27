using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToEat.Data.Models
{
    [Table("Groups")]
    public class Group
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Guid> Admins { get; set; }

        public List<Guid> Members { get; set; }
    }
}
