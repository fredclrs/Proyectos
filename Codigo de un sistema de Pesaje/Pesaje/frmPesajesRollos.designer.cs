namespace Geshil.UI.TEJ
{
    partial class frmPesajesRollos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPesajesRollos));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.grIntercambioDetalle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSerie = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colArtCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCalidad = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCodFalla = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grMovimientos = new DevExpress.XtraGrid.GridControl();
            this.grIntercambios = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCodMov = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colArtCod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProducto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKgBruto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBruto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFecha = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEstado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNroComp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrograma = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConfirmar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuitar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEditConfirmar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemButtonEditEliminar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnRealizar = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grIntercambioDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grMovimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grIntercambios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditConfirmar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditEliminar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grIntercambioDetalle
            // 
            this.grIntercambioDetalle.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSerie,
            this.colArtCode,
            this.colDescr,
            this.colCalidad,
            this.colCodFalla,
            this.colKg});
            this.grIntercambioDetalle.GridControl = this.grMovimientos;
            this.grIntercambioDetalle.Name = "grIntercambioDetalle";
            this.grIntercambioDetalle.OptionsView.ShowGroupPanel = false;
            this.grIntercambioDetalle.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grIntercambioDetalle_RowStyle);
            // 
            // colSerie
            // 
            this.colSerie.AppearanceCell.Options.UseTextOptions = true;
            this.colSerie.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSerie.AppearanceHeader.Options.UseTextOptions = true;
            this.colSerie.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSerie.Caption = "Número de Serie";
            this.colSerie.FieldName = "MOVITEMS_NSERIE";
            this.colSerie.Name = "colSerie";
            this.colSerie.OptionsColumn.AllowEdit = false;
            this.colSerie.OptionsColumn.FixedWidth = true;
            this.colSerie.Visible = true;
            this.colSerie.VisibleIndex = 0;
            // 
            // colArtCode
            // 
            this.colArtCode.AppearanceCell.Options.UseTextOptions = true;
            this.colArtCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colArtCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colArtCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colArtCode.Caption = "Artículo";
            this.colArtCode.FieldName = "MOVITEMS_ARTCOD";
            this.colArtCode.Name = "colArtCode";
            this.colArtCode.OptionsColumn.AllowEdit = false;
            // 
            // colDescr
            // 
            this.colDescr.AppearanceCell.Options.UseTextOptions = true;
            this.colDescr.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDescr.AppearanceHeader.Options.UseTextOptions = true;
            this.colDescr.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDescr.Caption = "Descripción";
            this.colDescr.FieldName = "MOVITEMS_DESCRP";
            this.colDescr.Name = "colDescr";
            this.colDescr.OptionsColumn.AllowEdit = false;
            this.colDescr.OptionsColumn.FixedWidth = true;
            // 
            // colCalidad
            // 
            this.colCalidad.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCalidad.AppearanceCell.Options.UseFont = true;
            this.colCalidad.AppearanceCell.Options.UseTextOptions = true;
            this.colCalidad.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCalidad.AppearanceHeader.Options.UseTextOptions = true;
            this.colCalidad.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCalidad.Caption = "Calidad";
            this.colCalidad.FieldName = "MOVITEMS_NOTROS";
            this.colCalidad.Name = "colCalidad";
            this.colCalidad.OptionsColumn.AllowEdit = false;
            this.colCalidad.OptionsColumn.FixedWidth = true;
            this.colCalidad.Visible = true;
            this.colCalidad.VisibleIndex = 1;
            // 
            // colCodFalla
            // 
            this.colCodFalla.AppearanceCell.Options.UseTextOptions = true;
            this.colCodFalla.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCodFalla.AppearanceHeader.Options.UseTextOptions = true;
            this.colCodFalla.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCodFalla.Caption = "Falla";
            this.colCodFalla.FieldName = "MOVITEMS_FALLA_ID";
            this.colCodFalla.Name = "colCodFalla";
            this.colCodFalla.OptionsColumn.AllowEdit = false;
            this.colCodFalla.OptionsColumn.FixedWidth = true;
            this.colCodFalla.Visible = true;
            this.colCodFalla.VisibleIndex = 2;
            // 
            // colKg
            // 
            this.colKg.AppearanceCell.Options.UseTextOptions = true;
            this.colKg.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKg.AppearanceHeader.Options.UseTextOptions = true;
            this.colKg.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKg.Caption = "Kgs";
            this.colKg.DisplayFormat.FormatString = "N2";
            this.colKg.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colKg.FieldName = "MOVITEMS_NETO";
            this.colKg.Name = "colKg";
            this.colKg.OptionsColumn.AllowEdit = false;
            this.colKg.OptionsColumn.FixedWidth = true;
            this.colKg.Visible = true;
            this.colKg.VisibleIndex = 3;
            // 
            // grMovimientos
            // 
            this.grMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            gridLevelNode1.LevelTemplate = this.grIntercambioDetalle;
            gridLevelNode1.RelationName = "MOVCAB_NROMOV";
            this.grMovimientos.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.grMovimientos.Location = new System.Drawing.Point(5, 5);
            this.grMovimientos.MainView = this.grIntercambios;
            this.grMovimientos.Name = "grMovimientos";
            this.grMovimientos.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditConfirmar,
            this.repositoryItemButtonEditEliminar});
            this.grMovimientos.Size = new System.Drawing.Size(1073, 456);
            this.grMovimientos.TabIndex = 0;
            this.grMovimientos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grIntercambios,
            this.grIntercambioDetalle});
            // 
            // grIntercambios
            // 
            this.grIntercambios.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.grIntercambios.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.grIntercambios.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grIntercambios.Appearance.FocusedCell.Options.UseBackColor = true;
            this.grIntercambios.Appearance.FocusedCell.Options.UseFont = true;
            this.grIntercambios.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.grIntercambios.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.grIntercambios.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grIntercambios.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grIntercambios.Appearance.FocusedRow.Options.UseFont = true;
            this.grIntercambios.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.grIntercambios.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.grIntercambios.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grIntercambios.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grIntercambios.Appearance.SelectedRow.Options.UseFont = true;
            this.grIntercambios.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCodMov,
            this.colArtCod,
            this.colProducto,
            this.colKgBruto,
            this.colBruto,
            this.colFecha,
            this.colEstado,
            this.gridColumn1,
            this.colNroComp,
            this.colPrograma,
            this.colConfirmar,
            this.colQuitar});
            this.grIntercambios.GridControl = this.grMovimientos;
            this.grIntercambios.Name = "grIntercambios";
            this.grIntercambios.OptionsCustomization.AllowFilter = false;
            this.grIntercambios.OptionsCustomization.AllowGroup = false;
            this.grIntercambios.OptionsCustomization.AllowSort = false;
            this.grIntercambios.OptionsDetail.ShowDetailTabs = false;
            this.grIntercambios.OptionsDetail.SmartDetailExpandButtonMode = DevExpress.XtraGrid.Views.Grid.DetailExpandButtonMode.AlwaysEnabled;
            this.grIntercambios.OptionsMenu.EnableColumnMenu = false;
            this.grIntercambios.OptionsView.ShowAutoFilterRow = true;
            this.grIntercambios.OptionsView.ShowFooter = true;
            this.grIntercambios.OptionsView.ShowGroupPanel = false;
            this.grIntercambios.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grIntercambios_RowStyle);
            this.grIntercambios.MasterRowEmpty += new DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventHandler(this.grIntercambios_MasterRowEmpty);
            this.grIntercambios.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.grIntercambios_MasterRowExpanded);
            this.grIntercambios.MasterRowGetChildList += new DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventHandler(this.grIntercambios_MasterRowGetChildList);
            this.grIntercambios.MasterRowGetRelationName += new DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventHandler(this.grIntercambios_MasterRowGetRelationName);
            this.grIntercambios.MasterRowGetRelationCount += new DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventHandler(this.grIntercambios_MasterRowGetRelationCount);
            this.grIntercambios.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.grIntercambios_CustomRowCellEdit);
            // 
            // colCodMov
            // 
            this.colCodMov.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colCodMov.AppearanceCell.Options.UseFont = true;
            this.colCodMov.AppearanceCell.Options.UseTextOptions = true;
            this.colCodMov.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCodMov.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.colCodMov.AppearanceHeader.Options.UseBackColor = true;
            this.colCodMov.AppearanceHeader.Options.UseTextOptions = true;
            this.colCodMov.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCodMov.Caption = "Nro";
            this.colCodMov.FieldName = "MOVCAB_NROMOV";
            this.colCodMov.Name = "colCodMov";
            this.colCodMov.OptionsColumn.AllowEdit = false;
            this.colCodMov.Visible = true;
            this.colCodMov.VisibleIndex = 0;
            this.colCodMov.Width = 60;
            // 
            // colArtCod
            // 
            this.colArtCod.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colArtCod.AppearanceCell.Options.UseFont = true;
            this.colArtCod.AppearanceCell.Options.UseTextOptions = true;
            this.colArtCod.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colArtCod.AppearanceHeader.Options.UseTextOptions = true;
            this.colArtCod.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colArtCod.Caption = "Título";
            this.colArtCod.FieldName = "MOVCAB_ARTCOD";
            this.colArtCod.Name = "colArtCod";
            this.colArtCod.OptionsColumn.AllowEdit = false;
            this.colArtCod.Visible = true;
            this.colArtCod.VisibleIndex = 2;
            this.colArtCod.Width = 57;
            // 
            // colProducto
            // 
            this.colProducto.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProducto.AppearanceCell.Options.UseFont = true;
            this.colProducto.AppearanceCell.Options.UseTextOptions = true;
            this.colProducto.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProducto.AppearanceHeader.Options.UseTextOptions = true;
            this.colProducto.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProducto.Caption = "Producto";
            this.colProducto.FieldName = "MOVCAB_DESCRP";
            this.colProducto.Name = "colProducto";
            this.colProducto.OptionsColumn.AllowEdit = false;
            this.colProducto.Visible = true;
            this.colProducto.VisibleIndex = 1;
            this.colProducto.Width = 180;
            // 
            // colKgBruto
            // 
            this.colKgBruto.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colKgBruto.AppearanceCell.Options.UseFont = true;
            this.colKgBruto.AppearanceCell.Options.UseTextOptions = true;
            this.colKgBruto.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKgBruto.AppearanceHeader.Options.UseTextOptions = true;
            this.colKgBruto.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKgBruto.Caption = "Kgs ";
            this.colKgBruto.FieldName = "MOVCAB_NETO";
            this.colKgBruto.Name = "colKgBruto";
            this.colKgBruto.OptionsColumn.AllowEdit = false;
            this.colKgBruto.Visible = true;
            this.colKgBruto.VisibleIndex = 5;
            this.colKgBruto.Width = 158;
            // 
            // colBruto
            // 
            this.colBruto.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colBruto.AppearanceCell.Options.UseFont = true;
            this.colBruto.AppearanceCell.Options.UseTextOptions = true;
            this.colBruto.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBruto.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.colBruto.AppearanceHeader.Options.UseBackColor = true;
            this.colBruto.AppearanceHeader.Options.UseTextOptions = true;
            this.colBruto.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBruto.Caption = "Cantidad";
            this.colBruto.DisplayFormat.FormatString = "N0";
            this.colBruto.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBruto.FieldName = "MOVCAB_CANTBULBAL";
            this.colBruto.Name = "colBruto";
            this.colBruto.OptionsColumn.AllowEdit = false;
            this.colBruto.Visible = true;
            this.colBruto.VisibleIndex = 4;
            // 
            // colFecha
            // 
            this.colFecha.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFecha.AppearanceCell.Options.UseFont = true;
            this.colFecha.AppearanceCell.Options.UseTextOptions = true;
            this.colFecha.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFecha.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.colFecha.AppearanceHeader.Options.UseBackColor = true;
            this.colFecha.AppearanceHeader.Options.UseTextOptions = true;
            this.colFecha.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFecha.Caption = "Fecha";
            this.colFecha.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colFecha.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colFecha.FieldName = "MOVCAB_FECALT";
            this.colFecha.Name = "colFecha";
            this.colFecha.OptionsColumn.AllowEdit = false;
            this.colFecha.Visible = true;
            this.colFecha.VisibleIndex = 6;
            this.colFecha.Width = 157;
            // 
            // colEstado
            // 
            this.colEstado.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colEstado.AppearanceCell.Options.UseFont = true;
            this.colEstado.AppearanceCell.Options.UseTextOptions = true;
            this.colEstado.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEstado.AppearanceHeader.Options.UseTextOptions = true;
            this.colEstado.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEstado.Caption = "Nro Cta";
            this.colEstado.FieldName = "MOVCAB_CTAORI";
            this.colEstado.Name = "colEstado";
            this.colEstado.OptionsColumn.AllowEdit = false;
            this.colEstado.Width = 152;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Remito";
            this.gridColumn1.FieldName = "MOVCAB_NROCPTEORI";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // colNroComp
            // 
            this.colNroComp.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNroComp.AppearanceCell.Options.UseFont = true;
            this.colNroComp.AppearanceCell.Options.UseTextOptions = true;
            this.colNroComp.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNroComp.AppearanceHeader.Options.UseTextOptions = true;
            this.colNroComp.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNroComp.Caption = "Nro. Comprobante";
            this.colNroComp.FieldName = "MOVCAB_INTER_NROFOR";
            this.colNroComp.Name = "colNroComp";
            this.colNroComp.OptionsColumn.AllowEdit = false;
            this.colNroComp.Visible = true;
            this.colNroComp.VisibleIndex = 7;
            // 
            // colPrograma
            // 
            this.colPrograma.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPrograma.AppearanceCell.Options.UseFont = true;
            this.colPrograma.AppearanceCell.Options.UseTextOptions = true;
            this.colPrograma.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPrograma.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPrograma.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPrograma.AppearanceHeader.Options.UseFont = true;
            this.colPrograma.AppearanceHeader.Options.UseTextOptions = true;
            this.colPrograma.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPrograma.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPrograma.Caption = "Programa";
            this.colPrograma.FieldName = "MOVCAB_PROG_NRO";
            this.colPrograma.Name = "colPrograma";
            this.colPrograma.OptionsColumn.AllowEdit = false;
            this.colPrograma.Visible = true;
            this.colPrograma.VisibleIndex = 3;
            // 
            // colConfirmar
            // 
            this.colConfirmar.AppearanceHeader.Options.UseTextOptions = true;
            this.colConfirmar.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colConfirmar.Caption = "Confirmar";
            this.colConfirmar.Name = "colConfirmar";
            this.colConfirmar.OptionsColumn.AllowEdit = false;
            this.colConfirmar.Width = 62;
            // 
            // colQuitar
            // 
            this.colQuitar.AppearanceCell.Options.UseTextOptions = true;
            this.colQuitar.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuitar.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuitar.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuitar.Caption = "Quitar";
            this.colQuitar.Name = "colQuitar";
            this.colQuitar.OptionsColumn.AllowEdit = false;
            this.colQuitar.Width = 71;
            // 
            // repositoryItemButtonEditConfirmar
            // 
            this.repositoryItemButtonEditConfirmar.AutoHeight = false;
            this.repositoryItemButtonEditConfirmar.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEditConfirmar.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.repositoryItemButtonEditConfirmar.Name = "repositoryItemButtonEditConfirmar";
            this.repositoryItemButtonEditConfirmar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEditConfirmar.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditConfirmar_ButtonClick);
            this.repositoryItemButtonEditConfirmar.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditConfirmar_ButtonPressed);
            // 
            // repositoryItemButtonEditEliminar
            // 
            this.repositoryItemButtonEditEliminar.AutoHeight = false;
            this.repositoryItemButtonEditEliminar.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEditEliminar.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.repositoryItemButtonEditEliminar.Name = "repositoryItemButtonEditEliminar";
            this.repositoryItemButtonEditEliminar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEditEliminar.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditEliminar_ButtonClick);
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.grMovimientos);
            this.panelControl1.Location = new System.Drawing.Point(21, 76);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1083, 474);
            this.panelControl1.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnRealizar);
            this.panelControl2.Location = new System.Drawing.Point(23, 24);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(162, 46);
            this.panelControl2.TabIndex = 186;
            // 
            // btnRealizar
            // 
            this.btnRealizar.Image = ((System.Drawing.Image)(resources.GetObject("btnRealizar.Image")));
            this.btnRealizar.Location = new System.Drawing.Point(6, 5);
            this.btnRealizar.Name = "btnRealizar";
            this.btnRealizar.Size = new System.Drawing.Size(147, 35);
            this.btnRealizar.TabIndex = 187;
            this.btnRealizar.Text = "Nuevo Pesaje";
            this.btnRealizar.Click += new System.EventHandler(this.btnRealizar_Click_1);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.label1.Appearance.Font = new System.Drawing.Font("Franklin Gothic Medium", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.label1.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Show;
            this.label1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.label1.Location = new System.Drawing.Point(433, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 37);
            this.label1.TabIndex = 187;
            this.label1.Text = "Pesajes realizados";
            this.label1.ToolTip = "Confirmar Recepción";
            // 
            // frmPesajesRollos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 590);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPesajesRollos";
            this.Padding = new System.Windows.Forms.Padding(50, 70, 50, 50);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Romaneos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPesajesRollos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grIntercambioDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grMovimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grIntercambios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditConfirmar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditEliminar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void RepositoryItemButtonEdit1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private DevExpress.XtraGrid.GridControl grMovimientos;
        private DevExpress.XtraGrid.Views.Grid.GridView grIntercambios;
        private DevExpress.XtraGrid.Columns.GridColumn colCodMov;
        private DevExpress.XtraGrid.Columns.GridColumn colFecha;
        private DevExpress.XtraGrid.Views.Grid.GridView grIntercambioDetalle;
        private DevExpress.XtraGrid.Columns.GridColumn colDescr;
        private DevExpress.XtraGrid.Columns.GridColumn colKg;
        private DevExpress.XtraGrid.Columns.GridColumn colArtCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSerie;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colBruto;
        private DevExpress.XtraGrid.Columns.GridColumn colQuitar;
        private DevExpress.XtraGrid.Columns.GridColumn colEstado;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnRealizar;
        private DevExpress.XtraGrid.Columns.GridColumn colKgBruto;
        private DevExpress.XtraGrid.Columns.GridColumn colConfirmar;
        private DevExpress.XtraGrid.Columns.GridColumn colProducto;
        private DevExpress.XtraGrid.Columns.GridColumn colArtCod;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditConfirmar;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditEliminar;
		private DevExpress.XtraEditors.LabelControl label1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn colNroComp;
		private DevExpress.XtraGrid.Columns.GridColumn colCalidad;
		private DevExpress.XtraGrid.Columns.GridColumn colCodFalla;
		private DevExpress.XtraGrid.Columns.GridColumn colPrograma;
	}
}