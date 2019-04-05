using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Handlers;

namespace SnowConeTycoon.Shared.Animations
{
    public class FadeOutImage
    {
        string ImageName = string.Empty;
        public Vector2 Position;
        int ImageWidth;
        int ImageHeight;
        int FadeTime = 0;
        int FadeTimeTotal = 500;
        Vector2 FadeStart = new Vector2(1, 1);
        Vector2 FadeEnd = new Vector2(0, 0);
        float Fade = 1f;

        public FadeOutImage(string imageName, Vector2 position, int fadeTimeTotal = 500)
        {
            ImageName = imageName;
            FadeTimeTotal = fadeTimeTotal;
            ImageWidth = ContentHandler.Images[imageName].Width;
            ImageHeight = ContentHandler.Images[imageName].Height;
            Position = position;
        }

        public void Reset()
        {
            Fade = 1;
            FadeTime = 0;
        }

        public void Update(GameTime gameTime)
        {
            FadeTime += gameTime.ElapsedGameTime.Milliseconds;

            var amt = FadeTime / (float)FadeTimeTotal;

            Fade = Vector2.SmoothStep(FadeStart, FadeEnd, amt).X;

            if (Fade < 0f)
            {
                Fade = 0;
            }
        }

        public bool IsDoneAnimating()
        {
            if (Fade <= 0)
            {
                return true;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images[ImageName], new Rectangle((int)Position.X, (int)Position.Y, ContentHandler.Images[ImageName].Width, ContentHandler.Images[ImageName].Height), null, Color.FromNonPremultiplied(new Vector4(1, 1, 1, Fade)), 0f, new Vector2(ContentHandler.Images[ImageName].Width / 2, ContentHandler.Images[ImageName].Height / 2), SpriteEffects.None, 1f);
        }
    }
}
