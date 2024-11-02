using ClientsMVVM.Models;
using System;

namespace ClientsMVVM.ViewModels;

public class ClientVisitViewModel : ViewModelBase
{
    public readonly ClientVisit ClientVisit;

    private DateTimeOffset _dateTime = DateTimeOffset.Now;
    public DateTimeOffset DateTime 
    {
        get => _dateTime;
        set => SetProperty(ref _dateTime, value);
    }
    private string? _text;
    public string? Text 
    {
        get => _text;
        set => SetProperty(ref _text, value); 
    }

    public ClientVisitViewModel(ClientVisit clientVisit)
    {
        ClientVisit = clientVisit;

        CopyFromModel();
    }

    private void CopyFromModel()
    {
        DateTime = ClientVisit.DateTime;
        Text = ClientVisit.Text;
    }

    private void CopyToModel()
    {
        ClientVisit.DateTime = DateTime;
        ClientVisit.Text = Text;
    }
}
