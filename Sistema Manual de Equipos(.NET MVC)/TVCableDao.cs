using Cotas800.Models.Dato;
using Cotas800.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Cotas800.Models.Daos
{
    public class TVCableDao
    {

        GestionDB gestiondb = new GestionDB();

        public string Guardar(TVCable tv)
        {
            string consulta = string.Format("insert into TVCable(Nombre,Img1,Img2,Img3,Img4,Manual,Descripcion,Tecnologia) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", tv.Nombre, tv.Img1, tv.Img2, tv.Img3, tv.Img4, tv.Manual, tv.Descripcion ,tv.Tecnologia);
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

        public string Actualizar(TVCable tv)
        {
            string consulta = string.Format("update TVCable set Nombre ='{0}', Img1 = '{1}', Img2 = '{2}', Img3 = '{3}' ,Img4 = '{4}', Manual = '{5}', Descripcion = '{6}', Tecnologia = '{7}' where Id = {8}", tv.Nombre, tv.Img1, tv.Img2, tv.Img3, tv.Img4, tv.Manual, tv.Descripcion, tv.Tecnologia, Convert.ToInt32(tv.Id));
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
            string consulta = "delete from TVCable where Id =" + id;
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

        public TVCable ObtenerTVCable(int id)
        {
            string consulta = "select *from TVCable where Id = " + id;
            gestiondb.BuscarResultados(consulta);

            TVCable tv = new TVCable();

            DataRow dt = gestiondb.ObtenerResultados().Rows[0];

            tv.Id = Convert.ToInt32(dt["Id"].ToString());
            tv.Nombre = dt["Nombre"].ToString();
            tv.Img1 = dt["Img1"].ToString();
            tv.Img2 = dt["Img2"].ToString();
            tv.Img3 = dt["Img3"].ToString();
            tv.Img4 = dt["Img4"].ToString();
            tv.Manual = dt["Manual"].ToString();
            tv.Descripcion = dt["Descripcion"].ToString();
            tv.Tecnologia = dt["Tecnologia"].ToString();

            return tv;
        }


        public IEnumerable<TVCable> GetAll()
        {
            string consulta = "Select * from TVCable";

            gestiondb.BuscarResultados(consulta);

            DataTable dt = gestiondb.ObtenerResultados();
            IEnumerable<TVCable> ListTVCable = gestiondb.ConvertirDataTableToList<TVCable>(dt);

            return ListTVCable;
        }

        public IEnumerable<TVCable> GetAllDig()
        {
            string consulta = "Select * from TVCable where Tecnologia = 'DIGITAL'";

            gestiondb.BuscarResultados(consulta);

            DataTable dt = gestiondb.ObtenerResultados();
            IEnumerable<TVCable> ListTVCable = gestiondb.ConvertirDataTableToList<TVCable>(dt);

            return ListTVCable;
        }

        public IEnumerable<TVCable> GetAllDTH()
        {
            string consulta = "Select * from TVCable where Tecnologia = 'SATELITAL'";

            gestiondb.BuscarResultados(consulta);

            DataTable dt = gestiondb.ObtenerResultados();
            IEnumerable<TVCable> ListTVCable = gestiondb.ConvertirDataTableToList<TVCable>(dt);

            return ListTVCable;
        }

    }
}