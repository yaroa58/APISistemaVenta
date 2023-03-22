using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;


namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioServicio;

        public UsuarioController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }
        //Devolver la lista de usuarios

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<UsuarioDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _usuarioServicio.lista();
            }
            catch (Exception ex)
            {

                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
        [HttpPost]
        [Route("IniciarSesion")]

        //contenedor de credenciales de usuario
        public async Task<IActionResult> IniciarSesion([FromBody]LoginDTO login)
        {
            var rsp = new Response<SesionDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _usuarioServicio.ValidarCredenciales(login.Correo,login.Clave);
            }
            catch (Exception ex)
            {

                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("Guardar")]

        //contenedor para guardar usuario
        public async Task<IActionResult> Guardar([FromBody] UsuarioDTO usuario)
        {
            var rsp = new Response<UsuarioDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _usuarioServicio.Crear(usuario);
            }
            catch (Exception ex)
            {

                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPut]
        [Route("Editar")]

        //contenedor para editar usuario
        public async Task<IActionResult> Editar([FromBody] UsuarioDTO usuario)
        {
            var rsp = new Response<bool>();
            try
            {
                rsp.status = true;
                rsp.value = await _usuarioServicio.Editar(usuario);
            }
            catch (Exception ex)
            {

                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]

        //contenedor para eliminar usuario
        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = new Response<bool>();
            try
            {
                rsp.status = true;
                rsp.value = await _usuarioServicio.Eliminar(id);
            }
            catch (Exception ex)
            {

                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
    }
}
