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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

using HospiPlus.ModeloPaciente;
using HospiPlus.DataAcces;


using HospiPlus.SistemaAdministrador;
using HospiPlus.SistemaMedico;
using HospiPlus.SistemaSecretario;
using HospiPlus.ServicePaciente;

using System.Collections;
using MaterialDesignThemes.Wpf;

namespace HospiPlus.SistemaSecretario
{
    /// <summary>
    /// Lógica de interacción para GestionPacienteSecretario.xaml
    /// </summary>
    public partial class GestionPacienteSecretario : Page
    {
        public GestionPacienteSecretario()
        {
            InitializeComponent();
            MostrarPaciente();
            CargarDepartamentos();
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

            //txtNamePacienteS
            if (string.IsNullOrWhiteSpace(txtNamePacienteS.Text))
            {
                estado = false;
                mensaje += "Nombres\n";
            }

            //txtApellidoPacienteS
            if (string.IsNullOrWhiteSpace(txtApellidoPacienteS.Text))
            {
                estado = false;
                mensaje += "Apellidos\n";
            }


            //cmbEstadoPacienteS
            if (cmbEstadoCivilPacienteS.SelectedItem == null)
            {
                estado = false;
                mensaje += "Estado civil\n";
            }

            //cmbGeneroPacienteS
            if (cmbGeneroPacienteS.SelectedItem == null)
            {
                estado = false;
                mensaje += "Genero\n";
            }

            //dpFNPacienteS
            if (string.IsNullOrWhiteSpace(dpFNPacienteS.Text))
            {
                estado = false;
                mensaje += "Fecha de nacimiento\n";
            }

            //txtDUIPacienteS
            if (string.IsNullOrWhiteSpace(txtDUIPacienteS.Text))
            {
                estado = false;
                mensaje += "DUI\n";
            }

            //txtTelPacienteS
            if (string.IsNullOrWhiteSpace(txtTelPacienteS.Text))
            {
                estado = false;
                mensaje += "Telefono\n";
            }

            //txtCorreoPacienteS
            if (string.IsNullOrWhiteSpace(txtCorreoPacienteS.Text))
            {
                estado = false;
                mensaje += "Correo\n";
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

        }

        //METODO PARA MOSTRAR PACIENTE
        void MostrarPaciente()
        {
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    gridGestorPacienteSecretaria.ItemsSource = DatosPaciente.MuestraPacientes();
                    txtNamePacienteS.Focus();

                } catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al mostrar los pacientes: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }

        //METODO PARA LIMPIAR CAMPOS
        void LimpiarCampos()
        {
            txtNamePacienteS.Text = string.Empty;
            txtApellidoPacienteS.Text = string.Empty;
            txtApellidoCasadaPacienteS.Text = string.Empty;
            cmbEstadoCivilPacienteS.SelectedIndex = -1;
            cmbGeneroPacienteS.SelectedIndex = -1;
            cmbEstadoPciente.SelectedIndex = -1;
            cbDepartamentoPacientes.SelectedIndex = -1;
            cbMunicipioPacientes.SelectedIndex = -1;
            dpFNPacienteS.Text = string.Empty;
            txtDUIPacienteS.Text = string.Empty;
            txtTelPacienteS.Text = string.Empty;
            txtCorreoPacienteS.Text = string.Empty;
            txtBuscarPacienteS.Text = string.Empty;

        }

        //METODO PARA BUSCAR POR DUI
        void buscarPacienteDUI()
        {
            string dui = txtBuscarPacienteS.Text;
            if (string.IsNullOrWhiteSpace(dui))
            {
                MessageBox.Show("Por favor, ingresa un número de DUI válido.", "Error ||Hospi Plus", MessageBoxButton.OK, MessageBoxImage.Warning);
                MostrarPaciente();
                return;
            }

            if (dui.Length != 10)
            {
                MessageBox.Show("El DUI debe contener exactamente 10 dígitos incluyendo '-'.", "Error ||Hospi Plus", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtBuscarPacienteS.Clear();
                return;
            }

            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    //SE LLAMA AL METODO QUE EJECUTA EL PROCEDIMIENTO ALMACENADO
                    var pacientesEncontrados = DatosPaciente.BuscarPacientePorDUI(dui);

                    if (pacientesEncontrados != null && pacientesEncontrados.Count > 0)
                    {
                        gridGestorPacienteSecretaria.ItemsSource = pacientesEncontrados;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún paciente con ese DUI.", "Informacion ||HospiPlus", MessageBoxButton.OK, MessageBoxImage.Information);
                        txtBuscarPacienteS.Clear();
                        MostrarPaciente();
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar el paciente: " + ex.Message, "Error de Conexión ||HospiPlus", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtBuscarPacienteS.Clear(); // O puedes usar txtBuscarPacientesAdmi.Text = "";
                    MostrarPaciente();
                    return;
                }
            }
        }

        //METODOS PARA LLENAR COMBOS DE DEPARTAMENTOS
        public void CargarDepartamentos()
        {
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (SqlCommand command = new SqlCommand("ObtenerDepartamentos", conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();

                        cbDepartamentoPacientes.Items.Clear();
                        while (reader.Read())
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = reader["NombreDepartamento"].ToString();
                            item.Tag = reader["DepartamentosID"];
                            cbDepartamentoPacientes.Items.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar departamentos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }

        //METODOS PARA LLENAR COMBOS DE MUNICIPIOS
        public void CargarMunicipios(int departamentoId)
        {
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (SqlCommand command = new SqlCommand("ObtenerMunicipiosPorDepartamento", conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DepartamentosID", departamentoId);
                        SqlDataReader reader = command.ExecuteReader();

                        cbMunicipioPacientes.Items.Clear();
                        while (reader.Read())
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = reader["NombreMunicipio"].ToString();
                            item.Tag = reader["MunicipioID"];
                            cbMunicipioPacientes.Items.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar municipios: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }

        #endregion
        //Fin de la region de METODOS PERSONALIZADOS

        //METODOS AGREGAR Y EDITAR
        #region METODOS AGREGAR Y EDITAR PACIENTE

        //METODO PARA AGREGAR PACIENTE
        private void AgregarPaciente()
        {
            ValidarFormulario();
            if (MessageBox.Show("Esta seguro de que quiere insertar los datos?", "Validacion || Hospi Plus", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (SqlConnection conexion = ConexionDB.ObtenerCnx())
                {
                    try
                    {
                        ConexionDB.AbrirConexion(conexion);
                        using (SqlCommand command = new SqlCommand("InsertarPaciente", conexion))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@NombrePaciente", txtNamePacienteS.Text);
                            command.Parameters.AddWithValue("@ApellidoPaciente", txtApellidoPacienteS.Text);
                            command.Parameters.AddWithValue("@ApellidoDeCasada", txtApellidoCasadaPacienteS.Text);
                            command.Parameters.AddWithValue("@FechaNacimiento", DateTime.Parse(dpFNPacienteS.Text));
                            command.Parameters.AddWithValue("SexoID", Convert.ToInt32(((ComboBoxItem)cmbGeneroPacienteS.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@EstadoCivilID", Convert.ToInt32(((ComboBoxItem)cmbEstadoCivilPacienteS.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@DUIPaciente", txtDUIPacienteS.Text);
                            command.Parameters.AddWithValue("@TelefonoPaciente", txtTelPacienteS.Text);
                            command.Parameters.AddWithValue("@CorreoPaciente", txtCorreoPacienteS.Text);
                            command.Parameters.AddWithValue("@DepartamentosID", Convert.ToInt32(((ComboBoxItem)cbDepartamentoPacientes.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@MunicipioID", Convert.ToInt32(((ComboBoxItem)cbMunicipioPacientes.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@EstadoID", Convert.ToInt32(((ComboBoxItem)cmbEstadoPciente.SelectedItem).Tag));

                            var result = command.ExecuteScalar();
                            MessageBox.Show("Paciente con ID: " + result + " insertado correctamente", "Exito || HospiPlus", MessageBoxButton.OK, MessageBoxImage.Information);
                            MostrarPaciente();
                            LimpiarCampos();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al insertar el paciente: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        ConexionDB.CerrarConexion(conexion);
                    }
                }
            }
        }

        //METODO PARA ACTUALIZAR PACIENTE
        private void editarPaciente(PacientesModel pacientes)
        {
            ValidarFormulario();
            if (MessageBox.Show("Esta seguro de quere modificar los datos?", "Validacion || Hospi Plus", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (SqlConnection conexion = ConexionDB.ObtenerCnx())
                {
                    try
                    {
                        ConexionDB.AbrirConexion(conexion);

                        using (SqlCommand command = new SqlCommand("EditarPaciente", conexion))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@PacienteID", pacientes.PacienteID);
                            command.Parameters.AddWithValue("@NombrePaciente", pacientes.NombrePaciente);
                            command.Parameters.AddWithValue("@ApellidoPaciente", pacientes.ApellidoPaciente);
                            command.Parameters.AddWithValue("@ApellidoDeCasada", string.IsNullOrEmpty(pacientes.ApellidoDeCasada) ? (object)DBNull.Value : pacientes.ApellidoDeCasada);
                            DateTime fechaNacimiento;
                            if (!DateTime.TryParse(dpFNPacienteS.Text, out fechaNacimiento) || fechaNacimiento < new DateTime(1753, 1, 1))
                            {
                                MessageBox.Show("La fecha de nacimiento no es válida o está fuera del rango permitido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return; // Salir si la fecha es inválida
                            }
                            command.Parameters.AddWithValue("@FechaNacimientoPaciente", fechaNacimiento);

                            //command.Parameters.AddWithValue("@FechaNacimiento", pacientes.FechaNacimientoPaciente);
                            command.Parameters.AddWithValue("SexoID", Convert.ToInt32(((ComboBoxItem)cmbGeneroPacienteS.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@EstadoCivilID", Convert.ToInt32(((ComboBoxItem)cmbEstadoCivilPacienteS.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@DUIPaciente", pacientes.DUIPaciente);
                            command.Parameters.AddWithValue("@TelefonoPaciente", pacientes.TelefonoPaciente);
                            command.Parameters.AddWithValue("@CorreoPaciente", pacientes.CorreoPaciente);
                            command.Parameters.AddWithValue("@DepartamentosID", Convert.ToInt32(((ComboBoxItem)cbDepartamentoPacientes.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@MunicipioID", Convert.ToInt32(((ComboBoxItem)cbMunicipioPacientes.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@EstadoID", Convert.ToInt32(((ComboBoxItem)cmbEstadoPciente.SelectedItem).Tag));

                            var result = command.ExecuteScalar();
                            MessageBox.Show("Paciente " + result + " editado exitosamente", "Exito || HospiPlus", MessageBoxButton.OK, MessageBoxImage.Information);
                            MostrarPaciente();
                            LimpiarCampos();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al editar el paciente: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        MostrarPaciente();
                        LimpiarCampos();
                    }
                    finally
                    {
                        ConexionDB.CerrarConexion(conexion);
                    }
                }
            }

        }
        #endregion
        //Fin de la region de METODOS AGREGAR Y EDITAR

        //METODOS FORMULARIOS
        #region METODOS DEL FORMULARIO

        //btnAgregarPacienteS
        private void btnAgregarPacienteS_Click(object sender, RoutedEventArgs e)
        {
            ValidarFormulario();

            //CONDICION PARA LA VALIDACION DEL DUI
            if (txtDUIPacienteS.Text.Length != 10)
            {
                MessageBox.Show("El DUI debe tener 10 caracteres, incluyendo '-'.", "DUI Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AgregarPaciente();
            MostrarPaciente();
            LimpiarCampos();

        }


        //btnModificarPacienteS
        private void btnModificarPacienteS_Click(object sender, RoutedEventArgs e)
        {
            ValidarFormulario();
            // Condicion para validar que el DUI tenga 10 caracteres
            if (txtDUIPacienteS.Text.Length != 10)
            {
                MessageBox.Show("El DUI debe tener 10 caracteres, incluyendo '-'.",
                    "DUI Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (gridGestorPacienteSecretaria.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un paciente de la lista.", "Paciente no seleccionado ||HospiPlus", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //OBTENIENDO EL PACIENTE SELECCIONADO
            PacientesModel pacienteSeleccionado = (PacientesModel)gridGestorPacienteSecretaria.SelectedItem;

            //CREANDO UNA INSTANCIA DE PACIENTE MODEL 
            PacientesModel paciente = new PacientesModel
            {
                PacienteID = pacienteSeleccionado.PacienteID,
                NombrePaciente = txtNamePacienteS.Text,
                ApellidoPaciente = txtApellidoPacienteS.Text,
                ApellidoDeCasada = txtApellidoCasadaPacienteS.Text,
                EstadoCivilPaciente = ((ComboBoxItem)cmbEstadoCivilPacienteS.SelectedItem).Tag.ToString(),
                SexoPaciente = ((ComboBoxItem)cmbGeneroPacienteS.SelectedItem).Tag.ToString(),
                DUIPaciente = txtDUIPacienteS.Text,
                TelefonoPaciente = txtTelPacienteS.Text,
                CorreoPaciente = txtCorreoPacienteS.Text,
                //DepartamentosPaciente = ((ComboBoxItem)),
                //MunicipioPaciente = ((ComboBoxItem))
            };

            //LLAMANDO AL METDO PARA ACTUALIZAR
            editarPaciente(paciente);
            MostrarPaciente();
            LimpiarCampos();
        }


        //CancelarPacienteS
        private void btnCancelarPacienteS_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar el proceso?", "Cancelar ||HosiPlus", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LimpiarCampos(); // Limpiar los campos 
                MostrarPaciente(); // Y actualiza el grid de los Médicos
                return;
            }
        }

        //btnBuscarPacienteS
        private void btnBuscarPacienteS_Click(object sender, RoutedEventArgs e)
        {
            buscarPacienteDUI();
        }

        private void cbMunicipioPacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbDepartamentoPacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDepartamentoPacientes.SelectedItem is ComboBoxItem selectedItem)
            {
                int departamentoId = (int)selectedItem.Tag;
                CargarMunicipios(departamentoId);
            }
        }

        private void gridGestorPacienteSecretaria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PacientesModel paciente = (PacientesModel)gridGestorPacienteSecretaria.SelectedItem;
            if (paciente == null)
            {
                return;
            }

            pacienteid = paciente.PacienteID;
            txtNamePacienteS.Text = paciente.NombrePaciente;
            txtApellidoPacienteS.Text = paciente.ApellidoPaciente;
            txtApellidoCasadaPacienteS.Text = paciente.ApellidoDeCasada;
            cmbEstadoCivilPacienteS.Text = paciente.EstadoCivilPaciente;
            cmbGeneroPacienteS.Text = paciente.SexoPaciente;
            dpFNPacienteS.Text = paciente.FechaNacimientoPaciente.ToString("yyyy-MM-dd");
            txtDUIPacienteS.Text = paciente.DUIPaciente;
            txtTelPacienteS.Text = paciente.TelefonoPaciente;
            txtCorreoPacienteS.Text = paciente.CorreoPaciente;
            cbDepartamentoPacientes.Text = paciente.DepartamentosPaciente;
            cbMunicipioPacientes.Text = paciente.MunicipioPaciente;
            //DIRECCION
            cmbEstadoPciente.Text = paciente.EstadoPaciente;
        }

        private void txtBuscarPacienteS_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                buscarPacienteDUI();
            }
        }

        private void txtNamePacienteS_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidarFormulario();
            }
        }

        private void txtApellidoPacienteS_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidarFormulario();
            }
        }

        private void txtApellidoCasadaPacienteS_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidarFormulario();
            }
        }

        private void cmbEstadoCivilPacienteS_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidarFormulario();
            }
        }

        private void cmbGeneroPacienteS_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidarFormulario();
            }
        }

        private void dpFNPacienteS_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidarFormulario();
            }
        }

        private void txtDUIPacienteS_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidarFormulario();
            }
        }

        private void txtTelPacienteS_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidarFormulario();
            }
        }

        private void txtCorreoPacienteS_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidarFormulario();
            }
        }

        private void cbDepartamentoPacientes_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidarFormulario();
            }
        }

        private void cbMunicipioPacientes_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidarFormulario();
            }
        }

        private void cmbEstadoPciente_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidarFormulario();
            }
        }

        private void cmbEstadoCivilPacienteS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verificar si la opción seleccionada es "Casado"
            string seleccion = cmbEstadoCivilPacienteS.SelectedItem.ToString();

            if (seleccion.Contains("Casado"))
            {
                txtApellidoCasadaPacienteS.IsEnabled = true; // Habilitar el TextBox
            }
            else if (seleccion.Contains("Soltero"))
            {
                txtApellidoCasadaPacienteS.IsEnabled = false; // Deshabilitar el TextBox
                txtApellidoCasadaPacienteS.Text = string.Empty; // Limpiar el contenido si se desactiva
            }
            else if (seleccion.Contains("Divorciado"))
            {
                txtApellidoCasadaPacienteS.IsEnabled = false; // Deshabilitar el TextBox
                txtApellidoCasadaPacienteS.Text = string.Empty; // Limpiar el contenido si se desactiva
            }
            if (seleccion.Contains("Viudo"))
            {
                txtApellidoCasadaPacienteS.IsEnabled = true; // Habilitar el TextBox
            }

        }
        #endregion
        //Fin de la region de METODOS FORMULARIOS

    }
}
