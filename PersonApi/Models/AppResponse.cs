using System.Globalization;

namespace PersonApi.Models;

public class AppResponse
{
    public bool Ok { get; private set; } = true;
    public List<string> Messages { get; private set; } = new();
    public void AddError(string message, params object[] args)
    {
        Ok = false;
        Messages.Add(string.Format(CultureInfo.CurrentCulture, message, args));
    }
}
