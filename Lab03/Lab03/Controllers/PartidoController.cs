using Lab03.Classes.Models;
using Lab03.Models;
using Libreria_de_Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab03.Controllers
{
    public class PartidoController : Controller
    {

        //Se crea un Jugador Momentaneo para pasar los datos (Carga del archivo)
        List<Partido> ListadePartidos = new List<Partido>();

        public static void imprimirArchivo()
        {
           /* StreamWriter escritor = new StreamWriter(@"C:\Users\josue\Desktop");
            //Cambiar luego C:\Users\Admin\Desktop\Bitácora.txt 

            foreach (var linea in DataBase.Instance.ArchivoTexto)
            {
                escritor.WriteLine(linea);
            }
            escritor.Close();*/
        }

        // GET: Partido
        public ActionResult Index()
        {
            try
            {
                return View(DataBase.Instance.ArbolPartido.Orders("InOrder"));
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ArbolBinario/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Partido nuevopartido = new Partido(Convert.ToInt32(collection["NoPartido"]), Convert.ToDateTime(collection["FechaPartido"]), collection["Grupo"], 
                    collection["Pais1"], collection["Pais2"], collection["Estadio"]);

                DataBase.Instance.ArchivoTexto.Add("INSERCION");
                DataBase.Instance.ArchivoTexto.Add("\tPartido Numero: " + Convert.ToInt32(collection["NoPartido"]));
                DataBase.Instance.ArchivoTexto.Add("\tFecha del Partido: " + Convert.ToDateTime(collection["FechaPartido"]));
                DataBase.Instance.ArchivoTexto.Add("\tGrupo: " + collection["Grupo"]);
                DataBase.Instance.ArchivoTexto.Add("\tPais 1: " + collection["Pais1"]);
                DataBase.Instance.ArchivoTexto.Add("\tPais 2: " + collection["Pais2"]);
                DataBase.Instance.ArchivoTexto.Add("\tEstadio: " + collection["Estadio"]);
                DataBase.Instance.ArchivoTexto.Add("");

                imprimirArchivo();

                DataBase.Instance.ArbolPartido.Insertar(nuevopartido);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Editar/5
        public ActionResult Edit(int? id)
        {
            return View();
        }

        // POST: Partido/Editar/5
        [HttpPost]
        public ActionResult Edit(int? id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Delete/5
        public ActionResult Delete(int? id)
        {
            return View();
        }

        // POST: Partido/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, int NoPartido, DateTime FechaPartido, string Grupo , string Pais1, string Pais2, string Estadio)
        {
            try
            {
                Partido nuevopartido = new Partido(NoPartido,FechaPartido,Grupo,Pais1,Pais2,Estadio);

                DataBase.Instance.ArbolPartido.Eliminar(nuevopartido, ref DataBase.Instance.ArbolPartido.Raiz);

                DataBase.Instance.ArchivoTexto.Add("ELIMINACION");
                DataBase.Instance.ArchivoTexto.Add("\tPartido Numero: " + NoPartido);
                DataBase.Instance.ArchivoTexto.Add("\tFecha del Partido: " + FechaPartido);
                DataBase.Instance.ArchivoTexto.Add("\tGrupo: " + Grupo);
                DataBase.Instance.ArchivoTexto.Add("\tPais 1: " + Pais1);
                DataBase.Instance.ArchivoTexto.Add("\tPais 2: " + Pais2);
                DataBase.Instance.ArchivoTexto.Add("\tEstadio: " + Estadio);
                DataBase.Instance.ArchivoTexto.Add("");

                imprimirArchivo();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Aca se hace el Ingreso por medio de Archivo de Texto, ya que el Boton de Result esta Linkeado.
        public ActionResult UploadFile(HttpPostedFileBase file, int? Tipo)
        {
            if (Path.GetExtension(file.FileName) != ".json")
            {
                //Aca se debe de Agregar una Vista de Error, o de Datos No Cargados
                TempData["msg"] = "<script> alert('Error El Archivo Cargado No es de Tipo Json');</script>";
                return RedirectToAction("Error", "Shared");
            }

            Stream Direccion = file.InputStream;
            //Se lee el Archivo que se subio, por medio del Lector

            StreamReader Lector = new StreamReader(Direccion);
            //El Archivo se lee en una linea para luego ingresarlo

            string Dato = "";
            Dato = Lector.ReadLine();

            while (Dato != null)
            {
                Dato = Lector.ReadLine();
                Dato = Lector.ReadLine();

                string Linea = "{";

                for (int i = 0; i < 6; i++)
                {
                    Linea = Linea + Dato;
                    Dato = Lector.ReadLine();
                }

                Linea = Linea + "}";

                Partido objTemporal = JsonConvert.DeserializeObject<Partido>(Linea);
                ListadePartidos.Add(objTemporal);

                Linea = "";
            }

            try
            {
                if (Tipo == 1)
                {
                    foreach (var item in ListadePartidos)
                    {
                        if (item.Estadio != null)
                        {
                            item.codigoPK = 1;

                            DataBase.Instance.ArchivoTexto.Add("INSERCION POR NUMERO DE PARTIDO");
                            DataBase.Instance.ArchivoTexto.Add("\tPartido Numero: " + item.noPartido);
                            DataBase.Instance.ArchivoTexto.Add("\tFecha del Partido: " + item.FechaPartido);
                            DataBase.Instance.ArchivoTexto.Add("\tGrupo: " + item.Grupo);
                            DataBase.Instance.ArchivoTexto.Add("\tPais 1: " + item.Pais1);
                            DataBase.Instance.ArchivoTexto.Add("\tPais 2: " + item.Pais2);
                            DataBase.Instance.ArchivoTexto.Add("\tEstadio: " + item.Estadio);
                            DataBase.Instance.ArchivoTexto.Add("");

                            imprimirArchivo();

                            DataBase.Instance.ArbolPartido.Insertar(item);
                        }
                    }
                }
                else if (Tipo == 2)
                {
                    foreach (var item in ListadePartidos)
                    {
                        if (item.Estadio != null)
                        {
                            item.codigoPK = 2;

                            DataBase.Instance.ArchivoTexto.Add("INSERCION POR FECHA DEL PARTIDO");
                            DataBase.Instance.ArchivoTexto.Add("\tPartido Numero: " + item.noPartido);
                            DataBase.Instance.ArchivoTexto.Add("\tFecha del Partido: " + item.FechaPartido);
                            DataBase.Instance.ArchivoTexto.Add("\tGrupo: " + item.Grupo);
                            DataBase.Instance.ArchivoTexto.Add("\tPais 1: " + item.Pais1);
                            DataBase.Instance.ArchivoTexto.Add("\tPais 2: " + item.Pais2);
                            DataBase.Instance.ArchivoTexto.Add("\tEstadio: " + item.Estadio);
                            DataBase.Instance.ArchivoTexto.Add("");

                            imprimirArchivo();

                            DataBase.Instance.ArbolPartido.Insertar(item);
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["msg"] = "<script> alert('Error Los Datos del Archivo Json no se pudieron InsertarHijos');</script>";
                return RedirectToAction("Index");
            }  

        }

        public ActionResult Busqueda(string Tipo, string Search)
        {
            if (Tipo == "No Partido")
            {

                List<Partido> ListaDeBuscados = new List<Partido>();
                List<Partido> NoPartidoBuscado = new List<Partido>();
                NoPartidoBuscado.Clear();
                ListaDeBuscados.Clear();
                ListaDeBuscados = DataBase.Instance.ArbolPartido.ObtenerArbol();
                foreach (var item in ListaDeBuscados)
                {
                    if (item.noPartido == Convert.ToInt32(Search))
                    {
                        NoPartidoBuscado.Add(item);
                    }

                }
                return View("Index", NoPartidoBuscado);


            }
            else if (Tipo == "Fecha de Partido")
            {
                List<Partido> ListaDeBuscados = new List<Partido>();
                List<Partido> FechasBuscadas = new List<Partido>();
                FechasBuscadas.Clear();
                ListaDeBuscados.Clear();
                ListaDeBuscados = DataBase.Instance.ArbolPartido.ObtenerArbol();
                foreach (var item in ListaDeBuscados)
                {
                    if (item.FechaPartido == Convert.ToDateTime(Search))
                    {
                        FechasBuscadas.Add(item);
                    }

                }
                return View("Index", FechasBuscadas);

            }

            return View();
        }
    }
}

