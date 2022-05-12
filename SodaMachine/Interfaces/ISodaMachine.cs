using SodaMachine.Models;

namespace SodaMachine.Interfaces;

internal interface ISodaMachine
{
    void Start();
    void DisplayMenu();
    void ExecuteUserRequest(Request request);
}