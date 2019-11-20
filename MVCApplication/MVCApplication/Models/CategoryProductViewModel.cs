using System.Collections.Generic;

namespace MVCApplication.Models
{
    public class CategoryProductViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

        public int SelectedCategoryId { get; set; }
        public string SelectedCategoryName { get; set; }
    }
}
