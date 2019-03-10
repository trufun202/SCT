using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Forms
{
    public class LabelWithImage : IFormControl
    {
        string Text = string.Empty;
        Color color = Defaults.Cream;
        string Image = string.Empty;
        Align Align = Align.Right;
        float Scale = 1.0f;
        Vector2 ScaleEnd = new Vector2(1.25f);
        Vector2 ScaleStart = new Vector2(1.0f);
        int ScaleTime = 0;
        int ScaleTimeTotal = 100;
        bool ScalingUp = false;
        bool ScalingDown = false;
        int ImagePaddingX = 10;
        int ImagePaddingY = 10;

        public LabelWithImage(string text, Vector2 position, Color c, string image, Align align, int imagePaddingX, int imagePaddingY)
        {
            Text = text;
            Bounds = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            color = c;
            Image = image;
            Align = align;
            ImagePaddingX = imagePaddingX;
            ImagePaddingY = imagePaddingY;
        }

        public Rectangle Bounds { get; set; }
        public bool Visible { get; set; }

        public void SetText(string text, bool pulse)
        {
            Text = text;

            if (pulse)
            {
                Pulse();
            }
        }

        public void Pulse()
        {
            ScaleTime = 0;
            ScalingUp = true;
            ScalingDown = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                if (Align == Align.Right)
                {
                    var spacer = "  ";
                    spriteBatch.DrawString(Defaults.Font, Text, new Vector2(Bounds.X - 25, Bounds.Y), color, 0f, new Vector2(Defaults.Font.MeasureString(Text).X / 2, Defaults.Font.MeasureString(Text).Y / 2), Scale, SpriteEffects.None, 1f);
                    spriteBatch.Draw(ContentHandler.Images[Image], new Rectangle((int)(Bounds.X + (Defaults.Font.MeasureString(spacer).X / 2) + (ContentHandler.Images[Image].Width / 2) + ImagePaddingX), (int)(Bounds.Y + (ContentHandler.Images[Image].Height / 2) + ImagePaddingY),
                        (int)(ContentHandler.Images[Image].Width * Scale), (int)(ContentHandler.Images[Image].Height * Scale)), null, Color.White, 0f, new Vector2((int)(ContentHandler.Images[Image].Width / 2), (int)(ContentHandler.Images[Image].Height / 2)), SpriteEffects.None, 1f);
                }
                else
                {
                    //TODO handle Align.Left if needed...
                }
            }
        }

        public bool HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
            return false;
        }

        public void Update(GameTime gameTime)
        {
            if (ScalingUp || ScalingDown)
            {
                ScaleTime += gameTime.ElapsedGameTime.Milliseconds;
                var amt = ScaleTime / (float)ScaleTimeTotal;

                if (ScalingUp)
                {
                    Scale = Vector2.SmoothStep(ScaleStart, ScaleEnd, amt).X;

                    if (ScaleTime >= ScaleTimeTotal)
                    {
                        ScalingUp = false;
                        ScalingDown = true;
                        ScaleTime = 0;
                    }
                }
                else if (ScalingDown)
                {
                    Scale = Vector2.SmoothStep(ScaleEnd, ScaleStart, amt).X;

                    if (ScaleTime >= ScaleTimeTotal)
                    {
                        ScalingUp = false;
                        ScalingDown = false;
                        ScaleTime = 0;
                    }
                }
            }
        }
    }
}
