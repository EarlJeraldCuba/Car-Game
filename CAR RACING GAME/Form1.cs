using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAR_RACING_GAME
{
    public partial class Form1 : Form
    {
        int roadspeed;
        int trafficspeed;
        int playerspeed = 12;
        int score;
        int carimage;

        Random rand = new Random();
        Random carposition = new Random();

        bool goleft, goright;


        public Form1()
        {
            InitializeComponent();
            resetgame();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                goright = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
        }

        private void gametimerevent(object sender, EventArgs e)
        {
            txtscore.Text = "SCORE: " + score;
            score++;


            if(goleft == true && player.Left > 3 )
            {
                player.Left -= playerspeed;
            }
            if( goright== true && player.Left < 422)
            {
                player.Left += playerspeed;
            }

            roadtrack1.Top += roadspeed;
            roadtrack2.Top += roadspeed;

            if( roadtrack2.Top > 575)
            {
                roadtrack2.Top = -575;
            }
            if (roadtrack1.Top > 575)
            {
                roadtrack1.Top = -575;
            }

            AI1.Top += trafficspeed;
            AI2.Top += trafficspeed;

            if(AI1.Top > 530)
            {
                changeAIcars(AI1);
            }

            if (AI2.Top > 530)
            {
                changeAIcars(AI2);
            }

            if(player.Bounds.IntersectsWith(AI1.Bounds) || player.Bounds.IntersectsWith(AI2.Bounds))
            {
                gameover();
            }

            if (score > 40 && score < 500)
            {
                award.Image = Properties.Resources.bronze;
            }

            if (score > 500 && score < 2000)
            {
                award.Image = Properties.Resources.silver;
                roadspeed = 20;
                trafficspeed = 22;
            }
            if (score > 2000)
            {
                award.Image = Properties.Resources.gold;
                roadspeed = 25;
                trafficspeed = 27;
            }


        }

        private void changeAIcars(PictureBox tempCar)
        {
            carimage = rand.Next(1, 8);
            switch(carimage)
            {
                case 1:
                    tempCar.Image = Properties.Resources.ambulance;
                    break;
                case 2:
                    tempCar.Image = Properties.Resources.carOrange;
                    break;
                case 3:
                    tempCar.Image = Properties.Resources.carPink;
                    break;
                case 4:
                    tempCar.Image = Properties.Resources.TruckBlue;
                    break;
                case 5:
                    tempCar.Image = Properties.Resources.TruckWhite;
                    break;
                case 6:
                    tempCar.Image = Properties.Resources.carGrey;
                    break;
                case 7:
                    tempCar.Image = Properties.Resources.carYellow;
                    break;
                case 8:
                    tempCar.Image = Properties.Resources.carGreen;
                    break;

            }

            tempCar.Top = carposition.Next(100, 400) * -1;

            if((string)tempCar.Tag =="carleft")
            {
                tempCar.Left = carposition.Next(5, 200);
            }
            if ((string)tempCar.Tag == "carright")
            {
                tempCar.Left = carposition.Next(245, 422);
            }

        }

        private void gameover()
        {
            playsound();
            gametimer.Stop();
            explosion.Visible = true;
            player.Controls.Add(explosion);
            explosion.Location = new Point(-8, 5);
            explosion.BackColor = Color.Transparent;

            award.Visible = true;
            award.BringToFront();

            btnStart.Enabled = true;

        }

        private void resetgame()
        {
            btnStart.Enabled = false;
            explosion.Visible = false;
            award.Visible = false;
            goleft = false;
            goright = false;
            score = 0;
            award.Image = Properties.Resources.bronze;

            roadspeed = 12;
            trafficspeed = 15;

            AI1.Top = carposition.Next(200, 500) * -1;
            AI1.Left = carposition.Next(5, 200);

            AI2.Top = carposition.Next(200, 500) * -1;
            AI2.Left = carposition.Next(245, 422);

            gametimer.Start();
        }

        private void restartgame(object sender, EventArgs e)
        {
            resetgame();
        }

        private void playsound()
        {
            System.Media.SoundPlayer playercrash = new System.Media.SoundPlayer(Properties.Resources.hit);
            playercrash.Play();

        }

    }
}
