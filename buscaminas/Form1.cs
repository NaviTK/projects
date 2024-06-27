using ImageMagick;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using System;
using System.CodeDom;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace buscaminas
{
    public partial class Buscaminitas : Form
    {
        //clases definidas
        public struct Pair
        {
            public int first;
            public int second;
            public Pair(int First, int Second)
            {
                first = First;
                second = Second;
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
            public const string blanco = "blanco";
            public const string marcada = "marcada";
            public const string sin_inicializar = "sin_inicializar";
            public const string bomba = "bomba";
            public struct Numerada
            {
                public const string n1 = "numero1"; public const string n2 = "numero2";
                public const string n3 = "numero3"; public const string n4 = "numero4";
                public const string n5 = "numero5"; public const string n6 = "numero6";
                public const string n7 = "numero7"; public const string n8 = "numero8";

            }
            //public Numerada numerada;
        }
        static class resize_factor
        {
            public const int numerador = 11;
            public const int denomiandor = 10;
        }
        //path to buscaminas folder
        string root = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
        //variables globales
        PictureBox[,] tablero;
        bool first_time = true;
        bool first_click = true;
        int filas = 0;
        int columnas = 0;
        int sizeframe_h = 0;
        int sizeframe_v = 0;
        int dificultat = 3; // 3-> facil, 2->intermedia, 1-->dificil se modificara con botones mas adelante
        bool ended;
        Matriz mat;
        public Buscaminitas()
        {
            InitializeComponent();
            filas = Int32.Parse(tbdefilas.Text);
            columnas = Int32.Parse(tbdecolumnas.Text);

        }
        private void resizea_imagenes()
        {
            int x_ini = Int32.Parse(tbdefilas.Text);
            int y_ini = Int32.Parse(tbdecolumnas.Text);
            filas = tablerocon.Size.Height;
            columnas = tablerocon.Size.Width;
            sizeframe_h = filas / x_ini;
            sizeframe_v = columnas / y_ini;
            string path_to_ini = root + "/imagenes/"; // path_to_ini + <nombre_imagen.png>
            string path_to_rs = path_to_ini; // path_to_rs + <nombre_imagen_resized>
            string[] archivos = { cell_state.blanco, cell_state.marcada, cell_state.sin_inicializar, cell_state.bomba,
            cell_state.Numerada.n1, cell_state.Numerada.n2, cell_state.Numerada.n3, cell_state.Numerada.n4,
            cell_state.Numerada.n5, cell_state.Numerada.n6, cell_state.Numerada.n7, cell_state.Numerada.n8 };
            for (int i = 0; i < archivos.Length; i++)
            {
                string path = path_to_ini + archivos[i] + ".jpg";
                string path2 = path_to_ini + archivos[i] + "_resized" + ".jpg";
                // magick image
                // omagickimage.Resize(900,0) --> 0 recalcula proporcionalmente la otra coordenada sino se deforma
                using (MagickImage omagickimage = new MagickImage(path))
                {
                    omagickimage.Resize(sizeframe_h * resize_factor.numerador / resize_factor.denomiandor, 0);

                    omagickimage.Write(path2);
                }
            }
        }
        private bool changed(int x, int y)
        {
            if (filas != x || columnas != y) return true;
            return false;
        }
        private void gt(int x_ini, int y_ini)
        {
            if (first_time || changed(x_ini, y_ini))
            {
                resizea_imagenes();
                first_time = true;
            }
            first_click = true;
            ended = false;
            //------------------------------
            //tamaño tablero
            tablero = new PictureBox[x_ini, y_ini];
            int c = 0;
            sizeframe_h = filas / x_ini;
            sizeframe_v = columnas / y_ini;
            // calculo del path de la imagen
            string path2 = Path.Combine(root, @"imagenes", "sin_inicializar_resized.jpg");

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
                    using (StreamReader stream = new StreamReader(path2))
                    {
                        tablero[i, j].Image = Image.FromStream(stream.BaseStream);
                    }
                    c++;
                    tablerocon.Controls.Add(tablero[i, j]);
                }
            }
        }
        // pre una matriz no inicializada con el valor de la celda del click de parametro
        //post matriz rellenada con bombas en funcion de la dificultat
        private void posiciona_bombas(int celda_inicial)
        {
            switch (Int32.Parse(DifficultyBox.Text))
            {
                case 1:
                    dificultat = 3; //facil
                    break;
                case 2:
                    dificultat = 2; //medio
                    break;
                case 3:
                    dificultat = 1; //dificil
                    break;
                default:
                    break;
            }
            int numero_de_bombas = (tablero.GetLength(0) * tablero.GetLength(1)) / (4 * dificultat);
            Random random = new Random();
            for (int i = 0; i < numero_de_bombas; i++)
            {
                int random_number = random.Next(0, tablero.GetLength(0) * tablero.GetLength(1));
                while (random_number == celda_inicial) random_number = random.Next(0, tablero.GetLength(0) * tablero.GetLength(1));
                while (mat.matx[random_number].GetBomba() && random_number < tablero.GetLength(0) * tablero.GetLength(1)) random_number++;
                if (random_number == tablero.GetLength(0) * tablero.GetLength(1)) numero_de_bombas++;
                else
                {
                    mat.matx[random_number].SetBomba(true);
                }
            }
        }
        private bool pos_ok(int celda, int mov_h, int mov_v)
        {
            int nc = tablero.GetLength(1);
            int nf = tablero.GetLength(0);
            if (celda / nc == 0 && mov_v == -1) return false;
            if (celda / nc == nf - 1 && mov_v == 1) return false;
            if (celda % nc == 0 && mov_h == -1) return false;
            if (celda % nc == nc - 1 && mov_h == 1) return false;
            return true;
        }
        private int cuenta_bombas(int mi_celda, ref Pair[] adj)
        {
            int contador = 0;
            for (int i = 0; i < 9; i++)
            {
                int casilla = mi_celda + adj[i].first + (adj[i].second * tablero.GetLength(1));
                if (pos_ok(mi_celda, adj[i].first, adj[i].second))
                {
                    if (mat.matx[casilla].Bomba) contador++;
                }
            }
            return contador;
        }
        private void marca_bombas()
        {
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    if (mat.matx[i * tablero.GetLength(1) + j].Bomba) mat.matx[i * tablero.GetLength(1) + j].EstadoCelda = cell_state.bomba;
                }
            }
        }
        private void actualiza_tablero()
        {
            string path_to_ini = root + "/imagenes/"; // path_to_ini + <nombre_imagen.png>
            string archivetype = ".jpg"; // path_to_rs + <nombre_imagen_resized>
            string path = path_to_ini + "sin_inicializar" + archivetype;
            if (ended) marca_bombas();
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    switch (mat.matx[i * tablero.GetLength(1) + j].EstadoCelda)
                    {
                        case cell_state.Numerada.n1:
                            path = path_to_ini + "numero1_resized" + archivetype;
                            break;
                        case cell_state.Numerada.n2:
                            path = path_to_ini + "numero2_resized" + archivetype;
                            break;
                        case cell_state.Numerada.n3:
                            path = path_to_ini + "numero3_resized" + archivetype;
                            break;
                        case cell_state.Numerada.n4:
                            path = path_to_ini + "numero4_resized" + archivetype;
                            tablero[i, j].Image = Image.FromFile(path);
                            break;
                        case cell_state.Numerada.n5:
                            path = path_to_ini + "numero5_resized" + archivetype;
                            break;
                        case cell_state.Numerada.n6:
                            path = path_to_ini + "numero6_resized" + archivetype;
                            break;
                        case cell_state.Numerada.n7:
                            path = path_to_ini + "numero7_resized" + archivetype;
                            break;
                        case cell_state.Numerada.n8:
                            path = path_to_ini + "numero8_resized" + archivetype;
                            break;
                        case cell_state.marcada:
                            path = path_to_ini + "marcada_resized" + archivetype;
                            break;
                        case cell_state.blanco:
                            path = path_to_ini + "blanco_resized" + archivetype;
                            break;
                        case cell_state.sin_inicializar:
                            path = path_to_ini + "sin_inicializar_resized" + archivetype;
                            break;
                        case cell_state.bomba:
                            path = path_to_ini + "bomba_resized" + archivetype;
                            break;
                        default:
                            break;
                    }
                    using (StreamReader stream = new StreamReader(path))
                    {
                        tablero[i, j].Image = Image.FromStream(stream.BaseStream);
                    }
                }
            }
        }
        private void Comprueba_victoria()
        {
            int all = 0;
            int cdone = 0;
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    all++;
                    if (mat.matx[i * tablero.GetLength(1) + j].EstadoCelda != cell_state.sin_inicializar) cdone++;
                }
            }
            if (cdone == all)
            {
                MessageBox.Show("Victoria");
                ended = true;
            }
        }
        private void bfs_enblanquecedor(int mi_celda)
        {
            Pair[] adj = new Pair[9];adj[0] = new Pair(-1, -1);adj[1] = new Pair(0, -1);
            adj[2] = new Pair(1, -1);adj[3] = new Pair(-1, 0);adj[4] = new Pair(0, 0);
            adj[5] = new Pair(1, 0);adj[6] = new Pair(-1, 1);adj[7] = new Pair(0, 1);
            adj[8] = new Pair(1, 1);
            Queue<int> cola = new Queue<int>();
            cola.Enqueue(mi_celda);
            bool[] visited = new bool[tablero.GetLength(0) * tablero.GetLength(1)]; // se inicializa automaticamente en false
            for (int i = 0; i < visited.Length; i++) visited[i] = false;
            while (cola.Any()) // devuelve true si contiene algun elemento
            {
                int casilla = cola.Dequeue();
                if (!visited[casilla])
                {
                    visited[casilla] = true;
                    if (mat.matx[casilla].EstadoCelda == cell_state.sin_inicializar)
                    {
                        int num_bombas;
                        if ((num_bombas = cuenta_bombas(casilla, ref adj)) != 0) 
                            mat.matx[casilla].EstadoCelda = "numero" + num_bombas.ToString();
                        else mat.matx[casilla].EstadoCelda = cell_state.blanco;
                    }
                    if (mat.matx[casilla].EstadoCelda == cell_state.blanco)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            int cas_temp = casilla + adj[i].first + (adj[i].second * tablero.GetLength(1));
                            if (pos_ok(casilla, adj[i].first, adj[i].second)) cola.Enqueue(cas_temp);
                        }
                    }
                }
            }
        }
        private void click_cell(object sender, MouseEventArgs e)
        {
            //picture es la cajita
            PictureBox picture = sender as PictureBox;
            int mi_celda = int.Parse(picture.Name);
            if (!ended)
            {
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
                    posiciona_bombas(mi_celda);
                    int num_bombas;
                    if ((num_bombas = cuenta_bombas(mi_celda, ref adj)) != 0)
                    {
                        mat.matx[mi_celda].EstadoCelda = "numero" + num_bombas.ToString();
                    }
                    else
                    {
                        mat.matx[mi_celda].EstadoCelda = cell_state.blanco;
                    }
                }
                else
                {
                    switch (e.Button)
                    {
                        case MouseButtons.Left:
                            mat.matx[mi_celda].EstadoCelda = "marcada";
                            ; break;
                        case MouseButtons.Right:
                            int num_bombas;
                            if (mat.matx[mi_celda].Bomba)
                            {
                                MessageBox.Show("Skill Issue!");
                                ended = true;
                            }
                            else if ((num_bombas = cuenta_bombas(mi_celda, ref adj)) != 0)
                            {
                                mat.matx[mi_celda].EstadoCelda = "numero" + num_bombas.ToString();
                            }
                            else
                            {
                                mat.matx[mi_celda].EstadoCelda = cell_state.blanco;
                            }
                            break;
                        default:
                            MessageBox.Show("que has tocao?");
                            break;
                    }
                }
                if (mat.matx[mi_celda].EstadoCelda == cell_state.blanco) bfs_enblanquecedor(mi_celda);
            }
            actualiza_tablero();
            if (!ended) Comprueba_victoria();
        }
        private void limpia_picture_box()
        {
            if (tablero != null)
            {
                for (int i = 0; i < tablero.GetLength(0); i++)
                {
                    for (int j = 0; j < tablero.GetLength(1); j++)
                    {
                        this.Controls.Remove(tablero[i, j]);
                        tablero[i, j].Dispose();
                    }
                }
                tablero = null; // Liberar la referencia a la matriz
            }
        }
        private void gen_func(object sender, EventArgs e)
        {
            limpia_picture_box();
            int x, y;
            x = Int32.Parse(tbdefilas.Text);
            y = Int32.Parse(tbdecolumnas.Text);
            // control de errores
            if (x <= 0 || x > 40 || y <= 0 || y > 40) MessageBox.Show("       X AND Y must be \n    less or equal than 40");
            else gt(x, y);
        }
        private void bar_Scroll(object sender, ScrollEventArgs e)
        {
            DifficultyBox.Text = (bar.Value / 4).ToString();
        }
    }
}