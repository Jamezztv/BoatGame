using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Boat_game
{
    class Player
    {
        // declare fields to use in the class

        public int x, y, width, height;//variables for the Player rectangle
        public Image player;//variable for the Player's image
        public int rotationAngle; //angle of rotation
        public Matrix matrix; // used when rotating image
        Point centre; // centre of image

        public Rectangle PlayerRec;//variable for a rectangle to place our image in

        //Create a constructor (initialises the values of the fields)
        public Player()
        {
            // sets default values
            x = 400;
            y = 300;
            width = 40;
            height = 40;
            player = Image.FromFile("player.png");
            PlayerRec = new Rectangle(x, y, width, height);

        }

        // draws the player onto game panel 
        public void drawPlayer(Graphics g)
        {
            //find the centre point of playerRec
            centre = new Point(PlayerRec.X + width / 2, PlayerRec.Y + width / 2);
            //creates a new Matrix object called matrix
            matrix = new Matrix();
            //rotate the matrix about its centre ( playerRec)
            matrix.RotateAt(rotationAngle, centre);
            //Set the current draw location to the rotated matrix point
            g.Transform = matrix;
            //draw the Player
            g.DrawImage(player, PlayerRec);
        }


    }
}