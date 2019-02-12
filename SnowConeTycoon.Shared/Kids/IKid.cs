using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Enums;

namespace SnowConeTycoon.Shared.Kids
{
    public interface IKid
    {
        string Name { get; set; }
        bool IsLocked { get; set; }
        UnlockMechanism UnlockMechanism { get; set; }
        int UnlockPrice { get; set; }
        void Update(GameTime gameTime);
        void MakeHappy();
        void MakeMad();
        void MakeSad();
        void Unlock();
        string GetName();
        void Draw(SpriteBatch spriteBatch, int x, int y, bool facingAway, int? size);        
    }
}
