namespace buscaminas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            gt(4, 4);
        }
        int size_x = 400;
        int size_y = 600;

        private void gt(int x_ini, int y_ini)
        {
            // control de errores
            if (x_ini <= 0 || x_ini > 40) ;
            if (y_ini <= 0 || y_ini > 40) ;
            //------------------------------
            PictureBox[,] tablero = new PictureBox[x_ini, y_ini];
            int c = 0;
            int sizeframe_h = size_x / x_ini;
            int sizeframe_v = size_y / y_ini;

            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    tablero[i, j] = new PictureBox();
                    tablero[i, j].Name = "frame" + c.ToString();
                    tablero[i, j].Size = new Size(sizeframe_v, sizeframe_h);
                    tablero[i, j].Location = new Point(j * sizeframe_v, i * sizeframe_h);
                    tablero[i, j].BorderStyle = BorderStyle.FixedSingle;
                    tablero[i, j].Click += new EventHandler(click_cell);
                    c++;
                    tablerocon.Controls.Add(tablero[i, j]);
                }
            }
        }

        private void click_cell(object sender, EventArgs args)
        {
            PictureBox picture = sender as PictureBox;
            MessageBox.Show(picture.Name);
        }
        private void gen_func(object sender, EventArgs args)
        {
            //int x, y;
            //x = Int32.Parse("");
        }
    }
}