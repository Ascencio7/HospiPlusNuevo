using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using HospiPlus.Reportes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace HospiPlus.ReporteVista
{
    /// <summary>
    /// Lógica de interacción para frmReporteConsultasMedicas.xaml
    /// </summary>
    public partial class frmReporteConsultasMedicas : Window
    {
        public frmReporteConsultasMedicas()
        {
            InitializeComponent();
        }

        private void btnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            // Validar el dui
            string duiMedico = txtDUIMedicoReporte.Text.Trim();

            if(duiMedico.Length != 10)
            {
                MessageBox.Show("Ingrese un DUI valido de 10 caracteres incluyendo el '-'.","HOSPI PLUS | Error", MessageBoxButton.OK, MessageBoxImage.Error );
                return;
            }

            // Se valida la especialidad
            if(!int.TryParse(txtEspecialidadMedicoReporte.Text, out int especialidad) || especialidad <= 0)
            {
                MessageBox.Show("Especialidad incorrecta.", "HOSPI PLUS | Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Se intenta mostrar el reporte
            try
            {
                rptConsultas rpt = new rptConsultas();
                consultasMedicas visor = new consultasMedicas();


                rpt.Load("@rptConsultas.rpt");

                rpt.SetParameterValue("@DUIMedico", duiMedico);
                rpt.SetParameterValue("@EspecialidadID", especialidad);

                visor.crystalConsultasMedicas.ViewerCore.ReportSource = rpt;

                visor.Show();

                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}