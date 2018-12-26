using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace SnowConeTycoon.Shared.Forms
{
    public interface IFormControl
    {
        void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
