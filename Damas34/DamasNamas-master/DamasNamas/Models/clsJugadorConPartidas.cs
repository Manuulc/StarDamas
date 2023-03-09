using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamasNamas.Models
{
	public class clsJugadorConPartidas : clsJugador
	{
		public List<clsSala> partidas { get; set; }

		public int partidasGanadas { get; set; }

		public clsJugadorConPartidas(int idJugador, String nombre, String password, List<clsSala> partidasJugadas, int _partidasGanadas)
		{
			base.idJugador = idJugador;
			base.nombre=nombre;
			base.password=password;
			partidas=partidasJugadas;
			partidasGanadas = _partidasGanadas;
		}
		public clsJugadorConPartidas(int idJugador, String nombre, String password, List<clsSala> partidasJugadas)
		{
			base.idJugador = idJugador;
			base.nombre=nombre;
			base.password=password;
			partidas=partidasJugadas;
			
			comprobarPartidas();
		}


		public int comprobarPartidas()
		{
			var pg = 0;
			foreach (var partida in partidas)
			{
				if ((partida.cantidadFichasAbajo > partida.cantidadFichasArriba && idJugador == partida.jugadorAbajo) || (partida.cantidadFichasArriba > partida.cantidadFichasAbajo && idJugador == partida.jugadorArriba))
				{
					pg++;
				}
			}
			partidasGanadas = pg;
			return pg;
		}
	}
}
