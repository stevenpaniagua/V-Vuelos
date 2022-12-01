using System.Data.SqlClient;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using V_Vuelos.Page.Tools;
using V_Vuelos.Pages.Shared;
using static V_Vuelos.Pages.Cuentas.IndexModel;

namespace V_Vuelos.Pages.Login
{
    public class RegisterModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();

        public String errorMessage = "", successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            userInfo.usuario = Request.Form["usuario"];
            userInfo.contrasena = Request.Form["contrasena"];
            userInfo.correo = Request.Form["correo"];
            userInfo.nombre = Request.Form["nombre"];
            userInfo.apellido1 = Request.Form["apellido1"];
            userInfo.apellido2 = Request.Form["apellido2"];
            userInfo.rol = 0;

            if (userInfo.usuario.Length == 0 || userInfo.contrasena.Length == 0 || userInfo.correo.Length == 0
                || userInfo.nombre.Length == 0 || userInfo.apellido1.Length == 0 || userInfo.apellido2.Length == 0)
            {
                errorMessage = "Please fill every field.";
                return;
            }

            bool invalidUser = false;

            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String check = "SELECT COUNT(*) FROM Cuenta WHERE usuario='" + userInfo.usuario + "'";
                    using(SqlCommand checkCommand = new SqlCommand(check, connection))
                    {
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                        if(count != 0)
                        {
                            invalidUser = true;

                            Random random = new Random();
                            int x = random.Next(100);

                            errorMessage = "The specified user is already registered! You can try using: " + userInfo.usuario + x;
                            return;
                        }
                    }

                    if (!invalidUser)
                    {
                        String sql = "INSERT INTO Cuenta " +
                            "(usuario, contrasena, correo, nombre, apellido1, apellido2, rol) VALUES " +
                            "(@usuario, @contrasena, @correo, @nombre, @apellido1, @apellido2, @rol)";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@usuario", userInfo.usuario);
                            command.Parameters.AddWithValue("@contrasena", Encrypt.encode(userInfo.contrasena));
                            command.Parameters.AddWithValue("@correo", userInfo.correo);
                            command.Parameters.AddWithValue("@nombre", userInfo.nombre);
                            command.Parameters.AddWithValue("@apellido1", userInfo.apellido1);
                            command.Parameters.AddWithValue("@apellido2", userInfo.apellido2);
                            command.Parameters.AddWithValue("@rol", userInfo.rol);

                            command.ExecuteNonQuery();
                        }
                    }
                }

                if (!invalidUser)
                {
                    Session session = IndexModel.session;
                    session.completeRegistration(userInfo);

                    if (!IndexModel.logind)
                    {
                        errorMessage = "An error has occurred. Please refresh the page.";
                    }
                    else
                    {
                        successMessage = "Your account has been created! Thank you for choosing us!";
                    }
                }

                userInfo.usuario = "";
                userInfo.contrasena = "";
                userInfo.correo = "";
                userInfo.nombre = "";
                userInfo.apellido1 = "";
                userInfo.apellido2 = "";
                userInfo.rol = -1;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }
        }
    }
}
