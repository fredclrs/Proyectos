using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProduccionBI;
using System.IO.Ports;
using Geshil.BI;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid;

namespace Geshil.UI.TEJ
{
	public partial class frmPesajeRollos : XtraForm
	{
		ProduccionBI.Serial PuertoSerial;
		List<csProducto> ListaProductos;
		List<csItems> ListaPesaje;
		List<csFalla> ListaFallas;
		List<Seguridad.BI.csUser> ListaUsuarios;
		csMovimientos MovPesaje;

		public frmPesajeRollos()
		{
			InitializeComponent();
			ListaPesaje = new List<csItems>();
			CargarCboProductos();
			CargarConfigBalanza();
			CargarCboFallas();
			CargarCboTaras();
			CargarCboRevisadorYTejedor();
		}

		private void CargarCboRevisadorYTejedor()
		{
			ListaUsuarios = Seguridad.BI.csUsuario.GetAll();
			cboRevisador.Properties.DataSource = ListaUsuarios;
			cboTejedor.Properties.DataSource = ListaUsuarios;
		}

		private void ObtenerMovimientoPendiente(csProducto producto)
		{
			ListaPesaje = new List<csItems>();
			var movsInc = csMovimientos.GetInconclusos(Seguridad.BI.csUsuario.Empresa, csEstados.TipoMovimiento.PROBTJ, producto.STMPDH_TIPPRO, producto.STMPDH_ARTCOD);

			if(movsInc == null)
			{
				return;					
			}

			var movsIncMaq = movsInc.Where(x => x.MOVCAB_MAQUINA == Environment.MachineName).ToList();

			if(movsIncMaq.Count() > 0)
			{
				this.MovPesaje = movsIncMaq.First();
				this.spnNroProg.Value = this.MovPesaje.MOVCAB_PROG_NRO;
				this.spnNroProg.ReadOnly = true;
				this.ListaPesaje = csTejeduria.GetDetallePesaje(this.MovPesaje);

			}

		}

		private void GrabarNuevoMovimiento(csProducto producto)
		{
            this.MovPesaje = csTejeduria.GrabarPesaje(producto, (int)this.spnNroProg.Value);
		}

		private void CargarCboTaras()
		{
			cboTara.Properties.DataSource = csTara.GetAll();
		}

		private void CargarCboFallas()
		{
			ListaFallas = csFalla.GetAll();
			cboFalla.Properties.DataSource = ListaFallas;
			cboFalla.EditValue = ListaFallas.Single(x => x.FALLA_ID.Trim() == "9999");
        }

		private void CargarListaPesaje()
		{
			grdEmbalaje.DataSource = ListaPesaje.OrderByDescending(x=>x.MOVITEMS_NROITM).ToList();
			calcularMtsKgs();

		}

		private void CargarCboProductos()
		{
			ListaProductos = csProducto.GetAll("PU", consideraBajas: false).ToList();
			cboProductos.Properties.DataSource = ListaProductos;
		}

		private void CargarConfigBalanza()
		{
			ProduccionBI.GestionBalanza.Iniciar();
			ProduccionBI.GestionBalanza.Cargar();
			Control.CheckForIllegalCrossThreadCalls = false;

			if (ProduccionBI.GestionBalanza.Estado.HayError)
			{
				XtraMessageBox.Show("Error al cargar datos configuración de balanza", "Error en la balanza", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			ConfigSerial configPuertoSerial = new ConfigSerial();
			configPuertoSerial.PUERTO_SERIE = "COM" + GestionBalanza.Balanza.Puerto.ToString();
			PuertoSerial = new ProduccionBI.Serial(configPuertoSerial);
			PuertoSerial.mySerialPort.DataReceived += Puerto_DataReceived;

			spnKilosBalanza.ReadOnly = (ProduccionBI.GestionBalanza.Balanza.IncluyeBalanza);
			spnKilosBalanza.TabStop = (ProduccionBI.GestionBalanza.Balanza.IncluyeBalanza);

		}

		private void Puerto_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			SerialPort sp = (SerialPort)sender;
			string indata = sp.ReadExisting();

			decimal result = FormatearResultado(indata);

			spnKilosBalanza.Value = result;
		}

		private decimal FormatearResultado(string pResultado)
		{
			if (string.IsNullOrEmpty(pResultado))
			{
				return 0;
			}
			try
			{
				//pResultado = pResultado.Remove('.');
				//pResultado = pResultado.Replace("EE", "D");
				//pResultado = pResultado.Replace("AA", "D");
				//pResultado = pResultado.Replace("F", "D");
				//pResultado = pResultado.Replace("@", "D");
				//pResultado = pResultado.Replace("\r", string.Empty);

				//var parser = pResultado.Split('D');

				//var resultados = parser.Where(a => a != string.Empty).ToList();

				////var parser = resultado.Split('D');
				//if (resultados.Count == 0) return 0;
				//pResultado = resultados.Last();
				//string resultado = null;
				//for (int i = 0; i < pResultado.Length; i++)
				//{
				//    if (char.IsDigit(pResultado[i]))
				//    {
				//        resultado = resultado + pResultado[i];
				//    }
				//}

				//string resultado = pResultado;
				//if (pResultado.Length > 161)
				//{
				//    resultado = pResultado.Substring(161, 5);
				//}
				//Console.WriteLine("Resultado LENGHT " + pResultado.Length);

				string resultado = pResultado;
				if (resultado.Contains("\n"))
				{
					resultado = resultado.Substring(0, resultado.IndexOf("\n"));
				}

				string res = null;

				for (int i = 0; i < resultado.Length; i++)
				{
					if (char.IsDigit(resultado[i]))
					{
						res = res + resultado[i];
					}
				}


				resultado = res;
				if (resultado.Length > GestionBalanza.Balanza.CONFIBAL_POSCAR_BALANZA)
				{
					resultado = resultado.Substring(GestionBalanza.Balanza.CONFIBAL_POSCAR_BALANZA, GestionBalanza.Balanza.CONFIBAL_CNTCAR_BALANZA);
				}


				int decimales = GestionBalanza.Balanza.CONFIBAL_DECIMALES;
				decimal RESULTADO = 0;

				if (decimales > 0)
				{

					string StrNroDividir = "1";
					for (int i = 0; i < GestionBalanza.Balanza.CONFIBAL_DECIMALES; i++)
					{
						StrNroDividir = StrNroDividir + "0";
					}

					RESULTADO = decimal.Parse(resultado) / decimal.Parse(StrNroDividir);

					if (RESULTADO == 0)
					{
						RESULTADO = decimal.Parse(resultado);
					}
				}
				else
				{
					RESULTADO = decimal.Parse(resultado);
				}

				return RESULTADO;

			}
			catch (Exception)
			{
				return 0;
			}
		}


		private void btnAgregarCaja_Click(object sender, EventArgs e)
		{

			if (!ValidarTara())
			{
				return;
			}

			if (!ValidarMts())
			{
				return;
			}

			if (!ValidarKgs())
			{
				return;
			}

			if (!ValidarTejedor())
			{
				return;
			}

			if (!ValidarRevisador())
			{
				return;
			}

			if (!ValidarNroPrograma())
			{
				return;
			}

			var item = Grabar();

			Imprimir(item);

            CargarListaPesaje();
		}

		private bool ValidarNroPrograma()
		{
			if(spnNroProg.Value == 0)
			{
				XtraMessageBox.Show("Ingrese el número de programa");
				return false;
			}

			return true;
		}

		private bool ValidarRevisador()
		{
			if (cboRevisador.EditValue == null ||string.IsNullOrEmpty(cboRevisador.EditValue.ToString()))
			{
				XtraMessageBox.Show("Debe ingresar el revisador");
				cboRevisador.Focus();
				return false;
			}

			return true;
		}

		private bool ValidarTejedor()
		{
			if (cboTejedor.EditValue == null || string.IsNullOrEmpty(cboTejedor.EditValue.ToString()))
			{
				XtraMessageBox.Show("Debe ingresar el tejedor");
				cboTejedor.Focus();
				return false;
			}

			return true;
		}

		private void Imprimir(csItems item)
		{
			frmWaitForm carga = new frmWaitForm();

			try
			{
				carga.MostrarSplashScreenManager("Procesando...");
				ListaPesaje.Add(item);
				List<csItems> Items = new List<csItems>();
				Items.Add(item);
				var rptEtiquetas = new Reports.UI.TE.rptEtiquetaRollo(item);
				rptEtiquetas.DataSource = Items;
				ReportPrintTool pt = new ReportPrintTool(rptEtiquetas);
				pt.Print();

			}
			catch (Exception ex)
			{
				XtraMessageBox.Show("Ocurrió un error al imprimir: " + Environment.NewLine + ex.Message.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			finally
			{
				carga.CerrarSplashScreenManager();
			}
		}



		private csItems Grabar()
		{
			csProducto prodEleg = ((csProducto)cboProductos.EditValue);

			if (ListaPesaje.Count() == 0 && MovPesaje == null)
			{
				GrabarNuevoMovimiento(prodEleg);
			}

			csItems item = new csItems();
			item.MOVITEMS_BRUTO = spnKilosBalanza.Value;
			item.MOVITEMS_TARA = ((csTara)cboTara.EditValue).TARAS_PESO;
			item.MOVITEMS_NETO = item.MOVITEMS_BRUTO - item.MOVITEMS_TARA;
			item.MOVITEMS_CANTID = item.MOVITEMS_NETO;
			item.MOVITEMS_CNTSEC = spnMetros.Value;
			item.MOVITEMS_DEPOSI = csDeposito.TipoDepositos.CRUDO;
			item.MOVITEMS_SECTOR = csSector.TipoSectores.TEJEDURIA;
            item.MOVITEMS_ARTCOD = prodEleg.STMPDH_ARTCOD;
			item.MOVITEMS_TIPPRO = prodEleg.STMPDH_TIPPRO;
			item.MOVITEMS_DESCRP = prodEleg.STMPDH_DESCRP;
			string falla = (cboFalla.EditValue == null ? null : ((csFalla)cboFalla.EditValue).FALLA_ID);
            item.MOVITEMS_NOTROS = (string.IsNullOrEmpty(falla) || falla.Trim() == "9999") ? "B" : "F";
			item.MOVITEMS_FALLA_ID = falla;
			item.MOVITEMS_TEJE_TEJEDOR = GetTejedor();
			item.MOVITEMS_TEJE_REVISADOR = GetRevisador();
			
			item = csTejeduria.GrabarItemPesaje(this.MovPesaje, item);

			if (csTejeduria.Estado.HayError)
			{
				XtraMessageBox.Show(csTejeduria.Estado.Observacion + Environment.NewLine + csTejeduria.Estado.Mensaje);
			}

			return item;
        }

		private string GetRevisador()
		{
			Seguridad.BI.csUser usuario = ((Seguridad.BI.csUser)cboRevisador.EditValue);

			string revisador = usuario.Apellido + ", " + usuario.Nombre;

			return revisador;
		}

		private string GetTejedor()
		{
			Seguridad.BI.csUser usuario = ((Seguridad.BI.csUser)cboTejedor.EditValue);
			string tejedor = usuario.Apellido + ", " + usuario.Nombre;

			return tejedor;
		}

		private bool ValidarTara()
		{
			if (cboTara.EditValue == null)
			{
				XtraMessageBox.Show("Debe ingresar la Tara");
				cboTara.Focus();
				return false;
			}

			return true;
		}

		private bool ValidarMts()
		{
			if (spnMetros.Value <= 0)
			{
				XtraMessageBox.Show("Debe ingresar los metros");
				spnMetros.SelectAll();
				return false;
			}

			if (GestionBalanza.Balanza.ControlMetros) { 

				if (spnMetros.Value > GestionBalanza.Balanza.MaximoMts)
				{
					XtraMessageBox.Show("Los metros no pueden superar el máximo establecido (X)".Replace("X", GestionBalanza.Balanza.MaximoMts.ToString("N2")));
					spnMetros.SelectAll();
					return false;
				}

				if (spnMetros.Value < GestionBalanza.Balanza.MinimoMts)
				{
					XtraMessageBox.Show("Los metros no pueden superar el mínimo establecido (X)".Replace("X", GestionBalanza.Balanza.MinimoMts.ToString("N2")));
					spnMetros.SelectAll();
					return false;
				}
			}

			return true;
		}

		private bool ValidarKgs()
		{
			if (GestionBalanza.Balanza.ControlKilos)
			{

				if (spnKilosBalanza.Value > GestionBalanza.Balanza.MaximoKgs)
				{
					XtraMessageBox.Show("Los kgs no pueden superar el máximo establecido (X)".Replace("X", GestionBalanza.Balanza.MaximoKgs.ToString("N2")));
					spnKilosBalanza.SelectAll();
					return false;
				}

				if (spnKilosBalanza.Value < GestionBalanza.Balanza.MinimoKgs)
				{
					XtraMessageBox.Show("Los kgs no pueden superar el mínimo establecido (X)".Replace("X", GestionBalanza.Balanza.MinimoKgs.ToString("N2")));
					spnKilosBalanza.SelectAll();
					return false;
				}

			}

			return true;
		}

		private void calcularMtsKgs()
		{
		//	spnKgsPesados.Value = ListaPesaje.Sum(x => x.MOVITEMS_BRUTO);
		//	spnMtsPesados.Value = ListaPesaje.Sum(x => (decimal) x.MOVITEMS_CNTSEC);
		}

		private void cboFalla_EditValueChanged(object sender, EventArgs e)
		{

		}

		private void cboProductos_EditValueChanged(object sender, EventArgs e)
		{
			csProducto producto =  (csProducto)cboProductos.EditValue;
			if(producto != null) {
				ConfigurarProducto(producto);
				ObtenerMovimientoPendiente(producto);
				CargarListaPesaje();
			}
		}

		private void ConfigurarProducto(csProducto producto)
		{
			txtArtCod.Text = producto.STMPDH_ARTCOD;
			txtDescripcion.Text = producto.STMPDH_DESCRP;
			tabs.Enabled = true;
			btnOk.Enabled = true;
		}

		private void repositoryItemButtonEditQuitar_Click(object sender, EventArgs e)
		{
			csItems item =(csItems)grdListadoEmbalajes.GetFocusedRow();

			if(item == null)
			{
				return;
			}

			if(XtraMessageBox.Show("¿Está seguro de eliminar este registro?","Atención",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning) == DialogResult.Yes) { 

				csTejeduria.EliminarPiezaPesaje(this.MovPesaje,item);
				var itmBorrar = ListaPesaje.Single(x => x.MOVITEMS_NROITM == item.MOVITEMS_NROITM);
				ListaPesaje.Remove(itmBorrar);

			}
			CargarListaPesaje();
		}

		private void cboTara_EditValueChanged(object sender, EventArgs e)
		{
			if(((csTara)cboTara.EditValue) != null) { 
				spnKgsTara.Value = ((csTara)cboTara.EditValue).TARAS_PESO;
			}
		}

		private void spnKilosBalanza_Click(object sender, EventArgs e)
		{
			((SpinEdit)sender).SelectAll();
		}

		private void spnMetros_Click(object sender, EventArgs e)
		{
			((SpinEdit)sender).SelectAll();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (!Validar())
			{
				return;
			}
			 
			frmWaitForm carga = new frmWaitForm();
			carga.MostrarSplashScreenManager("Procesando...");

			try {

				AsignarObservacion();

				csTejeduria.CompletarPesaje(MovPesaje);

				if (csTejeduria.Estado.HayError)
				{
					throw new NullReferenceException(csTejeduria.Estado.Mensaje);
				}
				
			}catch(Exception ex)
			{
				MessageBox.Show("Ocurrió un error inesperado.." + Environment.NewLine + ex.Message);
			}
			finally
			{
				carga.CerrarSplashScreenManager();
			}

			Limpiar();

		}

		private void AsignarObservacion()
		{
			if(cboMemo.EditValue != null)
			{
				this.MovPesaje.MOVCAB_OBSERVACIONES = cboMemo.EditValue.ToString();
			}
		}

		private bool Validar()
		{
			if (ListaPesaje.Count == 0)
			{
				XtraMessageBox.Show("No completó el pesaje");
				return false;
			}


			return true;
		}

		void Limpiar()
		{
			ListaPesaje.Clear();
			grdEmbalaje.DataSource = ListaPesaje;
			grdEmbalaje.RefreshDataSource();
			txtArtCod.Text = null;
			txtDescripcion.Text = null;
			cboTara.EditValue = null;
			cboFalla.EditValue = null;
			spnKilosBalanza.Value = 0;
			spnMetros.Value = 0;
			cboRevisador.EditValue = null;
			cboTejedor.EditValue = null;
			//spnKgsPesados.Value = 0;
			spnKgsTara.Value = 0;
			//spnMtsPesados.Value = 0;
			cboProductos.EditValue = null;
			tabs.Enabled = false;
			btnOk.Enabled = false;
			spnNroProg.ReadOnly = false;
			spnNroProg.Value = 0;
			cboFalla.EditValue = ListaFallas.Single(x => x.FALLA_ID.Trim() == "9999");
		}

	

		private void gridView2_RowStyle_1(object sender, RowStyleEventArgs e)
		{
			try
			{
				if (((csFalla)((GridView)sender).GetRow(e.RowHandle)).FALLA_ID.Trim() == "9999")
				{
					e.Appearance.ForeColor = Color.Green;
				}
				else
				{
					e.Appearance.ForeColor = Color.Salmon;
				}


			}
			catch (Exception) { }
		}

		private void spnNroProg_Click(object sender, EventArgs e)
		{
			((SpinEdit)sender).SelectAll();
		}
	}
}
