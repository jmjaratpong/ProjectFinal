using static System.Console;
namespace PatternDesign;

public class Withdraw
{
    private WithdrawState state = WithdrawState.OnWithdraw;
    private int amount = 0;

    public void UserWithdraw()
    {
        while (true)
        {
            switch (state)
            {
                case WithdrawState.OnWithdraw:
                    OnWithDraw();
                    break;
                case WithdrawState.OnWithdrawError:
                    OnWithdrawError();
                    break;
                case WithdrawState.OnWithdrawSuccesses:
                    OnWithdrawSuccesses();
                    break;
            }
        }
    }

    private void OnWithDraw()
    {
        Clear();
        WriteLine($"Your amount : {UsersIdentifier.CurrentUser.Money}.\n");
        WriteLine("Enter your amount to withdraw...(type -1 to menu.)");
        amount = ConvertInt();
        if (amount > 0 && amount != null && amount.ToString() != string.Empty && amount <= UsersIdentifier.CurrentUser.Money) state = WithdrawState.OnWithdrawSuccesses;
        else state = WithdrawState.OnWithdrawError;
    }
    
    private void OnWithdrawError()
    {
        switch (amount)
        {
            case < 0:
            {
                Clear();
                var menu = new Menu();
                menu.UserMenuDisplay();
                break;
            }
            case 0:
                Clear();
                WriteLine("Your amount cant not be 0.");
                ReadLine();
                state = WithdrawState.OnWithdraw;
                break;
            default:
            {
                if (amount > UsersIdentifier.CurrentUser.Money)
                {
                    Clear();
                    WriteLine("Your amount cant not more than your total amount.");
                    ReadLine();
                    state = WithdrawState.OnWithdraw;
                }

                break;
            }
        }
    }
    
    private void OnWithdrawSuccesses()
    {
        Clear();
        UsersIdentifier.CurrentUser.Money -= amount;
        WriteLine($"Your action complete. Thank your! {UsersIdentifier.CurrentUser.Name}\n");
        UsersIdentifier.CurrentUser.History.Enqueue(("Me", "Withdraw", amount, "", DateTime.Now.ToString()));
        WriteLine($"Action : Withdraw | Amount : {amount} | DateTime : {DateTime.Now}");
        WriteLine($"Your total amount : {UsersIdentifier.CurrentUser.Money}");
        ReadLine();
        JsonUserData.Storage();
        Menu menu = new Menu();
        menu.UserMenuDisplay();
    }

    private int ConvertInt()
    {
        if (int.TryParse(ReadLine(), out int number))
        {
            return number;
        }
        else return 0;
    }
}