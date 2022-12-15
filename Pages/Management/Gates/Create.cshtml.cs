using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static V_Vuelos.Pages.Management.Gates.IndexModel;
using V_Vuelos.Pages.Tools.image;
using V_Vuelos.Models;
using V_Vuelos.Tools.bitacora;

namespace V_Vuelos.Pages.Management.Gates
{
    public class CreateModel : PageModel
    {
        public GateInfo gateinfo = new GateInfo();

        public String errorMessage = "", successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            gateinfo.cod = Request.Form["cod"];
            gateinfo.estado = Request.Form["estado"];

            if (gateinfo.cod.Length == 0 || gateinfo.estado.Length == 0)
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
                    String sql = "INSERT INTO Puerta " +
                        "(cod, estado) VALUES " +
                        "(@cod, @estado)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@cod", gateinfo.cod);
                        command.Parameters.AddWithValue("@estado", gateinfo.estado);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            LogHandler.log(LogHandler.LogType.AGREGAR, "Registered new GATE (gate code =" + gateinfo.cod + ")");

            gateinfo.cod = "";
            gateinfo.estado = null;

            successMessage = "Successfully registered the new gate.";
        }
    }
}
