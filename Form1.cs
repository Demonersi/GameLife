using System.Drawing;
namespace GameLife
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private int resolution;
        private bool[,] field;
        public Form1()
        {
            InitializeComponent();
        }
        private void StartGame()
        {
            if (timer1.Enabled)
                return;
            numDensity.Enabled = false;
            numResolution.Enabled = false;

            resolution = (int)numResolution.Value;
            pictureBox1.Image = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void bStart_Click(object sender, EventArgs e)
        {
            StartGame();
            //graphics.FillRectangle(Brushes.Crimson, 0, 0, resolution,resolution);
        }

        private void bStop_Click(object sender, EventArgs e)
        {

        }
    }
}