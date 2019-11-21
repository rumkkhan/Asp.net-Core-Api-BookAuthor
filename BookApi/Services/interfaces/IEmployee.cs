using BookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services.interfaces
{
    public interface IEmployee
    {
        Task<bool> save(IEnumerable<Employee> employee);
    }
}
