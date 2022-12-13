using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using V_Vuelos.Models;

namespace V_Vuelos.Pages.Management.Countries
{
    public class IndexModel : PageModel
    {
        public List<CountryInfo> countryList = new List<CountryInfo>();

        public void OnGet()
        {
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
                                CountryInfo countryInfo = new CountryInfo();
                                countryInfo.cod = reader.GetString(0);
                                countryInfo.nombre = reader.GetString(1);

                                countryList.Add(countryInfo);
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
