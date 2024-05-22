using choapi.Models;

namespace choapi.Messages
{
    public class CategoryResponse : ResponseBase
    {
        public Category Category { get; set; } = new Category();
    }
}
