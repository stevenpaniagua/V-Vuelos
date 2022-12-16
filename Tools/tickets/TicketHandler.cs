using System.Data.SqlClient;
using System.Text;
using V_Vuelos.Page.Tools;
using V_Vuelos.Pages;

namespace V_Vuelos.Tools.tickets
{
    public class TicketHandler
    {
        public static void purchase(string cod_vuelo, int cantidad, string metodoPago)
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Boleto " +
                        "(usuario_cuenta, cod_vuelo, cantidad, metodo_pago) VALUES " +
                        "(@usuario_cuenta, @cod_vuelo, @cantidad, @metodo_pago)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@usuario_cuenta", Encrypt.encode(IndexModel.session.usuario));
                        command.Parameters.AddWithValue("@cod_vuelo", cod_vuelo);
                        command.Parameters.AddWithValue("@cantidad", cantidad);
                        command.Parameters.AddWithValue("@metodo_pago", Encrypt.encode(metodoPago));

                        command.ExecuteNonQuery();
                        Console.WriteLine("Bought ticket");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}
