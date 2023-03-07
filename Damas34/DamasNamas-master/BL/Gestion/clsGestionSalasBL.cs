using DAL.Gestion;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Gestion
{
	public class clsGestionSalasBL
	{

		/// <summary>
		/// Metodo que inserta una sala en la API llamando al insertarSala de la capa DAL y le aplicaria una logica de negocio
		/// </summary>
		/// <param name="sala"></param>
		public static async void insertarSalaBL(clsSala sala)
		{

			await clsGestionSalasDAL.insertarSalaDAL(sala);
		}

		/// <summary>
		/// Metodo que actualiza una sala en la API llamando al editarSala de la capa DAL y le aplicaria una logica de negocio
		/// </summary>
		/// <param name="sala"></param>
		public static async void editarSalaBL(clsSala sala)
		{

			await clsGestionSalasDAL.editarSalaDAL(sala);
		}

		/// <summary>
		/// Metodo que borra una sala de la API llamando a borrarSala de la capa DAL y le aplciaria una logica de negocio
		/// </summary>
		/// <param name="codSala"></param>
		public static async void borrarSalaBL(int codSala)
		{

			await clsGestionSalasDAL.borrarSalaDAL(codSala);
		}

		/// <summary>
		/// Metodo para obtener una sala por ID de la API llamando a getSala de la capa DAL y le aplicaria una logica de negocio
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static async Task<clsSala> getSalaBL(int id)
		{

			return await clsGestionSalasDAL.getSalaDAL(id);
		}
	}
}
