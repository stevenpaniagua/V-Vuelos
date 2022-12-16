using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using V_Vuelos.Models;
using V_Vuelos.Models.Flights;
using V_Vuelos.Page.Tools;

namespace V_Vuelos.Pages.Users
{
    public class MyTicketsModel : PageModel
    {
        public List<TicketInfo> ticketList = new List<TicketInfo>();

        public void OnGet()
        {
            String user = Encrypt.encode(IndexModel.session.usuario);
            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "SELECT * from Boleto WHERE usuario_cuenta='" + Encrypt.encode(IndexModel.session.usuario) + "'";
                    Console.WriteLine(command.CommandText);
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        TicketInfo ticketInfo = new TicketInfo
                        {
                            cod_vuelo = reader.GetString(1),
                            cantidad = Int32.Parse(reader.GetString(2)),
                            metodo_pago = Encrypt.decode(reader.GetString(3))
                        };

                        ticketList.Add(ticketInfo);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
