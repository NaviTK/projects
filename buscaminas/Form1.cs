using ImageMagick;
using Microsoft.VisualBasic.Devices;
using System.CodeDom;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;

namespace buscaminas
{
    public partial class Form1 : Form
    {
        //clases definidas
        public struct Pair
        {
            public int first;
            public int second;
            public Pair(int First, int Second)
            {
                first = First;
                Second = second;
            }
        }
        public class Celda
        {
                public string EstadoCelda { get; set; }
                public bool Bomba { get; set; }

                // Constructor
                public Celda(string estado, bool bomba)
                {
                    EstadoCelda = estado;
                    Bomba = bomba;
                }

            // Método para establecer el valor de Bomba
            public void SetBomba(bool bomba)
            {
                Bomba = bomba;
            }
            public bool GetBomba()
            {
                return Bomba;
            }
            //metodo establecer valor estado_celda
            public void Setestado(string estado)
            {
                EstadoCelda = estado;
            }
            public string Getestado()
            {
                return EstadoCelda;
            }
        };
        public struct Matriz
        {
            public Dictionary<int, Celda> matx { get; set; }
            public Matriz()
            {
                matx = new Dictionary<int, Celda>();
            }
        }
        static class cell_state
        {
            public const string blanco = "en blanco";
            public const string marcada = "marcada";
            public const string sin_inicializar = "sin_inicializar";
            public struct Numerada
            {
                public readonly string n1; public readonly string n2;
                public readonly string n3; public readonly string n4;
                public readonly string n5; public readonly string n6;
                public readonly string n7; public readonly string n8;
                public Numerada()
                {
                    n1 = "numero1"; n2 = "numero2";
                    n3 = "numero3"; n4 = "numero4";
                    n5 = "numero5"; n6 = "numero6"; 
                    n7 = "numero7"; n8 = "numero8";
                }
          
            }
        }
        static class resize_factor
        {
            //factor default
            public const int default1 = 48;
            public const int default2 = 32;
            //factor bomba
            public const int bomba1 = 1;
            public const int bomba2 = 1;
            //factor mina
            public const int mina1 = 5;
            public const int mina2 = 3;
            //casilla en blanco
            public const int casilla_en_blanco1 = 48;
            public const int casilla_en_blanco2 = 32;
            //factor numeros
            public const int num1_1 = 1;
            public const int num1_2 = 1;
            public const int num2_1 = 1;
            public const int num2_2 = 1;
            public const int num3_1 = 1;
            public const int num3_2 = 1;
            public const int num4_1 = 1;
            public const int num4_2 = 1;
            public const int num5_1 = 1;
            public const int num5_2 = 1;
            public const int num6_1 = 1;
            public const int num6_2 = 1;
            public const int num7_1 = 1;
            public const int num7_2 = 1;
            public const int num8_1 = 1;
            public const int num8_2 = 1;

        }
        //path to buscaminas folder
        string root = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
        //variables globales
        PictureBox[,] tablero;
        bool first_click = true;
        bool casillas_quadradas = false;
        int filas = 0;
        int columnas = 0;
        int sizeframe_h = 0;
        int sizeframe_v = 0;
        int dificultat = 3; // 3-> facil, 2->intermedia, 1-->dificil se modificara con botones mas adelante
        Matriz mat;
        public Form1()
        {
            InitializeComponent();
            gt(4, 4);
        }
        private void gt(int x_ini, int y_ini)
        {
            // control de errores
            if (x_ini <= 0 || x_ini > 40) ;
            if (y_ini <= 0 || y_ini > 40) ;
            //------------------------------
            //tamaño tablero
            filas = tablerocon.Size.Height;
            columnas= tablerocon.Size.Width; 
            tablero = new PictureBox[x_ini, y_ini];
            int c = 0;
            sizeframe_h = filas / x_ini;
            sizeframe_v = columnas / y_ini;

            // calculo del path de la imagen
            string path = Path.Combine(root, @"imagenes", "default.png");
            string path2 = Path.Combine(root, @"imagenes", "resized_default.png");
            // magick image
            // omagickimage.Resize(900,0) --> 0 recalcula proporcionalmente la otra coordenada sino se deforma
            using (MagickImage omagickimage = new MagickImage(path))
            {
                omagickimage.Resize(sizeframe_h*resize_factor.default1/resize_factor.default2, 0);

                omagickimage.Write(path2);
            }

            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    tablero[i, j] = new PictureBox();
                    tablero[i, j].Name = c.ToString();
                    tablero[i, j].Size = new Size(sizeframe_v, sizeframe_h);
                    tablero[i, j].Location = new Point(j * sizeframe_v, i * sizeframe_h);
                    tablero[i, j].BorderStyle = BorderStyle.FixedSingle;
                    tablero[i, j].MouseClick += new MouseEventHandler(click_cell);
                    //string path = Path.Combine(root, "imagenes", "bomba.jpg");
                    tablero[i, j].Image = Image.FromFile(path2);
                    c++;
                    tablerocon.Controls.Add(tablero[i, j]);
                }
            }
        }
        // pre una matriz no inicializada con el valor de la celda del click de parametro
        //post matriz rellenada con bombas en funcion de la dificultat
        private void posiciona_bombas(int celda_inicial)
        {
            int numero_de_bombas = (tablero.GetLength(0) * tablero.GetLength(1)) / (4 * dificultat);
            Random random = new Random();
            for (int i = 0; i < numero_de_bombas;i++)
            {
                int random_number = random.Next(0, tablero.GetLength(0) * tablero.GetLength(1));
                while (random_number == celda_inicial) random_number = random.Next(0, tablero.GetLength(0) * tablero.GetLength(1));
                while (mat.matx[random_number].GetBomba() && random_number < tablero.GetLength(0) * tablero.GetLength(1)) random_number++;
                if(random_number == tablero.GetLength(0) * tablero.GetLength(1)) numero_de_bombas++;
                else
                {
                    mat.matx[random_number].SetBomba(true);
                }
            }
        }
        private int cuenta_bombas(int mi_celda, ref Pair[] adj)
        {
            int contador = 0;
            for (int i = 0; i < 9; i++) if ((mat.matx[mi_celda + adj[i].first + (adj[i].second * tablero.GetLength(1))]).GetBomba()) contador++;
            return contador;
        }
        private void actualiza_tablero()
        {
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    switch(mat.matx[i* tablero.GetLength(1) + j].EstadoCelda)
                    {

                    }
                    tablero[i, j].Image = null;
                }
            }
        }
        private void click_cell(object sender, MouseEventArgs e)
        {
            //picture es la cajita
            PictureBox picture = sender as PictureBox;
            Pair[] adj = new Pair[9];
            adj[0] = new Pair(-1, -1);
            adj[1] = new Pair(0, -1);
            adj[2] = new Pair(1, -1);
            adj[3] = new Pair(-1, 0);
            adj[4] = new Pair(0, 0);
            adj[5] = new Pair(1, 0);
            adj[6] = new Pair(-1, 1);
            adj[7] = new Pair(0, 1);
            adj[8] = new Pair(1, 1);
            if (first_click)
            {
                first_click = !first_click;
                mat = new Matriz();
                for (int i = 0; i < tablero.GetLength(0); i++)
                {
                    for (int j = 0; j < tablero.GetLength(1); j++)
                    {
                        Celda cell = new Celda(cell_state.sin_inicializar, false);
                        mat.matx.Add(i * tablero.GetLength(1) + j, cell);
                    }
                }
                int mi_celda = int.Parse(picture.Name);
                posiciona_bombas(mi_celda);
                int num_bombas;
                if ((num_bombas = cuenta_bombas(mi_celda,ref adj)) != 0)
                {
                    mat.matx[mi_celda].EstadoCelda = "numero"+num_bombas.ToString();
                }
            }
            else
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        MessageBox.Show("Left Click");
                        ; break;
                    case MouseButtons.Right:
                        MessageBox.Show("Right Click");
                        break;
                    default:
                        MessageBox.Show("que has tocao?");
                        break;
                }
            }
            actualiza_tablero();
        }
        private void gen_func(object sender, EventArgs e)
        {
            //int x, y;
            //x = Int32.Parse("");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (casillas_quadradas) casillas_quadradas = !casillas_quadradas;
            //.... mismo codigo que generar
        }
    }
}