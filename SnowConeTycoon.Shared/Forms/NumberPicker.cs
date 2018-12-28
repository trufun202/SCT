using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Forms
{
    public class NumberPicker : IFormControl
    {
        private string Icon;
        private string Label;
        private int IconWidth = 200;
        private int LabelWidth = 450;
        private Vector2 Position;
        private int Min;
        private int Max;
        private int Value;
        public Rectangle Bounds { get; set; }
        Button LessButton;
        Button MoreButton;
        public bool Visible { get;set;}

        public NumberPicker(string icon, string label, Vector2 position, int min, int max, double scaleX, double scaleY, bool visible)
        {
            Visible = visible;
            Icon = icon;
            Label = label;
            Position = position;
            Min = min;
            Max = max;
            Value = min;
            Bounds = new Rectangle((int)position.X + IconWidth + LabelWidth, (int)position.Y, ContentHandler.Images["DaySetup_NumControl"].Width, ContentHandler.Images["DaySetup_NumControl"].Height);

            LessButton = new Button(new Rectangle((int)position.X + IconWidth + LabelWidth, (int)position.Y, Bounds.Height, Bounds.Height),
             () =>
                {
                    Value--;
                    if (Value < Min)
                    {
                        Value = Min;
                    }
                    return true;
                },
             "",
             scaleX,
             scaleY);

            MoreButton = new Button(new Rectangle((int)position.X + IconWidth + LabelWidth + Bounds.Width - Bounds.Height, (int)position.Y, Bounds.Height, Bounds.Height),
             () =>
             {
                 Value++;
                 if (Value > Max)
                 {
                     Value = Max;
                 }
                 return true;
             },
             "",
             scaleX,
             scaleY);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(ContentHandler.Images[Icon], Position, Color.White);
                spriteBatch.DrawString(Defaults.Font, Label, new Vector2(Position.X + IconWidth, Position.Y), Color.Brown);
                spriteBatch.Draw(ContentHandler.Images["DaySetup_NumControl"], new Vector2(Position.X + IconWidth + LabelWidth, Position.Y), Color.White);
                spriteBatch.DrawString(Defaults.Font, Value.ToString(), new Vector2(Position.X + IconWidth + LabelWidth + (Bounds.Width / 2), Position.Y + (Bounds.Height / 2)), Color.White, 0f, Defaults.Font.MeasureString(Value.ToString()) / 2, 1f, SpriteEffects.None, 1f);
                LessButton.Draw(spriteBatch);
                MoreButton.Draw(spriteBatch);
            }
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
            if (Visible)
            {
                LessButton.HandleInput(previousTouchCollection, currentTouchCollection);
                MoreButton.HandleInput(previousTouchCollection, currentTouchCollection);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Visible)
            {
                LessButton.Update(gameTime);
                MoreButton.Update(gameTime);
            }
        }
    }
}
