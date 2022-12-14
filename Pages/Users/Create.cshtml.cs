using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using V_Vuelos.Models;
using V_Vuelos.Page.Tools;
using static V_Vuelos.Pages.Cuentas.IndexModel;

namespace V_Vuelos.Pages.Users
{
    public class CreateModel : PageModel
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
            userInfo.rol = Convert.ToInt32(Request.Form["rol"]);

            if(userInfo.usuario.Length == 0 || userInfo.contrasena.Length == 0 || userInfo.correo.Length == 0 
                || userInfo.nombre.Length == 0 || userInfo.apellido1.Length == 0 || userInfo.apellido2.Length == 0)
            {
                errorMessage = "Please fill every field.";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Cuenta " +
                        "(usuario, contrasena, correo, nombre, apellido1, apellido2, rol) VALUES " +
                        "(@usuario, @contrasena, @correo, @nombre, @apellido1, @apellido2, @rol)";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@usuario", Encrypt.encode(userInfo.usuario));
                        command.Parameters.AddWithValue("@contrasena", Encrypt.encode(userInfo.contrasena));
                        command.Parameters.AddWithValue("@correo", Encrypt.encode(userInfo.correo));
                        command.Parameters.AddWithValue("@nombre", Encrypt.encode(userInfo.nombre));
                        command.Parameters.AddWithValue("@apellido1", Encrypt.encode(userInfo.apellido1));
                        command.Parameters.AddWithValue("@apellido2", Encrypt.encode(userInfo.apellido2));
                        command.Parameters.AddWithValue("@rol", userInfo.rol);

                        command.ExecuteNonQuery();
                    }
                }

            } catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            userInfo.usuario = "";
            userInfo.contrasena = "";
            userInfo.correo = "";
            userInfo.nombre = "";
            userInfo.apellido1 = "";
            userInfo.apellido2 = "";
            userInfo.rol = -1;

            successMessage = "Successfully registered the new user.";
        }
    }
}
