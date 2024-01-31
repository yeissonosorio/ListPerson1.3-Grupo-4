using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using ListPerson_1._3_Grupo__4.Models;

namespace ListPerson_1._3_Grupo__4.Controllers
{
    public class PersonasController
    {
        SQLiteAsyncConnection _connection;

        // Constructor vacío
        public PersonasController() { }

        // Inicialización de la conexión a la base de datos
        public async Task Init()
        {
            if (_connection is not null)
            {
                return;
            }

            SQLiteOpenFlags extensiones = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DBPersonas.db3"), extensiones);
            await _connection.CreateTableAsync<Personas>();
        }

        // Método para obtener todas las personas
        public async Task<List<Personas>> ObtenerTodasLasPersonas()
        {
            await Init();
            return await _connection.Table<Personas>().ToListAsync();
        }

        // Método para almacenar una persona
        public async Task<int> StorePerson(Personas persona)
        {
            await Init();

            if (persona.Id == 0)
            {
                return await _connection.InsertAsync(persona);
            }
            else
            {
                return await _connection.UpdateAsync(persona);
            }
        }
        public async Task<int> DeletePerson(Personas personas)
        {
            await Init();
            return await _connection.DeleteAsync(personas);
        }
    }
}
