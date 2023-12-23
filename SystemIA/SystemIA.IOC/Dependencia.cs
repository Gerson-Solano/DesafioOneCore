using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SystemIA.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using SystemIA.DAL.Interfaces;

namespace SystemIA.IOC
{
    public static class Dependencia
    {
        public static void DependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBIASYSTEMContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("StringConexion"));
            });
        }
    }
}
