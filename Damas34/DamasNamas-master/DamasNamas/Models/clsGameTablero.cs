using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamasNamas.Models
{
	partial class clsGameTablero : ObservableObject
	{
		public const int FILAS = 8;
		public const int COLUMNAS = 8;

		List<Square> huecos;

		int piezasBlancas;

		int piezasNegras;

		long tiempo;

		public List<Square> Huecos
		{
			get => huecos;
			set => huecos = value;
		}

		public int PiezasBlancas
		{
			get => piezasBlancas;
			set { piezasBlancas = value; OnPropertyChanged(nameof(PiezasBlancas)); }

		}

		public int PiezasNegras
		{
			get => piezasNegras;
			set { piezasNegras = value; OnPropertyChanged(nameof(PiezasNegras)); }
			
		}

		public long Tiempo { get { return tiempo; } set { tiempo = value; } }

		public clsGameTablero()
		{
			//Rellena los arrays de piezas
			PiezasNegras=12;
			PiezasBlancas=12;
			Tiempo = 0;
			//Rellena el tablero
			SetUpPiezas();
		}







		void SetUpPiezas()
		{
			var contadorPB = 0;
			var contadorPN = 0;

			Huecos = new List<Square>();
			for (int fila = 0; fila<FILAS; fila++)
			{
				for (int columna = 0; columna < COLUMNAS; columna++)
				{
					if (fila < 3)
					{
						if ((fila%2==0 && columna%2==0) || (fila%2!=0 && columna%2!=0))
						{
							Huecos.Add(new Square(fila, columna, "piezablanca", "lightynone"));
							contadorPB++;
						}
						else
						{
							Huecos.Add(new Square(fila, columna, "shadynone", "shadynone"));
						}
					}
					else if (fila == 3)
					{
						if (columna%2==0)
						{
							Huecos.Add(new Square(fila, columna, "shadynone", "shadynone"));

						}
						else
						{
							Huecos.Add(new Square(fila, columna, "lightynone", "lightynone"));

						}
					}
					else if (fila == 4)
					{
						if (columna%2!=0)
						{
							Huecos.Add(new Square(fila, columna, "shadynone", "shadynone"));
						}
						else
						{
							Huecos.Add(new Square(fila, columna, "lightynone", "lightynone"));
						}
					}
					else if (fila>4)
					{
						if ((fila%2==0 && columna%2==0) || (fila%2!=0 && columna%2!=0))
						{
							Huecos.Add(new Square(fila, columna, "piezanegra", "lightynone"));
							contadorPN++;
						}
						else
						{
							Huecos.Add(new Square(fila, columna, "shadynone", "shadynone"));
						}
					}
				}
			}
		}

		
	}
}
