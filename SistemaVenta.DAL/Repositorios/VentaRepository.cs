using SistemaVenta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositorios.Contrato;



namespace SistemaVenta.DAL.Repositorios
{
    public class VentaRepository: GenericRepository<Venta>,IVentaRepository
    {
        private readonly VntasdevContext _dbcontext;

        public VentaRepository(VntasdevContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        { //usar transaccion y generar id en detalle venta adeverso da error reestablecer a 0
            Venta ventaGenerada = new Venta();
            using (var transaction = _dbcontext.Database.BeginTransaction())
            { // Implementando la lógica prodcuto dentro de la Venta
                try
                { //foreach para poder interactuar con los productos que estan en detalle venta
                    foreach(DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbcontext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();
                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;

                        // accede a la base de datos para actualizar

                        _dbcontext.Productos.Update(producto_encontrado);
                    }
                    await _dbcontext.SaveChangesAsync();

                    NumeroDocumento correlativo = _dbcontext.NumeroDocumentos.First();
                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbcontext.NumeroDocumentos.Update(correlativo);
                    await _dbcontext.SaveChangesAsync();

                    // generando formato numero de venta 

                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();

                    //Borrar 0 para quye no sean 5 digito sino 4

                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    modelo.NumeroDocumento = numeroVenta;

                    await _dbcontext.Venta.AddAsync(modelo);
                    await _dbcontext.SaveChangesAsync();

                    ventaGenerada = modelo;

                    transaction.Commit();
                }
                catch
                { //reestablecer todo si hay error
                    transaction.Rollback();
                    throw;
                }
                return ventaGenerada;
            }
        }
    }
}
