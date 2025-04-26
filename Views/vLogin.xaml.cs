namespace _008_jdelgadoDS3.Views;

public partial class vLogin : ContentPage
{
    public vLogin()
    {
        InitializeComponent();
    }

    private async void btnIngresar_Clicked(object sender, EventArgs e)
    {
        var user = new string[] { "Carlos", "Ana", "Jose" };
        var pass = new string[] { "carlos123", "ana123", "jose123" };

        var usuario = txtUsuario.Text;
        var contrasena = txtContrasena.Text;
        if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
        {
            await DisplayAlert("ERROR", "Por favor, complete todos los campos.", "Aceptar");
            return;
        }
        // Verificar si las credenciales son correctas
        int index = Array.IndexOf(user, usuario);

        if (index >= 0 && pass[index] == contrasena)
        {
            await DisplayAlert("Bienvenido", $"¡Bienvenido {usuario}!", "Aceptar");
            await Navigation.PushAsync(new Views.vPrincipal());
            limpiar();
        }
        else
        {
            await DisplayAlert("ERROR", "Usuario o contraseña incorrectos.", "Aceptar");
            limpiar();
        }

    }

    private void limpiar()
    {
        txtUsuario.Text = "";
        txtContrasena.Text = "";
    }
}
