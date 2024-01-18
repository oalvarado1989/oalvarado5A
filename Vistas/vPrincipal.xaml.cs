using oalvarado5A.Models;

namespace oalvarado5A.Vistas;

public partial class vPrincipal : ContentPage
{
	public vPrincipal()
	{
		InitializeComponent();
	}

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        App.personRepo.AddNewPerson(txtName.Text);
        statusMessage.Text = App.personRepo.StatusMessage;
    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        List<Persona> people = App.personRepo.GetAllPeople();
        ListaPersona.ItemsSource = people;
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        string nombre = txtName.Text;

        if (!string.IsNullOrEmpty(nombre))
        {
            App.personRepo.DeletePerson(nombre);
            RefreshPersonList();
        }
        else
        {
            statusMessage.Text = "Ingrese un nombre para eliminar";
        }
        statusMessage.Text = App.personRepo.StatusMessage;
    }

    private void RefreshPersonList()
    {
        List<Persona> people = App.personRepo.GetAllPeople();
        ListaPersona.ItemsSource = people;
    }
   
    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";

        string nuevoNombre = txtNuevoNombre.Text;
        int personaId;

        if (!string.IsNullOrEmpty(nuevoNombre) && int.TryParse(txtPersonaId.Text, out personaId))
        {
            Persona personaActualizada = new Persona { Id = personaId, Name = nuevoNombre };
            App.personRepo.UpdatePerson(personaActualizada);
            RefreshPersonList();
        }
        else
        {
            statusMessage.Text = "Ingrese un nombre válido y un Id para actualizar";
        }
    }
}
