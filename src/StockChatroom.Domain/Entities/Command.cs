using StockChatroom.Domain.Enums;

namespace StockChatroom.Domain.Entities;

public class Command
{
    public CommandKind Kind { get; set; }
    public string Value { get; set; }

    public Command(string commandLine)
    {
        ParseCommandLine(commandLine);
    }

    private void ParseCommandLine(string commandLine)
    {
        if (!commandLine.StartsWith("/"))
        {
            throw new ArgumentException("A command needs to start with /");
        }

        var separatedCommand = commandLine[1..].Split('=');

        if (!Enum.TryParse(typeof(CommandKind), separatedCommand[0], true, out var kind))
        {
            throw new ArgumentException("Command not recognized");
        }

        Kind = (CommandKind)kind;
        Value = separatedCommand[1];
    }
}
