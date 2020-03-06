using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CIS580_content_project
{
    [ContentImporter(".colorlist", DisplayName = "Ball Colorset Importer", DefaultProcessor = "BallColorsetProcessor")]
    public class BallColorsetImporter : ContentImporter<BallColorContent>
    {

        public override BallColorContent Import(string filename, ContentImporterContext context)
        {
            BallColorContent content = new BallColorContent();
            content.ColorList = new List<Color>();

            StreamReader reader = new StreamReader(filename);
            using (reader)
            {
                while (!reader.EndOfStream)
                {
                    string[] data = reader.ReadLine().Split(',');
                    content.ColorList.Add(new Color(int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2])));
                }
            }

            return content;
        }

    }
}
