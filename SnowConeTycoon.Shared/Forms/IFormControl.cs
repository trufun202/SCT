﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace SnowConeTycoon.Shared.Forms
{
    public interface IFormControl
    {
        Rectangle Bounds { get; set; }
        bool Visible { get; set; }
        bool HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection, GameTime gameTime);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
