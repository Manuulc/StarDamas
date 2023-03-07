using BL.Gestion;
using BL.Listados;
using DamasNamas.ViewModels.Utilidades;
using Entities;
using System.Collections.ObjectModel;

namespace DamasNamas.ViewModels
{
	[QueryProperty(nameof(JugadorQueLlega), "JugadorQueMando")]
	partial class RoomsVM : VMBase
	{
		#region Atributos


		private ObservableCollection<clsSala> listadoSalasBackup;

		private ObservableCollection<clsSala> listadoSalasMostrado;

		private clsSala salaSeleccionada;

		private string cadenaBusqueda;

		private int contJugadorArriba;

		private int contJugadorAbajo;

		private clsJugador jugadorQueLlega;

		private DelegateCommand _comandoBotonEntrarSala;

		private DelegateCommand _comandoBotonCrearSala;

		private DelegateCommand _comandoBotonBuscarSala;

		#endregion

		#region Propiedades


		public clsSala SalaSeleccionada
		{
			get
			{
				return salaSeleccionada;
			}
			set
			{
				OnPropertyChanged(nameof(ComandoBuscarSala));
				OnPropertyChanged(nameof(ComandoEntrarSala));
				salaSeleccionada = value;
				OnPropertyChanged("SalaSeleccionada");
			}
		}


		public ObservableCollection<clsSala> ListadoSalasMostrado
		{
			get
			{
				return listadoSalasMostrado;
			}
			set
			{
				listadoSalasMostrado = value;
				OnPropertyChanged("ListadoSalasMostrado");
			}
		}

		public clsJugador JugadorQueLlega
		{
			get
			{
				return jugadorQueLlega;
			}
			set
			{
				jugadorQueLlega = value;
				OnPropertyChanged("JugadorQueLlega");
			}
		}

		public String CadenaBusqueda
		{
			get
			{
				return cadenaBusqueda;
			}
			set
			{
				cadenaBusqueda = value;
				OnPropertyChanged("CadenaBusqueda");
			}
		}


		public DelegateCommand ComandoCrearSala
		{
			get
			{
				_comandoBotonCrearSala = new DelegateCommand(ComandoBotonCrearSala_Executed);
				return _comandoBotonCrearSala;
			}
		}

		public DelegateCommand ComandoEntrarSala
		{
			get
			{
				_comandoBotonEntrarSala = new DelegateCommand(ComandoBotonEntrarSala_Executed, ComandoBotonEntrarSala_CanExecute);
				return _comandoBotonEntrarSala;
			}
		}

		public DelegateCommand ComandoBuscarSala
		{
			get
			{
				_comandoBotonBuscarSala = new DelegateCommand(ComandoBotonBuscarSala_Executed, ComandoBotonBuscarSala_CanExecute);
				return _comandoBotonBuscarSala;
			}
		}

		#endregion

		#region Constructores

		public RoomsVM()
		{
			rellenar();
		}

		#endregion

		#region Comandos

		/// <summary>
		/// Metodo que se ejecuta al pulsar el boton para crear una sala nueva y entrar en ella al crearla.
		/// </summary>
		private async void ComandoBotonCrearSala_Executed()
		{
			
			//Cuando el usuario pulsa el boton crear sala, se mostrará un DisplayActionSheet para que el usuario introduzca el nombre de la sala
			//y se creará la sala en la base de datos
			var nombre = await Shell.Current.DisplayPromptAsync("Crear Sala", "Escribe el nombre de la sala a crear");
			if (nombre != null)
			{
				var registrado = false;
				foreach (var q in await clsListadoSalasBL.getSalasBL())
				{
					if (nombre.Equals(q.nombreSala))
					{
						registrado= true;
						return;
					}
				}
				if (!registrado)
				{
					try
					{
						clsSala sala = new clsSala(nombre, JugadorQueLlega.idJugador);

						clsGestionSalasBL.insertarSalaBL(sala);
						var dic = new Dictionary<string, object>();
						dic.Add("SalaEnviada", sala );
						dic.Add("EsOnline", true);
				



						await Shell.Current.GoToAsync("///Game", true,  dic);
					}
					catch (Exception e)
					{
						await Shell.Current.DisplayAlert("Los muertos de MAUI","*1000", "Ya ve rmano");
					}
					
		

					
				}
				else
				{
					await Shell.Current.DisplayAlert("Error", "El nombre de esta sala ya esta registrado", "Ok");
				}
			}





			//Recojo las salas de la base de datos para comprobar que el nombre no exista
		}

		/// <summary>
		/// Metodo que se ejecuta al pulsar el boton para entrar a la sala que hemos seleccionado. Navegaremos hacia ella pasandole
		/// el usuario y la sala.
		/// </summary>
		private async void ComandoBotonEntrarSala_Executed()
		{
			SalaSeleccionada = await clsGestionSalasBL.getSalaBL(SalaSeleccionada.codSala);
			if (SalaSeleccionada.jugadorAbajo == 0)
			{

				SalaSeleccionada.jugadorAbajo = JugadorQueLlega.idJugador;
				SalaSeleccionada.espacio = 2;
				var dic = new Dictionary<string, object>
					{
						{ "SalaEnviada", SalaSeleccionada },
						{ "EsOnline", true}
					};


				await Shell.Current.GoToAsync("///Game",true, dic);

			}
			else
			{
				await Shell.Current.DisplayAlert("Error", "La sala que intentas seleccionar está llena, por favor elige otra", "Ok");
			}
		}

		/// <summary>
		/// Funcion que comprueba que una sala esta seleccionada y activar el boton para entrar a esa sala.
		/// </summary>
		/// <returns></returns>
		private bool ComandoBotonEntrarSala_CanExecute()
		{
			bool botonEntrarSala = true;

			if (SalaSeleccionada == null)
			{
				botonEntrarSala = false;
			}

			return botonEntrarSala;
		}

		/// <summary>
		/// Funcion que comprueba si se ha introducido algun texto en el buscador para activar el boton de busqueda
		/// </summary>
		/// <returns></returns>
		private bool ComandoBotonBuscarSala_CanExecute()
		{
			bool botonBuscar = true;

			if (string.IsNullOrEmpty(CadenaBusqueda))
			{
				botonBuscar = false;
				ListadoSalasMostrado.Clear();
				ListadoSalasMostrado = new ObservableCollection<clsSala>(listadoSalasBackup);
			}

			return botonBuscar;
		}

		/// <summary>
		/// Metodo que rellena la lista de salas que se muestran con el nombre de la sala que coincida con el texto introducido en la busqueda
		/// de la sala
		/// </summary>
		private void ComandoBotonBuscarSala_Executed()
		{
			ListadoSalasMostrado.Clear();


			foreach (clsSala sala in listadoSalasBackup)
			{

				string nombreSala = sala.nombreSala;

				if (nombreSala.Contains(CadenaBusqueda))
				{
					ListadoSalasMostrado.Add(sala);
				}
			}
		}

		#endregion

		#region Metodos


		private void rellenar()
		{
			rellenarSalasMostradas();
		}


		/// <summary>
		/// Método para rellenar la listabackup y la lista de salas mostradas al usuario
		/// Esto se hace para que al usuario no le aparezcan las salas llenas en el listado
		/// </summary>
		private async void rellenarSalasMostradas()
		{
			listadoSalasBackup = await clsListadoSalasBL.getSalasBL();
			ListadoSalasMostrado = new ObservableCollection<clsSala>();

			//TODO: Usar Metoido de la dal
			//Recorremos la lista de salas
			foreach (var sala in listadoSalasBackup)
			{
				//Añado la sala a la lista mostrada si no está llena
				if (sala.espacio != 2)
				{
					ListadoSalasMostrado.Add(sala);
				}
			}

		}

		#endregion
	}
}
