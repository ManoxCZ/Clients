using Avalonia.Data.Converters;
using Clients.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Clients.Converters;

internal class ClientsFilterConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count >= 2 &&
            values[0] is IEnumerable<ClientViewModel> clients &&
            values[1] is string filter)
        {
            return clients
                .Where(client =>
                    string.IsNullOrEmpty(filter) ||
                    RemoveDiacritics(client.FirstName).Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
                    RemoveDiacritics(client.Surname).Contains(filter, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(client => client.Surname + ' ' + client.FirstName)
                .ToList();
        }

        return null;
    }

    private static string RemoveDiacritics(string text)
    {
        string formD = text.Normalize(NormalizationForm.FormD);

        StringBuilder sb = new();

        foreach (char ch in formD)
        {
            UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);

            if (uc != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(ch);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
}
