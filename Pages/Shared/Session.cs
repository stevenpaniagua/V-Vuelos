using static V_Vuelos.Pages.Cuentas.IndexModel;
using System.Data.SqlClient;
using V_Vuelos.Page.Tools;
using System.Reflection.PortableExecutable;
using V_Vuelos.Models;

namespace V_Vuelos.Pages.Shared
{
    public class Session
    {
        public String usuario;
        public String contrasena;
        public String correo;
        public String nombre;
        public String apellido1, apellido2;
        public int rol;

        public void completeRegistration(UserInfo userInfo)
        {
            usuario = userInfo.usuario;
            contrasena = userInfo.contrasena;
            correo = userInfo.correo;
            nombre = userInfo.nombre;
            apellido1 = userInfo.apellido1;
            apellido2 = userInfo.apellido2;
            rol = userInfo.rol;
            IndexModel.logind = true;
        }

        public void attempt(string usuarioX, string contrasenaX)
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";

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
                                string usuarioY = Encrypt.decode(reader.GetString(0));
                                string contrasenaY = Encrypt.decode(reader.GetString(1));

                                if(usuarioX == usuarioY && contrasenaX == contrasenaY)
                                {
                                    usuario = Encrypt.decode(reader.GetString(0));
                                    contrasena = Encrypt.decode(reader.GetString(1));
                                    correo = Encrypt.decode(reader.GetString(2));
                                    nombre = Encrypt.decode(reader.GetString(3));
                                    apellido1 = Encrypt.decode(reader.GetString(4));
                                    apellido2 = Encrypt.decode(reader.GetString(5));
                                    rol = reader.GetInt32(6);
                                    IndexModel.logind = true;
                                }
                            }
                        }
                    }
                }
            } catch (Exception e)
            {
                usuario = "ERROR";
            }
        }
    }
}
