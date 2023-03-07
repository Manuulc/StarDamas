using DAL.Conexion;
using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Listados
{
	public class clsListadoSalasDAL
	{
		/// <summary>
		/// Descripcion: Método que se encarga de recoger un objeto Json que contiene los datos de todos las salas de la base de datos, lo convierte en objetos clsSala, los introduce en una lista y la devuelve. Accediendo a la API.
		/// Precondiciones: Ninguna.
		/// Postcondiciones: Ninguna.
		/// 
		/// </summary>
		/// <returns></returns>
		public static async Task<List<clsSala>> getSalasDAL()
		{
			List<clsSala> salas = new List<clsSala>();
			HttpClient client = new HttpClient();
			string data;
			HttpContent content;
			string urlString = clsUriBase.getUriBase();
			Uri miUri = new($"{urlString}salas");
			HttpResponseMessage miCodigoRespuesta;
			try
			{
				miCodigoRespuesta = await client.GetAsync(miUri);

				if (miCodigoRespuesta.IsSuccessStatusCode)
				{
					data = await client.GetStringAsync(miUri);

					client.Dispose();

					if(data != null) 
						salas = JsonConvert.DeserializeObject<List<clsSala>>(data);


				}
			}
			catch (Exception e)
			{
				throw e;
			}

			return salas;
		}

	}
}
