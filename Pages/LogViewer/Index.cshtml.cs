using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using V_Vuelos.Models;
using V_Vuelos.Page.Tools;

namespace V_Vuelos.Pages.LogViewer
{
    public class IndexModel : PageModel
    {
        public List<LogInfo> logList = new List<LogInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * from Bitacora";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LogInfo logInfo = new LogInfo
                                {
                                    cod_registro = reader.GetInt32(0),
                                    usuario = Encrypt.decode(reader.GetString(1)),
                                    fecha = reader.GetDateTime(2),
                                    tipo = reader.GetInt32(3),
                                    descripcion = Encrypt.decode(reader.GetString(4))
                                };

                                logList.Add(logInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
