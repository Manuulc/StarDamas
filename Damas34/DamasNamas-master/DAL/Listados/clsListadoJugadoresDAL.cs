using DAL.Conexion;
using Entities;
using Newtonsoft.Json;
namespace DAL.Listados
{
	public class clsListadoJugadoresDAL
	{
		/// <summary>
		/// Descripcion: Método que se encarga de recoger un objeto Json que contiene los datos de todos los jugadores de la base de datos, lo convierte en objetos clsJugador, los introduce en una lista y la devuelve. Accediendo a la API
		/// Precondiciones: ninguna.
		/// Postcondiciones: ninguna.
		/// 
		/// </summary>
		/// <returns>List<clsJugador> jugadores</returns>
		public static async Task<List<clsJugador>> getJugadoresDAL()
		{
			List<clsJugador> jugadores = new List<clsJugador>();
			HttpClient client = new HttpClient();
			string data;
			HttpContent content;
			string urlString = clsUriBase.getUriBase();
			Uri miUri = new($"{urlString}jugadores");
			HttpResponseMessage miCodigoRespuesta;

			try
			{
				miCodigoRespuesta = await client.GetAsync(miUri);

				if (miCodigoRespuesta.IsSuccessStatusCode)
				{
					data = await client.GetStringAsync(miUri);

					client.Dispose();

					jugadores = JsonConvert.DeserializeObject<List<clsJugador>>(data);

				}
			}
			catch (Exception e)
			{
				throw e;
			}

			return jugadores;
		}

	}
}
