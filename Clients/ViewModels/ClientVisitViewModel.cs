using Clients.Models;
using System;

namespace Clients.ViewModels;

public class ClientVisitViewModel : ViewModelBase
{
    public readonly ClientVisit ClientVisit;

    public DateTimeOffset DateTime
    {
        get => ClientVisit.DateTime;
        set
        {
            DateTimeOffset dateTime = ClientVisit.DateTime;

            if (SetProperty(ref dateTime, value))
            {
                ClientVisit.DateTime = dateTime;
            }
        }
    }
    public string? Text
    {
        get => ClientVisit.Text;
        set
        {
            string? text = ClientVisit.Text;

            if (SetProperty(ref text, value))
            {
                ClientVisit.Text = text;
            }
        }
    }

    public ClientVisitViewModel(ClientVisit clientVisit)
    {
        ClientVisit = clientVisit;    
    }    
}
