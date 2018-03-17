using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab03.Models;
using Libreria_de_Clases;

namespace Lab03.Classes.Models
{
    public class DataBase
    {
        private static DataBase instance;
        public static DataBase Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataBase();
                return instance;
            }
        }

        public ArbolAVL<Partido> ArbolPartido;
        public List<string> ArchivoTexto;

        public DataBase()
        {
            ArbolPartido = new ArbolAVL<Partido>();
            ArchivoTexto = new List<string>();
        }
    }
}