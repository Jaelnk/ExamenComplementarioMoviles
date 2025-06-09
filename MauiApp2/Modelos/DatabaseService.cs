using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Modelos
{
    public class DatabaseService
    {
        String dbPath;
        private SQLiteConnection conn;
        public String Message { get; set; }
        public int Resultado { get; set; }

        public DatabaseService(String path)
        {
            dbPath = path;
        }

        private void Init()
        {
            if (conn is not null)
            {
                return;
            }
            conn = new(dbPath);
            conn.CreateTable<Estudiantes>();
            conn.CreateTable<EstudiantesLogin>();

            if (conn.Table<EstudiantesLogin>().Count() == 0)
            {
                EstudiantesLogin credentials = new() { USUARIO = "admin", CONTRASEÑA = "123" };
                Resultado = conn.Insert(credentials);
            }
        }

        public bool ValidateLogin(string usuario, string contraseña)
        {
            try
            {
                Init();
                var user = conn.Table<EstudiantesLogin>()
                    .Where(u => u.USUARIO == usuario && u.CONTRASEÑA == contraseña)
                    .FirstOrDefault();
                return user != null;
            }
            catch (Exception ex)
            {
                Message = "Error: " + Resultado + ex.Message;
                return false;
            }
        }

        // CRUD para Estudiantes
        public List<Estudiantes> GetAllEstudiantes()
        {
            try
            {
                Init();
                return conn.Table<Estudiantes>().ToList();
            }
            catch (Exception ex)
            {
                Message = "Error al obtener estudiantes: " + ex.Message;
                return new List<Estudiantes>();
            }
        }

        public int InsertEstudiante(Estudiantes estudiante)
        {
            try
            {
                Init();
                Resultado = conn.Insert(estudiante);
                Message = "Estudiante agregado correctamente";
                return Resultado;
            }
            catch (Exception ex)
            {
                Message = "Error al agregar estudiante: " + ex.Message;
                return 0;
            }
        }

        public int UpdateEstudiante(Estudiantes estudiante)
        {
            try
            {
                Init();
                Resultado = conn.Update(estudiante);
                Message = "Estudiante actualizado correctamente";
                return Resultado;
            }
            catch (Exception ex)
            {
                Message = "Error al actualizar estudiante: " + ex.Message;
                return 0;
            }
        }

        public int DeleteEstudiante(int codEstudiante)
        {
            try
            {
                Init();
                Resultado = conn.Delete<Estudiantes>(codEstudiante);
                Message = "Estudiante eliminado correctamente";
                return Resultado;
            }
            catch (Exception ex)
            {
                Message = "Error al eliminar estudiante: " + ex.Message;
                return 0;
            }
        }

        public Estudiantes GetEstudianteById(int codEstudiante)
        {
            try
            {
                Init();
                return conn.Table<Estudiantes>()
                    .Where(e => e.COD_ESTUDIANTE == codEstudiante)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                Message = "Error al obtener estudiante: " + ex.Message;
                return null;
            }
        }
    }
}