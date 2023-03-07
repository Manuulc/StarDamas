using BL.Gestion;
using BL.Listados;
using DamasNamas.ViewModels.Utilidades;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamasNamas.ViewModels
{
	[QueryProperty(nameof(JugadorLoggeado), "JugadorQueMando")]
	public class EleccionModoJuegoVM
	{

		private clsJugador jugadorLoggeado;
		private DelegateCommand _comandoLocal;
		private DelegateCommand _comandoOnline;

		public clsJugador JugadorLoggeado
		{
			get
			{
				return jugadorLoggeado;
			}
			set
			{
				jugadorLoggeado = value;

			}
		}
		public DelegateCommand ComandoLocal
		{
			get
			{
				_comandoLocal = new DelegateCommand(ComandoLocal_Execute);
				return _comandoLocal;
			}
		}

		public DelegateCommand ComandoOnline
		{
			get
			{
				_comandoOnline = new DelegateCommand(ComandoOnline_Execute);
				return _comandoOnline;
			}
		}

		public EleccionModoJuegoVM()
		{

		}

		/// <summary>
		/// Metodo que se acciona al pulsar el boton de Sign Up, cuando se pulse, navegaremos hacia la siguiente pagina
		/// </summary>
		private async void ComandoLocal_Execute()
		{

			String nombreSala = "";
			clsSala sala = new clsSala(JugadorLoggeado.idJugador);
			//Recogemos el jugador que se ha loggeado y lo establecemos como jugador de arriba
			//luego creamos el diccionario que se mandará a la siguiente página y comprobaremos si el segundo jugador quiere loggearse
			sala.jugadorArriba = JugadorLoggeado.idJugador;
			var dic = new Dictionary<string, object>();
			int idAbajo = 0;
			var respuesta = await Shell.Current.DisplayActionSheet("Desea identificarte?", "Cancel", null, "Log in", "Sign up");
			if (respuesta == null)
				respuesta = "Cancel";
			if (respuesta.Equals("Cancel"))
			{
				dic.Add("SalaEnviada", sala);
			}

			//Si decide que quiere loggearse, siempre con total seguridad.
			//Se le pedirán sus datos de inicio de sesión de la manera más segura que se puede mediante promptasync
			else if (respuesta.Equals("Log in"))
			{
				string pass = null;
				string name = await Shell.Current.DisplayPromptAsync("Identificate", "nombre de usuario", "Ok", "Cancel");
				if (name == null)
					name = "Cancel";
				if (!name.Equals("Cancel"))
				{

					pass = await Shell.Current.DisplayPromptAsync("Identificate", ("contraseña"));
					if (pass == null)
						pass = "Cancel";
					if (!pass.Equals("Cancel"))
					{
						//Comprobamos que su usuario y contraseña sean correctos
						var jugadorAbajo = await LoginVM.TestLogin(name, pass);
						if (jugadorAbajo != null)
						{
							sala.jugadorAbajo = jugadorAbajo.idJugador;
						}
						else
						{
							return;
						}
						//Añadimos la sala que se enviará a la página del juego
						dic.Add("SalaEnviada", sala);
					}

				}



			}
			//Si el usuario invitado decide crear una cuenta, se le peirá ingresar un nombre y contraseña 
			//de nuevo, de la forma mas segura que hemos encontrado usando promptAsync y sin recurrir a abrir un nuevo loggin
			else if (respuesta.Equals("Sign up"))
			{
				//Comprobamos que quiera seguir con el proceso
				var salir = false;

				clsJugador jugadorAbajo = null;
				string pass = "";
				string name = await Shell.Current.DisplayPromptAsync("Identificate", "nombre de usuario", "Ok", "Cancel");
				if (name == null)
					name = "Cancel";
				if (!name.Equals("Cancel"))
				{
					pass = await Shell.Current.DisplayPromptAsync("Identificate de la forma más segura", ("contraseña"), "Ok", "cancel");
					if (!pass.Equals("Cancel"))
					{
						var lista = await clsListadoJugadoresBL.getJugadoresBL();
						jugadorAbajo = await LoginVM.TestSignUp(name, pass);

						var encontrado = false;
						for (var i= 0;i < lista.Count() && !encontrado;i++)
						{
							if(lista.ElementAt(i).nombre.Equals(jugadorAbajo.nombre) && lista.ElementAt(i).password.Equals(jugadorAbajo.password))
							{
								jugadorAbajo = lista.ElementAt(i);
								encontrado = true;
							}
						}
					}
					else
					{
						return;
					}

				}
				else
				{

					return;
				}


				if (jugadorAbajo != null)
				{
					sala.jugadorAbajo = jugadorAbajo.idJugador;

				}
				dic.Add("SalaEnviada", sala);

			}

			nombreSala = await comprobarSiTieneNombre();
			if (!nombreSala.Equals("Cancel"))
			{
				sala.espacio = 2;
				sala.nombreSala = nombreSala;
				await Shell.Current.GoToAsync("///Game", true, dic);
			}

		}



		async Task<String> comprobarSiTieneNombre()
		{
			var nombreSala = await Shell.Current.DisplayPromptAsync("", "Por ultimo, elige un nombre para vuestra sala", "Ok", "Cancel");
			if (nombreSala == null)
			{
				nombreSala = "Cancel";
			}
			if (!nombreSala.Equals("Cancel"))
			{
				if (nombreSala.Equals(""))
				{
					nombreSala = "Cancel";
				}
				try
				{
					var listaSalas = await clsListadoSalasBL.getSalasBL();
					foreach (var salaRecogida in listaSalas)
					{
						if (salaRecogida.nombreSala.Equals(nombreSala))
						{
							nombreSala = "Cancel";
							return nombreSala;
						}
					}

				}
				catch (Exception e)
				{
					await Shell.Current.DisplayAlert("Error", "No se pudo conectar con el servidor", "Ok");
					nombreSala = "Cancel";
				}


			}
			else
			{
				await Shell.Current.DisplayAlert("Error", "La sala tiene que tener un nombre", "Ok");
				nombreSala = "Cancel";

			}
			return nombreSala;
		}


		/// <summary>
		/// Metodo que se acciona al pulsar el boton de Sign Up, cuando se pulse, navegaremos hacia la siguiente pagina
		/// </summary>
		private async void ComandoOnline_Execute()
		{
			await Shell.Current.DisplayAlert("Lo sentimos", "El modo On-line está inactivo actualmente", "Ok");

		}





	}
}
