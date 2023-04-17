using System.Drawing;
using static GameLife.GameEngine;
namespace GameLife
{
    public partial class Form1 : Form
    {
        
        private Graphics graphics;
        private int resolution;
        private GameEngine gameEngine;
        
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

            gameEngine = new GameEngine
                (
                    cols: pictureBox1.Width/resolution,
                    rows: pictureBox1.Height / resolution,
                    density:(int)(numDensity.Minimum)+(int)(numDensity.Maximum)-(int)numDensity.Value
                );




            Text = $"Поколение {gameEngine.currentGeneration}";

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
        }
        private void DrawNextGeneration()
        {
            graphics.Clear(Color.Black);
            var field = gameEngine.GetCurrentGeneration();
            for(int x=0;x<field.GetLength(0);x++)
                for(int y=0;y<field.GetLength(1);y++)
                    if (field[x,y])
                        graphics.FillRectangle(Brushes.DarkCyan, x * resolution, y * resolution, resolution - 1, resolution - 1);
            

            pictureBox1.Refresh();
            Text = $"Поколение {gameEngine.currentGeneration}";
            gameEngine.NextGeneration();
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawNextGeneration();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            StartGame();
            //graphics.FillRectangle(Brushes.Crimson, 0, 0, resolution,resolution);
        }
        private void StopGame()
        {
            if (!timer1.Enabled)
                return;
            timer1.Stop();
            numDensity.Enabled = true;
            numResolution.Enabled = true;
        }
        private void bStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
                return;
            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                gameEngine.AddCell(x, y);
            }
            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                gameEngine.RemoveCell(x, y);
            }
        }
        
    }
}