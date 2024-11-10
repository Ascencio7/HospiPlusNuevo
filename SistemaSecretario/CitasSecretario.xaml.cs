using HospiPlus.DataAcces;
using HospiPlus.ModeloMedico;
using HospiPlus.ServiceMedico;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace HospiPlus.SistemaSecretario
{
    /// <summary>
    /// Lógica de interacción para CitasSecretario.xaml
    /// </summary>
    public partial class CitasSecretario : Page
    {

        public List<CitasModel> VerCitas {  get; set; }

        public CitasSecretario()
        {
            InitializeComponent();
            MostrarCitas();
            CargarComboBoxes();
        }

        //DECLARACION DE VARIABLES
        int pacienteid = 0;

        //METODOS PERSONALIZADOS
        #region METODOS PERSONALIZADOS

        //VALIDAR EL FORMULARIO
         bool ValidarFormulario()
         {
             bool estado = true;
             string mensaje = null;

             //cmbBuscarEspSecre
             if (cmbBuscarEspSecre.SelectedItem == null)
             {
                 estado = false;
                 mensaje += "Especialidad\n";
             }

            //txtDuiPaciente
            if (string.IsNullOrWhiteSpace(txtDuiPaciente.Text))
             {
                 estado = false;
                 mensaje += "DUI del paciente\n";
             }

             //txtApellidoPacienteS
             if (cmbMedicoID.SelectedItem == null)
             {
                 estado = false;
                 mensaje += "Medico\n";
             }

             //txtDUIPacienteS
             if (cmbEstadoCitaID.SelectedItem == null)
             {
                 estado = false;
                 mensaje += "Estado de la cita\n";
             }

             //txtTelPacienteS
             if (string.IsNullOrWhiteSpace(dtFechaCitasS.Text))
             {
                 estado = false;
                 mensaje += "Fecha de la cita\n";
             }

             //dtFechaCitasS
             if (string.IsNullOrWhiteSpace(timePickerHoraCita.Text))
             {
                 estado = false;
                 mensaje += "hora de la cita\n";
             }

             //MENSAJE
             if (!estado)
             {
                 MessageBox.Show("Debe completar o cumplir estos campos:\n" + mensaje,
                 "Validación de Formulario",
                 MessageBoxButton.OK,
                 MessageBoxImage.Error);
             }
             return estado;
        
    }//FIN DE VALIDAR FORMULARIO

        //MOSTRAR CITAS
        void MostrarCitas()
        {
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (SqlCommand command = new SqlCommand("MostrarMedicosConHorarios", conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Crear un DataTable para llenar los datos leídos
                        DataTable dtMedicos = new DataTable();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtMedicos);
                        }

                        // Mostrar los datos en el DataGrid u otro control de interfaz de usuario
                        gridAgendarCitaMedico.ItemsSource = dtMedicos.DefaultView;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al obtener los médicos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }//FIN DE MOSTRAR CITA

        //METODO PARA LIMPIAR
        private void LimpiarCampos()
        {
            txtDuiPaciente.Clear();
            cmbEspecialidadID.SelectedIndex = -1;
            cmbEstadoCitaID.SelectedIndex = -1;
            cmbMedicoID.SelectedIndex = -1;
            dtFechaCitasS.SelectedDate = null;
            timePickerHoraCita.SelectedTime = null;
        }
        //FIN DEL METODO LIMPIAR

        private void CargarComboBoxes()
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);

                    // Cargar Médicos
                    SqlCommand cmdMedico = new SqlCommand("SELECT MedicoID, NombreMedico FROM Medicos WHERE EstadoID = 1", conexion);
                    SqlDataReader drMedico = cmdMedico.ExecuteReader();
                    while (drMedico.Read())
                    {
                        cmbMedicoID.Items.Add(new ComboBoxItem()
                        {
                            Content = $"{drMedico["MedicoID"]} -> {drMedico["NombreMedico"]}",
                            Tag = drMedico["MedicoID"]
                        });
                    }
                    drMedico.Close();

                    // Cargar Estado de Cita
                    SqlCommand cmdEstado = new SqlCommand("SELECT EstadoCitaID, CitaEstado FROM EstadoCita", conexion);
                    SqlDataReader drEstado = cmdEstado.ExecuteReader();
                    while (drEstado.Read())
                    {
                        cmbEstadoCitaID.Items.Add(new ComboBoxItem()
                        {
                            Content = drEstado["CitaEstado"],
                            Tag = drEstado["EstadoCitaID"]
                        });
                    }
                    drEstado.Close();

                    // Cargar Especialidades
                    SqlCommand cmdEspecialidad = new SqlCommand("SELECT EspecialidadID, NombreEspecialidad FROM Especialidades", conexion);
                    SqlDataReader drEspecialidad = cmdEspecialidad.ExecuteReader();
                    while (drEspecialidad.Read())
                    {
                        cmbEspecialidadID.Items.Add(new ComboBoxItem()
                        {
                            Content = drEspecialidad["NombreEspecialidad"],
                            Tag = drEspecialidad["EspecialidadID"]
                        });
                    }
                    drEspecialidad.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        #endregion

        #region METODOS AGREGAR, MODIFICAR
        //METODO PARA AGREGAR CITAS
        private void AgendarCita()
        {
            ValidarFormulario();

            if (MessageBox.Show("Esta seguro de que quiere agendar la cita?", "Validacion || Hospi Plus", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    if (cmbMedicoID.SelectedItem == null || cmbEstadoCitaID.SelectedItem == null || cmbEspecialidadID.SelectedItem == null)
                    {
                        MessageBox.Show("Por favor, seleccione un valor en todos los campos desplegables.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (dtFechaCitasS.SelectedDate == null || timePickerHoraCita.SelectedTime == null)
                    {
                        MessageBox.Show("Por favor, seleccione una fecha y hora válidas.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    using (SqlConnection conexion = ConexionDB.ObtenerCnx())
                    {
                        ConexionDB.AbrirConexion(conexion);
                        using (SqlCommand command = new SqlCommand("AgendarNuevaCita", conexion))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Añadir parámetros al procedimiento almacenado
                            command.Parameters.AddWithValue("@DuiPaciente", txtDuiPaciente.Text.Trim());
                            command.Parameters.AddWithValue("@MedicoID", Convert.ToInt32(((ComboBoxItem)cmbMedicoID.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@EstadoCitaID", Convert.ToInt32(((ComboBoxItem)cmbEstadoCitaID.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@FechaCita", dtFechaCitasS.SelectedDate.Value);
                            command.Parameters.AddWithValue("@HoraCita", timePickerHoraCita.SelectedTime.Value);
                            command.Parameters.AddWithValue("@EspecialidadID", Convert.ToInt32(((ComboBoxItem)cmbEspecialidadID.SelectedItem).Tag));

                            // Ejecutar el comando
                            int resultado = command.ExecuteNonQuery();

                            if (resultado > 0)
                            {
                                MessageBox.Show("Cita agendada exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                                LimpiarCampos();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo agendar la cita. Verifique los datos e intente nuevamente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al agendar la cita: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("La cita no sera agendada!", "Validacion || Hospi Plus", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

          
        }




        #endregion

        #region METODOS DEL FORMULARIO
        private void btnBuscarConsulSecre_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si se seleccionó una especialidad en el ComboBox
            if (cmbBuscarEspSecre.SelectedItem != null)
            {
                // Obtener el Tag de la especialidad seleccionada
                int especialidadID = Convert.ToInt32(((ComboBoxItem)cmbBuscarEspSecre.SelectedItem).Tag);

                // Llamar al procedimiento almacenado que filtra por especialidad
                using (SqlConnection conexion = ConexionDB.ObtenerCnx())
                {
                    try
                    {
                        ConexionDB.AbrirConexion(conexion);
                        using (SqlCommand command = new SqlCommand("MostrarMedicosPorEspecialidad", conexion))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@EspecialidadID", especialidadID);

                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Enlazar los resultados al DataGrid
                            gridAgendarCitaMedico.ItemsSource = dataTable.DefaultView;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al filtrar los datos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        ConexionDB.CerrarConexion(conexion);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una especialidad para buscar", "Advertencia || HospiPlus", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnAgendarCita_Click(object sender, RoutedEventArgs e)
        {
            if (txtDuiPaciente.Text.Length != 10)
            {
                MessageBox.Show("El DUI debe tener 10 caracteres, incluyendo '-'.", "DUI Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AgendarCita();
            LimpiarCampos();
        }



        #endregion

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


    }
}
