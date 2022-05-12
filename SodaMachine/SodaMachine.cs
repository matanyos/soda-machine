using System.Globalization;
using SodaMachine.Interfaces;
using SodaMachine.Models;
using SodaMachine.Models.Enums;

namespace SodaMachine;

public class SodaMachine : ISodaMachine
{
    private readonly Inventory inventory;
    private static decimal money;

    public SodaMachine(Inventory inventory)
    {
        this.inventory = inventory;
    }

    /// <summary>
    /// This is the starter method for the machine
    /// </summary>
    public void Start()
    {
        while (true)
        {
            DisplayMenu();

            var input = Console.ReadLine();

            var request = ExtractUserRequest(input);
            if (request == null) continue;

            if (request is Request.InsertMoneyRequest insertMoneyRequest)
            {
                //Add to credit
                money += insertMoneyRequest.Money;
                Console.WriteLine($"Adding {insertMoneyRequest.Money} to credit");
            }
            if (request is Request.OrderRequest orderRequest)
            {
                HandleOrderRequest(orderRequest.SodaType);
            }
            if (request is Request.SmsOrderRequest smsOrderRequest)
            {
                if(inventory.Sodas.Any(x => x.Type == smsOrderRequest.SodaType && x.Quantity > 0))
                {
                    Console.WriteLine($"Giving {smsOrderRequest.SodaType} out");
                    inventory.TakeOneSodaOut(smsOrderRequest.SodaType);
                }
                else
                {
                    Console.WriteLine($"No {smsOrderRequest.SodaType} left");
                }
            }
            if (request is Request.RecallRequest)
            {
                if(money > 0)
                {
                    Console.WriteLine("Returning " + money + " to customer");
                    money = 0;
                }
                else
                {
                    Console.WriteLine("No Credit");
                }
            }
        }
    }
    public void DisplayMenu()
    {
        Console.WriteLine("\n\nAvailable commands:");
        Console.WriteLine("insert (money) - Money put into money slot");
        Console.WriteLine("order (coke, sprite, fanta) - Order from machine's inventory");
        Console.WriteLine("sms order (coke, sprite, fanta) - Order sent by sms");
        Console.WriteLine("recall - gives money back");
        Console.WriteLine("-------");
        Console.WriteLine("Inserted money: " + money);
        Console.WriteLine("-------\n\n");
    }
    private void HandleOrderRequest(SodaType sodaType)
    {
        if (inventory.Sodas.Any(x => x.Type == sodaType && x.Quantity > 0))
        {
            var price = inventory.Sodas.First(x => x.Type == sodaType).Price;
            if (money >= price)
            {
                Console.WriteLine($"Giving {sodaType} out");
                money -= price;
                if (money > 0)
                {
                    Console.WriteLine("Giving " + money + " out in change");
                    money = 0;
                }

                inventory.TakeOneSodaOut(sodaType);
            }
            else
                Console.WriteLine("Need " + (price - money) + " more");
        }
        else
        {
            Console.WriteLine($"No {sodaType} left");
        }
    }

    private static Request? ExtractUserRequest(string? input)
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

    private static Request? CreateSmsOrderRequest(string input)
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

    private static Request? CreateOrderRequest(string input)
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

    private static Request? CreateInsertRequest(string input)
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