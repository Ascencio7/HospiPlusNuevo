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
    /// Lógica de interacción para frmReportesConsultaMedico.xaml
    /// </summary>
    public partial class frmReportesConsultaMedico : Window
    {
        public frmReportesConsultaMedico()
        {
            InitializeComponent();
        }

        private void btnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            string duiMedico = txtDuiMedico.Text.Trim();
            int especialidadID;
            bool isEspecialidadIDValid = int.TryParse(txtEspecialidadID.Text, out especialidadID);

            if (!string.IsNullOrEmpty(duiMedico) || isEspecialidadIDValid)
            {
                try
                {
                    rptConsultasMedico rpt = new rptConsultasMedico();
                    consultasMedico visor = new consultasMedico();

                    rpt.Load("@rptConsultasMedico.rpt");

                    // Establecer valores de los parámetros
                    if (!string.IsNullOrEmpty(duiMedico))
                    {
                        rpt.SetParameterValue("@DUIMedico", duiMedico);
                    }
                    if (isEspecialidadIDValid)
                    {
                        rpt.SetParameterValue("@EspecialidadID", especialidadID);
                    }

                    visor.crystalConsultaMedico.ViewerCore.ReportSource = rpt;
                    visor.Show();

                    lblStatus.Content = "Reporte generado exitosamente.";
                    lblStatus.Foreground = Brushes.Green;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el reporte: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    lblStatus.Content = "Error al generar el reporte.";
                    lblStatus.Foreground = Brushes.Red;
                }
            }
            else
            {
                MessageBox.Show("Ingrese al menos un DUI del médico o un ID de especialidad válido.", "Error de validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                lblStatus.Content = "Validación fallida.";
                lblStatus.Foreground = Brushes.Orange;
            }
        }


    }
}
