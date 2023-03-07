using DAL.Gestion;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Gestion
{
	public class clsGestionJugadoresBL
	{
		/// <summary>
		/// Metodo que inserta un jugador en la API llamando al insertarJugador de la capa DAL y le aplicaria una logica de negocio
		/// </summary>
		/// <param name="jugador"></param>
		public static async void insertarJugadorBL(clsJugador jugador)
		{

			await clsGestionJugadoresDAL.insertarJugadorDAL(jugador);
		}

		/// <summary>
		/// Metodo que edita un jugador en la API llamando al editarJugador de la capa DAL y le aplicaria una logica de negocio
		/// </summary>
		/// <param name="jugador"></param>
		public static async void editarJugadorBL(clsJugador jugador)
		{
			await clsGestionJugadoresDAL.editarJugadorDAL(jugador);
		}

		/// <summary>
		/// Metodo que borra un jugador de la API llamando al borrarJugador de la capa DAL y le aplicaria una logica de negocio
		/// </summary>
		/// <param name="codJugador"></param>
		public static async void borrarJugadorBL(int codJugador)
		{
			await clsGestionJugadoresDAL.borrarJugadorDAL(codJugador);
		}

		/// <summary>
		/// Metodo para obtener un jugador por ID de la API llamando a getJugador de la capa DAL y le aplicaria una logica de negocio
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static async Task<clsJugador> getJugadorBL(int id)
		{

			return await clsGestionJugadoresDAL.getJugadorDAL(id);
		}
	}
}
