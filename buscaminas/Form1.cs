using ImageMagick;
using Microsoft.VisualBasic.Devices;
using System.CodeDom;
using System.Drawing.Imaging;
using System.IO;

namespace buscaminas
{
    public partial class Form1 : Form
    {
        //path to buscaminas folder
        string root = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
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
        public Form1()
        {
            InitializeComponent();
            gt(4, 4);
        }
        bool casillas_quadradas = false;
        private void gt(int x_ini, int y_ini)
        {
            // control de errores
            if (x_ini <= 0 || x_ini > 40) ;
            if (y_ini <= 0 || y_ini > 40) ;
            //------------------------------
            //tamaño tablero
            int size_x = tablerocon.Size.Height;
            int size_y = tablerocon.Size.Width;
            PictureBox[,] tablero = new PictureBox[x_ini, y_ini];
            int c = 0;
            int sizeframe_h = size_x / x_ini;
            int sizeframe_v = size_y / y_ini;

            // calculo del path de la imagen
            string path = Path.Combine(root, "imagenes", "default.png");
            string path2 = Path.Combine(root, "imagenes", "resized_default.png");
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
                    tablero[i, j].Name = "frame" + c.ToString();
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

        private void click_cell(object sender, MouseEventArgs e)
        {
            PictureBox picture = sender as PictureBox;
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