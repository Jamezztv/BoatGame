using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Boat_game
{
    public partial class Form1 : Form
    {
        Graphics g; //declare a graphics object called g
        Enemies[] Enemies = new Enemies[7]; // enemy array (left going right)
        Enemies2[] Enemies2 = new Enemies2[7]; // enemy2 array ( right going left)
        Player Player = new Player(); // player instance
        bool hastimespeedup; // bool used for checking if player used speed up
        bool Space,turnLeft, turnRight; //bool 
        int score = 0, lives; // values for life and score
        //declare a list for harppons from the harpoon class
        List<Harpoon> harpoon = new List<Harpoon>();

        public Form1()
        {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered",
              BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panel1, new object[] { true });

            //initializes the enemies
            for (int i = 0; i < 7; i++)
            {
                int y = 40 + (i * 100);
                Enemies[i] = new Enemies(y);
                Enemies2[i] = new Enemies2(y);
            }



        }

        // enables/disables timers need
        private void Start_click(object sender, EventArgs e)
        {
            tmrBoat.Enabled = true;
            tmrEnemies.Enabled = false;

            textBox1.Enabled = false;
            button2.Enabled = false;
            button1.Enabled = true;
        }


       
        // enables/disables timers need
        private void Stop_click(object sender, EventArgs e)
        {
            tmrEnemies.Enabled = false;
            tmrBoat.Enabled = false;


            textBox1.Enabled = false;
            button2.Enabled = true;
            button1.Enabled = false;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            button2.Enabled = true;
            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, @"^[a-zA-Z-Key.Back]+$"))
            {
                button2.Enabled = false;
                MessageBox.Show("This textbox accepts only alphabetical characters");
            }
        }

        // update call
        private void tmrEnemies_Tick(object sender, EventArgs e)
        {

            //loops enemies 
            for (int i = 0; i < 7; i++)
            {

                Enemies2[i].moveEnemies();

                if (countDown <= 0)
                {
                    tmrEnemies.Enabled = false;
                }

                label4.Text = score.ToString();// display score
            }
            panel1.Invalidate();//makes the paint event fire to redraw the panel



        }


        //on keydown
       
        //on keyup 
        

        private void BoatForm_Load(object sender, EventArgs e)
        {
            MessageBox.Show("The aim of the game is to get the highest score within a minute the enemies get harder and harder in intervals \nUse the Left and right arrow keys to rotate \nPush spacebar to shot harpoons at oncoming enemy boats");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tmrBoat_Tick(object sender, EventArgs e)
        {
          
        }

        private void tmrShoot_Tick(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            //get the graphics used to paint on the panel control
            g = e.Graphics;
            for (int i = 0; i < 7; i++)
            {
                // generate a random number from 5 to 20 and put it in rndmspeed


                //call the Enemy class's drawEnemies method to draw the images
                Enemies[i].drawEnemies(g);
                Enemies2[i].drawEnemies(g);


            }
            Player.drawPlayer(g);
            foreach (Harpoon m in harpoon)
            {
                m.drawHarpoon(g);
                m.moveHarpoon(g);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left) { turnLeft = true; }
            if (e.KeyData == Keys.Right) { turnRight = true; }
            if (e.KeyData == Keys.Space) { Space = true; }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left) { turnLeft = false; }
            if (e.KeyData == Keys.Right) { turnRight = false; }
            if (e.KeyData == Keys.Space) { Space = false; }
            {
                // adds harpoon
                harpoon.Add(new Harpoon(Player.PlayerRec, Player.rotationAngle));

            }

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
	frmHighScores frmHighScore2 = new frmHighScores();
            Hide();
            frmHighScore2.ShowDialog();


        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        //current time value in the countdown
        float countDown = 30;


        private void update_tmr(object sender, EventArgs e)
        {
            //starts the region
            #region Player

            // rotates the players 
            if (turnRight)
            {
                Player.rotationAngle += 25;
            }
            if (turnLeft)
                Player.rotationAngle -= 25;
            //ends the region
            #endregion

            #region Enemies
            // looping through all the enemies
            for (int i = 0; i < 7; i++)
            {

                Enemies[i].moveEnemies();

                label4.Text = score.ToString();// displays the score
            }
            panel1.Invalidate();//makes the paint event fire to redraw the panel



            #endregion

            #region Harpoon


            foreach (Enemies p in Enemies)
            {

                foreach (Harpoon m in harpoon)
                {
                    //checks all the collision enemies
                    if (p.EnemiesRec.IntersectsWith(m.harpoonRec))
                    {

                        score += 1;
                        p.x = -20;// relocate enemiy to the top of the form
                        harpoon.Remove(m);//removes harpoon
                        break; //get out of the loop
                    }


                }
            }
            foreach (Enemies2 p in Enemies2)
            {

                foreach (Harpoon m in harpoon)
{
                   // checks collision for enemies2
                  if (p.EnemiesRec.IntersectsWith(m.harpoonRec))
                    {

                        score += 1;
                        p.x = 800;// relocate planet to the top of the form
                        harpoon.Remove(m); // removes harpoon
                        break; // get out of loop
                    }


                }
           }
            #endregion

            #region Countdown
            // this checks all the countdown 
            countDown -= 10f / tmrBoat.Interval; //goes down correct interval
            label7.Text = countDown.ToString();
            if (countDown <= 0)
            {
                tmrBoat.Stop();
                label7.Text = "0";
            }
            if (countDown <= 20)
            {
                //speeds enemies up when time is checked and has passed
                if (!hastimespeedup)
                {


                    for (int i = 0; i < 7; i++)
                    {
                        // after time speedup change speed to 20
                        Enemies[i].speed += 20;
                        Enemies2[i].speed += 20;
                    }

                    hastimespeedup = true; // setting the bool
                }
            }
            if (countDown <= 10)
            {
                tmrEnemies.Start(); //starts the timer for enemies2 after countdown reaches 10
            }
            #endregion
        }



    }
}

