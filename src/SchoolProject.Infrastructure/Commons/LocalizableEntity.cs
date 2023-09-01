using System.Globalization;

namespace SchoolProject.Infrastructure.Commons;

public class LocalizableEntity
{
    public string NameUa { get; set; }
    public string NameUs { get; set; }

    public string GetLocalized()
    {
        CultureInfo culture = Thread.CurrentThread.CurrentCulture;
        if (culture.TwoLetterISOLanguageName.ToLower().Equals("uk"))
            return NameUa;
        return NameUs;
    }
}
