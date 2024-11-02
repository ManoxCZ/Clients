using ClientsMVVM.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClientsMVVM.ViewModels;

public class ClientViewModel : ViewModelBase
{
    public readonly Client _client;

    private string _firstName = null!;
    private string _surname = null!;
    private string _description = null!;

    
    public string FirstName 
    { 
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }
    public string Surname 
    { 
        get => _surname;
        set => SetProperty(ref _surname, value); 
    }
    public string Description 
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public ClientVisitViewModel _newVisit = new(new());
    public ClientVisitViewModel NewVisit
    {
        get => _newVisit;
        set => SetProperty(ref _newVisit, value);
    }

    public List<ClientVisitViewModel> _visits = [];
    public List<ClientVisitViewModel> Visits 
    {
        get => _visits;
        set => SetProperty(ref _visits, value);
    }


    public ClientViewModel(Client client)
    {
        _client = client;

        CopyFromModel();
    }

    private void CopyFromModel()
    {
        FirstName = _client.FirstName;
        Surname = _client.Surname;
        Description = _client.Description;
        Visits = _client.Visits.Select(visit => new ClientVisitViewModel(visit)).ToList();
    }

    private void CopyToModel()
    {
        _client.FirstName = FirstName;
        _client.Surname = Surname;
        _client.Description = Description;
        _client.Visits = Visits.Select(visit => visit.ClientVisit).ToList();
    }

    public void AddNewVisitCommand(object? parameter)
    {
        Visits = Visits.Concat([NewVisit])
            .OrderByDescending(visit => visit.DateTime)
            .ToList();

        NewVisit = new(new());
    }
}
