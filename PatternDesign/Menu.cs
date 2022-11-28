using static System.Console;
namespace PatternDesign;

public class Menu
{
    private List<MenuState> menuState = new List<MenuState>()
        { MenuState.Withdraw, MenuState.Deposit, MenuState.Transfer, MenuState.History, MenuState.Account, MenuState.Logout };
    private MenuState? state = null;

    public void UserMenuDisplay()
    {
        Clear();
        WriteLine($"Welcome back. {UsersIdentifier.CurrentUser.Name}!");
        WriteLine($"Your amount : {UsersIdentifier.CurrentUser.Money}");
        WriteLine("Select Menu...\n");
        for (int i = 0; i < menuState.Count; i++)
        {
            var state = menuState[i];
            WriteLine($"{i} : {state}");
        }

        var menu = ConvertMenuSelect();

        if (menu > menuState.Count - 1) throw Logger.OutOfRangeError(menu, "Menu");
        else
        {
            var menuselected = menuState[menu];
            state = menuselected;
            StateMenu();
        }
    }
    
    private int ConvertMenuSelect()
    {
        if (int.TryParse(ReadLine(), out int menu))
        {
            return menu;
        }

        throw Logger.ConvertError("MenuInput (string)", "int");
    }

    private void StateMenu()
    {
        while (true)
        {
            switch (state)
            {
                case MenuState.Withdraw:
                    Withdraw();
                    break;
                case MenuState.Deposit:
                    Deposit();
                    break;
                case MenuState.Transfer:
                    Transfer();
                    break;
                case MenuState.History:
                    History();
                    break;
                case MenuState.Account:
                    Account();
                    break;
                case MenuState.Logout:
                    Logout();
                    break;
            }
        }
    }

    private void Withdraw()
    {
        Clear();
        WriteLine("Please Input your Pin.");
        if (Pin())
        {
            var withdraw = new Withdraw();
            withdraw.UserWithdraw();
        }
        else
        {
            Clear();
            WriteLine("Your pin is incorrect.");
            ReadLine();
            UserMenuDisplay();
        }
    }
    
    private void Deposit()
    {
        Clear();
        WriteLine("Please Input your Pin.");
        if (Pin())
        {
            var deposit = new Deposit();
            deposit.UserDeposit();
        }
        else
        {
            Clear();
            WriteLine("Your pin is incorrect.");
            ReadLine();
            UserMenuDisplay();
        }
    }
    
    private void Transfer()
    {
        Clear();
        WriteLine("Please Input your Pin.");
        if (Pin())
        {
            var transfer = new Transfer();
            transfer.UserTransfer();
        }
        else
        {
            Clear();
            WriteLine("Your pin is incorrect.");
            ReadLine();
            UserMenuDisplay();
        }
    }
    
    private void History()
    {
        var history = new History();
        history.UserHistoryDisplay();
    }
    
    private void Account()
    {
        Clear();
        WriteLine("Please Input your Pin.");
        if (Pin())
        {
            var account = new Account();
            account.UserAccountDisplay();
        }
        else
        {
            Clear();
            WriteLine("Your pin is incorrect.");
            ReadLine();
            UserMenuDisplay();
        }
    }

    private void Logout()
    {
        Clear();
        WriteLine("Logout complete.");
        ReadLine();
        Clear();
        UsersIdentifier user = new UsersIdentifier();
        user.UserIdentifyDisplay();
    }

    private bool Pin()
    {
        var input = ConvertPin();

        return input == UsersIdentifier.CurrentUser.Pin;
    }
    
    private int ConvertPin()
    {
        if (int.TryParse(ReadLine(), out int newpin))
        {
            return newpin;
        }
        
        throw Logger.ConvertError("Pin (string)", "int");
    }
}