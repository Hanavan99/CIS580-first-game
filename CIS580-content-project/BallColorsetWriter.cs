using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace CIS580_content_project
{
    [ContentTypeWriter]
    public class BallColorsetWriter : ContentTypeWriter<BallColorContent>
    {

        protected override void Write(ContentWriter output, BallColorContent value)
        {
            output.Write(value.ColorList.Count);
            foreach (Color c in value.ColorList)
            {
                output.Write(c.R);
                output.Write(c.G);
                output.Write(c.B);
            }
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(BallColorContent).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(BallColorsetReader).AssemblyQualifiedName;
        }

    }
}
