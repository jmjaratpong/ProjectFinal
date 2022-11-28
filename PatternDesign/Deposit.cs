using static System.Console;
namespace PatternDesign;

public class Deposit
{
    private DepositState state = DepositState.OnDeposit;
    private int amount = 0;

    public void UserDeposit()
    {
        while (true)
        {
            switch (state)
            {
                case DepositState.OnDeposit:
                    OnDeposit();
                    break;
                case DepositState.OnDepositError:
                    OnDepositError();
                    break;
                case DepositState.OnDepositSuccesses:
                    OnDepositSuccesses();
                    break;
            }
        }
    }

    private void OnDeposit()
    {
        Clear();
        WriteLine($"Your amount : {UsersIdentifier.CurrentUser.Money}.\n");
        WriteLine("Enter your amount to deposit...(type -1 to menu.)");
        amount = ConvertInt();
        if (amount > 0 && amount != null && amount.ToString() != string.Empty) state = DepositState.OnDepositSuccesses;
        else state = DepositState.OnDepositError;
    }
    
    private void OnDepositError()
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
                state = DepositState.OnDeposit;
                break;
        }
    }
    
    private void OnDepositSuccesses()
    {
        Clear();
        UsersIdentifier.CurrentUser.Money += amount;
        WriteLine($"Your action complete. Thank your! {UsersIdentifier.CurrentUser.Name}\n");
        UsersIdentifier.CurrentUser.History.Enqueue(("Me", "Deposit", amount, "", DateTime.Now.ToString()));
        WriteLine($"Action : Deposit | Amount : {amount} | DateTime : {DateTime.Now}");
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