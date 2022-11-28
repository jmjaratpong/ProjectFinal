namespace PatternDesign;

public class User
{
    private string name;
    private string username;
    private string password;
    private int money;
    private int pin;
    private Queue<(string, string, int, string, string)> history;


    public User(string name, string username, string password, int money, int pin)
    {
        this.name = name;
        this.username = username;
        this.password = password;
        this.money = money;
        this.pin = pin;
        history = new Queue<(string, string, int, string, string)>();
    }

    public static class Factory
    {
        public static User CreateUser(string name, string username, string password,int money, int pin)
        {
            return new User(name, username, password, money, pin);
        }
    }
    
    public string Name
    {
        get => name;
        set => name = value;
    }
    
    public string UserName
    {
        get => username;
        set => username = value;
    }
    
    public string Password
    {
        get => password;
        set => password = value;
    }

    public int Money
    {
        get => money;
        set => money = value;
    }

    public Queue<(string, string, int, string, string)> History
    {
        get => history;
        set => history = value;
    }

    public int Pin
    {
        get => pin;
        set => pin = value;
    }
}