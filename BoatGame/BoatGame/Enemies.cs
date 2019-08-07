using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Boat_game
{
    class Enemies
    {
        // declare fields to use in the class
        public int x, y, width, height;//variables for the rectangle
        public Image EnemiesImage;//variable for the Enemie's image
        public int speed = 10;
        public Rectangle EnemiesRec;//variable for a rectangle to place our image in
        public int score;
        //Create a constructor (initialises the values of the fields)
        public Enemies(int spacing)
        {
            y = spacing;

            // sets default values
            x = 10;
            width = 60;
            height = 60;
            EnemiesImage = Image.FromFile("player.png");
            EnemiesRec = new Rectangle(x, y, width, height);
        }


        // Methods for the Ememies class
        public void drawEnemies(Graphics g)
        {
            EnemiesRec = new Rectangle(x, y, width, height);
            g.DrawImage(EnemiesImage, EnemiesRec);
        }
        public void moveEnemies()
        {
            //sets speed to 10
            x += speed;

            //sets barrier so if player reaches certain location spawn back at start location
            EnemiesRec.Location = new Point(x, y);
            if (EnemiesRec.Location.X > 800)
            {
                x = 20;
                EnemiesRec.Location = new Point(x, y);

            }
        }
    }
}
