using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS580_first_game.Collisions
{
    public static class Collisions
    {

        public static bool CollidesWith(this BoundingRectangle a, BoundingRectangle b)
        {
            return !(a.X > b.X + b.Width || a.X + a.Width < b.X || a.Y > b.Y + b.Height || a.Y + a.Height < b.Y);
        }

        public static bool CollidesWith(this BoundingCircle a, BoundingCircle b)
        {
            float distance = (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
            return distance < (a.Radius + b.Radius) * (a.Radius + b.Radius);
        }

        public static bool CollidesWith(this BoundingCircle c, BoundingRectangle r)
        {
            //var closestX = Math.Max(Math.Min(c.X, r.X + r.Width), r.X);
            //var closestY = Math.Max(Math.Min(c.Y, r.Y + r.Height), r.Y);
            //return (c.Radius * c.Radius <= Math.Pow(closestX - r.X, 2) + Math.Pow(closestY - r.Y, 2));
            var cX = c.X - c.Radius;
            var cY = c.Y - c.Radius;
            var cRadius = c.Radius;
            var rX = r.X;
            var rY = r.Y;
            var rWidth = r.Width;
            var rHeight = r.Height;

            // adapted from https://stackoverflow.com/questions/401847/circle-rectangle-collision-detection-intersection
            // --------------------------------------------------
            var distX = Math.Abs(cX - rX);
            var distY = Math.Abs(cY - rY);

            if (distX > (rWidth / 2 + cRadius)) { return false; }
            if (distY > (rHeight / 2 + cRadius)) { return false; }

            if (distX <= (rWidth / 2)) { return true; }
            if (distY <= (rHeight / 2)) { return true; }

            var cornerDistance_sq = Math.Pow(distX - rWidth / 2, 2) +
                                 Math.Pow(distY - rHeight / 2, 2);

            return (cornerDistance_sq <= (cRadius * cRadius));
            // --------------------------------------------------
        }

        public static bool CollidesWith(this BoundingRectangle r, BoundingCircle c)
        {
            return c.CollidesWith(r);
        }

        public static bool CollidesWith(this Vector2 v, BoundingCircle c)
        {
            return Math.Pow(c.Radius, 2) >= Math.Pow(v.X - c.X, 2) + Math.Pow(v.Y - c.Y, 2);
        }

        public static bool CollidesWith(this BoundingCircle c, Vector2 v)
        {
            return v.CollidesWith(c);
        }

    }
}
