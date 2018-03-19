using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab03.Models
{
    public class Partido : IComparable
    {
        // Codigo Primary Key
        public int codigoPK = 1;

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Numero de Partido es Requerido")]
        public int noPartido { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La Fecha del Partido es Requerida")]
        public DateTime FechaPartido { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre del Grupo es Requerido")]
        public string Grupo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre del Pais es Requerido")]
        public string Pais1 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre del Pais es Requerido")]
        public string Pais2 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre del Estadio es Requerido")]
        public string Estadio { get; set; }

        public Partido(int NoPartido, DateTime FechaPartido, string Grupo, string Pais1, string Pais2, string Estadio)
        {
            this.noPartido = NoPartido;
            this.FechaPartido = FechaPartido;
            this.Grupo = Grupo;
            this.Pais1 = Pais1;
            this.Pais2 = Pais2;
            this.Estadio = Estadio;
            this.codigoPK = 1;
        }


        public Partido()
        {
            this.Grupo = null;
            this.Pais1 = null;
            this.Pais2 = null;
            this.Estadio = null;
        }

        public int CompareByNoPartido(Partido partido)
        {
            return noPartido.CompareTo(partido.noPartido);
        }

        public int CompareByFecha(Partido Partido)
        {
            return FechaPartido.CompareTo(Partido.FechaPartido);
        }

        public int CompareTo(object obj)
        {
            try
            {
                Partido partido = obj as Partido;
                
                if (partido.codigoPK == 1)
                    return CompareByNoPartido(partido);
                else 
                    return CompareByFecha(partido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public delegate int Comparar(Partido Partido);

        public int CompareTo(Partido partido, Comparar criterio)
        {
            return criterio(partido);
        }



    }



}