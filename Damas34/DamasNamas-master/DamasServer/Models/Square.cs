using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamasServer.Models
{
    public class Square
    {
        int posX;
        int posY;


        bool esReina;

        ColorPieza tipoPieza;


        String color;

        private String pieza;



        public bool EsReina
        {
            get
            {
                return esReina;
            }
            set
            {
                esReina = value;
            }
        }




        public String Pieza
        {
            get { return pieza; }
            set
            {
                if (value == "piezablanca" || value == "reinablanca")
                {
                    TipoPieza = ColorPieza.Blanca;
                }
                else if (value == "piezanegra" || value == "reinanegra")
                {
                    TipoPieza = ColorPieza.Negra;
                }
                else
                {
                    TipoPieza = ColorPieza.None;
                }
                pieza = value;
            }
        }


        public ColorPieza TipoPieza
        {
            get
            {
                return tipoPieza;
            }
            set
            {
                tipoPieza = value;
            }
        }


        public String ColorFondo { get { return color; } set { color = value; } }

        public int PosX { get; set; }
        public int PosY { get; set; }



        //Constructor para insertar directamente una pieza.
        public Square(int posX, int posY, String _pieza, String color)
        {

            PosX = posX;
            PosY = posY;
            Pieza = _pieza;
            ColorFondo = color;
            esReina = false;

        }


        //Constructor para insertar un square sin pieza.
        public Square(int posX, int posY, String color)
        {
            PosX = posX;
            PosY = posY;
            Pieza = "";
            ColorFondo = color;
            esReina = false;
        }

        //Constructor copia
        public Square(Square _square)
        {
            PosX = _square.PosX;
            PosY = _square.PosY;
            Pieza = _square.Pieza;
            ColorFondo = _square.ColorFondo;
            esReina = _square.EsReina;
        }


    }
}