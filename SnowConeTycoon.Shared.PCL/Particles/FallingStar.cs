using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Particles
{
    public class FallingStar : IParticle
    {
        private int ScaleMin = 1;
        private int ScaleMax = 20;
        private float Scale = 1.0f;
        private int Speed = 2000;
        private Vector2 Direction = new Vector2(0, 1);
        private Vector2 Position;
        public bool IsAlive { get; set; }
        TimedEvent lifeEvent;
        private int Width;
        private int Height;

        public FallingStar(int x, int y)
        {
            lifeEvent = new TimedEvent(3000,
            () =>
                {
                    IsAlive = false;
                },
                false);
            IsAlive = true;
            Scale = Utilities.GetRandomInt(ScaleMin, ScaleMax) / (float)10;
            Position = new Vector2(x, y);
            Width = ContentHandler.Images["particle"].Width;
            Height = ContentHandler.Images["particle"].Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images["particle"], new Rectangle((int)Position.X, (int)Position.Y, (int)(Width * Scale), (int)(Height * Scale)), null, Color.White, 0f, new Vector2(Width / 2, Height / 2), SpriteEffects.None, 1f);
        }

        public void Update(GameTime gameTime)
        {
            lifeEvent.Update(gameTime);
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds * (Scale / 2);

            if (Position.Y > Defaults.GraphicsHeight)
            {
                IsAlive = false;
            }
        }
    }
}
