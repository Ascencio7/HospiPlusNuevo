using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using HospiPlus.Reportes;

namespace HospiPlus.ReporteVista
{
    /// <summary>
    /// Lógica de interacción para frmExamenesPaciente.xaml
    /// </summary>
    public partial class frmExamenesPaciente : Window
    {
        public frmExamenesPaciente()
        {
            InitializeComponent();
        }

        private void btnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDuiPaciente.Text))
            {
                string duiPaciente = txtDuiPaciente.Text.Trim();

                try
                {
                    // Carga el reporte
                    rptExamenPaciente rpt = new rptExamenPaciente();
                    examenesPaciente visor = new examenesPaciente();

                    rpt.Load("@rptExamenPaciente.rpt");


                    visor.crystalExamenesPaciente.ViewerCore.ReportSource = rpt;

                    visor.Show();

                    lblStatus.Content = "Reporte generado exitosamente.";
                    lblStatus.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el reporte: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Ingrese un número de DUI válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
