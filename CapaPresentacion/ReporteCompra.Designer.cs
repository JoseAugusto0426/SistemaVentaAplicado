namespace CapaPresentacion
{
    partial class ReporteCompra
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dETALLECOMPRABindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.celularesDataSet = new CapaPresentacion.CelularesDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dETALLE_COMPRATableAdapter = new CapaPresentacion.CelularesDataSetTableAdapters.DETALLE_COMPRATableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dETALLECOMPRABindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.celularesDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dETALLECOMPRABindingSource
            // 
            this.dETALLECOMPRABindingSource.DataMember = "DETALLE_COMPRA";
            this.dETALLECOMPRABindingSource.DataSource = this.celularesDataSet;
            // 
            // celularesDataSet
            // 
            this.celularesDataSet.DataSetName = "CelularesDataSet";
            this.celularesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.dETALLECOMPRABindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CapaPresentacion.ReportCompra.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // dETALLE_COMPRATableAdapter
            // 
            this.dETALLE_COMPRATableAdapter.ClearBeforeFill = true;
            // 
            // ReporteCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteCompra";
            this.Text = "ReporteCompra";
            this.Load += new System.EventHandler(this.ReporteCompra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dETALLECOMPRABindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.celularesDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private CelularesDataSet celularesDataSet;
        private System.Windows.Forms.BindingSource dETALLECOMPRABindingSource;
        private CelularesDataSetTableAdapters.DETALLE_COMPRATableAdapter dETALLE_COMPRATableAdapter;
    }
}