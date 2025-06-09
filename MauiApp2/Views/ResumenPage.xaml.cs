namespace MauiApp2.Views;

public partial class ResumenPage : ContentPage
{
    private string contenidoTexto;
    public ResumenPage()
	{
		InitializeComponent();
	}

    public ResumenPage(string tipoId, string numeroId, string nombres, string apellidos, string fecha, string correo, string salarioStr)
    {
        InitializeComponent();

        decimal salario = decimal.TryParse(salarioStr, out var s) ? s : 0;
        decimal aporte = salario * 0.0945m;

        contenidoTexto = $"Nombre: {nombres} {apellidos}\n" +
                         $"Identificación: {tipoId} {numeroId}\n" +
                         $"Fecha Nacimiento: {fecha}\n" +
                         $"Correo: {correo}\n" +
                         $"Salario: {salario:C}\n" +
                         $"Aporte IESS (9.45%): {aporte:C}";

        // Asignar a Labels
        NombreLabel.Text = $"Nombre: {nombres} {apellidos}";
        IdentificacionLabel.Text = $"Identificación: {tipoId} {numeroId}";
        FechaLabel.Text = $"Fecha: {fecha}";
        CorreoLabel.Text = $"Correo: {correo}";
        SalarioLabel.Text = $"Salario: {salario:C}";
        AporteIessLabel.Text = $"Aporte IESS (9.45%): {aporte:C}";
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "contacto.txt");
        File.WriteAllText(filePath, contenidoTexto);

        await DisplayAlert("Éxito", $"Archivo guardado en:\n{filePath}", "OK");
    }
}