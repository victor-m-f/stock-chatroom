using System.Net;

namespace StockChatroom.Application.UseCases;

public class OutputBase
{
    public bool IsValid { get; protected set; }
    public HttpStatusCode HttpStatusCode { get; protected set; }
    public List<string> Errors { get; }
    public bool IsInvalid => !IsValid;
    public int StatusCode => (int)HttpStatusCode;

    public OutputBase(bool isValid, HttpStatusCode httpStatusCode)
    {
        IsValid = isValid;
        HttpStatusCode = httpStatusCode;
        Errors = new List<string>();
    }

    public OutputBase(HttpStatusCode httpStatusCode)
    {
        IsValid = true;
        HttpStatusCode = httpStatusCode;
    }

    public void AddError(params string[] errorMessages)
    {
        foreach (var errorMessage in errorMessages)
        {
            Errors.Add(StandartizeErrorMessage(errorMessage));
        }
    }

    private static string StandartizeErrorMessage(string errorMessage)
    {
        errorMessage = errorMessage.Trim();

        if (!errorMessage.EndsWith("."))
        {
             errorMessage += ".";
        }

        return errorMessage;
    }
}
