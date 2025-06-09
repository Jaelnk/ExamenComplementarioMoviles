namespace MauiApp2.Views;

public partial class CameraPage : ContentPage
{
    public CameraPage()
    {
        InitializeComponent();
    }

    private async void OnTomarFotoClicked(object sender, EventArgs e)
    {
        try
        {
            var foto = await MediaPicker.CapturePhotoAsync();

            if (foto != null)
            {
                var stream = await foto.OpenReadAsync();
                imgFoto.Source = ImageSource.FromStream(() => stream);
                lblEstado.Text = "Foto tomada correctamente";
            }
        }
        catch (Exception ex)
        {
            lblEstado.Text = $"Error: {ex.Message}";
        }
    }
}