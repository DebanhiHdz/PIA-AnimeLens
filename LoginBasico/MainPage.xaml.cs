using Newtonsoft.Json;
using System.Text;
var connection = builder.Configuration["ConnectionStrings:DatabaseConnection"];

namespace LoginBasico
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();
        }

        private async void logginBTN_Clicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            // Validar que no estén vacíos
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Por favor ingresa un nombre y una contraseña.", "OK");
                return;
            }

            // Crear el objeto Usuario
            var usuario = new
            {
                Nombre = username,
                Contrasena = password
            };

            // Convertir el objeto a JSON
            var json = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Crear un HttpClient y enviar la solicitud POST
                var client = new HttpClient();

                // Asegúrate de que esta URL sea la correcta a tu backend.
                // Si estás trabajando localmente, considera usar ngrok para exponer tu servidor si pruebas en móvil.
                //var response = await client.PostAsync("https://local:5000/api/Usuarios/iniciar", content);
                var connection = builder.Configuration["ConnectionStrings:DatabaseConnection"];

                if (response.IsSuccessStatusCode)
                {
                    // Mostrar mensaje de éxito
                    await DisplayAlert("Inicio de sesión exitoso", "¡Has iniciado sesión con éxito!", "OK");
                    await Navigation.PushAsync(new NewPage1()); // Cambiar a la página de inicio
                }
                else
                {
                    // Mostrar mensaje de error
                    await DisplayAlert("Error", "Usuario o contraseña incorrectos.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error al realizar la solicitud
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            // Validar que no estén vacíos
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Por favor ingresa un nombre y una contraseña.", "OK");
                return;
            }

            // Crear el objeto Usuario
            var usuario = new
            {
                Nombre = username,
                Contrasena = password
            };

            // Convertir el objeto a JSON
            var json = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Crear un HttpClient y enviar la solicitud POST
                var client = new HttpClient();

                // Asegúrate de que esta URL sea la correcta a tu backend.
                // Si estás trabajando localmente, considera usar ngrok para exponer tu servidor si pruebas en móvil.
                //var response = await client.PostAsync("https://localhost:5000/api/Usuarios/registrar", content);
                //var response = await client.PostAsync("C:\\Program Files\\Microsoft SQL Server\\MSSQL16.MSSQLSERVER\\MSSQL", content);
                var connection = builder.Configuration["ConnectionStrings:DatabaseConnection"];

                if (response.IsSuccessStatusCode)
                {
                    // Mostrar mensaje de éxito
                    await DisplayAlert("Registro exitoso", "¡Usuario registrado con éxito!", "OK");
                }
                else
                {
                    // Mostrar mensaje de error
                    await DisplayAlert("Error", "No se pudo registrar el usuario.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error al realizar la solicitud
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }
    }
}