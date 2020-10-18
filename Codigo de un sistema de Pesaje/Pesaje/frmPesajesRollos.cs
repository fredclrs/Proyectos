using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using Geshil.BI;
using Geshil.UI.HI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Geshil.BI;

namespace Geshil.UI.TEJ
{
    public partial class frmPesajesRollos : Form
    {
		List<csMovimientos> Pesajes;

        public frmPesajesRollos()
        {
            InitializeComponent();
            Iniciar();
        }

        void Iniciar()
        {
			Pesajes = csTejeduria.GetAllPesajes();
            grMovimientos.DataSource = Pesajes;
        }

        private void frmPesajesRollos_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
        }

        private void grIntercambios_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            if (e.RelationIndex == -1) return;
            GridView view = sender as GridView;

            ((GridView)view.GetDetailView(e.RowHandle, e.RelationIndex)).BestFitColumns();

            GridView detailView = view.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;

            detailView.BestFitColumns();
        }

        private void grIntercambios_MasterRowEmpty(object sender, MasterRowEmptyEventArgs e)
        {
            e.IsEmpty = false;
        }

        private IList<csItems> GetDetailDataHistorico(ColumnView view, int rowHandle)
        {
			csMovimientos cabecera = (csMovimientos)grIntercambios.GetFocusedRow();

			return csTejeduria.GetAllDetalleRecepcion(cabecera);
        }

        bool IsRelationEmpty(int rowHandle, int relationIndex)
        {
            return rowHandle == GridControl.InvalidRowHandle || relations[0, relationIndex] == null;
        }
        private object[,] relations = { { "MOVCAB_NROMOV" } };

        private void grIntercambios_MasterRowGetChildList(object sender, MasterRowGetChildListEventArgs e)
        {
            if (IsRelationEmpty(0, e.RelationIndex)) return;
            string s = relations[0, e.RelationIndex].ToString();
            switch (s)
            {
                case "MOVCAB_NROMOV":
                    var source = new BindingSource();
                    source.DataSource = GetDetailDataHistorico((ColumnView)sender, e.RowHandle);
                    if (source.Count == 0)
                    {
                        XtraMessageBox.Show("No hay items para esta selección");
                    }
                    e.ChildList = source;
                    break;
            }
        }

        private void grIntercambios_MasterRowGetRelationCount(object sender, MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void grIntercambios_MasterRowGetRelationName(object sender, MasterRowGetRelationNameEventArgs e)
        {
            if (IsRelationEmpty(0, e.RelationIndex)) e.RelationName = "";
            else
                e.RelationName = relations[0, e.RelationIndex].ToString();
        }

        private void grIntercambioDetalle_RowStyle(object sender, RowStyleEventArgs e)
        {
           
        }

        private void grIntercambios_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle > -1) {

                var elemento = ((csMovimientos)((GridView)sender).GetRow(e.RowHandle));

				if (elemento.MOVCAB_ESTADO == csEstados.EstadosMovimiento.TERMINADO)
				{
					e.Appearance.BackColor = Color.LightGray;
				}

				if (elemento.MOVCAB_ESTADO == csEstados.EstadosMovimiento.HABILITADO)
				{
					e.Appearance.BackColor = Color.LightGreen;
				}

				if (elemento.MOVCAB_ESTADO == csEstados.EstadosMovimiento.BORRADO)
				{
					e.Appearance.BackColor = Color.LightSalmon;
				}
			}
        }

        private void btnRealizar_Click_1(object sender, EventArgs e)
        {
			frmPesajeRollos _frmPesajeRollos = new frmPesajeRollos();
			_frmPesajeRollos.ShowDialog();
            Iniciar();
			_frmPesajeRollos.Dispose();
            btnRealizar.Focus();
        }

		private void repositoryItemButtonEditEliminar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			var mov = (csMovimientos)grIntercambios.GetFocusedRow();

			if (mov != null) {

				csTejeduria.EliminarRomaneo(mov);

				if (csTejeduria.Estado.HayError)
				{
					XtraMessageBox.Show(csTejeduria.Estado.Observacion + Environment.NewLine + csTejeduria.Estado.Mensaje);
				}

				Iniciar();
			}
		}

		private void repositoryItemButtonEditConfirmar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			var mov = (csMovimientos)grIntercambios.GetFocusedRow();

			if(mov != null) { 

				csTejeduria.CompletarRomaneo(mov);

				if (csTejeduria.Estado.HayError)
				{
					XtraMessageBox.Show(csTejeduria.Estado.Observacion + Environment.NewLine + csTejeduria.Estado.Mensaje);
				}

				Iniciar();
			}
		}

		private void repositoryItemButtonEditConfirmar_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{

		}

		private void grIntercambios_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			if(e.RowHandle < 0)
			{
				return;
			}

			if( (e.Column.Name== "colConfirmar" || e.Column.Name == "colQuitar") &&  ((csMovimientos)((GridView)sender).GetRow(e.RowHandle)).MOVCAB_ESTADO == csEstados.EstadosMovimiento.HABILITADO)
			{
				if(e.Column.Name == "colConfirmar")
				{
					e.RepositoryItem = repositoryItemButtonEditConfirmar;
				}
				else
				{
					e.RepositoryItem = repositoryItemButtonEditEliminar;
				}

				e.Column.OptionsColumn.AllowEdit = true;
			}

		}
	}
}
