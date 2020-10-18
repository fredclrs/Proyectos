using Cotas800.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Cotas800.Models.Dato
{
    public class GestionDB
    {
        private SQLiteConnection conector;
        private SQLiteCommand comand;
        private SQLiteDataAdapter adaptador;
        private DataTable dtResultados;
        public string controldb;
        public string controlOpen;
        public string controlConsulta;
        public bool Estado = true;
       

        public GestionDB()
        {
            
            var conect = HttpContext.Current.Request.MapPath("~/ArchivosBD/BDcotas/DBCotas800.db");

            controldb = conect;
          
            conector = new SQLiteConnection("Data Source = " + conect);

        }

        public bool Conectar()
        {
            try
            {
                if (conector.State == ConnectionState.Closed)
                {
                    conector.Open();
                    controlOpen = "Se abrio conrrectamente la BD";
                    return true;
                }
                else {
                    return false;
                }

            }
            catch (Exception ex)
            {
                controlOpen = ex.ToString();               
                throw;
            }

            //if (conector.State == ConnectionState.Closed)
            //{
            //    conector.Open();
            //    controlOpen = "Se abrio conrrectamente la BD";
            //    return true;
            //}
            //else
            //{
            //    controlOpen = "No se pudo abrir la conexion BD";
            //    return false;
            //}

        }

        public bool Desconectar()
        {
            if (conector.State == ConnectionState.Open)
            {
                conector.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EjecutarConculta(string consulta)
        {
            try
            {
                Conectar();
                comand = new SQLiteCommand();
                comand.Connection = conector;
                comand.CommandText = consulta;

                comand.CommandTimeout = 300;
                comand.ExecuteNonQuery();
                controlConsulta = "Se ejecuto la Consulta Conrrectamente";                
                return true;                
            }
            catch (Exception ex)
            {
                controlConsulta = ex.ToString(); ;
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

        public void BuscarResultados(string strComando)
        {
            try
            {
                dtResultados = new DataTable();

                Conectar();

                if (comand == null)
                {
                    comand = new SQLiteCommand();
                    comand.Connection = conector;
                }
                else {
                    comand = new SQLiteCommand();
                    comand.Connection = conector;
                }
                comand.CommandText = strComando;
                comand.CommandType = CommandType.Text;
                adaptador = new SQLiteDataAdapter(comand);
                adaptador.Fill(dtResultados);
                comand.Dispose();
                controlConsulta = "Se ejecuto la Consulta Conrrectamente";
                Desconectar();
            }
            catch (Exception ex)
            {
                Estado = false;
                controlConsulta = ex.ToString(); ;
            }
        }

        public DataTable ObtenerResultados()
        {
            return dtResultados;
        }

        //convierte DataTable a una Lista
        public IEnumerable<T> ConvertirDataTableToList<T>(DataTable dt)
        {
           
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }           

            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {                
                foreach (PropertyInfo pro in temp.GetProperties())
                {                  
                    
                    if (pro.Name == column.ColumnName)
                      

                        if (dr[column.ColumnName].ToString() != "") {

                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                        
                    else
                        continue;
                }
            }           

            return obj;
        }

        /////////////// Sube la imagem a la capeta Content Imagenes ///////////////////

        public string SubirImagen(HttpPostedFileBase file, string carpeta)
        {
            var path = string.Empty;
            var pic = string.Empty;

            if (file != null)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".png")
                {
                    pic = Path.GetFileName(file.FileName);
                    path = Path.Combine(HttpContext.Current.Server.MapPath(carpeta), pic);
                    file.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                }
                else {
                    return null;
                }
                
            }

            return pic;        
        }

        public string SubirArchivoExel(HttpPostedFileBase file, string carpeta)
        {
            var path = string.Empty;
            var pic = string.Empty;

            if (file != null)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".xlsx" )
                {
                    pic = Path.GetFileName(file.FileName);
                    path = Path.Combine(HttpContext.Current.Server.MapPath(carpeta), pic);
                    file.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                }
                else
                {
                    return null;
                }

            }

            return pic;
        }

        public string SubirManules(HttpPostedFileBase file, string carpeta)
        {
            var path = string.Empty;
            var pic = string.Empty;

            if (file != null)
            {

                pic = Path.GetFileName(file.FileName);
                path = Path.Combine(HttpContext.Current.Server.MapPath(carpeta), pic);
                file.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }

            return pic;
        }


        public  DataTable ConvertXSLXtoDataTable(string NombreHoja, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            try
            {

                oledbConn.Open();
                using (OleDbCommand cmd = new OleDbCommand("Select * from [" + NombreHoja + "$]", oledbConn))
                {
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    oleda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    oleda.Fill(ds);

                    dt = ds.Tables[0];
                }
            }
            catch(Exception ex)
            {
            }
            finally
            {

                oledbConn.Close();
            }

            return dt;

        }


    }
}