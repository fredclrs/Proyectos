using Cotas800.Models.Daos;
using Cotas800.Models.Dato;
using Cotas800.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cotas800.Controllers
{
    public class ModemController : Controller
    {

        ModemDao modemd ;
        TecnologiaDao tecd;
        GestionDB gestiond;

        public ModemController()
        {                     
            modemd = new ModemDao();
            tecd = new TecnologiaDao();
            gestiond = new GestionDB();
        }

        // GET: Modem
        public ActionResult Index()        
        {
            //string conect = Request.MapPath("~/DBCotas800.db");
            string contdb = "";
            if (System.IO.File.Exists(gestiond.controldb))
            {
                contdb = "La base de Datos exite";
            }
            else
            {
                contdb = "La base de Datos no exite en esa Ruta";
            }
            IEnumerable<Modem> listMo = modemd.GetAll();         
            return View(modemd.GetAll());
        }

        // GET: Modem/Create
        public ActionResult Crear()
        {
            CargarCombo();            
            return View();
        }

        // POST: Modem/Create
        [HttpPost, ValidateInput(false)]
        public ActionResult Crear(Modem modem)
        {
            try
            {
                var pic = string.Empty;
                var carpeta = "~/ArchivosBD/Imagenes/Internet";

                if (modem.ImagenFile != null)
                {
                    pic = gestiond.SubirImagen(modem.ImagenFile, carpeta);
 
                    if (pic != null) {
                        pic = string.Format("{0}/{1}", carpeta, pic);
                        modem.Img1 = pic;
                    }
                    
                }
                if (modem.ImagenFile2 != null)
                {
                    pic = gestiond.SubirImagen(modem.ImagenFile2, carpeta);

                    if (pic != null)
                    {
                        pic = string.Format("{0}/{1}", carpeta, pic);
                        modem.Img2 = pic;
                    }                    
                }

                if (modem.Img1 == null || modem.Img2 == null)
                {
                    ViewBag.ErrorImg = "Campo Imagen esta Basia o No es de formato .jpg o .png";
                    CargarCombo();
                    return View();
                }
                else {
                    modemd.Guardar(modem);
                    return RedirectToAction("Index");
                }                                                                           
            }
            catch
            {
                return View();
            }
        }

        // GET: Modem/Edit/5
        public ActionResult Edit(int? id)
        {
            List<Tecnologia> ListTec = new List<Tecnologia>();
            ListTec = tecd.GetAll().ToList();

            List<SelectListItem> Items = ListTec.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.Id.ToString(),
                    Selected = false

                };

            });

            ViewBag.Items = Items;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Modem mode = modemd.ObtenerModem(Convert.ToInt32(id));
            if (mode == null)
            {
                return HttpNotFound();
            }
            return View(mode);
        }
        
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Modem modem)
        {
            try
            {
                Modem mod = new Modem();

                mod = modemd.ObtenerModem(Convert.ToInt32(modem.Id));

                var pic = string.Empty;
                var carpeta = "~/ArchivosBD/Imagenes/Internet";

                if (modem.ImagenFile != null)
                {                    
                    pic = gestiond.SubirImagen(modem.ImagenFile, carpeta);
                    pic = string.Format("{0}/{1}", carpeta, pic);
                    modem.Img1 = pic;
                }
                else {
                    modem.Img1 = mod.Img1;  
                }
                if (modem.ImagenFile2 != null)
                {
                    pic = gestiond.SubirImagen(modem.ImagenFile2, carpeta);
                    pic = string.Format("{0}/{1}", carpeta, pic);
                    modem.Img2 = pic;
                }
                else {
                    modem.Img2 = mod.Img2;
                }
       
                modemd.Actualizar(modem);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Modem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modem mod = modemd.ObtenerModem(Convert.ToInt32(id));
            Tecnologia tec = tecd.ObtenerTecnologia(mod.Tecnologia);
            ViewBag.tecnologia = tec.Nombre;
            return View(mod);
        }

        // POST: Modem/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Modem mod = modemd.ObtenerModem(id);
                
                RemoveFileFromServer(mod.Img1);
                RemoveFileFromServer(mod.Img2);
                modemd.Eliminar(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        private void CargarCombo()
        {
            List<Tecnologia> ListTec = new List<Tecnologia>();
            ListTec = tecd.GetAll().ToList();

            List<SelectListItem> Items = ListTec.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.Id.ToString(),
                    Selected = false

                };

            });

            ViewBag.Items = Items;
        }

        private bool RemoveFileFromServer(string path)
        {
            var fullPath = Request.MapPath(path);            
            if (!System.IO.File.Exists(fullPath)) return false;

            try //Maybe error could happen like Access denied or Presses Already User used
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
            }
            return false;
        }

    }
}
