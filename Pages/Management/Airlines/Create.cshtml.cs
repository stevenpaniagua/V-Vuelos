using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using V_Vuelos.Models;
using V_Vuelos.Pages.Tools.image;
using static V_Vuelos.Pages.Management.Airlines.IndexModel;
using static V_Vuelos.Pages.Management.Countries.IndexModel;

namespace V_Vuelos.Pages.Management.Airlines
{
    public class CreateModel : PageModel
    {
        public AirlineInfo airlineInfo = new AirlineInfo();
        public List<String> countries = new List<String>();

        public String errorMessage = "", successMessage = "";

        public void OnGet()
        {
            countries.Clear();
            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * from Pais";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                countries.Add(reader.GetString(0));
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
            airlineInfo.cod_pais = Request.Form["cod_pais"];
            airlineInfo.nombre = Request.Form["nombre"];
            airlineInfo.cod = Request.Form["cod"];

            if (airlineInfo.cod_pais.Length == 0 || airlineInfo.nombre.Length == 0 || airlineInfo.cod.Length == 0)
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
                    String sql = "INSERT INTO Aerolinea " +
                        "(cod_pais, nombre, cod) VALUES " +
                        "(@cod_pais, @nombre, @cod)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@cod_pais", airlineInfo.cod_pais);
                        command.Parameters.AddWithValue("@nombre", airlineInfo.nombre);
                        command.Parameters.AddWithValue("@cod", airlineInfo.cod);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            airlineInfo.cod_pais = "";
            airlineInfo.nombre = "";
            airlineInfo.cod = null;

            successMessage = "Successfully registered the new airline.";
        }
    }
}
