using MauiApp2.Modelos;

namespace MauiApp2.Views;

public partial class LoginPage : ContentPage
{
    private DatabaseService dbService;

    public LoginPage()
    {
        InitializeComponent();
        dbService = App.dbService;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string usuario = txtUsuario.Text?.Trim() ?? "";
        string contraseña = txtContraseña.Text?.Trim() ?? "";

        // Validar campos vacíos
        if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
        {
            await ShowError("Por favor, complete todos los campos");
            return;
        }

        // Validar credenciales
        if (dbService.ValidateLogin(usuario, contraseña))
        {
            // Login exitoso - navegar a la página principal
            await Navigation.PushAsync(new vPrincipal_estudiantes());
            
        }
        else
        {
            await ShowError("Usuario o contraseña incorrectos");
        }
    }

    private async Task ShowError(string message)
    {
        lblError.Text = message;
        lblError.IsVisible = true;

        // Ocultar el mensaje después de 3 segundos
        await Task.Delay(3000);
        lblError.IsVisible = false;
    }
}