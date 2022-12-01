using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace V_Vuelos.Pages.Management.Gates
{
    public class IndexModel : PageModel
    {
        public List<GateInfo> gateList = new List<GateInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * from Puerta";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GateInfo gateInfo = new GateInfo();
                                gateInfo.cod = reader.GetString(0);
                                gateInfo.estado = reader.GetString(1);

                                gateList.Add(gateInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public class GateInfo
        {
            public String cod;
            public String estado;
        }
    }
}
