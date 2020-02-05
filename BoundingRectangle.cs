using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS580_first_game
{
    public struct BoundingRectangle
    {
        public float X, Y, Width, Height;

        public BoundingRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        // allows for casting of a BoundingRectangle to a Rectangle
        public static implicit operator Rectangle(BoundingRectangle b) => new Rectangle((int) b.X, (int) b.Y, (int) b.Width, (int) b.Height);

        
    }
}
