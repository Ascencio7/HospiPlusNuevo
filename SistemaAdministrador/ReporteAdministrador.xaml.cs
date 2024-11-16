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
using System.Windows.Navigation;
using System.Windows.Shapes;


using HospiPlus.Reportes;
using CrystalDecisions.CrystalReports.Engine;
using HospiPlus.ReporteVista;


namespace HospiPlus.SistemaAdministrador
{
    /// <summary>
    /// Lógica de interacción para ReporteAdministrador.xaml
    /// </summary>
    public partial class ReporteAdministrador : Page
    {
        public ReporteAdministrador()
        {
            InitializeComponent();
        }

        int pacienteid = 0;

        // Primer reporte
        private void btnExpedientePacienteReporte_Click(object sender, RoutedEventArgs e)
        {
            frmReporteExpedientePaciente reporteExp = new frmReporteExpedientePaciente();
            reporteExp.Show();
        }


        // Segundo reporte
        private void btnRecetaPorPacienteYFechaReporte_Click(object sender, RoutedEventArgs e)
        {
            frmRporteRecetaPorPaciente reporteRec = new frmRporteRecetaPorPaciente();
            reporteRec.Show();
        }


        private void btnExamenPaciente_Click(object sender, RoutedEventArgs e)
        {
            frmExamenesPaciente frmExa = new frmExamenesPaciente();
            frmExa.Show();
        }


        private void btnConsultas_Click(object sender, RoutedEventArgs e)
        {
            frmReporteConsultasMedicas frmConsul = new frmReporteConsultasMedicas();
            frmConsul.Show();
        }
    }
}