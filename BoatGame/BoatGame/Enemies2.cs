using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Boat_game
{
    class Enemies2
    {
        // declare fields to use in the class
        public int x, y, width, height;//variables for the Enemies2 rectangle
        public Image EnemiesImage;//variable for the Enemies2 image
        public int speed = 10; // Variable for the speed
        public Rectangle EnemiesRec;//variable for a rectangle to place our image in
        public int score; //variable for the score

        //Create a constructor (initialises the values of the fields)
        public Enemies2(int spacing)
        {
            // sets default values
            y = spacing;
            x = 800;
            width = 60;
            height = 60;
            EnemiesImage = Image.FromFile("2Enemies.png");
            EnemiesRec = new Rectangle(x, y, width, height);
        }

        // Draws the enemies
        public void drawEnemies(Graphics g)
        {
            EnemiesRec = new Rectangle(x, y, width, height);
            g.DrawImage(EnemiesImage, EnemiesRec);
        }
        // declares how the enemies move
        public void moveEnemies()
        {
            x -= speed;

            // Sets a barrier so if the enemies hits a certain point go back to start location
            EnemiesRec.Location = new Point(x, y);
            if (EnemiesRec.Location.X < 10)
            {
                x = 800;
                EnemiesRec.Location = new Point(x, y);

            }
        }
    }
}
