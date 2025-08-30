using FastFood.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Application.Dtos.User
{
    public class ResponseUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaxId { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
