using Microsoft.AspNetCore.Mvc;
using Backend.Models; // Asegúrate de incluir el namespace donde está el modelo Usuario
using System.Collections.Generic;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private DataAccess _dataAccess; // Agregar un campo para DataAccess
        public UsuariosController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess; // Inicializa la variable
        }
        // POST: api/usuarios/registrar
        [HttpPost("registrar")]
        public IActionResult RegistrarUsuario([FromBody] Usuario nuevoUsuario)
        {
            if (nuevoUsuario == null || string.IsNullOrEmpty(nuevoUsuario.Nombre) || string.IsNullOrEmpty(nuevoUsuario.Contrasena))
            {
                return BadRequest("El nombre y la contraseña son requeridos.");
            }

            try
            {
                _dataAccess.InsertarUsuario(nuevoUsuario.Nombre, nuevoUsuario.Contrasena); // Llama a InsertarUsuario
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar usuario: {ex.Message}");
            }

            return Ok(new { mensaje = "Usuario registrado con éxito", usuario = nuevoUsuario });
        }

        [HttpPost("iniciar")]
        public IActionResult IniciarSesion([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(usuario.Contrasena))
            {
                return BadRequest("El nombre y la contraseña son requeridos.");
            }

            try
            {
                // Verificar si el usuario existe en la base de datos
                bool existeUsuario = _dataAccess.VerificarUsuario(usuario.Nombre, usuario.Contrasena); // Llama a la función para verificar usuario

                if (existeUsuario)
                {
                    return Ok(new { mensaje = "Inicio de sesión exitoso." });
                }
                else
                {
                    return Unauthorized("Credenciales inválidas.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al iniciar sesión: {ex.Message}");
            }
        }
    }
}
