using BookApi.Models;
using BookApi.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly BookDbContext _bookDbContext;
        public EmployeeService(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }
        public async Task<bool> save(IEnumerable<Employee> employee)
        {
            try
            {
                await _bookDbContext.AddRangeAsync(employee);
                var saved = await _bookDbContext.SaveChangesAsync();

                return saved >= 0 ? true : false;
            }
            catch (Exception e)
            {
               throw new Exception( e.Message);
                throw;
            }
          

        }

       
    }
}
