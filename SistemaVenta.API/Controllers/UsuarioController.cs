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

    }
}
