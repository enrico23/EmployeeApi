﻿using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Services
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
    }
}
