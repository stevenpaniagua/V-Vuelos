using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static V_Vuelos.Pages.Cuentas.IndexModel;
using V_Vuelos.Page.Tools;
using static V_Vuelos.Pages.Management.Countries.IndexModel;
using System.Reflection.PortableExecutable;
using V_Vuelos.Pages.Tools.image;
using System.Drawing;
using System.Web;
using V_Vuelos.Models;

namespace V_Vuelos.Pages.Management.Countries
{
    public class CreateModel : PageModel
    {

        public CountryInfo countryInfo = new CountryInfo();

        public String errorMessage = "", successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            countryInfo.cod = Request.Form["cod"];
            countryInfo.nombre = Request.Form["nombre"];
            countryInfo.imagen = ImageUtil.imageToByteArray(Image.FromFile("C:\\Users\\Steven\\source\\repos\\V-Vuelos\\Images\\Logo.png"));

            if (countryInfo.cod.Length == 0 || countryInfo.nombre.Length == 0)
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
                    String sql = "INSERT INTO Pais " +
                        "(cod, nombre, imagen) VALUES " +
                        "(@cod, @nombre, @imagen)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@cod", countryInfo.cod);
                        command.Parameters.AddWithValue("@nombre", countryInfo.nombre);
                        command.Parameters.AddWithValue("@imagen", countryInfo.imagen);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            countryInfo.cod = "";
            countryInfo.nombre = "";
            countryInfo.imagen = null;

            successMessage = "Successfully registered the new country.";
        }
    }
}
