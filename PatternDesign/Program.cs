using static System.Console;

namespace PatternDesign
{

    public class Program
    {
        static void Main()
        {
            UserData.Clear();
            if (!File.Exists(@"JsonUserData.json")) JsonUserData.Storage();
            JsonUserData.Read();
            UsersIdentifier user = new UsersIdentifier();
            user.UserIdentifyDisplay();
        }
    }
}