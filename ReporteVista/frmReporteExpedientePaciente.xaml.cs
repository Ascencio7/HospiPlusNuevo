using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using HospiPlus.Reportes;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para frmReporteExpedientePaciente.xaml
    /// </summary>
    public partial class frmReporteExpedientePaciente : Window
    {
        public frmReporteExpedientePaciente()
        {
            InitializeComponent();
        }

        private void btnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            
            if (int.TryParse(txtPacienteID.Text, out int pacienteID))
            {
                try
                {
                    // Crear y configurar el reporte
                    rptExamenPaciente rpt = new rptExamenPaciente();
                    expedientePaciente visor = new expedientePaciente();

                    // Configurar el parámetro de PacienteID
                    rpt.SetParameterValue("@PacienteID", pacienteID);

                    rpt.Load("@rptExpedientePaciente");

                    // Mostrar el reporte en un visor o exportarlo
                    visor.crystalExpedientePacienteReport.ViewerCore.ReportSource = rpt;
                    visor.Show();

                    lblStatus.Content = "Reporte generado exitosamente.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el reporte: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Ingrese un ID de paciente válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
