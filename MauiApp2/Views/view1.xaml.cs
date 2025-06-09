namespace MauiApp2.Views;

public partial class view1 : ContentPage
{
	public view1()
	{
		InitializeComponent();
	}
    string usuarion = "a";
    string contrasenian = "b";
    public view1(String usuario, String Contrasenia)
    {
        this.usuarion = usuario;
        this.contrasenian = Contrasenia;
        InitializeComponent();
    }

    private void btnIniciar_Clicked(object sender, EventArgs e)
    {
        //String usuario = "user";
        //String contrasenia = "12345";
        

		if (txtusuario.Text == this.usuarion &&txtcontraseña.Text== this.contrasenian)
		{
            Navigation.PushAsync(new Views.view2(usuarion));
        }
		else
		{
            DisplayAlert("Error", "datos incorrectos", "cerrar");
		}
    }

    private void btnRegistrar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.view3());
    }
}