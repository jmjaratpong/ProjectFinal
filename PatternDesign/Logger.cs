namespace PatternDesign;

public static class Logger
{
    public static Exception ConvertError(string type, string expect)
    {
        throw new Exception($"Can't Convert {type} to {expect}.");
    }

    public static Exception OutOfRangeError(int index, string topic)
    {
        throw new Exception($"{index} is out of {topic} range");
    }

    public static Exception NullError(string input)
    {
        throw new Exception($"{input} is empty.");
    }
}