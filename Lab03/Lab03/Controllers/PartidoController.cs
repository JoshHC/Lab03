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
        // GET: Partido
        public ActionResult Index()
        {
            return View();
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
            //   Partido nuevopartido = new Partido(Convert.ToInt32(collection["NoPartido"]), Convert.ToDateTime(collection["FechaPartido"]), collection["Grupo"], collection["Pais1"], collection["Pais2"], collection["Estadio"]);
            //   AVL<Partido> nNodo = new AVL<Partido>(nuevopartido, null, null, null);

             //   DataBase.Instance.ArbolPartido.Insertar(nuevopartido,nNodo);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Partido/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Partido/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

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
        public ActionResult UploadFile(HttpPostedFileBase file)
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
            //El Archivo se lee en una lista para luego ingresarlo

            //Se crea un Jugador Momentaneo para pasar los datos

            string lineap = Lector.ReadToEnd();
            string Dato = Lector.ReadLine();
            Dato = Lector.ReadLine();
            Dato = Lector.ReadLine();
            string Linea = "{"+Dato;

            var Objeto = JsonConvert.DeserializeObject(lineap);
            while (Dato != null)
            {
                Dato = Lector.ReadLine();
                for (int i = 0; i < 6; i++)
                {
                    if(i != 5)
                    {
                        Linea = Linea + Dato;
                        Dato = Lector.ReadLine();

                    }
                }
                Partido nodo = JsonConvert.DeserializeObject<Partido>(Linea);
                DataBase.Instance.ArbolPartido.Insertar(nodo, null);
                Dato = Lector.ReadLine();
                Linea = "";
            }

            try
            {
                
               // AVL<Partido> Nodo = JsonConvert.DeserializeObject<AVL<Partido>>(Linea);
             

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["msg"] = "<script> alert('Error Los Datos del Archivo Json no se pudieron Insertar');</script>";
                return RedirectToAction("Index");
            }  

        }
    }
}

