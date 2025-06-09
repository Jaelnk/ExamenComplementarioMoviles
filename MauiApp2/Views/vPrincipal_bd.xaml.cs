using MauiApp2.Modelos;

namespace MauiApp2.Views;

public partial class vPrincipal_bd : ContentPage
{
	public vPrincipal_bd()
	{
		InitializeComponent();
	}

    private void btnIngresar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        App.personRepo.AddNewPerson(txtxNombre.Text);
        statusMessage.Text = App.personRepo.Message;
    }

    private void btnListar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = string.Empty;
        RefreshList();
    }

    private void RefreshList()
    {
        var lista = App.personRepo.GetAllPerson();
        listaPersona.ItemsSource = lista;
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        if (button.CommandParameter is Persona persona)
        {
            string result = await DisplayPromptAsync(
                "Editar Persona",
                "Ingrese el nuevo nombre:",
                initialValue: persona.Name,
                maxLength: 25);

            if (!string.IsNullOrWhiteSpace(result))
            {
                App.personRepo.UpdatePerson(persona.Id, result);
                statusMessage.Text = App.personRepo.Message;
                RefreshList();
            }
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        if (button.CommandParameter is Persona persona)
        {
            bool confirm = await DisplayAlert(
                "Confirmar",
                $"¿Eliminar a {persona.Name}?",
                "Sí",
                "No");

            if (confirm)
            {
                App.personRepo.DeletePerson(persona.Id);
                statusMessage.Text = App.personRepo.Message;
                RefreshList();
            }
        }
    }
}