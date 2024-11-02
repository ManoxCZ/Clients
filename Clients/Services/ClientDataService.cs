using ClientsMVVM.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientsMVVM.Services;

public class ClientDataService
{
    public Task<List<Client>> GetClientsAsync()
    {
        return Task.Run(() => 
        {
            return new List<Client>([
                new Client() { Id = Guid.NewGuid(), FirstName = "Marek", Surname = "Novosad" },
                new Client() { Id = Guid.NewGuid(), FirstName = "Robert", Surname = "Růžička" },
                new Client() { Id = Guid.NewGuid(), FirstName = "Dagmar", Surname = "Hlaváčková" },
                ]);
        });
    }

    public Task SaveClientDataAsync(Client client)
    {
        return Task.CompletedTask;
    }
}
