
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Handlers;

namespace SnowConeTycoon.Shared.Animations
{
    public class PulseImage
    {
        string ImageName = string.Empty;
        public Vector2 Position;
        int ImageWidth;
        int ImageHeight;
        int ScaleTime = 0;
        int ScaleTimeTotal = 250;
        float Scale = 0.01f;
        Vector2 ScaleStart = new Vector2(1.0f, 1);
        Vector2 ScaleMiddle = new Vector2(2.5f, 1);
        Vector2 ScaleEnd = new Vector2(2.0f, 1);
        bool ScalingUp = true;
        bool IsDone = false;

        public PulseImage(string imageName, Vector2 position, float startScale, float middleScale, float endScale, int scaleTimeTotal = 250)
        {
            ImageName = imageName;
            ScaleTimeTotal = scaleTimeTotal;
            ImageWidth = ContentHandler.Images[imageName].Width;
            ImageHeight = ContentHandler.Images[imageName].Height;
            Position = position;
            ScaleStart.X = startScale;
            ScaleMiddle.X = middleScale;
            ScaleEnd.X = endScale;
        }

        public void Reset()
        {
            ScaleTime = 0;
            Scale = 0.5f;
            ScalingUp = true;
            IsDone = false;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsDoneAnimating())
            {
                ScaleTime += gameTime.ElapsedGameTime.Milliseconds;

                var amt = ScaleTime / (float)ScaleTimeTotal;

                if (ScalingUp)
                {
                    Scale = Vector2.SmoothStep(ScaleStart, ScaleMiddle, amt).X;

                    if (ScaleTime >= ScaleTimeTotal)
                    {
                        ScalingUp = false;
                        ScaleTime = 0;
                    }
                }
                else
                {
                    Scale = Vector2.SmoothStep(ScaleMiddle, ScaleEnd, amt).X;

                    if (ScaleTime >= ScaleTimeTotal)
                    {
                        IsDone = true;
                    }
                }
            }
        }

        public bool IsDoneAnimating()
        {
            return IsDone;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images[ImageName], new Rectangle((int)Position.X, (int)Position.Y, (int)(ImageWidth * Scale), (int)(ImageHeight * Scale)), null, Color.White, 0f, new Vector2(ImageWidth / 2, ImageHeight / 2), SpriteEffects.None, 1f);
        }
    }
}
