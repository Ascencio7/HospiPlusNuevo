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
using System.Data;
using System.Data.SqlClient;

using HospiPlus.ModeloMedico;
using HospiPlus.DataAcces;


using HospiPlus.SistemaAdministrador;
using HospiPlus.SistemaMedico;
using HospiPlus.SistemaSecretario;
using HospiPlus.ServiceMedico;
using System.Collections;
using MaterialDesignThemes.Wpf;


namespace HospiPlus.SistemaAdministrador
{
    /// <summary>
    /// Lógica de interacción para GestionMedicoAdmin.xaml
    /// </summary>
    public partial class GestionMedicoAdmin : Page
    {
        public GestionMedicoAdmin()
        {
            InitializeComponent();
            MostrarMedicos();
            cargarDepartamentosMedicos();
        }

        // Declaracion de variables
        int medicoid = 0;



        #region Mostrar los medicos
        void MostrarMedicos()
        {
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);


                    gridGestorMedicoAdmin.ItemsSource = DatosMedicos.MuestraMedico();
                    txtNombreMedico.Focus();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al mostrar los médicos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }
        #endregion




        #region Evento del Grid
        private void gridGestorMedicoAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MedicosModel medicos = (MedicosModel)gridGestorMedicoAdmin.SelectedItem;
            if (medicos == null)
            {
                return; // No hay médico seleccionado, salimos del método
            }

            // Asignación de valores a los TextBox
            txtNombreMedico.Text = medicos.NombreMedico;
            txtApellidoMedico.Text = medicos.ApellidoMedico;
            txtApellidoCasadaMedico.Text = medicos.ApellidoCasada;
            dtFechaNaciMedico.Text = medicos.FechaNacimientoMedico.ToString("yyyy-MM-dd");
            txtTelefonoMedico.Text = medicos.TelefonoMedico;
            cbDepartamentosMedicos.Text = medicos.DepartamentosMedicoID.ToString();
            cbMunicipiosMedicosAdmin.Text = medicos.MunicipioMedico.ToString();
            txtCorreoMedico.Text = medicos.CorreoMedico;
            txtDUIMedico.Text = medicos.DUIMedico;
            cmbSexoMedic.Text = medicos.SexoMedico; // Descripción del Sexo
            cmbEstadoCivilMedic.Text = medicos.EstadoCivilMedico; // Descripción del Estado Civil
            cmbEspMedic.Text = medicos.EspecialidadID; // Descripción de la Especialidad del Médico
            cmbEstadoMedic.Text = medicos.EstadoMedico;
            

            // Asignación de horas
            txtHoraInicio.Text = medicos.HoraInicio.ToString(@"hh\:mm");
            txtHoraFinal.Text = medicos.HoraFin.ToString(@"hh\:mm");

            // Asignación de días
            if (medicos.Dias != null)
            {
                cmbDiasMedic.SelectedItem = cmbDiasMedic.Items
                    .Cast<ComboBoxItem>()
                    .FirstOrDefault(item => string.Equals(item.Content.ToString(), medicos.Dias, StringComparison.OrdinalIgnoreCase));
            }
        }
        #endregion




        #region Validar Formulario

        bool ValidarFormulario()
        {
            bool estado = true;
            string mensaje = null;

            // Nombre del medico
            if (string.IsNullOrWhiteSpace(txtNombreMedico.Text))
            {
                estado = false;
                mensaje += "Nombre\n";
            }


            if (string.IsNullOrWhiteSpace(txtApellidoMedico.Text))
            {
                estado = false;
                mensaje += "Apellido\n";
            }


            if (string.IsNullOrWhiteSpace(dtFechaNaciMedico.Text))
            {
                estado = false;
                mensaje += "Fecha de nacimiento\n";
            }

            if (string.IsNullOrWhiteSpace(txtTelefonoMedico.Text))
            {
                estado = false;
                mensaje += "Teléfono\n";
            }
            

            if (string.IsNullOrWhiteSpace(txtCorreoMedico.Text))
            {
                estado = false;
                mensaje += "Correo\n";
            }

            if (string.IsNullOrWhiteSpace(txtDUIMedico.Text))
            {
                estado = false;
                mensaje += "DUI\n";
            }

            if (cmbSexoMedic.SelectedItem == null)
            {
                estado = false;
                mensaje += "Sexo\n";
            }

            if (cmbEstadoCivilMedic.SelectedItem == null)
            {
                estado = false;
                mensaje += "Estado Civil\n";
            }

            if (cmbDiasMedic.SelectedItem == null)
            {
                estado = false;
                mensaje += "Días\n";
            }

            if (string.IsNullOrWhiteSpace(txtHoraInicio.Text))
            {
                estado = false;
                mensaje += "Hora Inicio\n";
            }

            if (string.IsNullOrWhiteSpace(txtHoraFinal.Text))
            {
                estado = false;
                mensaje += "Hora Fin\n";
            }

            if (cmbEspMedic.SelectedItem == null)
            {
                estado = false;
                mensaje += "Especialidad Médico\n";
            }

            if (cmbEstadoMedic.SelectedItem == null)
            {
                estado = false;
                mensaje += "Estado\n";
            }

            if (!estado)
            {
                MessageBox.Show("Debe completar o cumplir estos campos:\n" + mensaje,
                                "Validación de Formulario",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            return estado;
        }
        #endregion




        #region Boton para Agregar Medicos
        private void btnAgregarMedicoAdmi_Click(object sender, RoutedEventArgs e)
        {
            ValidarFormulario();
            // Condicion para validar que el DUI tenga 10 caracteres
            if (txtDUIMedico.Text.Length != 10)
            {
                MessageBox.Show("El DUI debe tener 10 caracteres, incluyendo '-'.",
                    "DUI Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            agregarMedico();
        }
        #endregion



        #region Método para Agregar Medicos
        // Método para agregar médicos

        private void agregarMedico()
        {
            ValidarFormulario();

            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    using (SqlCommand cmd = new SqlCommand("InsertarMedico", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NombreMedico", txtNombreMedico.Text);
                        cmd.Parameters.AddWithValue("@ApellidoMedico", txtApellidoMedico.Text);
                        cmd.Parameters.AddWithValue("@ApellidoMedicoCasada", string.IsNullOrEmpty(txtApellidoCasadaMedico.Text) ? (object)DBNull.Value : txtApellidoCasadaMedico.Text);
                        cmd.Parameters.AddWithValue("@FechaNacimientoMedico", DateTime.Parse(dtFechaNaciMedico.Text));
                        cmd.Parameters.AddWithValue("@TelefonoMedico", txtTelefonoMedico.Text);
                        cmd.Parameters.AddWithValue("@DepartamentosID", ((ComboBoxItem)cbDepartamentosMedicos.SelectedItem).Tag);
                        cmd.Parameters.AddWithValue("@MunicipioID", ((ComboBoxItem)cbMunicipiosMedicosAdmin.SelectedItem).Tag);
                        cmd.Parameters.AddWithValue("@CorreoMedico", txtCorreoMedico.Text);
                        cmd.Parameters.AddWithValue("@DUIMedico", txtDUIMedico.Text);
                        cmd.Parameters.AddWithValue("@SexoID", ((ComboBoxItem)cmbSexoMedic.SelectedItem).Tag);
                        cmd.Parameters.AddWithValue("@EstadoCivilID", ((ComboBoxItem)cmbEstadoCivilMedic.SelectedItem).Tag);

                        // Enviar solo un día seleccionado como @DiaID
                        cmd.Parameters.AddWithValue("@DiaID", ((ComboBoxItem)cmbDiasMedic.SelectedItem).Tag);

                        // Convertir horas a TimeSpan
                        cmd.Parameters.AddWithValue("@HoraInicio", TimeSpan.Parse(txtHoraInicio.Text));
                        cmd.Parameters.AddWithValue("@HoraFin", TimeSpan.Parse(txtHoraFinal.Text));

                        cmd.Parameters.AddWithValue("@EspecialidadID", ((ComboBoxItem)cmbEspMedic.SelectedItem).Tag);
                        cmd.Parameters.AddWithValue("@EstadoID", ((ComboBoxItem)cmbEstadoMedic.SelectedItem).Tag);

                        var result = cmd.ExecuteScalar();
                        MessageBox.Show("Médico insertado exitosamente con ID: " + result, "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                        MostrarMedicos();
                        LimpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al insertar el médico: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
            MostrarMedicos();
            LimpiarCampos();
        } // Fin del método de agregar médicos
        #endregion



        #region Botón para Editar Médicos
        private void btnModificarMedicoAdmi_Click(object sender, RoutedEventArgs e)
        {
            ValidarFormulario();

            // Validación de DUI
            if (txtDUIMedico.Text.Length != 10)
            {
                MessageBox.Show("El DUI debe tener 10 caracteres, incluyendo '-'.", "DUI Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validar que un médico esté seleccionado
            if (gridGestorMedicoAdmin.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un médico de la lista.", "Médico no seleccionado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Obtener el médico seleccionado
            MedicosModel selectedMedico = (MedicosModel)gridGestorMedicoAdmin.SelectedItem;

            // Crear la instancia de MedicosModel con los datos del formulario y el ID del médico seleccionado
            MedicosModel medico = new MedicosModel
            {
                MedicoID = selectedMedico.MedicoID,
                NombreMedico = txtNombreMedico.Text,
                ApellidoMedico = txtApellidoMedico.Text,
                ApellidoCasada = txtApellidoCasadaMedico.Text,
                FechaNacimientoMedico = DateTime.Parse(dtFechaNaciMedico.Text),
                TelefonoMedico = txtTelefonoMedico.Text,
                CorreoMedico = txtCorreoMedico.Text,
                DUIMedico = txtDUIMedico.Text,
                SexoMedico = ObtenerTagDeComboBox(cmbSexoMedic),
                EstadoCivilMedico = ObtenerTagDeComboBox(cmbEstadoCivilMedic),

                // Obtener solo el día seleccionado de cmbDiasMedic
                Dias = ObtenerDiaSeleccionado(), // Método que obtiene el día seleccionado

                HoraInicio = TimeSpan.Parse(txtHoraInicio.Text),
                HoraFin = TimeSpan.Parse(txtHoraFinal.Text),
                EspecialidadID = ObtenerTagDeComboBox(cmbEspMedic),
                EstadoMedico = ObtenerTagDeComboBox(cmbEstadoMedic)
            };

            // Llamar al método para editar el médico
            editarMedico(medico);
            MostrarMedicos();
            LimpiarCampos();
        }
        #endregion




        #region Método para obtener el día seleccionado
        private string ObtenerDiaSeleccionado()
        {
            // Verificar si se ha seleccionado un día
            if (cmbDiasMedic.SelectedItem != null)
            {
                // Acceder al ComboBoxItem seleccionado
                ComboBoxItem selectedItem = cmbDiasMedic.SelectedItem as ComboBoxItem;

                if (selectedItem != null)
                {
                    // Obtener el valor del Tag que representa el día seleccionado
                    return selectedItem.Tag.ToString();
                }
                else
                {
                    MessageBox.Show("No se pudo obtener el Tag del día seleccionado.");
                    return string.Empty;
                }
            }

            // Si no se selecciona un día, retornar un valor predeterminado o manejar el error
            MessageBox.Show("Por favor, selecciona un día.");
            return string.Empty; // o el valor que prefieras
        }
        #endregion



        #region Método para obtener el Tag del ComboBox
        private string ObtenerTagDeComboBox(ComboBox comboBox)
        {
            // Verificar si hay un item seleccionado
            if (comboBox.SelectedItem != null)
            {
                // Obtener el ComboBoxItem seleccionado y retornar el Tag
                ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;
                return selectedItem?.Tag.ToString();
            }

            // Si no se selecciona un item, retornar un valor predeterminado o manejar el error
            return string.Empty;
        }
        #endregion



        #region Método para Editar al Médico
        public void editarMedico(MedicosModel medico)
        {
            ValidarFormulario();

            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    using (SqlCommand cmd = new SqlCommand("EditarMedico", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros
                        cmd.Parameters.AddWithValue("@MedicoID", medico.MedicoID);
                        cmd.Parameters.AddWithValue("@NombreMedico", medico.NombreMedico);
                        cmd.Parameters.AddWithValue("@ApellidoMedico", medico.ApellidoMedico);
                        cmd.Parameters.AddWithValue("@ApellidoMedicoCasada", string.IsNullOrEmpty(medico.ApellidoCasada) ? (object)DBNull.Value : medico.ApellidoCasada);
                        cmd.Parameters.AddWithValue("@FechaNacimientoMedico", medico.FechaNacimientoMedico);
                        cmd.Parameters.AddWithValue("@TelefonoMedico", medico.TelefonoMedico);
                        cmd.Parameters.AddWithValue("@DepartamentosID", Convert.ToInt32(((ComboBoxItem)cbDepartamentosMedicos.SelectedItem).Tag));
                        cmd.Parameters.AddWithValue("@MunicipioID", Convert.ToInt32(((ComboBoxItem)cbMunicipiosMedicosAdmin.SelectedItem).Tag)); // ID del municipio
                        cmd.Parameters.AddWithValue("@CorreoMedico", medico.CorreoMedico);
                        cmd.Parameters.AddWithValue("@DUIMedico", medico.DUIMedico);
                        cmd.Parameters.AddWithValue("@SexoID", Convert.ToInt32(medico.SexoMedico));
                        cmd.Parameters.AddWithValue("@EstadoCivilID", Convert.ToInt32(medico.EstadoCivilMedico));
                        cmd.Parameters.AddWithValue("@HoraInicio", medico.HoraInicio);
                        cmd.Parameters.AddWithValue("@HoraFin", medico.HoraFin);

                        // Usar un solo Día, no una lista
                        cmd.Parameters.AddWithValue("@DiaID", Convert.ToInt32(medico.Dias)); // Parámetro de Día

                        cmd.Parameters.AddWithValue("@EspecialidadID", Convert.ToInt32(medico.EspecialidadID));
                        cmd.Parameters.AddWithValue("@EstadoID", Convert.ToInt32(medico.EstadoMedico));

                        var result = cmd.ExecuteScalar(); // Ejecutar el procedimiento y obtener el resultado
                        MessageBox.Show("Médico editado exitosamente con ID: " + result, "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        MostrarMedicos();
                        LimpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al editar el médico: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    MostrarMedicos();
                    LimpiarCampos();
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }
        #endregion





        #region Botón Eliminar Médicos
        private void btnEliminarMedicoAdmi_Click(object sender, RoutedEventArgs e)
        {
            if(gridGestorMedicoAdmin.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un médico de la lista.", "Médico no seleccionado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MedicosModel medicoEliminar =  (MedicosModel)gridGestorMedicoAdmin.SelectedItem;


            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar al médico seleccionado?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
            {
                eliminarMedico(medicoEliminar.MedicoID);
            }
        }
        #endregion




        #region Método para Eliminar Médicos
        public void eliminarMedico(int medicoID)
        {
            using(SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (SqlCommand cmd = new SqlCommand("EliminarMedico", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MedicoID", medicoID);

                        var columnaAfectada = cmd.ExecuteNonQuery();
                        if(columnaAfectada > 0)
                        {
                            MessageBox.Show("Médico eliminado correctamente", "Eliminar",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("El médico con el ID especificado no existe",
                                "Error de Eliminación", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        MostrarMedicos();
                        LimpiarCampos();
                    }
                }catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al eliminar el médico: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    MostrarMedicos();
                    LimpiarCampos();
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }
        #endregion



        #region Método para la hora
        // Método para obtener TimeSpan en el formato adecuado
        private TimeSpan ObtenerTimeSpan(string hora)
        {
            // Se reemplaza el punto por dos puntos para que sea un formato de hora valido
            hora = hora.Replace(".", ":"); // La hora con minutos se debe ingresar con :
            if(TimeSpan.TryParse(hora, out TimeSpan resultado))
            {
                return resultado;
            }
            else
            {
                // Manejo de eror si el formato de hora no es correcta
                throw new FormatException("El formato de hora no es válido. Asegúrate de que esté en el formato HH:mm.");
            }
        }
        #endregion



        #region Boton de Cancelar
        // Botón para cancelar el ingreso de los datos del Médico
        private void btnMedicoAdmi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar el proceso?", "Cancelar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LimpiarCampos(); // Limpiar los campos 
                MostrarMedicos(); // Y actualiza el grid de los Médicos
                return;
            }

        }
        #endregion




        #region Método para limpiar campos
        void LimpiarCampos()
            {
                txtNombreMedico.Text = string.Empty;
                txtApellidoMedico.Text = string.Empty;
                txtApellidoCasadaMedico.Text = string.Empty;
                dtFechaNaciMedico.Text = string.Empty;
                txtTelefonoMedico.Text = string.Empty;
                cbDepartamentosMedicos.SelectedIndex = -1; // Vuelve a 0 el indice
                cbMunicipiosMedicosAdmin.SelectedIndex = -1;
                txtCorreoMedico.Text = string.Empty;
                txtDUIMedico.Text = string.Empty;
                cmbSexoMedic.SelectedIndex = -1; //Vuelve a 0 el indice
                cmbEstadoCivilMedic.SelectedIndex = -1; // Vuelve a 0 el indice
                cmbDiasMedic.SelectedIndex = -1; // O el índice que desees por defecto
                txtHoraInicio.Text = string.Empty;
                txtHoraFinal.Text = string.Empty;
                cmbEspMedic.SelectedIndex = -1; // Vuelve a 0 el indice
                cmbEstadoMedic.SelectedIndex = -1; // Vuelve a 0 el indice
        }
        #endregion



        #region EVENTO ENTER
        // Inicio Evento ENTER: para ahorrar tiempo en dar click al botón de ingresar los datos
        private void txtNombreMedico_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                agregarMedico();
            }
        }

        private void txtApellidoMedico_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                agregarMedico();
            }
        }

        private void cmbEstadoCivilMedic_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                agregarMedico();
            }
        }

        private void dtFechaNaciMedico_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                agregarMedico();
            }
        }

        private void cmbSexoMedic_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                agregarMedico();
            }
        }

        private void cmbEspMedic_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                agregarMedico();
            }
        }

        private void txtCorreoMedico_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                agregarMedico();
            }
        }

        private void txtTelefonoMedico_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                agregarMedico();
            }
        }

        private void txtDUIMedico_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                agregarMedico();
            }
        }

        private void cmbDiasMedic_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                agregarMedico();
            }
        }

        private void txtHoraInicio_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                agregarMedico();
            }
        }

        private void txtHoraFinal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                agregarMedico();
            }
        }

        private void cmbEstadoMedic_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                agregarMedico();
            }
        }

        // Fin del evento ENTER
        #endregion


        
        #region Validacion de 10 para DUI
        // Validar que el DUI tenga la longitud de 10 caracteres
        private void txtDUIMedico_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtDUIMedico.Text.Length > 10)
            {
                txtDUIMedico.Text = txtDUIMedico.Text.Substring(0, 10);
                txtDUIMedico.CaretIndex = txtDUIMedico.Text.Length;
                MessageBox.Show("El DUI debe tener 10 caracteres, incluyendo '-'.",
                    "DUI Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        #endregion



        #region Método llenar Departamentos
        // Metodo para llenar el combo de departamentos
        public void cargarDepartamentosMedicos()
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

                        cbDepartamentosMedicos.Items.Clear();
                        while (reader.Read())
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = reader["NombreDepartamento"].ToString();
                            item.Tag = reader["DepartamentosID"];
                            cbDepartamentosMedicos.Items.Add(item);
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

        }// fin del metodo
        #endregion



        #region Método llenar los Municipios
        // Metodo para llenar el combo de los municipios
        public void cargarMunicipios(int departamentoId)
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

                        cbMunicipiosMedicosAdmin.Items.Clear();
                        while (reader.Read())
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = reader["NombreMunicipio"].ToString();
                            item.Tag = reader["MunicipioID"];
                            cbMunicipiosMedicosAdmin.Items.Add(item);
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
        }// fin del método
        #endregion





        #region Selecciona Depar y carga Muni
        private void cbDepartamentosMedicos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbDepartamentosMedicos.SelectedItem is ComboBoxItem selectdItem)
            {
                int departamentoId = (int)selectdItem.Tag;
                cargarMunicipios(departamentoId);
            }
        }



        #endregion

        #region INHABILITAR CAMPO (APELLIDO DE CASADA)
        private void cmbEstadoCivilMedic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbEstadoCivilMedic.SelectedItem != null)
            {
                // Verificar si la opción seleccionada es "Casado"
                string seleccion = cmbEstadoCivilMedic.SelectedItem.ToString();

                if (seleccion.Contains("Casado"))
                {
                    txtApellidoCasadaMedico.IsEnabled = true; // Habilitar el TextBox
                }
                else if (seleccion.Contains("Soltero")) 
                {
                    txtApellidoCasadaMedico.IsEnabled = false; // Deshabilitar el TextBox
                    txtApellidoCasadaMedico.Text = string.Empty; // Limpiar el contenido si se desactiva
                }
                else if (seleccion.Contains("Divorciado")) 
                {
                    txtApellidoCasadaMedico.IsEnabled = false; // Deshabilitar el TextBox
                    txtApellidoCasadaMedico.Text = string.Empty; // Limpiar el contenido si se desactiva
                }
                if (seleccion.Contains("Viudo"))
                {
                    txtApellidoCasadaMedico.IsEnabled = true; // Habilitar el TextBox
                }
            }
        }
        #endregion


    }
}