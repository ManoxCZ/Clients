using Avalonia.Metadata;
using ClientsMVVM.Models;
using ClientsMVVM.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsMVVM.ViewModels;

public class ClientViewModel : ViewModelBase
{
    private readonly ClientDataService _clientDataService;
    private readonly Client _client;

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


    public ClientViewModel(ClientDataService clientDataService, Client client)
    {
        _clientDataService = clientDataService;
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

    private bool ViewModelEqualsModel()
    {
        return _client.FirstName == FirstName &&
            _client.Surname == Surname &&
            _client.Description == Description;
    }

    public void AddNewVisitCommand(object? parameter)
    {
        Visits = Visits.Concat([NewVisit])
            .OrderByDescending(visit => visit.DateTime)
            .ToList();
        

        NewVisit = new(new());
    }

    public void SaveInfoCommand(object? parameter)
    {
        CopyToModel();

        Task _ = _clientDataService.SaveClientDataAsync(_client);

        OnPropertyChanged(nameof(FirstName));
    }

    [DependsOn(nameof(FirstName))]
    [DependsOn(nameof(Surname))]
    [DependsOn(nameof(Description))]    
    public bool CanSaveInfoCommand(object? parameter)
    {
        return !ViewModelEqualsModel();
    }
}
