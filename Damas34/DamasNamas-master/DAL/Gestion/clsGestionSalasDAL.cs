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
	public class clsGestionSalasDAL
	{
		
		
		
		
		/// <summary>
		/// Descripcion: Metodo para insertar una sala en la BBDD mediante una llamada a la API.
		/// Precondiciones: Ninguna.
		/// Postcondiciones: Inserta la sala correctamente.
		/// 
		/// </summary>
		/// <param name="sala"></param>
		/// <returns></returns>
		public static async Task<HttpStatusCode> insertarSalaDAL(clsSala sala)
		{
			HttpClient mihttpClient = new HttpClient();
			string datos;
			HttpContent contenido;
			string miCadenaUrl = clsUriBase.getUriBase();
			Uri miUri = new Uri($"{miCadenaUrl}salas");
			HttpResponseMessage miRespuesta = new HttpResponseMessage();
			try
			{
				datos = JsonConvert.SerializeObject(sala);
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
		/// Descripcion: Metodo que edita una sala de la BBDD mediante una llamada a la API.
		/// Precondiciones: La sala existe previamente en la BBDD.
		/// Postcondiciones: Edita la sala correctamente.
		/// 
		/// </summary>
		/// <param name="sala"></param>
		/// <returns></returns>
		public static async Task<HttpStatusCode> editarSalaDAL(clsSala sala)
		{
			HttpClient mihttpClient = new HttpClient();
			string datos;
			HttpContent contenido;
			string miCadenaUrl = clsUriBase.getUriBase();
			Uri miUri = new Uri($"{miCadenaUrl}salas/{sala.codSala}");
			HttpResponseMessage miRespuesta = new HttpResponseMessage();
			try
			{
				datos = JsonConvert.SerializeObject(sala);
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
		/// Descripcion: Metodo que borra una sala de la BBDD llamando a la API.
		/// Precondiciones: La sala existe previamente en la BBDD.
		/// Poscondiciones: Borra la sala correctamete.
		/// 
		/// </summary>
		/// <param name="codSala"></param>
		/// <returns></returns>
		public static async Task<HttpStatusCode> borrarSalaDAL(int codSala)
		{
			HttpClient mihttpClient = new HttpClient();
			string datos;
			HttpContent contenido;
			string miCadenaUrl = clsUriBase.getUriBase();
			Uri miUri = new Uri($"{miCadenaUrl}salas/{codSala}");
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
		/// Descripcion: Metodo que obtiene una sala de la BBDD segun un id pasado por parametro llamando a la API.
		/// Precondiciones: La sala previamente en la BBDD.
		/// Postcondiciones: Devuelve una sala.
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static async Task<clsSala> getSalaDAL(int id)
		{
			clsSala sala = new clsSala();
			HttpClient client = new HttpClient();
			string data;
			HttpContent content;
			string miUri = clsUriBase.getUriBase();
			Uri uri = new($"{miUri}/salas/{id}");

			HttpResponseMessage miCodigoRespuesta;

			try
			{
				miCodigoRespuesta = await client.GetAsync(uri);
				if (miCodigoRespuesta.IsSuccessStatusCode)
				{
					data = await client.GetStringAsync(uri);
					client.Dispose();
					sala = JsonConvert.DeserializeObject<clsSala>(data);
				}
			}
			catch (Exception e)
			{
				throw e;
			}
			return sala;
		}
	}
}
