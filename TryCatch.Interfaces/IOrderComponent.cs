﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Models;

namespace TryCatch.Interfaces
{
    public interface IOrderComponent
    {
        Order Get(int id);
        List<Order> GetMany();
    }
}
