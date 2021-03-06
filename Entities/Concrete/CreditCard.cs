﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class CreditCard:IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Owner { get; set; }
        public string CardNumber { get; set; }
        public string ValidDate { get; set; }
        public string CVV { get; set; }
    }
}
