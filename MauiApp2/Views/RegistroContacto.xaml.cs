using System.Text.RegularExpressions;

namespace MauiApp2.Views;

public partial class RegistroContacto : ContentPage
{
	public RegistroContacto()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var tipoId = IdentificacionPicker.SelectedItem?.ToString();
        var numeroId = NumeroIdEntry.Text?.Trim();
        var nombres = NombresEntry.Text?.Trim();
        var apellidos = ApellidosEntry.Text?.Trim();
        var correo = CorreoEntry.Text?.Trim();
        var salario = SalarioEntry.Text?.Trim();
        var fecha = FechaPicker.Date.ToString("yyyy-MM-dd");

        if (string.IsNullOrWhiteSpace(tipoId) || string.IsNullOrWhiteSpace(numeroId) ||
            string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(apellidos))
        {
            DisplayAlert("Error", "Complete todos los campos requeridos.", "OK");
            return;
        }

        // Validaciones específicas
        if (tipoId == "CI" && numeroId.Length != 10)
        {
            DisplayAlert("Error", "La CI debe tener 10 dígitos.", "OK");
            return;
        }

        if (tipoId == "RUC" && numeroId.Length != 13)
        {
            DisplayAlert("Error", "El RUC debe tener 13 dígitos.", "OK");
            return;
        }

        if (!string.IsNullOrWhiteSpace(correo) && !Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            DisplayAlert("Error", "Correo no válido.", "OK");
            return;
        }

        DisplayAlert("Contacto Guardado", $"Nombre: {nombres} {apellidos}\nIdentificación: {tipoId} {numeroId}\nCorreo: {correo}\nFecha: {fecha}\nSalario: {salario}", "OK");
        await Navigation.PushAsync(new ResumenPage(tipoId, numeroId, nombres, apellidos, fecha, correo, salario));
    }
}