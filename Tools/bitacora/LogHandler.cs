using System.Data.SqlClient;
using System.Text;
using V_Vuelos.Page.Tools;
using V_Vuelos.Pages;

namespace V_Vuelos.Tools.bitacora
{
	public class LogHandler
	{

		public static void log(LogType tipo, String descripcion)
		{
            try
            {
                String connectionString = "Data Source=DESKTOP-PGN2S2Q\\SQLEXPRESS;Initial Catalog=V-Vuelos;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Bitacora " +
                        "(usuario, fecha, tipo, descripcion) VALUES " +
                        "(@usuario, @fecha, @tipo, @descripcion)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        string user = IndexModel.session.usuario;
                        DateTime date = DateTime.Now;
                        int type = (int)tipo;
                        string desc = descripcion;

                        command.Parameters.AddWithValue("@usuario", Encrypt.encode(user));
                        command.Parameters.AddWithValue("@fecha", date);
                        command.Parameters.AddWithValue("@tipo", type);
                        command.Parameters.AddWithValue("@descripcion", Encrypt.encode(desc));

                        command.ExecuteNonQuery();

                        StringBuilder sb = new StringBuilder();
                        sb.Append("NEW LOG");
                        sb.AppendLine("User: " + user);
                        sb.AppendLine("Date: " + date);
                        sb.AppendLine("Type: " + tipo);
                        sb.AppendLine(desc);
                        Console.WriteLine(sb);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

		public enum LogType
		{
			AGREGAR=1, MODIFICAR, ELIMINAR
		}
	}
}
