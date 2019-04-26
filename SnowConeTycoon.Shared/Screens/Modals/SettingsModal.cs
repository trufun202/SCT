using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Forms;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Screens
{
    public class SettingsModal
    {
        public bool Active = false;
        private Form Form;
        private SnowConeTycoonGame Game;

        public SettingsModal(SnowConeTycoonGame game, double scaleX, double scaleY)
        {
            Game = game;
            Form = new Form(0, 0);

            Form.Controls.Add(new Button(new Rectangle(1325, 500, 200, 200), () =>
            {
                Active = false;
                return true;
            }, string.Empty, scaleX, scaleY));
            Form.Controls.Add(new Button(new Rectangle(900, 1100, 140, 140), () =>
            {
                if (Player.Degrees == Enums.Degrees.Fahrenheit)
                {
                    Player.Degrees = Enums.Degrees.Celsius;
                }
                else
                {
                    Player.Degrees = Enums.Degrees.Fahrenheit;
                }

                return true;
            }, "pop", scaleX, scaleY));

            Form.Controls.Add(new Button(new Rectangle(900, 1400, 140, 140), () =>
            {
                Player.MusicEnabled = !Player.MusicEnabled;

                if (Player.MusicEnabled)
                {
                    game.songMainTheme.Resume();
                }
                else
                {
                    game.songMainTheme.Pause();
                }

                return true;
            }, "", scaleX, scaleY));

            Form.Controls.Add(new Button(new Rectangle(900, 1700, 140, 140), () =>
            {
                Player.SoundEnabled = !Player.SoundEnabled;

                return true;
            }, "pop", scaleX, scaleY));
        }

        public void Reset()
        {
            Active = true;
            Form.Ready = true;
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection, GameTime gameTime)
        {
            Form.HandleInput(previousTouchCollection, currentTouchCollection, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            Form.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images["WhiteDot"], new Rectangle(0, 0, Defaults.GraphicsWidth, Defaults.GraphicsHeight), Color.FromNonPremultiplied(new Vector4(0, 0, 0, 0.75f)));
            spriteBatch.Draw(ContentHandler.Images["SettingsModal"], new Rectangle((int)(Defaults.GraphicsWidth / 2), (int)(Defaults.GraphicsHeight / 2), ContentHandler.Images["SettingsModal"].Width, ContentHandler.Images["SettingsModal"].Height), null, Color.White, 0f, new Vector2((int)(ContentHandler.Images["SettingsModal"].Width / 2), (int)(ContentHandler.Images["SettingsModal"].Height / 2)), SpriteEffects.None, 1f);

            spriteBatch.DrawString(Defaults.Font, "degrees", new Vector2(300, 1100), Defaults.Brown);
            spriteBatch.DrawString(Defaults.Font, "music", new Vector2(300, 1400), Defaults.Brown);
            spriteBatch.DrawString(Defaults.Font, "sound fx", new Vector2(300, 1700), Defaults.Brown);

            if (Player.Degrees == Enums.Degrees.Fahrenheit)
            {
                spriteBatch.Draw(ContentHandler.Images["RadioButtonOn"], new Vector2(900, 1125), Color.White);
                spriteBatch.DrawString(Defaults.Font, "o", new Vector2(1075, 1075), Defaults.Brown, 0f, Vector2.Zero, 0.4f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, "F", new Vector2(1100, 1100), Defaults.Brown);
            }
            else if (Player.Degrees == Enums.Degrees.Celsius)
            {
                spriteBatch.Draw(ContentHandler.Images["RadioButtonOff"], new Vector2(900, 1125), Color.White);
                spriteBatch.DrawString(Defaults.Font, "o", new Vector2(1075, 1075), Defaults.Brown, 0f, Vector2.Zero, 0.4f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, "C", new Vector2(1100, 1100), Defaults.Brown);
            }

            if (Player.MusicEnabled)
            {
                spriteBatch.Draw(ContentHandler.Images["RadioButtonOn"], new Vector2(900, 1425), Color.White);
                spriteBatch.DrawString(Defaults.Font, "on", new Vector2(1100, 1400), Defaults.Brown);
            }
            else
            {
                spriteBatch.Draw(ContentHandler.Images["RadioButtonOff"], new Vector2(900, 1425), Color.White);
                spriteBatch.DrawString(Defaults.Font, "off", new Vector2(1100, 1400), Defaults.Brown);
            }

            if (Player.SoundEnabled)
            {
                spriteBatch.Draw(ContentHandler.Images["RadioButtonOn"], new Vector2(900, 1725), Color.White);
                spriteBatch.DrawString(Defaults.Font, "on", new Vector2(1100, 1700), Defaults.Brown);
            }
            else
            {
                spriteBatch.Draw(ContentHandler.Images["RadioButtonOff"], new Vector2(900, 1725), Color.White);
                spriteBatch.DrawString(Defaults.Font, "off", new Vector2(1100, 1700), Defaults.Brown);
            }

            Form.Draw(spriteBatch);
        }
    }
}
