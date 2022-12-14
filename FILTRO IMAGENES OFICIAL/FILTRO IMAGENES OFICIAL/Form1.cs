using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FILTRO_IMAGENES_OFICIAL
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        int[, ,] matrix = new int[3, 5000 + 1, 5000 + 1];
        int[, ,] new_matrix = new int[3, 5000 + 1, 5000 + 1];
        int[,] mask = null;
        int n = 3;
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            button2.Text = "Desabilidado";
            button2.BackColor = Color.Empty;
            button3.Text = "Desabilidado";
            button3.BackColor = Color.Empty;
            button4.Text = "Desabilidado";
            button4.BackColor = Color.Empty;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Todos|*.*|Archivos JPEG|*.jpg|Archivos GIF|*.gif";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            bmp = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = bmp;

            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;

            button2.Text = "Contorno Blanco";
            button2.BackColor = Color.Black;
            button3.Text = "Contorno Negro";
            button3.BackColor = Color.Black;
            button4.Text = "Limpiar Imagen";
            button4.BackColor = Color.Black;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Color c = new Color();
            String datos = "";
            
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    matrix[0, i, j] = c.R;
                    matrix[1, i, j] = c.G;
                    matrix[2, i, j] = c.B;
                }
            }

            //textBox1.Text = datos;
            filtro_imagen();

        }
        private void filtro_imagen() {
            mask = new int[,]{{0, 1, 0}, {1, -4, 1}, {0, 1, 0} };
            Color c = new Color();
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            for (int i = 0; i < bmp.Width; i++) {
                for (int j = 0; j < bmp.Height; j++) {
                    if(i > 0 && i + 1 < bmp.Width && j > 0 && j + 1 < bmp.Height) {
                        int value = 0;
                        for (int k = 0; k < 3; k++) { // para el color RGB
                            for (int i2 = 0; i2 < n; i2++) {  // mult matrix
                                for (int j2 = 0; j2 < n; j2++) { // mult matrix
                                    value += mask[i2, j2] * matrix[k, i - 1 + i2, j - 1 + j2];
                                }
                            }
                            if (value < 0)
                                value = 0;
                            if (value > 255)
                                value = 255;
                            new_matrix[k, i, j] = value;
                        }

                        bmp2.SetPixel(i, j, Color.FromArgb(new_matrix[0, i, j], new_matrix[0, i, j], new_matrix[0, i, j]));
                    }else {
                        for (int k = 0; k < 3; k++) {
                            new_matrix[k, i, j] = matrix[k, i, j];
                        }
                        bmp2.SetPixel(i, j, Color.FromArgb(new_matrix[0, i, j], new_matrix[0, i, j], new_matrix[0, i, j]));
                    }
                }
            }

            pictureBox2.Image = bmp2;
        }
        private void filtro_imagen2()
        {
            mask = new int[,] { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
            Color c = new Color();
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    if (i > 0 && i + 1 < bmp.Width && j > 0 && j + 1 < bmp.Height)
                    {
                        int value = 0;
                        for (int k = 0; k < 3; k++)
                        { // para el color RGB
                            for (int i2 = 0; i2 < n; i2++)
                            {  // mult matrix
                                for (int j2 = 0; j2 < n; j2++)
                                { // mult matrix
                                    value += mask[i2, j2] * matrix[k, i - 1 + i2, j - 1 + j2];
                                }
                            }
                            if (value < 0)
                                value = 0;
                            if (value > 255)
                                value = 255;
                            new_matrix[k, i, j] = value;
                        }

                        bmp2.SetPixel(i, j, Color.FromArgb(255 - new_matrix[0, i, j], 255 - new_matrix[0, i, j], 255 - new_matrix[0, i, j]));
                    }
                    else
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            new_matrix[k, i, j] = matrix[k, i, j];
                        }
                        bmp2.SetPixel(i, j, Color.FromArgb(new_matrix[0, i, j], new_matrix[0, i, j], new_matrix[0, i, j]));
                    }
                }
            }

            pictureBox2.Image = bmp2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    bmp2.SetPixel(i, j, bmp.GetPixel(i, j));
                }
            }
            pictureBox2.Image = bmp2;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            mask = new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            Color c = new Color();
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    if (i > 0 && i + 1 < bmp.Width && j > 0 && j + 1 < bmp.Height)
                    {
                        int value = 0;
                        for (int k = 0; k < 3; k++)
                        { // para el color RGB
                            for (int i2 = 0; i2 < n; i2++)
                            {  // mult matrix
                                for (int j2 = 0; j2 < n; j2++)
                                { // mult matrix
                                    value += mask[i2, j2] * matrix[k, i - 1 + i2, j - 1 + j2];
                                }
                            }
                            if (value < 0)
                                value = 0;
                            if (value > 255)
                                value = 255;
                            new_matrix[k, i, j] = value;
                        }

                        bmp2.SetPixel(i, j, Color.FromArgb(new_matrix[0, i, j], new_matrix[0, i, j], new_matrix[0, i, j]));
                    }
                    else
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            new_matrix[k, i, j] = matrix[k, i, j];
                        }
                        bmp2.SetPixel(i, j, Color.FromArgb(new_matrix[0, i, j], new_matrix[0, i, j], new_matrix[0, i, j]));
                    }
                }
            }

            pictureBox2.Image = bmp2;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Color c = new Color();
            String datos = "";

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    matrix[0, i, j] = c.R;
                    matrix[1, i, j] = c.G;
                    matrix[2, i, j] = c.B;
                }
            }

            //textBox1.Text = datos;
            filtro_imagen2();
        }

        
    }
}
