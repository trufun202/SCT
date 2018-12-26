using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnowConeTycoon.Shared.ScreenTransitions
{
    public delegate void OnTransitionDoneMethod();

    public interface IScreenTransition
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void Reset(OnTransitionDoneMethod method);
    }
}
