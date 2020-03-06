using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIS580_content_project
{
    public class BallColorsetReader : ContentTypeReader<BallColorContent>
    {

        protected override BallColorContent Read(ContentReader input, BallColorContent existingInstance)
        {
            List<Color> colorList = new List<Color>();
            int count = input.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                colorList.Add(new Color(input.ReadByte(), input.ReadByte(), input.ReadByte()));
            }
            BallColorContent content = new BallColorContent();
            content.ColorList = colorList;
            return content;
        }

    }
}
