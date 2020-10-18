using Cotas800.Models.Daos;
using Cotas800.Models.Dato;
using Cotas800.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cotas800.Controllers
{
    public class VistasController : Controller
    {

        ModemDao modD = new ModemDao();
        ModemGponDao modgD = new ModemGponDao();
        FallaDao fallaD = new FallaDao();
        TecnologiaDao tecD = new TecnologiaDao();
        GestionDao gesD = new GestionDao();
        TecnicosDao tecnD = new TecnicosDao();
        ManualesDao manualD = new ManualesDao();
        CentralesDao centD = new CentralesDao();
        IpsDnsDao ipDnsD = new IpsDnsDao();
        GestionDB gesDB = new GestionDB();
        TVCableDao tvD = new TVCableDao();



        // GET: ModemVista
        public ActionResult CamoAdsl()
        {
            CargarCombo2();            
            ViewBag.Modem = TempData["ModemP"];
            return View();
        }
                
        public ActionResult ModemAdslCamo(int id)
        {            
            TempData["ModemP"] = modD.ObtenerModem(id);
            return RedirectToAction("CamoAdsl");
        }
                                

        // GET: ModemVista/Delete/5
        public ActionResult Gpon()
        {
            CargarComboGpon();
            ViewBag.ModemG = TempData["ModemG"];
            return View();
        }

        public ActionResult GponCombo(int id)
        {
            TempData["ModemG"] = modgD.ObtenerModemGpon(id);
            return RedirectToAction("Gpon");
        }

        public ActionResult FallasAdslCamoGpon()
        {
            CargarComboFalla();
            ViewBag.Falla = TempData["Falla"];
            ViewBag.NombreTec = TempData["NombreTec"];
            return View();
        }

        public ActionResult FallaAdslCamoGponCombo(int id)
        {
            Falla f = new Falla();
            f = fallaD.ObtenerFalla(id);
            TempData["NombreTec"] = tecD.ObtenerTecnologia(Convert.ToInt32(f.IdTecnologia));
            TempData["Falla"] = f;
            return RedirectToAction("FallasAdslCamoGpon");
        }

        public ActionResult EnviarMail()
        {
            return View(gesD.GetAll());
        }

        public ActionResult Tecnicos()
        {   
            CargarComboTecnicos();
            ViewBag.Data = Session["Tecnicos"];
            ViewBag.Tecnicos = Session["tec"];
            ViewBag.NomTec = TempData["NomTec"];
            ViewBag.Telf = TempData["Telf"];
            ViewBag.Existe = TempData["Existe"];

            return View();
        }

        public ActionResult TecnicosCombo(int id)
        {
            Tecnicos tec = tecnD.ObtenerTecnicos(id);
            var fullPath = Request.MapPath(tec.ArchivoExel);
            string con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fullPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            DataTable dt = gesDB.ConvertXSLXtoDataTable(tec.NombreHoja, con);
            Session["Tecnicos"] = dt;
            Session["tec"] = tec;           
            return RedirectToAction("Tecnicos");
        }

        [HttpPost]
        public ActionResult CodTec(string Cod)
        {

            DataTable dt = (DataTable)Session["Tecnicos"];
        

            int i = 0;
            while (i < dt.Rows.Count && dt.Rows[i][0].ToString() != Cod)
            {
                i++;
            }

            if (i < dt.Rows.Count)
            {

                TempData["NomTec"] = dt.Rows[i][1].ToString();
                TempData["Telf"] = dt.Rows[i][2].ToString();
            }
            if (i >= dt.Rows.Count)
            {
                TempData["Existe"] = "NO SE ENCONTRO NINGUN TECNICO CON ESTE CODIGO O ESTA MAL ESCRITO EL CODIGO";
                
            }

            return RedirectToAction("Tecnicos");
        }

        public ActionResult Manuales()
        {
            return View(manualD.GetAll());
        }

        public ActionResult AbrirManual(int id)
        {
            Manuales manual = new Manuales();
            manual = manualD.ObtenerManuales(id);
            var archivo = Server.MapPath(manual.Archivo);
            //Process.Start(archivo);

            FileInfo file = new FileInfo(archivo);

            if (file.Exists)

            {

                Response.Clear();

                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

                Response.AddHeader("Content-Length", file.Length.ToString());

                Response.ContentType = "application/octet-stream";

                Response.TransmitFile(file.FullName);

                Response.End();

            }

            else

            {

                Response.Write("This file does not exist.");

            }


            return RedirectToAction("Manuales");
        }

        public ActionResult Centrales()
        {
            CargarComboCentrales();
            ViewBag.Cent = TempData["cent"];
            return View();
        }

        public ActionResult CentralesCombo(int id)
        {

            TempData["cent"] = centD.ObtenerCentrales(id);
            return RedirectToAction("Centrales");
        }

        public ActionResult ServerIPDns()
        {
            return View(ipDnsD.GetAll());
        }

        public ActionResult TVCable()
        {
            CargarComboTVDigital();
            CargarComboTVDth();
            ViewBag.TVDig = Session["TVDig"];
            return View(tvD.GetAll());
        }

        public ActionResult TVCableCombo(int id)
        {
            Session["TVDig"] = tvD.ObtenerTVCable(id);
            return RedirectToAction("TVCable");
        }

        public ActionResult AbrirManualCajaTv(int id)
        {
            TVCable tv = new TVCable();
            tv = tvD.ObtenerTVCable(id);
            var archivo = Request.MapPath(tv.Manual);
            Process.Start(archivo);
            return RedirectToAction("TVCable");
        }


        private void CargarComboGpon()
        {
            List<ModemGpon> ListModem = new List<ModemGpon>();
            ListModem = modgD.GetAll().ToList();           

            ViewBag.Items = ListModem;
        }


        private void CargarCombo2()
        {            
            List<Modem> ListModemAdsl = new List<Modem>();
            ListModemAdsl = modD.GetAllCamoAdsl("ADSL").ToList();

            List<Modem> ListModemCamo = new List<Modem>();
            ListModemCamo = modD.GetAllCamoAdsl("Cable Modem").ToList();
            
            ViewBag.ItemsAdsl = ListModemAdsl;
            ViewBag.ItemsCamo = ListModemCamo;

        }

        private void CargarComboFalla()
        {
            List<Falla> ListFallaAdsl = new List<Falla>();
            List<Falla> ListFallaCamo = new List<Falla>();
            List<Falla> ListFallaGpon = new List<Falla>();

            ListFallaAdsl = fallaD.GetAllFallaAdslCamoGpon("ADSL").ToList();
            ListFallaCamo = fallaD.GetAllFallaAdslCamoGpon("Cable Modem").ToList();
            ListFallaGpon = fallaD.GetAllFallaAdslCamoGpon("GPON").ToList();

            ViewBag.ItemsAdsl = ListFallaAdsl;
            ViewBag.ItemsCamo = ListFallaCamo;
            ViewBag.ItemsGpon = ListFallaGpon;
        }

        private void CargarComboTecnicos()
        {
            List<Tecnicos> ListTecnicos = new List<Tecnicos>();

            ListTecnicos = tecnD.GetAll().ToList();

            ViewBag.ItemsTec = ListTecnicos;
        }

        private void CargarComboCentrales()
        {
            List<CentralEntidad> ListCentrales = new List<CentralEntidad>();

            ListCentrales = centD.GetAll().ToList();

            ViewBag.ItemsCent = ListCentrales;
        }

        private void CargarComboTVDigital()
        {
            List<TVCable> ListTv = new List<TVCable>();

            ListTv = tvD.GetAllDig().ToList();

            ViewBag.ItemsTVDigital = ListTv;
        }

        private void CargarComboTVDth()
        {
            List<TVCable> ListTv = new List<TVCable>();

            ListTv = tvD.GetAllDTH().ToList();

            ViewBag.ItemsTVDth = ListTv;
        }

    }
}
