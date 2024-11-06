using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace Backend
{
    public class DataAccess
    {
        private readonly string _connectionString;

        // El constructor recibe la configuración (IConfiguration) para obtener la connection string desde appsettings
        public DataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region InsertarUsuario
        // Método para insertar un usuario en la base de datos
        public void InsertarUsuario(string nombre, string contrasena)
        {
            // Definir el query de inserción
            string query = "INSERT INTO Usuarios (Nombre, Contrasena) VALUES (@Nombre, @Contrasena)";

            try
            {
                // Usar SqlConnection para conectarse a la base de datos
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    // Abrir la conexión
                    conn.Open();

                    // Usar SqlCommand para ejecutar el query
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Agregar los parámetros al comando
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                        // Ejecutar el comando
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí podrías agregar manejo de errores o logging
                Console.WriteLine($"Error al insertar usuario: {ex.Message}");
                throw;
            }
        }
        #endregion

        #region VerificarUsuario
        public bool VerificarUsuario(string nombre, string contrasena)
        {
            // Definir el query para verificar si existe el usuario
            string query = "SELECT COUNT(*) FROM Usuarios WHERE Nombre = @Nombre AND Contrasena = @Contrasena";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                        int count = (int)cmd.ExecuteScalar(); // Obtiene el conteo de registros que coinciden
                        return count > 0; // Retorna true si el usuario existe
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí podrías agregar manejo de errores o logging
                Console.WriteLine($"Error al verificar usuario: {ex.Message}");
                throw;
            }
        }
        #endregion

    }
}
