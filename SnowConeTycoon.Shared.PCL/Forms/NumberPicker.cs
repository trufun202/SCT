﻿using System;
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
        public int Value;
        public Rectangle Bounds { get; set; }
        Button LessButton;
        Button MoreButton;
        bool ScalingIconUp = false;
        bool ScalingIconDown = false;
        int TimeScaleIcon = 0;
        int TimeScaleIconTotal = 250;
        public bool Visible { get;set;}
        float IconScale = 1.0f;

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
                spriteBatch.Draw(ContentHandler.Images[Icon], new Rectangle((int)Position.X + 70, (int)Position.Y + 40, (int)(ContentHandler.Images[Icon].Width * IconScale), (int)(ContentHandler.Images[Icon].Height * IconScale)), null, Color.White, 0f, new Vector2(ContentHandler.Images[Icon].Width / 2, ContentHandler.Images[Icon].Height / 2), SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, Label, new Vector2(Position.X + IconWidth, Position.Y), Defaults.Brown);
                spriteBatch.Draw(ContentHandler.Images["SupplyShop_Minus"], new Vector2(Position.X + IconWidth + LabelWidth + 20, Position.Y + 20), Color.White);
                spriteBatch.Draw(ContentHandler.Images["SupplyShop_Plus"], new Vector2(Position.X + IconWidth + LabelWidth + Bounds.Width - Bounds.Height + 10, Position.Y + 10), Color.White);
                spriteBatch.DrawString(Defaults.Font, Value.ToString(), new Vector2(Position.X + IconWidth + LabelWidth + (Bounds.Width / 2), Position.Y + (Bounds.Height / 2)), Defaults.Brown, 0f, Defaults.Font.MeasureString(Value.ToString()) / 2, IconScale, SpriteEffects.None, 1f);
                LessButton.Draw(spriteBatch);
                MoreButton.Draw(spriteBatch);
            }
        }

        public bool HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
            if (Visible)
            {
                if (LessButton.HandleInput(previousTouchCollection, currentTouchCollection))
                {
                    ScalingIconUp = true;
                    return true;
                }

                if (MoreButton.HandleInput(previousTouchCollection, currentTouchCollection))
                {
                    ScalingIconUp = true;
                    return true;
                }
            }

            return false;
        }

        public void Update(GameTime gameTime)
        {
            if (Visible)
            {
                LessButton.Update(gameTime);
                MoreButton.Update(gameTime);

                if (ScalingIconUp)
                {
                    TimeScaleIcon += gameTime.ElapsedGameTime.Milliseconds;

                    var amt = TimeScaleIcon / (float)TimeScaleIconTotal;

                    IconScale = Vector2.SmoothStep(Vector2.One, new Vector2(1.5f), amt).X;

                    if (TimeScaleIcon >= TimeScaleIconTotal)
                    {
                        ScalingIconUp = false;
                        ScalingIconDown = true;
                        TimeScaleIcon = 0;
                    }
                }
                else if (ScalingIconDown)
                {
                    TimeScaleIcon += gameTime.ElapsedGameTime.Milliseconds;

                    var amt = TimeScaleIcon / (float)TimeScaleIconTotal;

                    IconScale = Vector2.SmoothStep(new Vector2(1.5f), Vector2.One, amt).X;

                    if (TimeScaleIcon >= TimeScaleIconTotal)
                    {
                        ScalingIconDown = false;
                    }
                }
            }
        }
    }
}
