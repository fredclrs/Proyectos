using Cotas800.Models.Dato;
using Cotas800.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Cotas800.Models.Daos
{
    public class ModemDao
    {

        GestionDB gestiondb = new GestionDB();
        public string controlO;
        public string controlCons;

        public string Guardar(Modem mod)
        {
            string consulta = string.Format("insert into Modem(Nombre,Marca,Ip,FuncionesLed,Img1,Img2,IdTecnologia) values('{0}','{1}','{2}','{3}','{4}','{5}',{6})", mod.Nombre, mod.Marca, mod.Ip, mod.FuncionesLed ,mod.Img1, mod.Img2, mod.Tecnologia);
            bool result = gestiondb.EjecutarConculta(consulta);

            if (result)
            {
                return "Se guardo correctamente";
            }
            else
            {
                return "Error al guardar";
            }

        }

        public string Actualizar(Modem mod)
        {
            string consulta = string.Format("update Modem set Nombre ='{0}', Marca = '{1}', Ip = '{2}', FuncionesLed = '{3}' ,Img1 = '{4}', Img2 = '{5}', IdTecnologia = {6} where Id = {7}", mod.Nombre, mod.Marca, mod.Ip, mod.FuncionesLed ,mod.Img1, mod.Img2, mod.Tecnologia, Convert.ToInt32(mod.Id));
            bool result = gestiondb.EjecutarConculta(consulta);

            if (result)
            {
                return "Se Actualizo correctamente";
            }
            else
            {
                return "Error al Actualizar";
            }
        }

        public string Eliminar(int id)
        {
            string consulta = "delete from Modem where Id =" + id;
            bool result = gestiondb.EjecutarConculta(consulta);

            if (result)
            {
                return "Se Elimino correctamente";
            }
            else
            {
                return "Error al Eliminar";
            }

        }

        public Modem ObtenerModem(int id)
        {
            string consulta = "select *from Modem where Id = " + id;
            gestiondb.BuscarResultados(consulta);

            Modem modem = new Modem();

            DataRow dt = gestiondb.ObtenerResultados().Rows[0];

            modem.Nombre = dt["Nombre"].ToString();
            modem.Marca = dt["Marca"].ToString();
            modem.Ip = dt["Ip"].ToString();
            modem.FuncionesLed = dt["FuncionesLed"].ToString();
            modem.Img1 = dt["Img1"].ToString();
            modem.Img2 = dt["Img2"].ToString();
            modem.Tecnologia = Convert.ToInt32(dt["IdTecnologia"].ToString());

            return modem;
        }


        public IEnumerable<Modem> GetAll()
        {
            string consulta = "Select * from Modem";

            gestiondb.BuscarResultados(consulta);

            DataTable dt = gestiondb.ObtenerResultados();
            IEnumerable<Modem> ListModem = gestiondb.ConvertirDataTableToList<Modem>(dt);
            if (!string.IsNullOrEmpty(gestiondb.controlOpen))
            {
                controlO = gestiondb.controlOpen;
            }
            if(!string.IsNullOrEmpty(gestiondb.controlConsulta))
            {
                controlCons = gestiondb.controlConsulta;
            }
            return ListModem;
        }

        public IEnumerable<Modem> GetAllCamoAdsl(string tecnologia)
        {
            string consulta = string.Format("SELECT m.* FROM Modem m, Tecnologia t WHERE m.IdTecnologia = t.Id AND t.Nombre = '{0}'",tecnologia);

            gestiondb.BuscarResultados(consulta);

            DataTable dt = gestiondb.ObtenerResultados();
            IEnumerable<Modem> ListModem = gestiondb.ConvertirDataTableToList<Modem>(dt);

            return ListModem;
        }       

    }
}