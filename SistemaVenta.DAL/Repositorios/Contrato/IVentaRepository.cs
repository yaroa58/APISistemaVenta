using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.Model;

namespace SistemaVenta.DAL.Repositorios.Contrato
{ //Creacion de metodos para interactuar con el registro de una venta
    public interface IVentaRepository: IGenericRepository<Venta>
    {
        Task<Venta> Registrar(Venta modelo);
    }
}
