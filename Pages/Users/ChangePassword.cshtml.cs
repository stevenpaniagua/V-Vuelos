using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using V_Vuelos.Page.Tools;
using static V_Vuelos.Pages.Cuentas.IndexModel;

namespace V_Vuelos.Pages.Users
{
    public class ChangePasswordModel : PageModel
    {
        public string errorMessage = "", successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            string old = Request.Form["contrasena"];
            string pass = Request.Form["nueva"];
            string passC = Request.Form["nuevaC"];

            if (old.Length == 0 || pass.Length == 0 || passC.Length == 0)
            {
                errorMessage = "Please fill every field.";
                return;
            }

            if(pass != passC)
            {
                errorMessage = "Please make sure you entered the right password in both fields.";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";

                Boolean success = false;
                string updatedUser = "";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * from Cuenta";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string usuario = reader.GetString(0);
                                string contrasena = reader.GetString(1);

                                if (usuario == IndexModel.session.usuario && old == Encrypt.decode(contrasena))
                                {
                                    success = true;
                                    updatedUser = usuario;
                                }
                            }
                        }
                    }

                    if(success)
                    {
                        String sql2 = "UPDATE Cuenta SET contrasena=@contrasena WHERE usuario=@usuario";

                        using (SqlCommand cmd = new SqlCommand(sql2, connection))
                        {
                            cmd.Parameters.AddWithValue("@usuario", updatedUser);
                            cmd.Parameters.AddWithValue("@contrasena", Encrypt.encode(pass));
                            cmd.ExecuteNonQuery();
                        }
                        successMessage = "Successfully changed your password!";
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
        }
    }
}