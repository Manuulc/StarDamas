

namespace Entities
{
	public class clsSala
	{
		#region Attributes


		private int _codSala;

		private String _nombreSala;

		private int _jugadorArriba;

		private int _jugadorAbajo;

		private String _tiempo;

		private int _cantidadFichasArriba;

		private int _cantidadFichasAbajo;

		private int _espacio;
		#endregion

		#region Properties

		public int codSala { get { return _codSala; } set { _codSala = value; } }

		public String nombreSala { get { return _nombreSala; } set { _nombreSala = value; } }

		public int jugadorArriba { get { return _jugadorArriba; } set { _jugadorArriba = value; } }

		public int jugadorAbajo { get { return _jugadorAbajo; } set { _jugadorAbajo = value; } }

		public String tiempo { get { return _tiempo; } set { _tiempo = value; } }

		public int cantidadFichasArriba { get { return _cantidadFichasArriba; } set { _cantidadFichasArriba = value; } }

		public int cantidadFichasAbajo { get { return _cantidadFichasAbajo; } set { _cantidadFichasAbajo = value; } }
		//Propiedad que controla los huecos libres de la sala
		public int espacio { get { return _espacio; } set { _espacio = value; } }

		#endregion


		#region Constructors

		public clsSala(int _jugadorArriba)
		{
			nombreSala = ""; 
			jugadorArriba=_jugadorArriba;
			jugadorAbajo=0;
			tiempo="0";
			cantidadFichasArriba=12;
			cantidadFichasAbajo=12;
			espacio = 0;
		}

		public clsSala()
		{
		}




		//Constructor para la recogida de salas de la base de datos
		public clsSala(int _codSala, string _nombreSala, int _jugadorArriba, int _jugadorAbajo, String _tiempo, int _cantidadFichasArriba, int _cantidadFichasAbajo)
		{
			codSala=_codSala;
			nombreSala=_nombreSala;
			jugadorArriba=_jugadorArriba;
			jugadorAbajo=_jugadorAbajo;
			tiempo=_tiempo;
			cantidadFichasArriba=_cantidadFichasArriba;
			cantidadFichasAbajo=_cantidadFichasAbajo;
			espacio = 2;
		}

		//Constructor para cuando se crea una sala por defecto
		public clsSala(int _codSala, string _nombreSala, int _jugadorArriba, int _jugadorAbajo, String _tiempo)
		{
			codSala=_codSala;
			nombreSala=_nombreSala;
			jugadorArriba=_jugadorArriba;
			jugadorAbajo=_jugadorAbajo;
			tiempo=_tiempo;
			cantidadFichasArriba=12;
			cantidadFichasAbajo=12;
			espacio = 2;
		}

		//Constructor para cuando se crea una sala sin jugadores
		public clsSala(string _nombreSala)
		{
			nombreSala=_nombreSala;
			jugadorArriba=0;
			jugadorAbajo=0;
			tiempo="0";
			cantidadFichasArriba=12;
			cantidadFichasAbajo=12;
			espacio = 0;
		}

		//Constructor para cuando se crea una sala con un jugador
		public clsSala(string _nombreSala, int _jugadorArriba)
		{
			nombreSala=_nombreSala;
			jugadorArriba=_jugadorArriba;
			jugadorAbajo=0;
			tiempo="0";
			cantidadFichasArriba=12;
			cantidadFichasAbajo=12;
			espacio = 1;
		}



		#endregion
	}
}
