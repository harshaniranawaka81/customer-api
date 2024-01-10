using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CustomerApi.Repository;
using CustomerApi.Business.Services;
using CustomerApi.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerApi.Business.Extensions
{
    /// <summary>
    /// Class to store all service extensions
    /// </summary>
    public static class ServiceExtensions
    {    
        /// <summary>
        /// Configure the database connection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void ConfigureDb(this IServiceCollection services)
        {
            services.AddDbContext<CustomerApiContext>(
                options => options.UseInMemoryDatabase("CustomerDb"));
        }

        /// <summary>
        /// Register all custom services
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
