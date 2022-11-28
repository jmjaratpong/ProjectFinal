namespace PatternDesign;

public static class UserData
{
    private static List<User> userList = new List<User>();

    public static void Add(User user)
    {
        userList.Add(user);
    }

    public static int Count()
    {
        return userList.Count;
    }

    public static void Clear()
    {
        userList.Clear();
    }

    public static void Remove(User user)
    {
        userList.Remove(user);
    }

    public static List<User> UserList
    {
        get => userList;
        set => userList = value;
    } 
}