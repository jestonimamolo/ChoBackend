using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Category
    {
        [Key]
        public int Category_Id { get; set; }

        public string? Name { get; set; } = null;

        public bool? Is_Active { get; set; } = null;
    }
}
