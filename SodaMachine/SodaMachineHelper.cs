using System.Globalization;
using SodaMachine.Models;
using SodaMachine.Models.Enums;

namespace SodaMachine
{
    internal class SodaMachineHelper
    {
        public static Request ExtractUserRequest(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            if (input.StartsWith("insert", true, CultureInfo.InvariantCulture))
                return CreateInsertRequest(input);

            if (input.StartsWith("order", true, CultureInfo.InvariantCulture))
                return CreateOrderRequest(input);

            if (input.StartsWith("sms order", true, CultureInfo.InvariantCulture))
                return CreateSmsOrderRequest(input);

            if (input.StartsWith("recall", true, CultureInfo.InvariantCulture))
                return new Request.RecallRequest();

            return null;
        }

        private static Request CreateSmsOrderRequest(string input)
        {
            var inputs = input.Split(' ');
            if (inputs.Length < 3)
                return null;

            var argument = inputs[2];

            return
                argument != "coke" && argument != "sprite" && argument != "fanta"
                    ? null
                    : argument == "coke"
                        ? new Request.SmsOrderRequest(SodaType.Coke)
                        : argument == "sprite"
                            ? new Request.SmsOrderRequest(SodaType.Sprite)
                            : new Request.SmsOrderRequest(SodaType.Fanta);

        }

        private static Request CreateOrderRequest(string input)
        {
            var inputs = input.Split(' ');
            if (inputs.Length < 2)
                return null;

            var argument = inputs[1];

            return
                argument != "coke" && argument != "sprite" && argument != "fanta"
                    ? null
                    : argument == "coke"
                        ? new Request.OrderRequest(SodaType.Coke)
                        : argument == "sprite"
                            ? new Request.OrderRequest(SodaType.Sprite)
                            : new Request.OrderRequest(SodaType.Fanta);
        }

        private static Request CreateInsertRequest(string input)
        {
            var inputs = input.Split(' ');
            if (inputs.Length < 2)
                return null;

            var argument = inputs[1];

            return
                decimal.TryParse(argument, out var value)
                    ? new Request.InsertMoneyRequest(value)
                    : null;
        }
    }
}
