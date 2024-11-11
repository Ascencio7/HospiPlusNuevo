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
// Habilita las ventanas guardadas en las carpetas y poder mostrarlas
using HospiPlus.SistemaAdministrador;
using HospiPlus.SistemaMedico;
using HospiPlus.SistemaSecretario;

using HospiPlus.ModeloMedico;
using HospiPlus.ModeloExamen;
using HospiPlus.ModeloPaciente;
using HospiPlus.ModeloUsuario;

using HospiPlus.ServicePaciente;
using HospiPlus.ServiceMedico;
using HospiPlus.ServiceUsuario;
using HospiPlus.ServiceExamenes;


using System.Security.Principal;
using System.Data.SqlClient;
using HospiPlus.DataAcces;
using System.Data;

namespace HospiPlus.SistemaSecretario
{
    /// <summary>
    /// Lógica de interacción para ExamenesSecretario.xaml
    /// </summary>
    public partial class ExamenesSecretario : Page
    {
        public ExamenesSecretario()
        {
            InitializeComponent();
            mostrarExamenes();
            txtBuscarExamenPorDUI.Focus();
        }

        #region METODOS PERSONALIZADOS
        //MOSTRAR EXAMENES
        void mostrarExamenes()
        {
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    gridExamenesSecre.ItemsSource = DatosExamenes.MostrarExamenes();
                    txtBuscarExamenPorDUI.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al mostrar los Examenes: "
                        + ex.Message, "Error de Conexion", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }

        //BUSCAR EXAMENES POR DUI
        public DataTable buscarExamenPorDUI(string duiPaciente)
        {
            DataTable dtExamenes = new DataTable();

            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    // Crear el comando para ejecutar el procedimiento almacenado
                    SqlCommand comando = new SqlCommand("BuscarExamenesPorDUI", conexion);
                    comando.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro DUI
                    comando.Parameters.AddWithValue("@DuiPaciente", duiPaciente);

                    // Ejecutar la consulta y cargar los resultados en el DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(comando);
                    adapter.Fill(dtExamenes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar los Exámenes por DUI: " + ex.Message, "Error de Conexión", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }

            return dtExamenes;
        }



        #endregion

        #region METODOS DEL FORMULARIO
        private void btnBuscarConsulSecre_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBuscarExamenPorDUI.Text))
            {
                // Llamar al método para buscar exámenes por DUI si el campo de texto no está vacío
                DataTable resultados = buscarExamenPorDUI(txtBuscarExamenPorDUI.Text);

                if (resultados == null || resultados.Rows.Count == 0)
                {
                    MessageBox.Show("No existe ningún registro con ese número de DUI.", "Registro no encontrado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Mostrar los resultados en el DataGrid
                    gridExamenesSecre.ItemsSource = resultados.DefaultView;
                }
            }
            else
            {
                // Si el campo de texto está vacío, mostrar todos los exámenes
                txtBuscarExamenPorDUI.Clear();
                mostrarExamenes();
            }
        }


        private void btnCancelarConsulSecre_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea limpiar la búsqueda?", "Limpiar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                txtBuscarExamenPorDUI.Text = string.Empty;

                mostrarExamenes();
                return;
            }
        }

        #endregion


    }
}
