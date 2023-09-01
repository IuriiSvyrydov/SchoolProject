using System.Globalization;

namespace SchoolProject.Data.Commons;

public  class GeneralLocalizableEntity
{
    public  string Localized(string textUS, string textUA)
    {
        CultureInfo culture = Thread.CurrentThread.CurrentCulture;
        if (culture.TwoLetterISOLanguageName.ToLower().Equals("uk)"))
            return textUA;
        return textUS;
    }
}
