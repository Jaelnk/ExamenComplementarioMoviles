using MauiApp2.Modelos;
using System.Collections.ObjectModel;

namespace MauiApp2.Views;

public partial class vPrincipal_estudiantes : ContentPage
{
    private DatabaseService dbService;
    private ObservableCollection<Estudiantes> estudiantes;
    private Estudiantes estudianteSeleccionado;
    public vPrincipal_estudiantes()
    {
        InitializeComponent();
        dbService = App.dbService;
        estudiantes = new ObservableCollection<Estudiantes>();
        cvEstudiantes.ItemsSource = estudiantes;
        CargarEstudiantes();
    }

    private void CargarEstudiantes()
    {
        var listaEstudiantes = dbService.GetAllEstudiantes();
        estudiantes.Clear();
        foreach (var estudiante in listaEstudiantes)
        {
            estudiantes.Add(estudiante);
        }
    }

    private async void OnAgregarClicked(object sender, EventArgs e)
    {
        if (!ValidarCampos())
            return;

        var nuevoEstudiante = new Estudiantes
        {
            Nombre = txtNombre.Text.Trim(),
            Apellido = txtApellido.Text.Trim(),
            Curso = txtCurso.Text.Trim(),
            Paralelo = txtParalelo.Text.Trim().ToUpper(),
            NOTA_FINAL = float.Parse(txtNotaFinal.Text.Trim())
        };

        int resultado = dbService.InsertEstudiante(nuevoEstudiante);

        if (resultado > 0)
        {
            await DisplayAlert("Éxito", dbService.Message, "OK");
            LimpiarCampos();
            CargarEstudiantes();
        }
        else
        {
            await DisplayAlert("Error", dbService.Message, "OK");
        }
    }

    private async void OnActualizarClicked(object sender, EventArgs e)
    {
        if (estudianteSeleccionado == null || !ValidarCampos())
            return;

        estudianteSeleccionado.Nombre = txtNombre.Text.Trim();
        estudianteSeleccionado.Apellido = txtApellido.Text.Trim();
        estudianteSeleccionado.Curso = txtCurso.Text.Trim();
        estudianteSeleccionado.Paralelo = txtParalelo.Text.Trim().ToUpper();
        estudianteSeleccionado.NOTA_FINAL = float.Parse(txtNotaFinal.Text.Trim());

        int resultado = dbService.UpdateEstudiante(estudianteSeleccionado);

        if (resultado > 0)
        {
            await DisplayAlert("Éxito", dbService.Message, "OK");
            OnCancelarClicked(sender, e);
            CargarEstudiantes();
        }
        else
        {
            await DisplayAlert("Error", dbService.Message, "OK");
        }
    }

    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int codEstudiante)
        {
            bool confirmar = await DisplayAlert("Confirmar",
                "¿Está seguro que desea eliminar este estudiante?",
                "Sí", "No");

            if (confirmar)
            {
                int resultado = dbService.DeleteEstudiante(codEstudiante);

                if (resultado > 0)
                {
                    await DisplayAlert("Éxito", dbService.Message, "OK");
                    CargarEstudiantes();
                }
                else
                {
                    await DisplayAlert("Error", dbService.Message, "OK");
                }
            }
        }
    }

    private void OnEstudianteSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Estudiantes estudiante)
        {
            estudianteSeleccionado = estudiante;

            // Llenar los campos con los datos del estudiante seleccionado
            txtNombre.Text = estudiante.Nombre;
            txtApellido.Text = estudiante.Apellido;
            txtCurso.Text = estudiante.Curso;
            txtParalelo.Text = estudiante.Paralelo;
            txtNotaFinal.Text = estudiante.NOTA_FINAL.ToString();

            // Cambiar botones para modo edición
            btnAgregar.IsVisible = false;
            btnActualizar.IsVisible = true;
            btnCancelar.IsVisible = true;
        }
    }

    private void OnCancelarClicked(object sender, EventArgs e)
    {
        LimpiarCampos();
        estudianteSeleccionado = null;
        cvEstudiantes.SelectedItem = null;

        // Restaurar botones para modo agregar
        btnAgregar.IsVisible = true;
        btnActualizar.IsVisible = false;
        btnCancelar.IsVisible = false;
    }

    private bool ValidarCampos()
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            DisplayAlert("Error", "El nombre es obligatorio", "OK");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtApellido.Text))
        {
            DisplayAlert("Error", "El apellido es obligatorio", "OK");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtCurso.Text))
        {
            DisplayAlert("Error", "El curso es obligatorio", "OK");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtParalelo.Text) || txtParalelo.Text.Length != 1)
        {
            DisplayAlert("Error", "El paralelo debe ser una sola letra", "OK");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtNotaFinal.Text) ||
            !float.TryParse(txtNotaFinal.Text, out float nota) ||
            nota < 0 || nota > 20)
        {
            DisplayAlert("Error", "La nota final debe ser un número entre 0 y 20", "OK");
            return false;
        }

        return true;
    }

    private void LimpiarCampos()
    {
        txtNombre.Text = string.Empty;
        txtApellido.Text = string.Empty;
        txtCurso.Text = string.Empty;
        txtParalelo.Text = string.Empty;
        txtNotaFinal.Text = string.Empty;
    }

    private async void OnCamaraClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CameraPage());
    }
}