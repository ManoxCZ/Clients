using ClientsMVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsMVVM.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly ClientDataService _clientDataService;
    
    private string _filter = string.Empty;
    private string Filter
    {
        get => _filter;
        set => SetProperty(ref _filter, value);
    }

    private List<ClientViewModel> _clients = [];
    public List<ClientViewModel> Clients
    {
        get => _clients;
        set => SetProperty(ref _clients, value);
    }

    private ClientViewModel? _selectedClient;
    public ClientViewModel? SelectedClient
    {
        get => _selectedClient;
        set => SetProperty(ref _selectedClient, value);
    }

    public MainWindowViewModel(ClientDataService clientDataService)
    {
        _clientDataService = clientDataService;

        Task _ = RefreshAsync();
    }

    public async Task RefreshAsync()
    {
        Clients = (await _clientDataService.GetClientsAsync())
            .Select(client => new ClientViewModel(client))
            .ToList();
    }
    
    public void AddNewClientCommand(object? parameter)
    {
        ClientViewModel newClient = new(new()
        {
            Id = Guid.NewGuid(),
            FirstName = "Klient",
            Surname = "Nový"
        });

        Clients.Add(newClient);

        SelectedClient = newClient;
    }

    public void LockCommand()
    {
        SelectedClient = null;
    }
}
