using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnowConeTycoon.Shared.Kids
{
    public interface IKid
    {
        string Name { get; set; }
        bool IsLocked { get; set; }
        void Update(GameTime gameTime);
        void MakeHappy();
        void MakeMad();
        void MakeSad();
        void Draw(SpriteBatch spriteBatch, int x, int y, bool facingAway);        
    }
}
