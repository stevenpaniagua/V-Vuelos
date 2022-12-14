using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using V_Vuelos.Models;
using V_Vuelos.Models.Flights;

namespace V_Vuelos.Pages.Flights.Arrivals
{
    public class CreateModel : PageModel
    {
        public ArrivingFlightInfo flightInfo = new ArrivingFlightInfo();
        public List<String> gates = new List<String>(), airlines = new List<String>();

        public String errorMessage = "", successMessage = "";

        public void OnGet()
        {
            gates.Clear();
            airlines.Clear();

            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String gateSql = "SELECT * from Puerta";
                    String airlineSql = "SELECT * from Aerolinea";

                    using (SqlCommand command = new SqlCommand(gateSql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                gates.Add(reader.GetString(0));
                            }
                        }
                    }

                    using (SqlCommand command = new SqlCommand(airlineSql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                airlines.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public void OnPost()
        {
            flightInfo.cod = Request.Form["cod"];
            flightInfo.cod_puerta = Request.Form["cod_puerta"];
            flightInfo.cod_aerolinea = Request.Form["cod_aerolinea"];
            flightInfo.estado = Request.Form["estado"];
            flightInfo.procedencia = Request.Form["procedencia"];
            flightInfo.fecha = Convert.ToDateTime(Request.Form["fecha"]);

            if (flightInfo.cod.Length == 0 || flightInfo.cod_puerta.Length == 0 || flightInfo.cod_aerolinea.Length == 0 || flightInfo.estado.Length == 0 || flightInfo.procedencia.Length == 0)
            {
                errorMessage = "Please fill every field.";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Vuelos_Llegadas " +
                        "(cod, cod_puerta, cod_aerolinea, estado, procedencia, fecha) VALUES " +
                        "(@cod, @cod_puerta, @cod_aerolinea, @estado, @procedencia, @fecha)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@cod", flightInfo.cod);
                        command.Parameters.AddWithValue("@cod_puerta", flightInfo.cod_puerta);
                        command.Parameters.AddWithValue("@cod_aerolinea", flightInfo.cod_aerolinea);
                        command.Parameters.AddWithValue("@estado", flightInfo.estado);
                        command.Parameters.AddWithValue("@procedencia", flightInfo.procedencia);
                        command.Parameters.AddWithValue("@fecha", flightInfo.fecha);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            flightInfo.cod = "";
            flightInfo.cod_puerta = "";
            flightInfo.cod_aerolinea = "";
            flightInfo.estado = "";
            flightInfo.procedencia = "";
            flightInfo.fecha = DateTime.Now;

            successMessage = "Successfully registered the new arriving flight.";
        }
    }
}
