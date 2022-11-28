using static System.Console;

namespace PatternDesign;

public class History
{
    private List<HistoryState> historyStates = new List<HistoryState>()
        { HistoryState.ShowHistory, HistoryState.Exit};
    private HistoryState? state = null;
    
    public void UserHistoryDisplay()
    {
        Clear();
        WriteLine("Your history.\n");
        WriteLine("Select your menu.");
        for (int i = 0; i < historyStates.Count(); i++)
        {
            WriteLine($"{i} : {historyStates[i]}");
        }
        
        int input = SelectMenu();
        if (input > historyStates.Count - 1) throw Logger.OutOfRangeError(input, "History");
        else
        {
            var state = historyStates[input];
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
        while (true)
        {
            switch (state)
            {
                case HistoryState.ShowHistory:
                    ShowHistory();
                    break;
                case HistoryState.Exit:
                    Exit();
                    break;
            }
        }
    }

    private void ShowHistory()
    {
        Clear();
        foreach (var history in UsersIdentifier.CurrentUser.History)
        {
            switch (history.Item2)
            {
                case "Withdraw":
                    WriteLine($"Action : {history.Item2} | Amount : {history.Item3} | DateTime : {history.Item5}");
                    break;
                case "Deposit":
                    WriteLine($"Action : {history.Item2} | Amount : {history.Item3} | DateTime : {history.Item5}");
                    break;
                case "Transfer":
                    WriteLine(
                        $"From : {history.Item2} | Action : {history.Item2} | Amount : {history.Item3} | To : {history.Item4} | DateTime : {history.Item5}");
                    break;
                case "Earn":
                    WriteLine(
                        $"Action : {history.Item2} | Amount : {history.Item3} | From : {history.Item4} | DateTime : {history.Item5}");
                    break;
            }
        }
        WriteLine("Press enter to exit");
        ReadLine();
        Clear();
        state = HistoryState.Exit;
    }

    private void Exit()
    {
        Clear();
        Menu menu = new Menu();
        menu.UserMenuDisplay();
    }
}