using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS580_first_game
{
    public struct BoundingCircle
    {
        public float X, Y, Radius;

        public BoundingCircle(float x, float y, float radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public static implicit operator Rectangle(BoundingCircle c) => new Rectangle((int)c.X - (int)c.Radius, (int)c.Y - (int) c.Radius, (int)c.Radius * 2, (int)c.Radius * 2);


    }
}
