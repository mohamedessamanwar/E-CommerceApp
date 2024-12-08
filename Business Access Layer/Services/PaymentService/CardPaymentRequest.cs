namespace BusinessAccessLayer.Services.PaymentService
{
    public class CardPaymentRequest
    {
        public string CardNumber { get; set; }
        public long ExpMonth { get; set; }
        public long ExpYear { get; set; }
        public string CVC { get; set; }
    }
}