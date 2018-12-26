using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace SnowConeTycoon.Shared.Forms
{
    public class Form
    {
        public List<IFormControl> Controls;

        public Form()
        {
            Controls = new List<IFormControl>();
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
            foreach (var control in Controls)
            {
                control.HandleInput(previousTouchCollection, currentTouchCollection);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var control in Controls)
            {
                control.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var control in Controls)
            {
                control.Draw(spriteBatch);
            }
        }
    }
}
