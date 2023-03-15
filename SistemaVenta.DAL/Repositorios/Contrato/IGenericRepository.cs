using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SistemaVenta.DAL.Repositorios.Contrato
{
    //se convierte la interfaz publica para que actuen como contrato y crear metodos para interactuar con DB y models
    public interface IGenericRepository<TModel> where TModel : class
    {//expresion para obtener algún modelo menu rol ususario .....
        Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro);
        //recibir un modelo para crearlo Cateria menu....
        Task<TModel> Crear(TModel modelo);
        //devolver un bool
        Task<bool> Editar(TModel modelo);
        //eliminar modelo devolviendo bool
        Task<bool> Eliminar(TModel modelo);
        //devolver la consulta del modelo 
        Task<IQueryable<TModel>> Consultar(Expression<Func<TModel, bool>> filtro=null);
    }
}
