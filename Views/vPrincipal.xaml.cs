namespace _008_jdelgadoDS3.Views;

public partial class vPrincipal : ContentPage
{
    public vPrincipal()
    {
        InitializeComponent();
        CargarEstudiantes();
    }
    private List<Estudiante> estudiantes = new();

    private void CargarEstudiantes()
    {
        estudiantes = new List<Estudiante>
        {
            new Estudiante { Nombre = "Jimmy Ricardo", Apellido = "Delgado Muñoz", Cedula = "0102030405" },
            new Estudiante { Nombre = "Ana Sofía", Apellido = "Mendoza López", Cedula = "1102233445" },
            new Estudiante { Nombre = "Carlos", Apellido = "Pérez Sánchez", Cedula = "1203344556" },
            new Estudiante { Nombre = "Luisa Fernanda", Apellido = "Ramírez Torres", Cedula = "0987654321" },
            new Estudiante { Nombre = "Marco", Apellido = "Gómez Herrera", Cedula = "1029384756" }
        };
        pckEstudiantes.ItemsSource = estudiantes;
        pckEstudiantes.ItemDisplayBinding = new Binding("Descripcion");
    }

    public class Estudiante
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Descripcion => $"{Apellido} {Nombre} - {Cedula}";
    }

    public void Limpiar()
    {
        pckEstudiantes.SelectedItem = null;
        txtNotaSeg1.Text = string.Empty;
        txtNotaExa1.Text = string.Empty;
        txtNotaSeg2.Text = string.Empty;
        txtNotaExa2.Text = string.Empty;
        dtpckDate.Date = DateTime.Today;
        lblInfo.Text = string.Empty;
        frameResultado.BorderColor = Colors.Gray;

    }
    private void estadoF(string estadoE)
    {
        Color colorEstado;

        switch (estadoE)
        {
            case "Aprobado":
                colorEstado = Colors.Green;
                break;
            case "Complementario":
                colorEstado = Colors.Orange;
                break;
            case "Reprobado":
                colorEstado = Colors.Red;
                break;
            default:
                colorEstado = Colors.Gray;
                break;
        }
        frameResultado.BorderColor = colorEstado;
    }

    private async void btnVerificar_Clicked(object sender, EventArgs e)
    {
        try
        {
            string nota1 = txtNotaSeg1.Text?.Trim();
            string notaExa1 = txtNotaExa1.Text?.Trim();
            string nota2 = txtNotaSeg2.Text?.Trim();
            string notaExa2 = txtNotaExa2.Text?.Trim();

            if (string.IsNullOrEmpty(nota1) || string.IsNullOrEmpty(notaExa1) ||
                string.IsNullOrEmpty(nota2) || string.IsNullOrEmpty(notaExa2))
            {
                await DisplayAlert("ERROR", "Por favor, complete todos los campos.", "Aceptar");
                return;
            }

            if (!int.TryParse(nota1, out int nota1n) || !int.TryParse(notaExa1, out int notaExa1n) ||
               !int.TryParse(nota2, out int nota2n) || !int.TryParse(notaExa2, out int notaExa2n))
            {
                await DisplayAlert("ERROR", "Las notas deben contener solo números.", "Aceptar");
                return;
            }

            if (nota1n < 0 || nota1n > 10 || notaExa1n < 0 || notaExa1n > 10 ||
                nota2n < 0 || nota2n > 10 || notaExa2n < 0 || notaExa2n > 10)
            {
                await DisplayAlert("ERROR", "Las notas no deben ser menores de 0 ni mayor a 10", "Aceptar");
                return;
            }

            double nota1c = nota1n * 0.3;
            double notaExa1c = notaExa1n * 0.2;
            double notaParcial1 = nota1c + notaExa1c;
            double nota2c = nota2n * 0.3;
            double notaExa2c = notaExa2n * 0.2;
            double notaParcial2 = nota2c + notaExa2c;
            double notaFinal = notaParcial1 + notaParcial2;

            string estado = "";
            if (notaFinal >= 7)
            {
                estado = "Aprobado";
            }
            if (notaFinal >= 5 && notaFinal <= 6.9)
            {
                estado = "Complementario";
            }
            if (notaFinal < 5)
            {
                estado = "Reprobado";
            }

            var estudianteSeleccionado = pckEstudiantes.SelectedItem as Estudiante;
            if (estudianteSeleccionado == null)
            {
                await DisplayAlert("ERROR", "Debe seleccionar un estudiante.", "Aceptar");
                return;
            }

            string fecha = dtpckDate.Date.ToString();
            lblInfo.Text =
                     $"Estudiante: {estudianteSeleccionado.Descripcion}\n" +
                     $"Fecha: {fecha}\n" +
                     $"Nota Parcial 1: {notaParcial1}\n" +
                     $"Nota Parcial 2: {notaParcial2}\n" +
                     $"Nota Final: {notaFinal}\n" +
                     $"Estado: {estado}";

            estadoF(estado);

            await DisplayAlert("Sistema Calificaciones", "" + lblInfo.Text, "Aceptar");



        }
        catch (Exception ex)
        {

            lblInfo.Text = "ERROR al calcular: " + ex.Message;
        }
    }

    private void btnLimpiar_Clicked(object sender, EventArgs e)
    {
        Limpiar();
    }

}