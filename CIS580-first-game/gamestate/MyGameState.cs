using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIS580_first_game.GameState
{
    public class MyGameState
    {
        public int ViewportWidth { get; set; }

        public int ViewportHeight { get; set; }

        public Matrix CameraMatrix { get; set; }

        public float WorldSize { get; set; }

    }
}
