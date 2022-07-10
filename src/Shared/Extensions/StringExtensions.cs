namespace StockChatroom.Shared.Extensions;

public static class StringExtensions
{
    public static string SeparateCamelCase(this string input) =>
    new (input.Take(1).Concat(
        InsertSpacesBeforeCaps(input.Skip(1)))
    .ToArray());

    public static string RemoveText(this string input, string stringToRemove)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        var startIndex = input.IndexOf(stringToRemove, StringComparison.InvariantCulture);

        return startIndex == -1 ? input : input.Remove(startIndex, stringToRemove.Length);
    }

    private static IEnumerable<char> InsertSpacesBeforeCaps(IEnumerable<char> input)
    {
        foreach (var c in input)
        {
            if (char.IsUpper(c))
            {
                yield return ' ';
            }

            yield return c;
        }
    }
}
