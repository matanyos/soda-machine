using SodaMachine.Models.Enums;

namespace SodaMachine.Models;

public abstract class Request
{
    public sealed class InsertMoneyRequest : Request
    {
        public InsertMoneyRequest(RequestType type, decimal money)
        {
            Type = type;
            Money = money;
        }

        public RequestType Type { get; }
        public decimal Money { get; }
    }
    public sealed class OrderRequest : Request
    {
        public OrderRequest(RequestType type, SodaType sodaType)
        {
            Type = type;
            SodaType = sodaType;
        }

        public RequestType Type { get; }
        public SodaType SodaType { get; }
    }
    public sealed class SmsOrderRequest : Request
    {
        public SmsOrderRequest(RequestType type, SodaType sodaType)
        {
            Type = type;
            SodaType = sodaType;
        }
        public RequestType Type { get; }
        public SodaType SodaType { get; }
    }
    public sealed class RecallRequest : Request
    {
        public RecallRequest(RequestType type)
        {
            Type = type;
        }

        public RequestType Type { get; }
    }
}