using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Modelos
{
    public class PersonRepositorty
    {
        String dbPath;
        private SQLiteConnection conn;
        public String Message { get; set; }

        private void Init()
        {
            if (conn is not null)
            {
                return;
            }
            conn = new(dbPath);
            conn.CreateTable<Persona>();


        }

        public PersonRepositorty(String path)
        {
            dbPath = path;
        }

        public void AddNewPerson(string nombre)
        {
            int resultado = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(nombre))
                {
                    throw new Exception("Nombre es requerido");

                }
                Persona persona = new() { Name = nombre }; //constructor
                resultado = conn.Insert(persona);

                Message = string.Format("Dato Ingresado ", resultado, nombre);


            }
            catch (Exception ex)
            {
                Message = string.Format("Error: " + ex);
            }
        }

        public List<Persona> GetAllPerson()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();

            }
            catch (Exception ex)
            {
                Message = string.Format("Error: " + ex);
            }
            return new List<Persona>();
        }

        public void UpdatePerson(int id, string newName)
        {
            try
            {
                Init();

                if (string.IsNullOrWhiteSpace(newName))
                    throw new Exception("El nuevo nombre es requerido");

                var existing = conn.Find<Persona>(id);
                if (existing == null)
                    throw new Exception("Persona no encontrada");

                existing.Name = newName;
                int resultado = conn.Update(existing);

                Message = $"Registro actualizado correctamente (Filas afectadas: {resultado})";
            }
            catch (Exception ex)
            {
                Message = $"Error al actualizar: {ex.Message}";
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                Init();

                var existing = conn.Find<Persona>(id);
                if (existing == null)
                    throw new Exception("Persona no encontrada");

                int resultado = conn.Delete<Persona>(id);

                Message = $"Registro eliminado correctamente (Filas afectadas: {resultado})";
            }
            catch (Exception ex)
            {
                Message = $"Error al eliminar: {ex.Message}";
            }
        }

    }
}
