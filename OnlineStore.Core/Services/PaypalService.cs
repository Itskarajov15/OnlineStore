using Microsoft.Extensions.Configuration;
using PayPal.Api;

namespace OnlineStore.Core.Services
{
    public class PaypalService
    {
        private readonly IConfiguration configuration;
        private readonly Dictionary<string, string> config;

        public PaypalService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.config = new()
            {
                {"mode", this.configuration.GetSection("paypal:settings:mode").Value},
                {"clientId", this.configuration.GetSection("paypal:settings:clientId").Value},
                {"clientSecret", this.configuration.GetSection("paypal:settings:clientSecret").Value}
            };
        }

        public async Task<Payment> CreatePayment(decimal total)
        {
            var accessToken = new OAuthTokenCredential(this.config).GetAccessToken();

            var apiContext = new APIContext(accessToken)
            {
                Config = this.config,
            };

            try
            {
                Payment payment = new Payment
                {
                    intent = "sale",
                    payer = new Payer { payment_method = "paypal" },
                    transactions = new List<Transaction>
                    {
                        new Transaction
                        {
                            payee = new Payee
                            {
                                email = "sb-kjz7c21791669@business.example.com",
                            },
                            amount = new Amount
                            {
                                currency = "USD",
                                total = total.ToString(),
                            },
                            description = "Buying products",
                        },
                    },
                    redirect_urls = new RedirectUrls
                    {
                        cancel_url = @"https://localhost:7125/Product/All",
                        return_url = $@"https://localhost:7125/Paypal/SuccessedPayment?total={total}",
                    },
                };

                var createdPayment = await Task.Run(() => payment.Create(apiContext));
                return createdPayment;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Payment> ExecutePayment(string payerId, string paymentId, string token)
        {
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken)
            {
                Config = this.config,
            };

            PaymentExecution paymentExecution = new PaymentExecution() { payer_id = payerId };
            Payment payment = new Payment() { id = paymentId };
            Payment executedPayment = await Task.Run(() => payment.Execute(apiContext, paymentExecution));

            return executedPayment;
        }
    }
}