using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Application.Dtos.Cart
{
    public class ResponseCartDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Subtotal { get; set; }
        public bool IsFinished { get; set; }
    }
}
