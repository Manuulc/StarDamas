using DAL.Conexion;
using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Gestion
{
	public class clsGestionJugadoresDAL
	{       /// <summary>
			/// Descripcion: Metodo que inserta un jugador en la tabla jugadores llamando a la API.
			/// Precondiciones: Ninguna.
			/// Postcondiciones: Ninguna.
			/// 
			/// </summary>
			/// <param name="jugador"></param>
			/// <returns></returns>
			/// 
		public static async Task<HttpStatusCode> insertarJugadorDAL(clsJugador jugador)
		{
			HttpClient mihttpClient = new HttpClient();
			string datos;
			HttpContent contenido;
			string miCadenaUrl = clsUriBase.getUriBase();
			Uri miUri = new Uri($"{miCadenaUrl}jugadores");
			HttpResponseMessage miRespuesta = new HttpResponseMessage();
			try
			{
				datos = JsonConvert.SerializeObject(jugador);
				contenido = new StringContent(datos, Encoding.UTF8, "application/json");
				miRespuesta = await mihttpClient.PostAsync(miUri, contenido);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return miRespuesta.StatusCode;
		}

		/// <summary>
		/// Descripcion: Metodo que edita un jugador en la tabla jugadores llamando a la API.
		/// Precondiciones: El jugador existe previamente en la BBDD.
		/// Postcondiciones: El jugador se ha editado correctamente.
		/// 
		/// </summary>
		/// <param name="jugador"></param>
		/// <returns></returns>
		public static async Task<HttpStatusCode> editarJugadorDAL(clsJugador jugador)
		{
			HttpClient mihttpClient = new HttpClient();
			string datos;
			HttpContent contenido;
			string miCadenaUrl = clsUriBase.getUriBase();
			Uri miUri = new Uri($"{miCadenaUrl}departamentos/{jugador.idJugador}");
			HttpResponseMessage miRespuesta = new HttpResponseMessage();
			try
			{
				datos = JsonConvert.SerializeObject(jugador);
				contenido = new StringContent(datos, Encoding.UTF8, "application/json");
				miRespuesta = await mihttpClient.PutAsync(miUri, contenido);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return miRespuesta.StatusCode;
		}

		/// <summary>
		/// Descripcion: Metodo que borra un jugador en la tabla jugadores llamando a la API.
		/// Precondiciones: El jugador existe previamente en la BBDD.
		/// Postcondiciones: El jugador ha sido borrado correctamente.
		/// 
		/// </summary>
		/// <param name="idJugador"></param>
		/// <returns></returns>
		public static async Task<HttpStatusCode> borrarJugadorDAL(int idJugador)
		{
			HttpClient mihttpClient = new HttpClient();
			string datos;
			HttpContent contenido;
			string miCadenaUrl = clsUriBase.getUriBase();
			Uri miUri = new Uri($"{miCadenaUrl}departamentos/{idJugador}");
			HttpResponseMessage miRespuesta = new HttpResponseMessage();
			try
			{
				miRespuesta = await mihttpClient.DeleteAsync(miUri);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return miRespuesta.StatusCode;
		}


		/// <summary>
		/// Descripcion: Método que se encarga de recoger un registro de la tabla jugadores llamando a la API.
		/// Precondiciones: El jugador existe en la BBDD.
		/// Postcondiciones: Devuelve el jugador correctamente.
		/// 
		/// </summary>
		/// <param name="idJugador"></param>
		/// <returns></returns>
		public static async Task<clsJugador> getJugadorDAL(int idJugador)
		{

			clsJugador jugador = new clsJugador();
			HttpClient client = new HttpClient();
			string data;
			HttpContent content;
			string miUri = clsUriBase.getUriBase();
			Uri uri = new($"{miUri}jugadores/{idJugador}");

			HttpResponseMessage miCodigoRespuesta;

			try
			{
				miCodigoRespuesta = await client.GetAsync(uri);
				if (miCodigoRespuesta.IsSuccessStatusCode)
				{
					data = await client.GetStringAsync(uri);
					client.Dispose();
					jugador = JsonConvert.DeserializeObject<clsJugador>(data);
				}
			}
			catch (Exception e)
			{
				throw e;
			}
			return jugador;
		}
	}
}
