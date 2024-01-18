using oalvarado5A.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace oalvarado5A
{
    public class PersonReporitory
    {
        string dbPath;
        private SQLiteConnection conn;
        public string StatusMessage {  get; set; }

        public void Init()
        {
            if (conn is not null)
                return;
            conn = new(dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonReporitory(string dbPAth1)
        {
            dbPath = dbPAth1;
        }

        public void AddNewPerson(string nombre)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Nombre requerido");

                Persona persona = new Persona() { Name = nombre };
                result = conn.Insert(persona);
                StatusMessage = string.Format("Filas agregadas", result, nombre);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al insertar", nombre, ex.Message);
            }
        }

        public List<Persona> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error al mostrar los datos",  ex.Message);
            }
            return new List<Persona>();
        }

        public void DeletePerson(string nombre)
        {
            try
            {
                Init();

                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Nombre requerido");
                Persona persona = conn.Table<Persona>().FirstOrDefault(p => p.Name == nombre);

                if (persona != null)
                {                 
                    conn.Delete(persona);
                    StatusMessage = string.Format("Persona eliminada", nombre);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al eliminar la persona", nombre, ex.Message);
            }
        }

        public void UpdatePerson(Persona updatedPerson)
        {
            try
            {
                Init();

                if (updatedPerson == null)
                    throw new ArgumentNullException(nameof(updatedPerson));

                Persona existingPerson = conn.Table<Persona>().FirstOrDefault(p => p.Id == updatedPerson.Id);

                if (existingPerson != null)
                {
                    existingPerson.Name = updatedPerson.Name;
                    conn.Update(existingPerson);
                    StatusMessage = string.Format("Persona actualizada", updatedPerson.Name);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al actualizar la persona", ex.Message);
            }
        }

    }

}
