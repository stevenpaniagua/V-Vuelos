using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using V_Vuelos.Page.Tools;
using V_Vuelos.Pages.Shared;

namespace V_Vuelos.Pages
{
    public class LoginModel : PageModel
    {
        public string errorMessage = "", successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            string correo = Request.Form["usuario"];
            string contrasena = Request.Form["contrasena"];

            Session session = IndexModel.session;
            session.attempt(correo, contrasena);

            if(!IndexModel.logind)
            {
                errorMessage = "Invalid login credentials.";
            } else
            {
                Response.Redirect("/");
            }
        }
    }
}
