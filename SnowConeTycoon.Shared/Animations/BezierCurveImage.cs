using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Handlers;

namespace SnowConeTycoon.Shared.Animations
{
    public class BezierCurveImage
    {
        float Time = 0f;
        float TimeTotal = 1.0f;
        Vector2 Position;
        string ImageName = string.Empty;
        Vector2 P0;
        Vector2 P1;
        Vector2 P2;
        Vector2 P3;
        int X;
        int Y;
        int Width;
        int Height;
        bool DoneAnimating = false;

        public BezierCurveImage(string imageName, int x, int y)
        {
            X = x;
            Y = y;
            ImageName = imageName;
            Width = ContentHandler.Images[imageName].Width;
            Height = ContentHandler.Images[imageName].Height;
            Reset();
        }

        public void Reset()
        {
            DoneAnimating = false;
            Time = 0f;
            P0 = new Vector2(X, Y);
            P1 = new Vector2(-500, 1240);
            P2 = new Vector2(3240, 650);
            P3 = new Vector2(40, 40);
            Position = P0;
        }

        public void Update(GameTime gameTime)
        {       
            Time += 0.01f;

            if (Time >= TimeTotal)
            {
                DoneAnimating = true;
                Position = P0;
            }

            Position = GetBezierPoint(Time, P0, P1, P2, P3);
        }

        public bool IsDoneAnimating()
        {
            return DoneAnimating;
        }

        public Vector2 GetBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            float cx = 3 * (p1.X - p0.X);
            float cy = 3 * (p1.Y - p0.Y);

            float bx = 3 * (p2.X - p1.X) - cx;
            float by = 3 * (p2.Y - p1.Y) - cy;

            float ax = p3.X - p0.X - cx - bx;
            float ay = p3.Y - p0.Y - cy - by;

            float Cube = t * t * t;
            float Square = t * t;

            float resX = (ax * Cube) + (bx * Square) + (cx * t) + p0.X;
            float resY = (ay * Cube) + (by * Square) + (cy * t) + p0.Y;

            return new Vector2(resX, resY);
        }

        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            spriteBatch.Draw(ContentHandler.Images[ImageName], new Rectangle((int)Position.X, (int)Position.Y, (int)(Width * scale), (int)(Height * scale)), null, Color.White, 0f, new Vector2((Width)/ 2, (Height) / 2), SpriteEffects.None, 1f);
        }
    }
}
