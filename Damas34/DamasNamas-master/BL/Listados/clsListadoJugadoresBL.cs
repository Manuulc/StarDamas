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
	public class clsListadoJugadoresBL
	{ /// <summary>
	  /// Funcion que llama a la funcion GetJugadores de la capa DAL y le aplicaria una logica de negocio
	  /// </summary>
	  /// <returns> new List<clsPersona>(clsListadoPersonasDAL.ObtenerListadoPersonasDAL() </returns>
		public static async Task<ObservableCollection<clsJugador>> getJugadoresBL()
		{
			// aqui va la logica de negocio

			return new ObservableCollection<clsJugador>(await clsListadoJugadoresDAL.getJugadoresDAL());
		}
	}
}
