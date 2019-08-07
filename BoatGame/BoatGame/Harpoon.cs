using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Boat_game
{
    class Harpoon
    {
        public int x, y, width, height;
        public int harpoonRotated;
        public double xSpeed, ySpeed;
        public Image harpoon;//variable for the harpoon's image
        public Rectangle harpoonRec;//variable for a rectangle to place our image in
        public Matrix matrixHarpoon;//matrix to enable us to rotate harpoon in the same angle as the player
        Point centerHarpoon;//centre of harpoon

        // this gives us the position of the player which we can then use to place the harpoon where the player is located and at the correct angle
        public Harpoon(Rectangle PlayerRec, int harpoonRotate)
        {
            //set default values for harpoon
            width = 40;
            height = 40;
            harpoon = Image.FromFile("Harpoon.png");
            harpoonRec = new Rectangle(x, y, width, height);
            //this code works out the speed of the harpoon to be used in the moveHarpoon method
            xSpeed = 30 * (Math.Cos((harpoonRotate - 90) * Math.PI / 180));
            ySpeed = 30 * (Math.Sin((harpoonRotate + 90) * Math.PI / 180));
            //calculate x,y to move harpoon to middle of player in drawHarpoon method
            x = PlayerRec.X + PlayerRec.Width / 2;
            y = PlayerRec.Y + PlayerRec.Height / 2;
            //pass harpoonRotate angle to harpoonRotated so that it can be used in the drawHarpoon method
            harpoonRotated = harpoonRotate;

        }

        public void drawHarpoon(Graphics g)
        {
            //centre harpoon 
            centerHarpoon = new Point(x, y);
            //instantiate a Matrix object called matrixHarpoon
            matrixHarpoon = new Matrix();
            //rotate the matrix (harpoonRec) about its centre
            matrixHarpoon.RotateAt(harpoonRotated, centerHarpoon);
            //Set the current draw location to the rotated matrix point i.e. where harpponRec is now
            g.Transform = matrixHarpoon;

            //Draw the harpoon
            g.DrawImage(harpoon, harpoonRec);

        }
        public void moveHarpoon(Graphics g)
        {
            x += (int)xSpeed;//cast double to an integer value
            y -= (int)ySpeed;
            harpoonRec.Location = new Point(x, y);//harpoon new location


        }
    }


}
