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

            var request = SodaMachineHelper.ExtractUserRequest(input);

            switch (request)
            {
                case null:
                    continue;
                case Request.InsertMoneyRequest insertMoneyRequest:
                    ExecuteInsertMoneyRequest(insertMoneyRequest.Money);
                    break;
                case Request.OrderRequest orderRequest:
                    ExecuteOrderRequest(orderRequest.SodaType);
                    break;
                case Request.SmsOrderRequest smsOrderRequest:
                    ExecuteSmsOrderRequest(smsOrderRequest.SodaType);
                    break;
                case Request.RecallRequest:
                    ExecuteRecallRequest();
                    break;
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

    private static void ExecuteRecallRequest()
    {
        if (money > 0)
        {
            Console.WriteLine("Returning " + money + " to customer");
            money = 0;
        }
        else
        {
            Console.WriteLine("No Credit");
        }
    }
    private static void ExecuteInsertMoneyRequest(decimal value)
    {
        //Add to credit
        money += value;
        Console.WriteLine($"Adding {value} to credit");
    }
    private void ExecuteSmsOrderRequest(SodaType sodaType)
    {
        if (inventory.Sodas.Any(x => x.Type == sodaType && x.Quantity > 0))
        {
            Console.WriteLine($"Giving {sodaType} out");
            inventory.TakeOneSodaOut(sodaType);
        }
        else
        {
            Console.WriteLine($"No {sodaType} left");
        }
    }
    private void ExecuteOrderRequest(SodaType sodaType)
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
}