using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
                                userInfo.usuario = reader.GetString(0);
                                userInfo.contrasena = reader.GetString(1);
                                userInfo.correo = reader.GetString(2);
                                userInfo.nombre = reader.GetString(3);
                                userInfo.apellido1 = reader.GetString(4);
                                userInfo.apellido2 = reader.GetString(5);
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

        public class UserInfo
        {
            public String usuario;
            public String contrasena;
            public String correo;
            public String nombre;
            public String apellido1, apellido2;
            public int rol;
        }
    }
}
