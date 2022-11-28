using Newtonsoft.Json;

namespace PatternDesign;

public static class JsonUserData
{
    public static Action Storage = () => OnSave();
    public static Action Read = () => Onload();
    
    private static void OnSave()
    {
        var jsonfiles = JsonConvert.SerializeObject(UserData.UserList);
        File.WriteAllText(@"JsonUserData.json",jsonfiles);
    }

    private static void Onload()
    {
        var usersdata = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"JsonUserData.json"));
        foreach (var user in usersdata)
        {
            UserData.Add(user);
        }
    }
}