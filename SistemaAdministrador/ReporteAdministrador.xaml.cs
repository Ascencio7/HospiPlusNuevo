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
<<<<<<< Updated upstream

        // Primer reporte
=======
        int DUIPaciente = 0;
        int FechaExamen = 0;
>>>>>>> Stashed changes
        private void btnExpedientePacienteReporte_Click(object sender, RoutedEventArgs e)
        {
            rptExpedientePaciente rpt = new rptExpedientePaciente();
            expedientePaciente visor = new expedientePaciente();

            rpt.Load("@rptExpedientePaciente.rpt");

            rpt.SetParameterValue("@PacienteID", pacienteid);

            visor.crystalExpedientePacienteReport.ViewerCore.ReportSource = rpt;

            visor.Show();
        }

<<<<<<< Updated upstream

        // Segundo reporte
        private void btnRecetaPorPacienteYFechaReporte_Click(object sender, RoutedEventArgs e)
        {
            rptRecetaPorPacienteYFecha rpt = new rptRecetaPorPacienteYFecha();
            recetaPorPacienteYFecha visor = new recetaPorPacienteYFecha();

            rpt.Load("@rptRecetaPorPacienteYFecha.rpt");


            visor.crystalRecetaPorPacienteYFecha.ViewerCore.ReportSource = rpt;
=======
        private void btnExamenes_Click(object sender, RoutedEventArgs e)
        {
            rptExamenesPorPacienteFecha rpt = new rptExamenesPorPacienteFecha();
            reporteexpediente visor = new reporteexpediente();

            rpt.Load("@rptExamenesPorPacienteFecha.rpt");

            rpt.SetParameterValue("@DuiPaciente", DUIPaciente);

            visor.crystalExamenPacienteReport.ViewerCore.ReportSource = rpt;
>>>>>>> Stashed changes

            visor.Show();
        }
    }
}
