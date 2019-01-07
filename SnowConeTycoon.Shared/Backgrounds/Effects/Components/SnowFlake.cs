using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Backgrounds.Effects.Components
{
    public class SnowFlake : IBackgroundEffectComponent
    {
        private Vector2 Position;
        private Vector2 Direction = new Vector2(0, 1);
        private int Speed = 0;
        private int SpeedMin = 100;
        private int SpeedMax = 500;
        private int SizeMin = 4;
        private int SizeMax = 24;
        private int Size;
        private int XDirection = 1;
        private int Time = 0;
        private int TimeTotal = 1000;
        private int OffsetX = 10;
        private Vector2 PositionStart;
        private Vector2 PositionEnd;

        public SnowFlake()
        {
            Position = new Vector2()
            {
                X = Utilities.GetRandomInt(0, Defaults.GraphicsWidth),
                Y = Utilities.GetRandomInt(0, Defaults.GraphicsHeight)
            };

            PositionStart = new Vector2(Position.X - OffsetX, Position.Y);
            PositionEnd = new Vector2(Position.X + OffsetX, Position.Y);

            Size = Utilities.GetRandomInt(SizeMin, SizeMax);
            Speed = Utilities.GetRandomInt(SpeedMin, SpeedMax);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images["WhiteDot"], new Rectangle((int)Position.X, (int)Position.Y, Size, Size), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (XDirection == 1)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                Time -= gameTime.ElapsedGameTime.Milliseconds;
            }

            var amt = Time / (float)TimeTotal;

            var pos = Vector2.SmoothStep(PositionStart, PositionEnd, amt);
            Position.X = pos.X;

            if (Time >= TimeTotal || Time <= 0)
            {
                XDirection *= -1;
            }

            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Position.Y > Defaults.GraphicsHeight)
            {
                Position.Y = 0;
            }
        }
    }
}
