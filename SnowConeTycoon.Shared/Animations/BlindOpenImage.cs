using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Handlers;

namespace SnowConeTycoon.Shared.Animations
{
    public class BlindOpenImage
    {
        string ImageName = string.Empty;
        public Vector2 Position;
        int ImageWidth;
        int ImageHeight;
        int ScaleTime = 0;
        int ScaleTimeTotal = 250;
        float Scale = 0.01f;
        Vector2 ScaleStart = new Vector2(0.01f, 1);
        Vector2 ScaleEnd = new Vector2(1, 1);
        
        public BlindOpenImage(string imageName, Vector2 position, int scaleTimeTotal = 250)
        {
            ImageName = imageName;
            ScaleTimeTotal = scaleTimeTotal;
            ImageWidth = ContentHandler.Images[imageName].Width;
            ImageHeight = ContentHandler.Images[imageName].Height;
            Position = position;
        }

        public void Reset()
        {
            ScaleTime = 0;
            Scale = 0.01f;
        }

        public void Update(GameTime gameTime)
        {
            ScaleTime += gameTime.ElapsedGameTime.Milliseconds;

            var amt = ScaleTime / (float)ScaleTimeTotal;

            Scale = Vector2.SmoothStep(ScaleStart, ScaleEnd, amt).X;

            if (Scale > 1)
            {
                Scale = 1;
            }
        }

        public bool IsDoneAnimating()
        {
            if (Scale <= 1)
            {
                return true;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images[ImageName], new Rectangle((int)Position.X, (int)Position.Y, (int)(ImageWidth), (int)(ImageHeight * Scale)), null, Color.White, 0f, new Vector2(ImageWidth / 2, ImageHeight / 2), SpriteEffects.None, 1f);
        }
    }
}
