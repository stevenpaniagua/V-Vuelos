namespace V_Vuelos.Page.Tools
{
    public class Encrypt
    {

        public static string encode(string s)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(s);
            return System.Convert.ToBase64String(bytes);
        }

        public static string decode(string s)
        {
            var bytes = System.Convert.FromBase64String(s);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}
