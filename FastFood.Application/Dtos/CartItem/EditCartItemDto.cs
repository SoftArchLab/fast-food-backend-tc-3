﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Application.Dtos.CartItem
{
    public class EditCartItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
