using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Application.Dtos.Category
{
    public class EditCategoryDto
    {
        public int CategoryId { get; private set; }
        public string Name { get; set; }
    }
}
