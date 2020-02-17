﻿using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIS580_first_game.Model
{
    public class BallModel
    {

        public const int SpriteSize = 50;

        public Texture2D Texture { get; set; }
        public SoundEffect BounceSound { get; set; }

        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("ball");
            BounceSound = content.Load<SoundEffect>("impact");
        }


    }
}
