using Project_partA.Enums;
namespace Project_partA.Interfaces
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(PaymentType paymentType, decimal amount);
        void Refund(string transactionId);
    }
}
