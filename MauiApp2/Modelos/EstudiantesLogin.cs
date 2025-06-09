using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Modelos
{
    [SQLite.Table("ESTUDIANTES_LOGIN")]
    internal class EstudiantesLogin
    {
        [PrimaryKey, AutoIncrement]
        [Column("ID")]
        public int ID { get; set; }

        [MaxLength(25)]
        [Column("USUARIO")]
        public string USUARIO { get; set; }

        [MaxLength(15)]
        [Column("CONTRASEÑA")]
        public string CONTRASEÑA { get; set; }
    }
}
