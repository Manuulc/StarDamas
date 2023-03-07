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

namespace DamasNamas.ViewModels
{
	partial class TablaPuntuacionesVM : ObservableObject
	{

		#region Atributos

		private ObservableCollection<clsSala> listaSalas;
		private ObservableCollection<clsSala> jugadoresVelocistas;
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
		public ObservableCollection<clsSala> JugadoresVelocistas
		{
			get
			{
				return jugadoresVelocistas;
			}
			set
			{
				jugadoresVelocistas = value;
				OnPropertyChanged(nameof(JugadoresVelocistas));
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

			try
			{
				
				var listaJugadores = await clsListadoJugadoresBL.getJugadoresBL();
				var listaSalas = await clsListadoSalasBL.getSalasBL();

				foreach (var player in listaJugadores)
				{
					
					var listaPartidas = new List<clsSala>();
					for (int i = 0; i<listaSalas.Count(); i++)
					{
						var cantidadFichasArriba = listaSalas.ElementAt(i).cantidadFichasArriba;
						var cantidadFichasAbajo = listaSalas.ElementAt(i).cantidadFichasAbajo;
						var jugadorArribaSala = listaSalas.ElementAt(i).jugadorArriba;
						var jugadorAbajoSala = listaSalas.ElementAt(i).jugadorAbajo;

						if(jugadorArribaSala== player.idJugador || jugadorAbajoSala == player.idJugador)
						{
							listaPartidas.Add(listaSalas.ElementAt(i));
						}
						
					}
					var jugador = new clsJugadorConPartidas(player.nombre, player.password, listaPartidas);

					JugadoresBuenisimos.Add(jugador);

				}

			}
			catch (Exception e)
			{
				await Shell.Current.DisplayAlert("ERROR", "Se ha producido un error al obtener las salas de la API", "Po vale");
			}


		}



		/// <summary>
		/// Metodo para cargar la lista de jugadores ordenada por el tiempo de la partida. Aqui solo se ven los mas rapidos
		/// </summary>
		private async void CargarJugadoresVelocistas()
		{

			try
			{
				listaSalas = await clsListadoSalasBL.getSalasBL();
				jugadoresVelocistas = new ObservableCollection<clsSala>(listaSalas.OrderBy(p => p.tiempo));

			}
			catch (Exception e)
			{
				await Shell.Current.DisplayAlert("ERROR", "Se ha producido un error al obtener las salas de la API", "Po vale");
			}


		}



















	}
}
