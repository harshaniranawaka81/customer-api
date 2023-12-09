using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Business.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerApiContext _context;

        public CustomerRepository(CustomerApiContext context)
        {
            this._context = context;    
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();

            return customers;
        }

        public async Task<Customer?> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            return customer;
        }

        public async Task<bool> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return false;
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<Customer> SaveCustomer(Customer customer)
        {
            if (_context.Customers == null)
            {
                throw new Exception("Entity set 'customer_apiContext.Customer'  is null.");
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            customer.Id = customer.Id;
            return customer;
        }


        public async Task<bool> DeleteCustomer(int id)
        {
            if (_context.Customers == null)
            {
                return false;
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}