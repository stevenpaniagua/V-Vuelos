using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using V_Vuelos.Models;

namespace V_Vuelos.Pages.Management.Airlines
{
    public class IndexModel : PageModel
    {
        public List<AirlineInfo> airlineList = new List<AirlineInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * from Aerolinea";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AirlineInfo airlineInfo = new AirlineInfo();
                                airlineInfo.cod_pais = reader.GetString(0);
                                airlineInfo.nombre = reader.GetString(1);
                                airlineInfo.cod = reader.GetString(2);

                                airlineList.Add(airlineInfo);
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
