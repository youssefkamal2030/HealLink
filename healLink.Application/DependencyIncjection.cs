using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Common;
using Microsoft.Extensions.DependencyInjection;
using healLink.Application.Interfaces;

namespace healLink.Application
{
    public static class ApplicationDIExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
         
            return services;
        }
    }
}
