using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Forms
{
    public class Label : IFormControl
    {
        string Text = string.Empty;
        Color color = Defaults.Cream;
        
        public Label(string text, Vector2 position, Color c)
        {
            Text = text;
            Bounds = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            color = c;
        }

        public Rectangle Bounds { get; set; }
        public bool Visible { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.DrawString(Defaults.Font, Text, new Vector2(Bounds.X, Bounds.Y), color);
            }
        }

        public bool HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
            return false;
        }

        public void Update(GameTime gameTime)
        {
            return;
        }
    }
}
