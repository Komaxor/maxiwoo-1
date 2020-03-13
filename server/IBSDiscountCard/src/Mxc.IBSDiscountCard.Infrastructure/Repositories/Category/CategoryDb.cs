using System;
using System.ComponentModel.DataAnnotations;

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories.Category
{
    public class CategoryDb
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Lang_Key { get; set; }
        public string previewImage { get; set; }
    }
}
