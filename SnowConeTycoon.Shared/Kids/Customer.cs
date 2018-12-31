using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;
using static SnowConeTycoon.Shared.Handlers.KidHandler;

namespace SnowConeTycoon.Shared.Kids
{
    public class Customer
    {
        private bool IsApproaching = true;
        private bool IsLeaving = false;
        private KidType KidType = KidType.Girl;
        private int KidIndex = 1;
        private Vector2 Target = Vector2.Zero;
        private Vector2 Position = Vector2.Zero;
        private int sinTime = 0;
        private float YSinRadius = 1f;
        private float YSinPeriod = 200f;

        public Customer()
        {
            Reset();
        }

        public void Reset()
        {
            sinTime = 0;
            Position.Y = Defaults.GraphicsHeight + ContentHandler.Images["BoyAvatar_01"].Height - 2000;
            Position.X = (int)(Defaults.GraphicsWidth / 2);
            Target.Y = Defaults.GraphicsHeight + ContentHandler.Images["BoyAvatar_01"].Height;
            Target.X = (int)(Defaults.GraphicsWidth / 2) + 200;
            IsApproaching = true;
            IsLeaving = false;
            KidType = Utilities.GetRandomInt(1, 2) == 1 ? KidType.Boy : KidType.Girl;
            KidIndex = Utilities.GetRandomInt(1, 40);
        }

        public void Update(GameTime gameTime)
        {
            if (IsApproaching)
            {
                sinTime += gameTime.ElapsedGameTime.Milliseconds;
                Position.Y = Position.Y + (float)Math.Sin(sinTime / YSinPeriod) * YSinRadius;
            }
            else if (IsLeaving)
            {

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsApproaching)
            {
                KidHandler.DrawKid(KidType, KidIndex, spriteBatch, (int)Position.X, (int)Position.Y, true);
            }
            else if (IsLeaving)
            {
                KidHandler.DrawKid(KidType, KidIndex, spriteBatch, (int)Position.X, (int)Position.Y, false);
            }
        }
    }
}
