

namespace ListPerson_1._3_Grupo__4
{
    public partial class ListadoPersonasPage : ContentPage
    {
        ViewModels.ListadoPersonasViewModel _viewModel;

        public ListadoPersonasPage()
        {
            InitializeComponent();
            _viewModel = new ViewModels.ListadoPersonasViewModel();
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.CargarPersonas();
        }

        private async void RegistrarNuevaPersona_Clicked(object sender, EventArgs e)
        {
            // Navegar a la p�gina de registro (MainPage)
            await Navigation.PushAsync(new MainPage());
        }

        private async void Editar_Clicked(object sender, EventArgs e)
        {
            // Obtener la persona asociada al bot�n presionado
            var persona = (Models.Personas)((Button)sender).CommandParameter;

            // Implementar la l�gica para editar la persona (puedes navegar a una p�gina de edici�n)
            // Ejemplo: await Navigation.PushAsync(new EditarPersonaPage(persona));
        }

        private async void Eliminar_Clicked(object sender, EventArgs e)
        {
            var persona = (Models.Personas)((Button)sender).CommandParameter;

            // Mostrar un mensaje de confirmaci�n
            bool result = await DisplayAlert("Confirmaci�n", "�Seguro que quieres eliminar esta persona?", "S�", "No");

            if (result)
            {
                // Llamar al m�todo para eliminar la persona de la base de datos
                int deletedRows = await _viewModel.EliminarPersona(persona);

                if (deletedRows > 0)
                {
                    // Actualizar la lista de personas despu�s de la eliminaci�n
                    await _viewModel.CargarPersonas();
                }
            }
        }
    }
}
