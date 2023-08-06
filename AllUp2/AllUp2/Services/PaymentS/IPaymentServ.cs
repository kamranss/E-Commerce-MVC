namespace AllUp2.Services.PaymentS
{
    public interface IPaymentServ
    {
        public async Task<PaymentIntent> CreatePaymentIntent(decimal amount, string currency)
    }
}
