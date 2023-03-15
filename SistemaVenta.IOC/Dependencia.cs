using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.IOC
{
   public static class Dependencia
    {
        //create metodo publico para recibir servicio de conexiones  base de datos 
    public static void inyectarDependencias(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<VntasdevContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });
        }
    }
}
