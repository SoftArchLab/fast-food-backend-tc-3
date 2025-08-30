using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Application.Dtos.Order
{
    public class ResponseOrderDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int CartId { get; set; }
        public int PaymentId { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int OrderStatusId { get; set; }
    }
}
