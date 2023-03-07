using DAL.Listados;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Listados
{
	public class clsListadoSalasBL
	{
		/// <summary>
		/// Funcion que llama a la funcion GetSalas de la capa DAL y le aplicaria una logica de negocio
		/// </summary>
		/// <returns> new List<clsPersona>(clsListadoPersonasDAL.ObtenerListadoPersonasDAL() </returns>
		public static async Task<ObservableCollection<clsSala>> getSalasBL()
		{
			// aqui va la logica de negocio

			return new ObservableCollection<clsSala>(await clsListadoSalasDAL.getSalasDAL());
		}
	}
}
