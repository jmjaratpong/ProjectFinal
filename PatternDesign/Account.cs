using static System.Console;
namespace PatternDesign;

public class Account
{
    private AccountState state;
    private List<AccountState> accountState = new List<AccountState>()
    {
        AccountState.AccountStatus,
        AccountState.ChangeName,
        AccountState.ChangeUsername,
        AccountState.ChangePassword,
        AccountState.ChangePin,
        AccountState.DeleteAccount,
        AccountState.Exit
    };
    public void UserAccountDisplay()
    {
        Clear();
        WriteLine("Select Menu...");
        for (var index = 0; index < accountState.Count; index++)
        {
            WriteLine($"{index} : {accountState[index]}");
        }

        var input = ConvertMenuSelect();
        if (input > accountState.Count - 1) throw Logger.OutOfRangeError(input, "History");
        else
        {
            var state = accountState[input];
            this.state = state;
            StateMenu();
        }
    }

    private void StateMenu()
    {
        while (true)
        {
            switch (state)
            {
                case AccountState.AccountStatus:
                    AccountStatus();
                    break;
                case AccountState.ChangeName:
                    ChangeName();
                    break;
                case AccountState.ChangeUsername:
                    ChangeUsername();
                    break;
                case AccountState.ChangePassword:
                    ChangePassword();
                    break;
                case AccountState.ChangePin:
                    ChangePin();
                    break;
                case AccountState.DeleteAccount:
                    DeletedAccount();
                    break;
                case AccountState.Exit:
                    Exit();
                    break;
            }
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

    private void AccountStatus()
    {
        Clear();
        WriteLine("Your Account Status : No Issue");
        WriteLine($"Name : {UsersIdentifier.CurrentUser.Name}\n" +
                  $"Username : {UsersIdentifier.CurrentUser.UserName}\n" +
                  $"Password : {UsersIdentifier.CurrentUser.Password}\n" +
                  $"Amount : {UsersIdentifier.CurrentUser.Money}\n" +
                  $"Pin : {UsersIdentifier.CurrentUser.Pin}");
        WriteLine("\nPress enter to exit.");
        ReadLine();
        UserAccountDisplay();
    }

    private void ChangeName()
    {
        Clear();
        while (true)
        {
            WriteLine("Enter your new name... (Empty to menu.)");
            var input = ReadLine();
            if (input.Length > 0)
            {
                if (input != UsersIdentifier.CurrentUser.Name)
                {

                    UsersIdentifier.CurrentUser.Name = input;
                    WriteLine($"Your name is {UsersIdentifier.CurrentUser.Name}.");
                    ReadLine();
                    JsonUserData.Storage();
                    UserAccountDisplay();
                }
                else
                {
                    Clear();
                    WriteLine("Can't change your name. Your name has nothing change.");
                    ReadLine();
                }
            }
            else
            {
                Clear();
                UserAccountDisplay();
            }
        }
    }

    private void ChangeUsername()
    {
        Clear();
        WriteLine("Enter your new username... (Empty to menu.)");
        var input = ReadLine();
        if (input.Length > 0)
        {
            if (input != UsersIdentifier.CurrentUser.UserName)
            {

                if (Pin())
                {
                    Clear();
                    UsersIdentifier.CurrentUser.UserName = input;
                    WriteLine($"Your username is {UsersIdentifier.CurrentUser.UserName}.");
                    ReadLine();
                    JsonUserData.Storage();
                    UserAccountDisplay();
                }
                else
                {
                    Clear();
                    WriteLine("Your pin is wrong.");
                    ReadLine();
                }
            }
            else
            {
                Clear();
                WriteLine("Can't change your username. Your username has nothing change.");
                ReadLine();
            }
        }
        else
        {
            Clear();
            UserAccountDisplay();
        }
    }

    private void ChangePassword()
    {
        Clear();
        WriteLine("Enter your new password... (Empty to menu.)");
        var input = ReadLine();
        if (input.Length > 0)
        {
            if (input != UsersIdentifier.CurrentUser.Password)
            {

                WriteLine("Please enter your pin.");
                if (Pin())
                {
                    Clear();
                    UsersIdentifier.CurrentUser.Password = input;
                    WriteLine($"Your password is {UsersIdentifier.CurrentUser.Password}.");
                    ReadLine();
                    JsonUserData.Storage();
                    UserAccountDisplay();
                }
                else
                {
                    Clear();
                    WriteLine("Your pin is wrong.");
                    ReadLine();
                }
            }
            else
            {
                Clear();
                WriteLine("Can't change your password. Your password has nothing change.");
                ReadLine();
            }
        }
        else
        {
            Clear();
            UserAccountDisplay();
        }
    }

    private void ChangePin()
    {
        Clear();
        WriteLine("Enter your new password... (Empty to menu)");
        var input = ConvertPin();
        if (input.ToString() == string.Empty)
        {
            UserAccountDisplay();
        }
        
        if (input.ToString().Length == 6)
        {
            WriteLine("Please enter your new pin.");
            if (Password())
            {
                Clear();
                UsersIdentifier.CurrentUser.Pin = input;
                WriteLine($"Your pin is {UsersIdentifier.CurrentUser.Pin}.");
                ReadLine();
                JsonUserData.Storage();
                UserAccountDisplay();
            }
            else
            {
                Clear();
                WriteLine("Your password is wrong.");
                ReadLine();
            }
        }
        else
        {
            Clear();
            WriteLine("Your new pin is not 6 number.");
            ReadLine();
        }
    }

    private void Exit()
    {
        Clear();
        Menu menu = new Menu();
        menu.UserMenuDisplay();
    }

    private void DeletedAccount()
    {
        Clear();
        WriteLine("Do you sure to delete your account? (y/n)");
        var input = ReadLine().ToLower();
        if (input == "y")
        {
            Clear();
            WriteLine("Enter your password.");
            var password = ReadLine();
            if (password == UsersIdentifier.CurrentUser.Password)
            {
                Clear();
                UserData.Remove(UsersIdentifier.CurrentUser);
                WriteLine("Deleted Account Success.");
                ReadLine();
                JsonUserData.Storage();
                UsersIdentifier user = new UsersIdentifier();
                user.UserIdentifyDisplay();
            }
            else
            {
                Clear();
                WriteLine("Your password is incorrect.");
                ReadLine();
            }
        }

        if (input == "n" || input == string.Empty)
        {
            Clear();
            UserAccountDisplay();
        }
    }
    
    private bool Pin()
    {
        var input = ConvertPin();

        if (input == UsersIdentifier.CurrentUser.Pin) return true;
        else return false;
    }
    
    private bool Password()
    {
        var input = ReadLine();

        if (input == UsersIdentifier.CurrentUser.Password) return true;
        else return false;
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