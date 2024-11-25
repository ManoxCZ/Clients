using ClientsMVVM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using static CommunityToolkit.Mvvm.ComponentModel.__Internals.__TaskExtensions.TaskAwaitableWithoutEndValidation;

namespace ClientsMVVM.Services;

public class ClientDataService
{
    private readonly HashSet<Guid> _lockedGuids = new();
    private readonly object _lock = new();
    private readonly string _clientsDataFolder;

    public ClientDataService()
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string clientsPath = Path.Combine(appDataPath, "Clients");               
        _clientsDataFolder = Path.Combine(clientsPath, "Data");

        if (!Directory.Exists(clientsPath))
        {
            Directory.CreateDirectory(clientsPath);
        }

        if (!Directory.Exists(_clientsDataFolder))
        {
            Directory.CreateDirectory(_clientsDataFolder);
        }
    }

    public async Task<List<Client>> GetClientsAsync()
    {
        List<Client> clients = new();

        foreach (string filepath in Directory.GetFiles(_clientsDataFolder))
        {
            if (Path.GetFileNameWithoutExtension(filepath) is string idString &&
                Guid.TryParse(idString, out Guid id))
            {
                try
                {
                    await LockFile(id);

                    if (await ReadFileAsync(id) is Client client)
                    {
                        clients.Add(client);
                    }
                }
                finally
                {
                    await UnlockFile(id);
                }
            }
        }

        return clients;
    }

    public async Task SaveClientDataAsync(Client client)
    {
        try
        {
            await LockFile(client.Id);

            await WriteFileAsync(client);
        }
        finally
        {
            await UnlockFile(client.Id);
        }
    }

    private async Task LockFile(Guid id)
    {
        while (true)
        {
            lock (_lock)
            {
                if (!_lockedGuids.Contains(id))
                {
                    _lockedGuids.Add(id);

                    return;
                }                
            }

            await Task.Delay(100);
        }
    }

    private Task UnlockFile(Guid id)
    {        
        lock (_lock)
        {
            if (_lockedGuids.Contains(id))
            {
                _lockedGuids.Remove(id);
            }
        }

        return Task.CompletedTask;
    }

    private async Task<Client?> ReadFileAsync(Guid id)
    {
        using StreamReader reader = new(Path.Combine(_clientsDataFolder, id.ToString()));

        return await JsonSerializer.DeserializeAsync<Client>(reader.BaseStream);
    }

    private async Task WriteFileAsync(Client client)
    {
        using StreamWriter writer = new (Path.Combine(_clientsDataFolder, client.Id.ToString()));
        
        await JsonSerializer.SerializeAsync(writer.BaseStream, client);
    }
}
