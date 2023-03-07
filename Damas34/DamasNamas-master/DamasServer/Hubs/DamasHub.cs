using Microsoft.AspNetCore.SignalR;
using Entities;
using DamasServer.Models;

namespace DamasServer.Hubs
{
	public class DamasHub : Hub
	{
        public async Task MandarMovimiento (int idJugador, Square huecoPartida ,Square huecoAComer, Square huecoDestino, EstadosJuego estadoPartida)
        {
            await Clients.User(idJugador.ToString()).SendAsync("RecibirMovimiento", huecoPartida, huecoAComer, huecoDestino, estadoPartida);
        }
    }
}
