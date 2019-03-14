using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Handlers;

namespace SnowConeTycoon.Shared.Forms
{
    public class Form
    {
        public List<IFormControl> Controls;
        public Vector2 Position;
        public int Spacing = 20;
        TimedEvent ReveilEvent;
        bool Reveiling = false;
        int ReveilIndex = 0;
        public bool Ready = false;

        public Form(int x, int y)
        {
            Ready = false;
            Controls = new List<IFormControl>();
            Position = new Vector2(x, y);
            ReveilEvent = new TimedEvent(200,
            () =>
            {
                if (Reveiling)
                {
                    Controls[ReveilIndex].Visible = true;
                    ContentHandler.Sounds["Swoosh"].Play();

                    if (ReveilIndex < Controls.Count - 1)
                    {
                        ReveilIndex++;
                    }
                    else
                    {
                        Reveiling = false;
                        Ready = true;
                    }
                }
            }, -1);
        }

        public void Reveil()
        {
            Reveiling = true;
            ReveilIndex = 0;
            ReveilEvent.Reset();
        }

        public bool IsVisible()
        {
            foreach (var control in Controls)
            {
                if (!control.Visible)
                {
                    return false;
                }
            }

            return true;
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection, GameTime gameTime)
        {
            if (Ready)
            {
                foreach (var control in Controls)
                {
                    control.HandleInput(previousTouchCollection, currentTouchCollection, gameTime);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Reveiling)
            {
                ReveilEvent.Update(gameTime);
            }

            foreach (var control in Controls)
            {
                control.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var pos = Position;

            foreach (var control in Controls)
            {
                control.Draw(spriteBatch);
                pos.Y += control.Bounds.Height + Spacing;
            }
        }
    }
}
