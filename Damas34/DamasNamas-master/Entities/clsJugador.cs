using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class clsJugador
	{

		#region Attributes
		private int _idJugador;

		private String _nombre;

		private String _password;


		#endregion

		#region Properties

		public int idJugador { get { return _idJugador; } set { _idJugador = value; } }

		public String nombre { get { return _nombre; } set { _nombre = value;} }

		public String password { get { return _password; } set { _password = value; } }

		//public List<clsSala> partidasJugadas { get { return _partidasJugadas; } set { _partidasJugadas = value; } }

		#endregion

		#region Constructors

		public clsJugador() { }



		public clsJugador(int idJug, string _nombre, string _password)
		{
			idJugador = idJug;
			nombre=_nombre;
			password=_password;
			//partidasJugadas=_partidasJugadas;
		}


		public clsJugador(string _nombre, string _password)
		{
			nombre=_nombre;
			password=_password;
			//partidasJugadas=_partidasJugadas;
		}






		#endregion
	}
}
