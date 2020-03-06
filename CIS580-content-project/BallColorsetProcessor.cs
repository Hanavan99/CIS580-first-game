using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIS580_content_project
{
    [ContentProcessor(DisplayName = "Ball Colorset")]
    public class BallColorsetProcessor : ContentProcessor<BallColorContent, BallColorContent>
    {

        public override BallColorContent Process(BallColorContent input, ContentProcessorContext context)
        {
            // no need to do any processing here
            return input;
        }

    }
}
