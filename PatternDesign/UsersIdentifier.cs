using static System.Console;
namespace PatternDesign;

public class UsersIdentifier
{
    private Menu menu;
    private static User currentUser = null;
    IdentifyState? state;

    private List<IdentifyState> identifyState = new List<IdentifyState>()
    {
        IdentifyState.Register,
        IdentifyState.Login,
        IdentifyState.ForgotPassword,
    };

    public void UserIdentifyDisplay()
    {
        Clear();
        JsonUserData.Storage();
        WriteLine("Welcome to PayPally!\n");
        WriteLine("Select your Identify.");
        for (var i = 0; i < identifyState.Count; i++)
        {
            WriteLine($"{i} : {identifyState[i]}");
        }

        int input = SelectMenu();
        
        if(input > identifyState.Count - 1) throw Logger.OutOfRangeError(input, "Identify");
        else
        {
            var state = identifyState[input];
            this.state = state;
            StateMenu();
        }
    }
    
    private int SelectMenu()
    {
        if (int.TryParse(ReadLine(), out int number))
        {
            return number;
        }
        throw Logger.ConvertError("MenuInput (string)", "int");
    }

    private void StateMenu()
    {
        Init();
        while (true)
        {
            switch (state)
            {
                case IdentifyState.Register:
                    Register();
                    break;
                case IdentifyState.Login:
                    Login();
                    break;
                case IdentifyState.LoginError:
                    LoginError();
                    break;
                case IdentifyState.ForgotPassword:
                    ForgotPassword();
                    break;
                case IdentifyState.Successes:
                    Successes();
                    break;
            }
        }
    }

    private void Init()
    {
        menu = new Menu();
    }

    private void Register()
    {
        Clear();
        WriteLine("Register.\n");
        WriteLine("Input your name...");
        var newname = ReadLine();
        WriteLine("Input your Username...");
        var newusername = ReadLine();
        WriteLine("Input your password...");
        var newpassword = ReadLine();
        WriteLine("Input your start money...");
        var newmoney = ConvertMoney();
        WriteLine("Input your new pin (Must be 6 number)");
        var newpin = ConvertPin();
        if (newname.Length > 0
            && newusername.Length > 0 
            && newpassword.Length > 0
            && newmoney > 0)
        {
            var newUser = User.Factory.CreateUser (newname, newusername, newpassword, newmoney, newpin);
            if (UserData.Count() > 0)
            {
                foreach (var user in UserData.UserList)
                {
                    if (!user.UserName.Contains(newusername))
                    {
                        Clear();
                        UserData.UserList.Add(newUser);
                        currentUser = newUser;
                        WriteLine("Information complete.");
                        ReadLine();
                        JsonUserData.Storage();
                        UserIdentifyDisplay();
                    }
                    else
                    {
                        Clear();
                        WriteLine($"This username is already register.");
                        ReadLine();
                    }
                }
            }
            else
            {
                Clear();
                UserData.UserList.Add(newUser);
                currentUser = newUser;
                WriteLine("Information complete.");
                ReadLine();
                JsonUserData.Storage();
                UserIdentifyDisplay();
            }
        }
        else
        {
            Clear();
            WriteLine("Information incorrect! Try again.");
            ReadLine();
        }
            
    }

    private int ConvertMoney()
    {
        if (int.TryParse(ReadLine(), out int newmoney))
        {
            return newmoney;
        }

        throw Logger.ConvertError("Money (string)", "int");
    }

    private int ConvertPin()
    {
        if (int.TryParse(ReadLine(), out int newpin))
        {
            return newpin;
        }
        
        throw Logger.ConvertError("Pin (string)", "int");
    }

    private void Login()
    {
        Clear();
        WriteLine("Login.\n");
        if (UserData.Count() > 0)
        {
            WriteLine("Input your Username...");
            var username = ReadLine();
            foreach (var user in UserData.UserList)
            {
                if (user.UserName.Contains(username))
                {
                    currentUser = user;
                    WriteLine("Input your Password...");
                    var password = ReadLine();
                    if (password == currentUser.Password)
                    {
                        state = IdentifyState.Successes;
                        return;
                    }
                    else
                    {
                        state = IdentifyState.LoginError;
                    }

                    return;
                }
                else state = IdentifyState.LoginError;
            }
        }
        else
        {
            WriteLine("No account found! Please register.");
            ReadLine();
            UserIdentifyDisplay();
        }
    }

    private void LoginError()
    {
        Clear();
        WriteLine("Your Username or password incorrect!");
        WriteLine("Your new? Do you want to Register? (y/n)");
        var i = ReadLine().ToLower();
        state = (i == "y") ? IdentifyState.Register : IdentifyState.Login;
    }

    private void ForgotPassword()
    {
        Clear();
        if (UserData.Count() > 0)
        {
            while (true)
            {
                Clear();
                WriteLine("Enter you username to reset your password.");
                var username = ReadLine();
                foreach (var user in UserData.UserList)
                {
                    if (username == user.UserName)
                    {
                        while (true)
                        {
                            currentUser = user;
                            
                            Clear();
                            WriteLine("Reset your password.");
                            var password = ReadLine();
                            
                            Clear();
                            WriteLine("Confirm your password.");
                            var confirmPassword = ReadLine();
                            
                            Clear();
                            WriteLine("Enter your pin.");
                            var pin = ConvertPin();
                            
                            if (password.Equals(confirmPassword) && password.Length > 0 && confirmPassword.Length > 0)
                            {
                                if (pin == currentUser.Pin)
                                {
                                    currentUser.Password = password;
                                    WriteLine("Reset password complete.");
                                    ReadLine();
                                    UserIdentifyDisplay();
                                }
                                else
                                {
                                    Clear();
                                    WriteLine("Your pin is incorrect.");
                                    ReadLine();
                                }
                            }
                            else
                            {
                                Clear();
                                WriteLine("Your password Doesn't match. Try Again");
                                ReadLine();
                            }
                            
                        }
                    }
                    else
                    {
                        Clear();
                        WriteLine("Can't Find your email, Try Again");
                        ReadLine();
                    }
                }
            }
        }
        else
        {
            WriteLine("No Account found! Please register.");
            ReadLine();
            UserIdentifyDisplay();
        }

    }

    private void Successes()
    {
        Clear();
        WriteLine($"Welcome! {currentUser.Name}.");
        WriteLine("Please Input your pin");
        var pin = ConvertPin();
        while (true)
        {
            if (pin.ToString().Length == 6 && currentUser.Pin == pin)
            {
                menu.UserMenuDisplay();
            }
            else
            {
                Clear();
                WriteLine("Your pin is incorrect! Please try again.");
                ReadLine();
                return;
            }
        }
    }

    public static User CurrentUser
    {
        get => currentUser;
        set => currentUser = value;
    }
}