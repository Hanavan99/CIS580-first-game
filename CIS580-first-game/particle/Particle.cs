using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIS580_first_game.particle
{
    public struct Particle
    {
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 acceleration;
        public float scale;
        public float life;
        public Color color;
    }
}
