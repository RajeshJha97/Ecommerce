using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.Models.Models.DTO.Category
{
    public class CategoryUpdateDTO
    {
        
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }=DateTime.Now;


       
    }
}
