using HospiPlus.ModeloPaciente;
using HospiPlus.ServicePaciente;
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

namespace HospiPlus.SistemaMedico
{
    /// <summary>
    /// Lógica de interacción para ConsultasMedico.xaml
    /// </summary>
    public partial class ConsultasMedico : Page
    {
        public ConsultasMedico()
        {
            InitializeComponent();
            this.Loaded += ConsultasMedico_Loaded;
        }

        private void ConsultasMedico_Loaded(object sender, RoutedEventArgs e)
        {
            CargarConsultas();
        }

        private void CargarConsultas()
        {
            List<ModeloConsultaPaciente> consultas = ConsultaPaciente.ObtenerConsultas();
            gridGConsultM.ItemsSource = consultas;
        }
    }
}
