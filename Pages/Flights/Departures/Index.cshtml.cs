using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using V_Vuelos.Models;
using V_Vuelos.Models.Flights;

namespace V_Vuelos.Pages.Flights.Departures
{
    public class IndexModel : PageModel
    {
        public List<DepartingFlightInfo> flightsList = new List<DepartingFlightInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * from Vuelos_Salidas";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DepartingFlightInfo flightInfo = new DepartingFlightInfo();
                                flightInfo.cod = reader.GetString(0);
                                flightInfo.cod_puerta = reader.GetString(1);
                                flightInfo.cod_aerolinea = reader.GetString(2);
                                flightInfo.estado = reader.GetString(3);
                                flightInfo.destino = reader.GetString(4);
                                flightInfo.fecha = reader.GetDateTime(5);

                                flightsList.Add(flightInfo);
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
