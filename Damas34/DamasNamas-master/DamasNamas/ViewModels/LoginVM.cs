using BL.Gestion;
using BL.Listados;
using CommunityToolkit.Mvvm.ComponentModel;
using DamasNamas.ViewModels.Utilidades;
using Entities;
using System.Collections.ObjectModel;

namespace DamasNamas.ViewModels
{
	partial class LoginVM : VMBase
	{
		#region Atributos

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(ComandoLogin))]
		[NotifyPropertyChangedFor(nameof(ComandoSignup))]
		String username;

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(ComandoLogin))]
		[NotifyPropertyChangedFor(nameof(ComandoSignup))]
		String password;

		clsJugador Jugador;

		private DelegateCommand _comandoLogin;
		private DelegateCommand _comandoSignup;

		#endregion

		#region Propiedades


		public DelegateCommand ComandoLogin
		{
			get
			{
				_comandoLogin = new DelegateCommand(ComandoLogin_Execute, ComandoLogin_CanExecute);
				return _comandoLogin;
			}
		}

		public DelegateCommand ComandoSignup
		{
			get
			{
				_comandoSignup = new DelegateCommand(ComandoSignup_Execute, ComandoSignup_CanExecute);
				return _comandoSignup;
			}
		}

		#endregion

		#region Constructores

		public LoginVM()
		{
			IsBusy = false;
			Username = "";
			Password = "";
		}

		#endregion

		#region Comandos

		/// <summary>
		/// Metodo que se acciona al pulsar el boton de Log in, comprobara si podemos logearnos en la aplicacion, en caso de logearnos, navegaremos hacia la
		/// siguiente pagina, en caso contrario, aparecera un mensaje diciendo que el usuario no esta registrado.
		/// </summary>
		private async void ComandoLogin_Execute()
		{

			if ((Jugador = await TestLogin(Username, Password)) != null)
			{

				GoTo(Jugador);

			}
			else
			{
				await App.Current.MainPage.DisplayAlert("Imposible loggear", "Usuario o contraseña incorrectos", "Ok");

			}



		}



		/// <summary>
		/// Metodo que se acciona al comprobar que si podemos iniciar sesion. Navegamos hacia la siguiente pagina mandando el jugador logeado
		/// </summary>
		private async void GoTo(clsJugador jugadorAMandar)
		{
			var dict = new Dictionary<string, object>();
			dict.Add("JugadorQueMando", jugadorAMandar);
			await Shell.Current.GoToAsync("///Main", true, dict);
		}

		/// <summary>
		/// Metodo que comprueba si el nombre y el usuario estan vacios para activar el boton de logeo o no
		/// </summary>
		/// <returns> bool </returns>
		private bool ComandoLogin_CanExecute()
		{
			bool botonLogin = true;

			if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
			{

				botonLogin = false;

			}

			return botonLogin;
		}

		/// <summary>
		/// Metodo que se acciona al pulsar el boton de Sign Up, cuando se pulse, navegaremos hacia la siguiente pagina
		/// </summary>
		private async void ComandoSignup_Execute()
		{

			Jugador = await TestSignUp(Username, Password);

		}



		/// <summary>
		/// Metodo que comprueba si el nombre y el usuario estan vacios para activar el boton de logeo o no
		/// </summary>dddddd
		/// <returns> bool </returns>
		private bool ComandoSignup_CanExecute()
		{
			bool botonSignUp = true;

			if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
			{

				botonSignUp = false;

			}

			return botonSignUp;
		}

		#endregion

		#region Utilities

		/// <summary>
		/// Metodo que comprueba si podemos logearnos en la app o no. Obtenemos una lista completa de jugadores de la API, si el nombre y el usuario introducidos en los
		/// entrys coinciden con el nombre y la contraseña de alguno de los usuarios, se logeara con exito en la aplicacion.
		/// </summary>
		/// <returns></returns>
		public static async Task<clsJugador> TestLogin(String Username, String Password)
		{
			clsJugador JugadorALoggear = null;
			bool logeadoConExito = false;
			try
			{
				ObservableCollection<clsJugador> jugadores = await clsListadoJugadoresBL.getJugadoresBL();

				for (int i = 0; i < jugadores.Count && !logeadoConExito; i++)
				{
					if (jugadores[i].nombre.ToUpper().Equals(Username.ToUpper()) && jugadores[i].password.Equals(Password))
					{
						JugadorALoggear = jugadores[i];
						logeadoConExito = true;

					}
				}
				if (!logeadoConExito)
				{
					await Shell.Current.DisplayAlert("Error", "Usuario o contraseña incorrectos", "Ok");
				}
			}
			catch (Exception e)
			{
				await Shell.Current.DisplayAlert("ERROR", "Se ha producido un error al obtener jugadores de la API", "Po vale");
			}

			return JugadorALoggear;
		}

		/// <summary>
		/// Metodo que comprueba si podemos registrarnos en la app o no. Obtenemos una lista completa de los jugadores, si el nombre escrito en el entry
		// coincide con alguno de los jugadores rescatados de la API, significa que existe y por lo tanto si existe aparecera un mensaje diciendonos que el jugador ya existe. 
		// Si no existe, lo introducira en la API para registrarlo.
		/// </summary>
		public static async Task<clsJugador> TestSignUp(String Username, String Password)
		{
			clsJugador jugador = null;
			bool existe = false;

			try
			{
				ObservableCollection<clsJugador> jugadores = await clsListadoJugadoresBL.getJugadoresBL();

				for (int i = 0; i < jugadores.Count && !existe; i++)
				{
					if (jugadores[i].nombre.ToUpper().Equals(Username.ToUpper()))
					{
						existe = true;
					}
				}

				if (existe)
				{
					await Shell.Current.DisplayAlert("Jugador ya existente", "Lo siento, ese jugador ya existe", "Ok");
				}
				else
				{

					clsJugador jugadorRegistrado = new clsJugador(Username, Password);

					try
					{

						clsGestionJugadoresBL.insertarJugadorBL(jugadorRegistrado);
					
						jugador = jugadorRegistrado;

					}
					catch (Exception e)
					{
						await Shell.Current.DisplayAlert("ERROR", "Se ha producido un error al insertar en la API", "Po vale");
					}

				}

			}
			catch (Exception e)
			{
				await Shell.Current.DisplayAlert("ERROR", "Se ha producido un error al obtener jugadores de la API", "Po vale");
			}

			return jugador;
		}
		#endregion
	}
}
