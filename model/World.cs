using CIS580_first_game.GameState;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIS580_first_game.Model
{
    public class World
    {

        // allows for spatial partitioning
        private Dictionary<int, List<Ball>> balls = new Dictionary<int, List<Ball>>();
        private Dictionary<int, List<Ball>> tempballs = new Dictionary<int, List<Ball>>();

        public void Update(GameTime gameTime, MyGameState gameState)
        {
            tempballs.Clear();
            CopyDictionary(balls, tempballs);
            foreach (KeyValuePair<int, List<Ball>> l in balls)
            {
                int tile = l.Key;
                int xmin = tile * TerrainModel.TerrainWidth;
                int xmax = xmin + TerrainModel.TerrainWidth;
                foreach (Ball b in l.Value.ToArray())
                {
                    if (b.bounds.X < xmin)
                    {
                        if (!tempballs.ContainsKey(tile - 1))
                        {
                            tempballs[tile - 1] = new List<Ball>();
                        }
                        tempballs[tile - 1].Add(b);
                        tempballs[tile].Remove(b);
                    } else if (b.bounds.X > xmax)
                    {
                        if (!tempballs.ContainsKey(tile + 1))
                        {
                            tempballs[tile + 1] = new List<Ball>();
                        }
                        tempballs[tile + 1].Add(b);
                        tempballs[tile].Remove(b);
                    }
                }
            }
            CopyDictionary(tempballs, balls);
        }

        public void Clear()
        {
            foreach (List<Ball> l in balls.Values)
            {
                l.Clear();
            }
        }

        public void AddBall(Ball b)
        {
            int tile = (int) (b.bounds.X / TerrainModel.TerrainWidth);
            if (!balls.ContainsKey(tile))
            {
                balls[tile] = new List<Ball>();
            }
            balls[tile].Add(b);
        }

        private void CopyDictionary<K, V>(Dictionary<K, V> src, Dictionary<K, V> dest)
        {
            foreach(KeyValuePair<K, V> kvp in src)
            {
                dest[kvp.Key] = kvp.Value;
            }
        }

    }
}
