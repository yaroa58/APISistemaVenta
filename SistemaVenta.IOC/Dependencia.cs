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

using SistemaVenta.Utility;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.BLL.Servicios;

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

            // add dependencia automapper con todos los mapeos 

            services.AddAutoMapper(typeof(AutoMapperProfile));

            // add dependencia servicios  

            services.AddScoped<IRolService,RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICategoriaService,CategoriaService>();
            services.AddScoped<IProductoService,ProductoService>();
            services.AddScoped<IVentasService,VentasService>();
            services.AddScoped<IDashboardService,DashBoardService>();
            services.AddScoped<IMenuService,MenuService>();

        }
    }
}
