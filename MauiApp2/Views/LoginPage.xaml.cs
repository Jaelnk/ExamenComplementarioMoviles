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
        string contrase�a = txtContrase�a.Text?.Trim() ?? "";

        // Validar campos vac�os
        if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrase�a))
        {
            await ShowError("Por favor, complete todos los campos");
            return;
        }

        // Validar credenciales
        if (dbService.ValidateLogin(usuario, contrase�a))
        {
            // Login exitoso - navegar a la p�gina principal
            await Navigation.PushAsync(new vPrincipal_estudiantes());
            
        }
        else
        {
            await ShowError("Usuario o contrase�a incorrectos");
        }
    }

    private async Task ShowError(string message)
    {
        lblError.Text = message;
        lblError.IsVisible = true;

        // Ocultar el mensaje despu�s de 3 segundos
        await Task.Delay(3000);
        lblError.IsVisible = false;
    }
}