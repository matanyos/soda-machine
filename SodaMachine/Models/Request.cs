using SodaMachine.Models.Enums;

namespace SodaMachine.Models;

public class Request
{
    public Request(RequestType type, string input)
    {
        Type = type;
        Input = input;
    }

    public RequestType Type { get; }
    public string Input { get; }
}