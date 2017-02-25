﻿using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeesApplication.Services
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password) {
            using (EmployeeDbEntities db = new EmployeeDbEntities())
            {
                return db.users.Any(s => 
                    s.Username.Equals(username,StringComparison.OrdinalIgnoreCase) 
                    && s.Password == password);
            }
        }
    }
}