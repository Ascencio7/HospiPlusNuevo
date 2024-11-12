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
    /// Lógica de interacción para frmRporteRecetaPorPaciente.xaml
    /// </summary>
    public partial class frmRporteRecetaPorPaciente : Window
    {
        public frmRporteRecetaPorPaciente()
        {
            InitializeComponent();
        }

        private void btnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtPacienteID.Text, out int pacienteID) && dpFechaInicio.SelectedDate.HasValue && dpFechaFin.SelectedDate.HasValue)
            {
                DateTime fechaInicio = dpFechaInicio.SelectedDate.Value;
                DateTime fechaFin = dpFechaFin.SelectedDate.Value;

                try
                {
                    rptRecetaPorPacienteYFecha rpt = new rptRecetaPorPacienteYFecha();
                    recetaPorPacienteYFecha visor = new recetaPorPacienteYFecha();

                    rpt.Load("@rptRecetaPorPacienteYFecha.rpt");

                    rpt.SetParameterValue("@PacienteID",pacienteID);
                    rpt.SetParameterValue("@FechaInicio",fechaInicio);
                    rpt.SetParameterValue("@FechaFin",fechaFin);


                    visor.crystalRecetaPorPacienteYFecha.ViewerCore.ReportSource = rpt;

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
                MessageBox.Show("Ingrese un ID de paciente válido y seleccione las fechas de inicio y fin.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
