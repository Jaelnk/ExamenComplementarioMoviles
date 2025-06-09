using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Modelos
{
    [SQLite.Table("ESTUDIANTES")]
    public class Estudiantes
    {
        [PrimaryKey, AutoIncrement]
        public int COD_ESTUDIANTE { get; set; }

        [MaxLength(25)]
        public string Nombre { get; set; }

        [MaxLength(25)]
        public string Apellido { get; set; }

        [MaxLength(25)]
        public string Curso { get; set; }

        [MaxLength(1)]
        public string Paralelo { get; set; }

        public float NOTA_FINAL { get; set; }
    }
}
