using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ListPerson_1._3_Grupo__4.Controllers;
using ListPerson_1._3_Grupo__4.Models;

namespace ListPerson_1._3_Grupo__4.ViewModels
{
    public class ListadoPersonasViewModel : BindableObject
    {
        private ObservableCollection<Personas> _personasList;

        public ObservableCollection<Personas> PersonasList
        {
            get { return _personasList; }
            set
            {
                _personasList = value;
                OnPropertyChanged(nameof(PersonasList));
            }
        }

        private PersonasController _personasController;

        public ListadoPersonasViewModel()
        {
            _personasList = new ObservableCollection<Personas>();
            _personasController = new PersonasController();
        }

        public async Task CargarPersonas()
        {
            await _personasController.Init();
            var personas = await _personasController.ObtenerTodasLasPersonas();
            PersonasList = new ObservableCollection<Personas>(personas);
        }

        public async Task<int> EliminarPersona(Personas persona)
        {
            int deletedRows = await _personasController.DeletePerson(persona);
            if (deletedRows > 0)
            {   
                await CargarPersonas();
            }
            return deletedRows; 
        }

    }
}
