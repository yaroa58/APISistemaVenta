using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;

// esta clase que implementa la interfaz iusuarioservice
namespace SistemaVenta.BLL.Servicios
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> lista()
        {
            try
            {
                //Obetener todos los roles que tiene los usuarios
                var queryUsuario = await _usuarioRepositorio.Consultar();
                var listaUsuarios=queryUsuario.Include(rol=>rol.IdRolNavigation).ToList();
                // convertir a lista usuario tipo DTO y el origen listausuarios
                return _mapper.Map<List<UsuarioDTO>>(listaUsuarios);
            }
            catch 
            {

                throw;
            }
        }

        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consultar(u =>
                u.Correo == correo &&
                u.Clave == clave
                );

                //si no lo encuentra y se cancela la tarea
                if (queryUsuario.FirstOrDefault() == null)
                    throw new TaskCanceledException("El usuario no existe");
                //delvolver respuesta

                Usuario devolverUsuario=queryUsuario.Include(rol=> rol.IdRolNavigation).First();
                // retornar sesion DTO convirtiendo con mapp

                return _mapper.Map<SesionDTO>(devolverUsuario);
            }
            catch 
            {

                throw;
            }
        }

        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try
            {
                var usuarioCreado = await _usuarioRepositorio.Crear(_mapper.Map<Usuario>(modelo));
                //si no se crear devolverá mensaje 

                if (usuarioCreado.IdUsuario == 0)
                    throw new TaskCanceledException("El usuario no se pudo crear");

                var query = await _usuarioRepositorio.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);

                //Actualizar el usuario creado con descripcion de rol

                usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();
               // Retonar usuario en usuario dto 
                return _mapper.Map<UsuarioDTO>(usuarioCreado);  
            }
            catch
            {

                throw;
            }
        }

        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            { // convertir modelo en tipo usuario que tenga la clase de los modelo
                var usuarioModelo=_mapper.Map<Usuario>(modelo);

              //encontrar usuario con el id 
                var usuarioEncontrado=await _usuarioRepositorio.Obtener(u=>u.IdUsuario==usuarioModelo.IdUsuario);
                if(usuarioEncontrado==null)
                    throw new TaskCanceledException("El usuario no existe");

                //si existe editar sus propiedades
                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;

                // enviar usuario hacia el metodo para editarlo
                bool respuesta = await _usuarioRepositorio.Editar(usuarioEncontrado);
                if (!respuesta)
                    throw new TaskCanceledException("No se ha podido editar");
                return respuesta;
            }
            catch
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.IdUsuario == id);
                if(usuarioEncontrado==null)
                    throw new TaskCanceledException("Usuario No existe");
                bool respuesta = await _usuarioRepositorio.Eliminar(usuarioEncontrado);
                if (!respuesta)
                    throw new TaskCanceledException("No se ha podido Eliminar");
                return respuesta;
            }
            catch
            {

                throw;
            }
        }

    }
}
