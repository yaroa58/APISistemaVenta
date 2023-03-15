using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DAL.Repositorios;

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

            //inyectar dependencias DAL para que sea utlizada en esta capa (modelo generico)

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // dependencia para ventas (especificando el modelo exacto con scoped)
            services.AddScoped<IVentaRepository, VentaRepository>();
        }
    }
}
