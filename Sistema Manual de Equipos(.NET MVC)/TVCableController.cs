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
    public class TVCableController : Controller
    {

        TVCableDao tvD = new TVCableDao();
        GestionDB gestionD = new GestionDB();

        public ActionResult Index()
        {            
            return View(tvD.GetAll());
        }

        // GET: TVCable/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TVCable/Create
        public ActionResult Crear()
        {
            CargarCombo();
            return View();
        }

        // POST: TVCable/Create
        [HttpPost, ValidateInput(false)]
        public ActionResult Crear(TVCable tv)
        {
            try
            {
                var pic = string.Empty;
                var carpeta = "~/ArchivosBD/Imagenes/TVCable";
                var carpetaM = "~/ArchivosBD/Manuales";

                if (tv.ImagenFile != null)
                {
                    pic = gestionD.SubirImagen(tv.ImagenFile, carpeta);

                    if (pic != null)
                    {
                        pic = string.Format("{0}/{1}", carpeta, pic);
                        tv.Img1 = pic;
                    }

                }
                if (tv.ImagenFile2 != null)
                {
                    pic = gestionD.SubirImagen(tv.ImagenFile2, carpeta);

                    if (pic != null)
                    {
                        pic = string.Format("{0}/{1}", carpeta, pic);
                        tv.Img2 = pic;
                    }
                }

                if (tv.ImagenFile3 != null)
                {
                    pic = gestionD.SubirImagen(tv.ImagenFile3, carpeta);

                    if (pic != null)
                    {
                        pic = string.Format("{0}/{1}", carpeta, pic);
                        tv.Img3 = pic;
                    }

                }
                if (tv.ImagenFile4 != null)
                {
                    pic = gestionD.SubirImagen(tv.ImagenFile4, carpeta);

                    if (pic != null)
                    {
                        pic = string.Format("{0}/{1}", carpeta, pic);
                        tv.Img4 = pic;
                    }
                }

                if (tv.ManualFile != null)
                {
                    pic = gestionD.SubirManules(tv.ManualFile, carpetaM);

                    if (pic != null)
                    {
                        pic = string.Format("{0}/{1}", carpetaM, pic);
                        tv.Manual = pic;
                    }
                }


                if (tv.Img1 == null)
                {
                    ViewBag.ErrorImg = "Campo Img1 esta Basia o No es de formato .jpg o .png";
                    CargarCombo();
                    return View();
                }
                else
                {
                    tvD.Guardar(tv);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: TVCable/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargarCombo();
            TVCable tv = tvD.ObtenerTVCable(Convert.ToInt32(id));
            if (tv == null)
            {
                return HttpNotFound();
            }
            return View(tv);
        }

        // POST: TVCable/Edit/5
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(TVCable tv)
        {
            try
            {
                TVCable taux = new TVCable();                
                taux = tvD.ObtenerTVCable(Convert.ToInt32(tv.Id));                

                var pic = string.Empty;
                var carpeta = "~/ArchivosBD/Imagenes/TVCable";
                var carpetaM = "~/ArchivosBD/Manuales";

                if (tv.ImagenFile != null)
                {
                    pic = gestionD.SubirImagen(tv.ImagenFile, carpeta);
                    pic = string.Format("{0}/{1}", carpeta, pic);
                    tv.Img1 = pic;
                }
                else
                {
                    tv.Img1 = taux.Img1;
                }
                if (tv.ImagenFile2 != null)
                {
                    pic = gestionD.SubirImagen(tv.ImagenFile2, carpeta);
                    pic = string.Format("{0}/{1}", carpeta, pic);
                    tv.Img2 = pic;
                }
                else {
                    tv.Img2 = taux.Img2;
                }

                if (tv.ImagenFile3 != null)
                {
                    pic = gestionD.SubirImagen(tv.ImagenFile3, carpeta);
                    pic = string.Format("{0}/{1}", carpeta, pic);
                    tv.Img3 = pic;
                }
                else {
                    tv.Img3 = taux.Img3;
                }
                if (tv.ImagenFile4 != null)
                {
                    pic = gestionD.SubirImagen(tv.ImagenFile4, carpeta);
                    pic = string.Format("{0}/{1}", carpeta, pic);
                    tv.Img4 = pic;
                }
                else {
                    tv.Img4 = taux.Img4;
                }

                if (tv.ManualFile != null)
                {
                    pic = gestionD.SubirManules(tv.ManualFile, carpetaM);
                    pic = string.Format("{0}/{1}", carpetaM, pic);
                    tv.Manual = pic;
                }
                else {
                    tv.Manual = taux.Manual;
                }

                tvD.Actualizar(tv);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVCable tv = tvD.ObtenerTVCable(Convert.ToInt32(id));
            
            return View(tv);
        }

        // POST: TVCable/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                TVCable tv = tvD.ObtenerTVCable(id);

                RemoveFileFromServer(tv.Img1);
                RemoveFileFromServer(tv.Img2);
                RemoveFileFromServer(tv.Img3);
                RemoveFileFromServer(tv.Img4);
                RemoveFileFromServer(tv.Manual);
                tvD.Eliminar(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        private void CargarCombo()
        {
            List<string> ListTec = new List<string>();

            ListTec.Add("DIGITAL");
            ListTec.Add("SATELITAL");

            List<SelectListItem> Items = ListTec.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.ToString(),
                    Value = d.ToString(),
                    Selected = false

                };

            });

            ViewBag.ItemsTV = Items;
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
