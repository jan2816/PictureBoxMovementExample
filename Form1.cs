using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureBoxMovementExample
{
    public partial class Form1 : Form
    {

        private const int pictureBox1Speed = 5;
        private const int pictureBox2Speed = 10;
        private const int pictureBox2YOffset = 50;
        private bool pictureBox2MovingLeft = true;
        private int pictureBox2MoveCount = 0;
        private List<PictureBox> pictureBoxesToAvoid = new List<PictureBox>(); // Lista de pictureBoxes lanzados

        public Form1()
        {
            InitializeComponent();



            // Configurar eventos de teclado
            KeyDown += Form1_KeyDown;

            // Configurar temporizador para mover pictureBox2
            Timer timer = new Timer();
            timer.Interval = 50;
            timer.Tick += timer1_Tick;
            timer.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Mover pictureBox1 de izquierda a derecha (limitando dentro del formulario)
            if (e.KeyCode == Keys.Left)
            {
                if (pictureBox1.Left - pictureBox1Speed >= 0)
                {
                    pictureBox1.Left -= pictureBox1Speed;
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (pictureBox1.Right + pictureBox1Speed <= ClientSize.Width)
                {
                    pictureBox1.Left += pictureBox1Speed;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                // Mover pictureBox2 de derecha a izquierda y hacia abajo (limitando dentro del formulario)



                if (pictureBox2MovingLeft)
            { 
                if (pictureBox2.Left - pictureBox2Speed >= 0)
                {
                    pictureBox2.Left -= pictureBox2Speed;
                }
                else
                {
                    pictureBox2MovingLeft = false;
                    pictureBox2MoveCount++;
                }
            }
            else
            {
                if (pictureBox2.Right + pictureBox2Speed <= ClientSize.Width)
                {
                    pictureBox2.Left += pictureBox2Speed;
                }
                else
                {
                    pictureBox2MovingLeft = true;
                    if (pictureBox2MoveCount >= 2)

                    {
                          pictureBox2MoveCount = 0;
                           if (pictureBox2.Top + pictureBox2YOffset <=                    ClientSize.Height - pictureBox2.Height)
                            {
                            pictureBox2.Top += pictureBox2YOffset;
                           }
                        else
                        {
                                 if (pictureBox2.Bounds.IntersectsWith(pictureBox1.Bounds))
                                 {
                                Application.Exit();
                                 }
                                    else
                                    {
                                     // Cerrar la aplicación cuando llega final
                                        Application.Exit();
                                    }
                        }




                    }
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            // Lanzar un nuevo PictureBox de manera aleatoria con una probabilidad del 10% en cada intervalo
            if (pictureBox2MoveCount % 3 == 0 && pictureBox2MoveCount > 0 && new Random().Next(0, 10) == 0)
            {
                PictureBox newPictureBox = new PictureBox();
                newPictureBox.BackColor = Color.Green;
                newPictureBox.Size = new Size(30, 30);
                newPictureBox.Location = new Point(pictureBox2.Left, pictureBox2.Top);
                Controls.Add(newPictureBox);
                pictureBoxesToAvoid.Add(newPictureBox);
            }
        }
    }
}







