using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Listados;
using BL.Gestion;
using System.Collections.ObjectModel;
using Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using DamasNamas.Models;
using DamasNamas.ViewModels.Utilidades;

namespace DamasNamas.ViewModels
{
	partial class TablaPuntuacionesVM : VMBase
	{

		#region Atributos

		private ObservableCollection<clsSala> listaSalas;
		private ObservableCollection<clsJugadorConPartidas> jugadoresBuenisimos;

		#endregion


		#region Propiedades

		public ObservableCollection<clsSala> ListaSalas
		{
			get
			{
				return listaSalas;
			}
			set
			{
				listaSalas = value;
			}
		}

		public ObservableCollection<clsJugadorConPartidas> JugadoresBuenisimos
		{
			get
			{
				return jugadoresBuenisimos;
			}
			set
			{
				jugadoresBuenisimos = value;
				OnPropertyChanged(nameof(JugadoresBuenisimos));
			}
		}

		#endregion

		#region Constructores

		public TablaPuntuacionesVM()
		{
			JugadoresBuenisimos = new ObservableCollection<clsJugadorConPartidas>();
			CargarJugadoresBuenisimos();
			//CargarJugadoresVelocistas();
		}


		#endregion


		/// <summary>
		/// Metodo para rellenar la lista con los jugadores con mas partidas ganadas en las damas
		/// </summary>
		private async void CargarJugadoresBuenisimos()
		{
			var listAuxjugadoresBuenisimos = new ObservableCollection<clsJugadorConPartidas>();

			try
			{
				
				var listaJugadores = await clsListadoJugadoresBL.getJugadoresBL();
				var listaSalas = await clsListadoSalasBL.getSalasBL();

				foreach (var player in listaJugadores)
				{
					if (player.idJugador!=0)
					{
						var listaPartidas = new List<clsSala>();
						for (int i = 0; i<listaSalas.Count(); i++)
						{
							var cantidadFichasArriba = listaSalas.ElementAt(i).cantidadFichasArriba;
							var cantidadFichasAbajo = listaSalas.ElementAt(i).cantidadFichasAbajo;
							var jugadorArribaSala = listaSalas.ElementAt(i).jugadorArriba;
							var jugadorAbajoSala = listaSalas.ElementAt(i).jugadorAbajo;

							if (jugadorArribaSala== player.idJugador || jugadorAbajoSala == player.idJugador)
							{
								listaPartidas.Add(listaSalas.ElementAt(i));
							}

						}
						var jugador = new clsJugadorConPartidas(player.idJugador,player.nombre, player.password, listaPartidas);

						listAuxjugadoresBuenisimos.Add(jugador);
					}
				}

			}
			catch (Exception e)
			{
				await Shell.Current.DisplayAlert("ERROR", "Se ha producido un error al obtener las salas de la API", "Po vale");
			}

			var listaAux = listAuxjugadoresBuenisimos.OrderByDescending(x => x.partidasGanadas);
			JugadoresBuenisimos = new ObservableCollection<clsJugadorConPartidas>(listaAux);

		}





















	}
}
