using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using V_Vuelos.Models;
using V_Vuelos.Page.Tools;

namespace V_Vuelos.Pages.Cuentas
{
    public class IndexModel : PageModel
    {
        public List<UserInfo> usersList = new List<UserInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * from Cuenta";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                UserInfo userInfo = new UserInfo();
                                userInfo.usuario = Encrypt.decode(reader.GetString(0));
                                userInfo.contrasena = reader.GetString(1);
                                userInfo.correo = Encrypt.decode(reader.GetString(2));
                                userInfo.nombre = Encrypt.decode(reader.GetString(3));
                                userInfo.apellido1 = Encrypt.decode(reader.GetString(4));
                                userInfo.apellido2 = Encrypt.decode(reader.GetString(5));
                                userInfo.rol = reader.GetInt32(6);

                                usersList.Add(userInfo);
                            }
                        }
                    }
                }
            } catch (Exception e)
            {

            }
        }
    }
}
