using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIS580_first_game.Model
{
    public class TerrainModel
    {

        public const int TerrainWidth = 112;
        public const int TerrainHeight = 100;

        public Texture2D Texture { get; set; }

        public void LoadContent(ContentManager manager)
        {
            Texture = manager.Load<Texture2D>("terrain");
        }

    }
}
