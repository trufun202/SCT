using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Handlers;

namespace SnowConeTycoon.Shared.Particles
{
    public class Particle : IParticle
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Angle { get; set; }
        public float AngularVelocity { get; set; }
        public Color Color { get; set; }
        public float Size { get; set; }
        public int TTL { get; set; }
        public float Gravity { get; set; }
        private string Image;
        private int Width;
        private int Height;
        private Vector2 v = new Vector2();
        public bool IsAlive { get; set; }
        private TimedEvent lifeEvent;

        public Particle(string image, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Color color, int ttl, float size, float gravity)
        {
            Image = image;
            Width = ContentHandler.Images[Image].Width;
            Height = ContentHandler.Images[image].Height;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            Gravity = gravity;
            IsAlive = true;
            lifeEvent = new TimedEvent(ttl,
            () =>
                {
                    IsAlive = false;
                },
            false);
        }

        public void Update(GameTime gameTime)
        {
            lifeEvent.Update(gameTime);
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Angle += AngularVelocity;

            if (Gravity > 0)
            {
                v = Velocity;
                v.Y += Gravity;
                Velocity = v;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //int ninetyDegrees = (int)((float)(Angle * 57.2957795) / (float)45);

            //float angle = (ninetyDegrees * 45) * 0.0174532925f;

            Rectangle sourceRectangle = new Rectangle(0, 0, Width, Height);
            Vector2 origin = new Vector2(Width / 2, Height / 2);
            spriteBatch.Draw(ContentHandler.Images[Image], new Rectangle((int)Position.X, (int)Position.Y, (int)(Width * Size), (int)(Height * Size)), sourceRectangle, Color.White, Angle, origin, SpriteEffects.None, 0);
        }
    }
}