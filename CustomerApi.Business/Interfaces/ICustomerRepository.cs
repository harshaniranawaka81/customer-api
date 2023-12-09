using CustomerApi.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Business.Interfaces
{    
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomers();

        Task<Customer?> GetCustomer(int id);

        Task<bool> UpdateCustomer(int id, Customer customer);

        Task<Customer> SaveCustomer(Customer customer);

        Task<bool> DeleteCustomer(int id);
      
    }
}
