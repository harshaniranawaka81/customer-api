﻿using CustomerApi.Domain.Interfaces;
using CustomerApi.Domain.Models;
using System.Net;

namespace CustomerApi.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository _customerRepository)
        {
            this._customerRepository = _customerRepository;
        }

        public async Task<KeyValuePair<HttpStatusCode, List<Customer>>> GetCustomers()
        {
           var customers = await _customerRepository.GetCustomers();

           if (customers == null || customers.Count == 0)
           {
               return new KeyValuePair<HttpStatusCode, List<Customer>>(HttpStatusCode.NoContent, new List<Customer>());
           }

           return new KeyValuePair<HttpStatusCode, List<Customer>>(HttpStatusCode.OK, customers);
        }

        public async Task<KeyValuePair<HttpStatusCode, Customer?>> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomer(id);

            if (customer == null)
            {
                return new KeyValuePair<HttpStatusCode, Customer?>(HttpStatusCode.NotFound, null);
            }

            return new KeyValuePair<HttpStatusCode, Customer?>(HttpStatusCode.OK, customer);
        }

        public async Task<KeyValuePair<HttpStatusCode, bool>> EditCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return new KeyValuePair<HttpStatusCode, bool>(HttpStatusCode.BadRequest, false);
            }

            var updated = await _customerRepository.UpdateCustomer(id, customer);

            if(updated)
                return new KeyValuePair<HttpStatusCode, bool>(HttpStatusCode.OK, updated);
            else
                return new KeyValuePair<HttpStatusCode, bool>(HttpStatusCode.NoContent, false);
        }

        public async Task<KeyValuePair<HttpStatusCode, Customer?>> SaveCustomer(Customer customer)
        {
            var savedCustomer = await _customerRepository.SaveCustomer(customer);

            if (customer != null)
                return new KeyValuePair<HttpStatusCode, Customer?>(HttpStatusCode.Created, savedCustomer);
            else
                return new KeyValuePair<HttpStatusCode, Customer?>(HttpStatusCode.NoContent, null);
        }


        public async Task<KeyValuePair<HttpStatusCode, bool>> DeleteCustomer(int id)
        {
            var deleted = await _customerRepository.DeleteCustomer(id);

            if (deleted)
                return new KeyValuePair<HttpStatusCode, bool>(HttpStatusCode.NoContent, deleted);
            else
                return new KeyValuePair<HttpStatusCode, bool>(HttpStatusCode.NotFound, false);
        }

    }
}
