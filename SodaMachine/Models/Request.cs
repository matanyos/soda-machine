using SodaMachine.Models.Enums;

namespace SodaMachine.Models;

public abstract class Request
{
    public sealed class InsertMoneyRequest : Request
    {
        public InsertMoneyRequest(decimal money)
        {
            Money = money;
        }
        public decimal Money { get; }
    }
    public sealed class OrderRequest : Request
    {
        public OrderRequest(SodaType sodaType)
        {
            SodaType = sodaType;
        }
        public SodaType SodaType { get; }
    }
    public sealed class SmsOrderRequest : Request
    {
        public SmsOrderRequest(SodaType sodaType)
        {
            SodaType = sodaType;
        }
        public SodaType SodaType { get; }
    }
    public sealed class RecallRequest : Request
    {
    }
}