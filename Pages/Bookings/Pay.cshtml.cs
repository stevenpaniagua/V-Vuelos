using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
using Stripe;
using System.Xml.Linq;

namespace V_Vuelos.Pages.Bookings
{
    public class PayModel : PageModel
    {
        public string sessionId = "Session ID";
        public async void OnGet()
        {
            StripeConfiguration.ApiKey = "sk_test_51MFLTvHqn99Uu3sVrrs2x18H7O77CkBMfbLt6IKNqg1bF1ods2EVHu0gWhNWDp2APccM0bwD0qnqqTObTibf47R300n9nCzJcz";

            var options = new SessionCreateOptions
            {
                SuccessUrl = "https://localhost:5151/success",
                CancelUrl = "https://localhost:5151/cancel",
                LineItems = new List<SessionLineItemOptions>
  {
    new SessionLineItemOptions
    {
      Price = "price_1MFOjiHqn99Uu3sVlEnQ0YUZ",
      Quantity = 2,
    },
  },
                Mode = "payment",
            };
            var service = new SessionService();
            try
            {
                var session = await service.CreateAsync(options);
                sessionId = session.Id;
            } catch(StripeException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void OnPost()
        {
            Console.WriteLine(sessionId);
        }
    }
}
