using static System.Console;
namespace PatternDesign;

public class Transfer
{
    private TransferState state = TransferState.OnTransfer;
    private int amount = 0;
    private string nameRecevier = string.Empty;

    public void UserTransfer()
    {
        while (true)
        {
            switch (state)
            {
                case TransferState.OnTransfer:
                    OnTransfer();
                    break;
                case TransferState.OnTransferError:
                    OnTransferError();
                    break;
                case TransferState.OnTransferSuccesses:
                    OnTransferSuccesses();
                    break;
            }
        }
    }

    private void OnTransfer()
    {
        Clear();
        WriteLine($"Your amount : {UsersIdentifier.CurrentUser.Money}.\n");
        WriteLine("Enter your amount to transfer...(type -1 to menu.)");
        amount = ConvertInt();
        WriteLine("Enter your username receiver to transfer...");
        nameRecevier = ReadLine();
        foreach (var user in UserData.UserList)
        {
            if (amount > 0 && amount != null
                           && amount.ToString() != string.Empty
                           && amount <= UsersIdentifier.CurrentUser.Money
                           && user.UserName.Contains(nameRecevier))
            {
                state = TransferState.OnTransferSuccesses;
                return;
            }
            else state = TransferState.OnTransferError;
        }
    }
    
    private void OnTransferError()
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
                state = TransferState.OnTransfer;
                break;
            default:
            {
                if (amount > UsersIdentifier.CurrentUser.Money)
                {
                    Clear();
                    WriteLine("Your amount cant not more than your total amount.");
                    ReadLine();
                    state = TransferState.OnTransfer;
                }

                break;
            }
        }

        foreach (var user in UserData.UserList.Where(user => !user.UserName.Contains(nameRecevier)))
        {
            Clear();
            WriteLine($"Cant not found username {nameRecevier}");
            ReadLine();
            state = TransferState.OnTransfer;
        }
    }
    
    private void OnTransferSuccesses()
    {
        Clear();
        UsersIdentifier.CurrentUser.Money -= amount;
        foreach (var user in UserData.UserList.Where(user => user.UserName.Contains(nameRecevier)))
        {
            user.Money += amount;
            user.History.Enqueue(("","Earn", amount, $"{UsersIdentifier.CurrentUser.UserName}", DateTime.Now.ToString()));
        }
        WriteLine($"Your action complete. Thank your! {UsersIdentifier.CurrentUser.Name}\n");
        UsersIdentifier.CurrentUser.History.Enqueue(("Me", "Transfer", amount, nameRecevier, DateTime.Now.ToString()));
        WriteLine($"From : Me | Action : Withdraw | Amount : {amount} | To : {nameRecevier} | DateTime : {DateTime.Now}");
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