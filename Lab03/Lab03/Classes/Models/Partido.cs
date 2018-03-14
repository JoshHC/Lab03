using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab03.Models
{
    public class Partido : IComparable
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Numero de Partido es Requerido")]
        public int noPartido { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La Fecha del Partido es Requerida")]
        public string FechaPartido { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre del Grupo es Requerido")]
        public string Grupo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre del Pais es Requerido")]
        public string Pais1 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre del Pais es Requerido")]
        public string Pais2 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre del Estadio es Requerido")]
        public string Estadio { get; set; }

        public Partido(int NoPartido, string FechaPartido, string Grupo, string Pais1, string Pais2, string Estadio)
        {
            this.noPartido = NoPartido;
            this.FechaPartido = FechaPartido;
            this.Grupo = Grupo;
            this.Pais1 = Pais1;
            this.Pais2 = Pais2;
            this.Estadio = Estadio;
        }

        public Comparison<Partido> CompareByNoPartido = delegate (Partido i, Partido j)
        {
            return i.noPartido.CompareTo(j.noPartido);
        };

        public Comparison<Partido> CompareByFechaPartido = delegate (Partido i, Partido j)
        {
            return i.FechaPartido.CompareTo(j.FechaPartido);
        };

        public Comparison<Partido> CompareByPais1 = delegate (Partido i, Partido j)
        {
            return i.Pais1.CompareTo(j.Pais1);
        };

        public Comparison<Partido> CompareByPais2 = delegate (Partido i, Partido j)
        {
            return i.Pais2.CompareTo(j.Pais2);
        };

        public Comparison<Partido> CompareByEstadio = delegate (Partido i, Partido j)
        {
            return i.Estadio.CompareTo(j.Estadio);
        };

        public Comparison<Partido> CompareByGroup = delegate (Partido i, Partido j)
        {
            return i.Grupo.CompareTo(j.Grupo);
        };

        public override string ToString()
        {
            return $"{noPartido}|{FechaPartido}|{Grupo}|{Pais1}|{Pais2}|{Estadio}";
        }

        public bool Equals(Partido partido)
        {
            bool igual = partido.noPartido == noPartido;
            igual = igual && partido.FechaPartido == FechaPartido;
            igual = igual && partido.Grupo == Grupo;
            igual = igual && partido.Pais1 == Pais1;
            igual = igual && partido.Pais2 == Pais2;
            igual = igual && partido.Estadio == Estadio;
            return igual;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}