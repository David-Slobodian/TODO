using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.Dal.Entities
{
    public class TodoEntity : BaseEntity
    {
        [Required(ErrorMessage = "Title is required")]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Column(TypeName = "nvarchar(150)")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Column(TypeName = "bit")]
        public bool Status { get; set; }
    }
}
