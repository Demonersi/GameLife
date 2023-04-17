using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace GameLife
{
    internal class GameEngine
    {
        public uint currentGeneration { get; private set; }
        private bool[,] field;
        private readonly int rows;
        private readonly int cols;
        private Random random = new Random();
        public GameEngine(int cols, int rows, int density)
        {
            this.rows = rows;
            this.cols = cols;
            field = new bool[cols, rows];
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = random.Next(density) == 0;
                }
            }
        }

        private int CountNeighbours(int i, int j)
        {
            int count = 0;
            for (int k = -1; k < 2; k++)
            {
                for (int l = -1; l < 2; l++)
                {
                    var col = (i + k + cols) % cols;
                    var row = (j + l + rows) % rows;
                    var isSelfChecking = col == i && row == j;
                    var hasLife = field[col, row];
                    if (hasLife && !isSelfChecking)
                        count++;
                }
            }
            return count;
        }
        public void NextGeneration()
        {    
            var newField = new bool[cols, rows];

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    var neighboursCount = CountNeighbours(i, j);
                    var haslife = field[i, j];
                    if (!haslife && neighboursCount == 3)
                        newField[i, j] = true;
                    else if (haslife && (neighboursCount < 2 || neighboursCount > 3))
                        newField[i, j] = false;
                    else
                        newField[i, j] = field[i, j];
                }
            }
            field = newField;
            currentGeneration++;
        }
        public bool[,] GetCurrentGeneration()
        {
            var result = new bool[cols, rows];
            for(int x = 0; x<cols;x++)
            {
                for (int y = 0; y < rows; y++)
                    result[x, y] = field[x, y];
            }
            return result;
        }
        private bool ValidateCellPosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }
        private void UpdateCell(int x,int y, bool state)
        {
            if (ValidateCellPosition(x, y))
                field[x, y] = state;
        }
        public void AddCell(int x, int y)
        {
            UpdateCell(x, y, state: true);
        }
        public void RemoveCell(int x, int y)
        {
            UpdateCell(x, y, state: false);
        }
    }
}
