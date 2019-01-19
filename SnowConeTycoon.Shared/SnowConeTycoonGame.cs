#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using SnowConeTycoon.Shared.Backgrounds;
using SnowConeTycoon.Shared.Backgrounds.Effects;
using SnowConeTycoon.Shared.Backgrounds.Effects.Components;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Forms;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Kids;
using SnowConeTycoon.Shared.Screens;
using SnowConeTycoon.Shared.ScreenTransitions;
using SnowConeTycoon.Shared.Utils;
using XnaMediaPlayer = Microsoft.Xna.Framework.Media.MediaPlayer;

#endregion

namespace SnowConeTycoon.Shared
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SnowConeTycoonGame : Game
    {
        Screen CurrentScreen = Screen.Logo;
        GraphicsDeviceManager graphics;
        RenderTarget2D renderTarget;
        SpriteBatch spriteBatch;
        int ScreenHeight = 0;
        int ScreenWidth = 0;
        TouchCollection currentTouchCollection;
        TouchCollection previousTouchCollection;
        Form FormTitle;
        Form FormCharacterSelect;
        Form FormDaySetup;
        Form FormOpenForBusiness;
        Form FormResults;
        Form FormSupplyShop;

        LogoScreen LogoScreen;
        DaySetupScreen DaySetupScreen;
        OpenForBusinessScreen OpenForBusinessScreen;
        ResultsScreen ResultsScreen;
        SupplyShopScreen SupplyShopScreen;
                
        Dictionary<string, IBackground> Backgrounds;
        Dictionary<string, IBackgroundEffect> BackgroundEffects;
        IBackground CurrentBackground;
        IBackgroundEffect CurrentBackgroundEffect;
        FadeTransition Fade;
        
        int KidSwapTime = 0;
        int KidSwapTimeTotal = 500;
        bool SwappingKids = false;
        string SwapKidDirection = "LEFT";
        Vector2 KidLeftPosition = new Vector2(-1000, 650);
        Vector2 KidCenterPosition = new Vector2(240, 650);
        Vector2 KidRightPosition = new Vector2(Defaults.GraphicsWidth + 1, 650);
        Vector2 Kid1Position = new Vector2(240, 650);
        Vector2 Kid2Position = new Vector2(240, 650);

        int SelectedKidIndex = 1;
        KidHandler.KidType SelectedKidType = KidHandler.KidType.Girl;
        int OriginalSelectedKidIndex = 1;
        KidHandler.KidType OriginalSelectedKidType = KidHandler.KidType.Girl;
        TimedEvent LogoScreenEvent;

        public SnowConeTycoonGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.PreferredBackBufferWidth = ScreenWidth;

            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            renderTarget = new RenderTarget2D(
                GraphicsDevice,
                Defaults.GraphicsWidth,
                Defaults.GraphicsHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            Player.Reset();
            var scaleX = (double)ScreenWidth / (double)Defaults.GraphicsWidth;
            var scaleY = (double)ScreenHeight / (double)Defaults.GraphicsHeight;

            LogoScreenEvent = new TimedEvent(5500,
            () =>
            {
                Fade.Reset(() =>
                {
                    CurrentScreen = Screen.Title;
                    XnaMediaPlayer.Play(ContentHandler.Songs["Song1"]);
                    XnaMediaPlayer.IsRepeating = true;
                });
            },
            false);

            Fade = new FadeTransition(Color.White, null);

            FormTitle = new Form(0,0);
            /////////////////////////
            //NEW GAME
            /////////////////////////
            FormTitle.Controls.Add(new Button(new Rectangle(30, 1738, 1485, 205), () =>
            {
                Fade.Reset(() =>
                {
                    DaySetupScreen.Reset();
                    CurrentScreen = Screen.DaySetup;
                });

                return true;
            }, "pop", scaleX, scaleY));
            /////////////////////////
            //CHARACTER SELECT
            /////////////////////////
            FormTitle.Controls.Add(new Button(new Rectangle(30, 1944, 1485, 205), () => 
            {
                Fade.Reset(() => 
                {
                    OriginalSelectedKidIndex = SelectedKidIndex;
                    OriginalSelectedKidType = SelectedKidType;
                    KidHandler.SelectedKidIndex = SelectedKidIndex;
                    KidHandler.SelectedKidType = SelectedKidType;
                    CurrentScreen = Screen.CharacterSelect;
                });

                return true;
            }, "pop", scaleX, scaleY));
            /////////////////////////
            //TELL A FRIEND
            /////////////////////////
            FormTitle.Controls.Add(new Button(new Rectangle(30, 2144, 1485, 205), () =>
            {
                Fade.Reset(() =>
                {
                    ResultsScreen.Reset();
                    //CurrentScreen = Screen.Results;
                    CurrentScreen = Screen.SupplyShop;
                });

                return true;
            }, "pop", scaleX, scaleY));
            /////////////////////////
            //SETTINGS
            /////////////////////////
            FormTitle.Controls.Add(new Button(new Rectangle(1370, 30, 151, 152), () =>
            {
                Fade.Reset(() =>
                {
                    OriginalSelectedKidIndex = SelectedKidIndex;
                    OriginalSelectedKidType = SelectedKidType;
                    KidHandler.SelectedKidIndex = SelectedKidIndex;
                    KidHandler.SelectedKidType = SelectedKidType;
                    CurrentScreen = Screen.CharacterSelect;
                });

                return true;
            }, "pop", scaleX, scaleY));

            FormCharacterSelect = new Form(0,0);
            ///////////////////
            ///FEMALE BUTTON
            ///////////////////
            FormCharacterSelect.Controls.Add(new Button(new Rectangle(80, 700, 191, 171), () =>
            {
                SelectedKidType = KidHandler.KidType.Girl;
                KidHandler.SetKidType(KidHandler.KidType.Girl);
                return true;
            }, "pop", scaleX, scaleY));
            ///////////////////
            ///MALE BUTTON
            ///////////////////
            FormCharacterSelect.Controls.Add(new Button(new Rectangle(1280, 700, 191, 171), () =>
            {
                SelectedKidType = KidHandler.KidType.Boy;
                KidHandler.SetKidType(KidHandler.KidType.Boy);
                return true;
            }, "pop", scaleX, scaleY));
            ////////////////
            ///LEFT ARROW
            ////////////////
            FormCharacterSelect.Controls.Add(new Button(new Rectangle(110, 1105, 216, 216), () =>
            {
                if (!SwappingKids)
                {
                    SwappingKids = true;
                    KidSwapTime = 0;
                    SwapKidDirection = "RIGHT";
                    return true;
                }

                return false;
            }, "Swoosh", scaleX, scaleY));
            ////////////////
            ///RIGHT ARROW
            ////////////////
            FormCharacterSelect.Controls.Add(new Button(new Rectangle(1290, 1105, 216, 216), () =>
            {
                if (!SwappingKids)
                {
                    SwappingKids = true;
                    KidSwapTime = 0;
                    SwapKidDirection = "LEFT";
                    return true;
                }

                return false;
            }, "Swoosh", scaleX, scaleY));
            ////////////////
            ///OK BUTTON
            ////////////////
            FormCharacterSelect.Controls.Add(new Button(new Rectangle(30, 1948, 1485, 205), () =>
            {
                Fade.Reset(() =>
                {
                    SelectedKidIndex = KidHandler.SelectedKidIndex;
                    CurrentScreen = Screen.Title;
                });

                return true;
            }, "pop", scaleX, scaleY));
            ///////////////////
            ///CANCEL BUTTON
            ///////////////////
            FormCharacterSelect.Controls.Add(new Button(new Rectangle(30, 2160, 1485, 205), () => 
            {
                Fade.Reset(() => 
                {
                    //switch back to their originally selected character
                    SelectedKidIndex = OriginalSelectedKidIndex;
                    SelectedKidType = OriginalSelectedKidType;
                    KidHandler.SelectKid(SelectedKidType, SelectedKidIndex);
                    CurrentScreen = Screen.Title;
                });

                return true;
            }, "pop", scaleX, scaleY));

            FormDaySetup = new Form(0, 0);
            FormDaySetup.Controls.Add(new Button(new Rectangle(50, 2550, 150, 150), () =>
            {
                Fade.Reset(() =>
                {
                    CurrentScreen = Screen.Title;
                });

                return true;
            }, "pop", scaleX, scaleY));
            FormDaySetup.Controls.Add(new Button(new Rectangle(925, 2375, 592, 250), () =>
            {
                Fade.Reset(() =>
                {
                    DaySetupScreen.Reset();
                    CurrentScreen = Screen.OpenForBusiness;
                });

                return true;
            }, "pop", scaleX, scaleY));

            FormOpenForBusiness = new Form(0, 0);
            FormOpenForBusiness.Controls.Add(new Button(new Rectangle(1370, 30, 151, 152), () =>
            {
                Fade.Reset(() =>
                {
                    CurrentScreen = Screen.Title;
                });

                return true;
            }, "pop", scaleX, scaleY));

            FormResults = new Form(0, 0);
            FormResults.Controls.Add(new Button(new Rectangle(925, 2375, 592, 250), () =>
            {
                Fade.Reset(() =>
                {
                    DaySetupScreen.Reset();
                    CurrentScreen = Screen.DaySetup;
                });

                return true;
            }, "pop", scaleX, scaleY));

            FormSupplyShop = new Form(0, 0);
            FormSupplyShop = new Form(0, 0);
            FormSupplyShop.Controls.Add(new Button(new Rectangle(925, 2375, 592, 250), () =>
            {
                Fade.Reset(() =>
                {
                    DaySetupScreen.Reset();
                    CurrentScreen = Screen.DaySetup;
                });

                return true;
            }, "pop", scaleX, scaleY));

            previousTouchCollection = TouchPanel.GetState();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            var scaleX = (double)ScreenWidth / (double)Defaults.GraphicsWidth;
            var scaleY = (double)ScreenHeight / (double)Defaults.GraphicsHeight;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            Defaults.Font = Content.Load<SpriteFont>("cooper-black-80");

            ContentHandler.Init(Content);
            KidHandler.Init();

            Backgrounds = new Dictionary<string, IBackground>();
            Backgrounds.Add("cloudy", new BackgroundCloudy());
            Backgrounds.Add("sunny", new BackgroundSunny());
            Backgrounds.Add("partylycloudy", new BackgroundPartlyCloudy());
            Backgrounds.Add("rainy", new BackgroundRainy());
            Backgrounds.Add("snowing", new BackgroundSnowing());
            CurrentBackground = Backgrounds["snowing"];

            BackgroundEffects = new Dictionary<string, IBackgroundEffect>();
            BackgroundEffects.Add("rain", new Rain(100));
            BackgroundEffects.Add("snow", new Snow(100));
            CurrentBackgroundEffect = BackgroundEffects["snow"];

            LogoScreen = new LogoScreen();
            DaySetupScreen = new DaySetupScreen(scaleX, scaleY);
            OpenForBusinessScreen = new OpenForBusinessScreen(scaleX, scaleY);
            ResultsScreen = new ResultsScreen(scaleX, scaleY);
            SupplyShopScreen = new SupplyShopScreen(scaleX, scaleY);
        }

        /// <summar>(""));

        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
#endif
            if (Fade.ShowingFade)
            {
                Fade.Update(gameTime);
            }
            else
            {
                currentTouchCollection = TouchPanel.GetState();
                ////////////////////////////////////////////////
                //HANDLE INPUT
                ////////////////////////////////////////////////
                if (CurrentScreen == Screen.Logo)
                {
                    LogoScreen.HandleInput(previousTouchCollection, currentTouchCollection);
                }
                else if (CurrentScreen == Screen.Title)
                {
                    FormTitle.HandleInput(previousTouchCollection, currentTouchCollection);
                }
                else if (CurrentScreen == Screen.CharacterSelect)
                {
                    FormCharacterSelect.HandleInput(previousTouchCollection, currentTouchCollection);
                }
                else if (CurrentScreen == Screen.DaySetup)
                {
                    DaySetupScreen.HandleInput(previousTouchCollection, currentTouchCollection);
                    FormDaySetup.HandleInput(previousTouchCollection, currentTouchCollection);
                }
                else if (CurrentScreen == Screen.OpenForBusiness)
                {
                    OpenForBusinessScreen.HandleInput(previousTouchCollection, currentTouchCollection);
                    FormOpenForBusiness.HandleInput(previousTouchCollection, currentTouchCollection);
                }
                else if (CurrentScreen == Screen.Results)
                {
                    ResultsScreen.HandleInput(previousTouchCollection, currentTouchCollection);
                    FormResults.HandleInput(previousTouchCollection, currentTouchCollection);
                }
                else if (CurrentScreen == Screen.SupplyShop)
                {
                    SupplyShopScreen.HandleInput(previousTouchCollection, currentTouchCollection);
                    FormSupplyShop.HandleInput(previousTouchCollection, currentTouchCollection);
                }

                previousTouchCollection = currentTouchCollection;

                ////////////////////////////////////////////////
                //UPDATES
                ////////////////////////////////////////////////

                if (CurrentScreen == Screen.Logo)
                {
                    LogoScreenEvent.Update(gameTime);
                    LogoScreen.Update(gameTime);
                }
                else if (CurrentScreen == Screen.Title)
                {
                    CurrentBackground.Update(gameTime);
                    CurrentBackgroundEffect?.Update(gameTime);
                    KidHandler.Update(gameTime);
                    FormTitle.Update(gameTime);
                }
                else if (CurrentScreen == Screen.DaySetup)
                {
                    CurrentBackground.Update(gameTime);
                    CurrentBackgroundEffect?.Update(gameTime);
                    DaySetupScreen.Update(gameTime);
                    FormDaySetup.Update(gameTime);
                }
                else if (CurrentScreen == Screen.OpenForBusiness)
                {
                    CurrentBackground.Update(gameTime);
                    CurrentBackgroundEffect?.Update(gameTime);
                    KidHandler.Update(gameTime);
                    OpenForBusinessScreen.Update(gameTime);
                    FormOpenForBusiness.Update(gameTime);
                }
                else if (CurrentScreen == Screen.Results)
                {
                    CurrentBackground.Update(gameTime);
                    CurrentBackgroundEffect?.Update(gameTime);
                    ResultsScreen.Update(gameTime);
                    FormResults.Update(gameTime);
                }
                else if (CurrentScreen == Screen.SupplyShop)
                {
                    CurrentBackground.Update(gameTime);
                    CurrentBackgroundEffect?.Update(gameTime);
                    SupplyShopScreen.Update(gameTime);
                    FormSupplyShop.Update(gameTime);
                }
                else if (CurrentScreen == Screen.CharacterSelect)
                {
                    if (SwappingKids)
                    {
                        KidSwapTime += gameTime.ElapsedGameTime.Milliseconds;

                        var moveAmount = KidSwapTime / (float)KidSwapTimeTotal;

                        if (SwapKidDirection == "LEFT")
                        {
                            Kid1Position = Vector2.SmoothStep(KidCenterPosition, KidLeftPosition, moveAmount);
                            Kid2Position = Vector2.SmoothStep(KidRightPosition, KidCenterPosition, moveAmount);

                            if (moveAmount >= 1)
                            {
                                SwappingKids = false;
                                SelectedKidIndex++;
                                if (SelectedKidIndex > 40)
                                {
                                    SelectedKidIndex = 1;
                                }

                                KidHandler.SelectKid(SelectedKidType, SelectedKidIndex);
                                Kid1Position = KidCenterPosition;
                            }
                        }
                        else
                        {
                            Kid1Position = Vector2.SmoothStep(KidCenterPosition, KidRightPosition, moveAmount);
                            Kid2Position = Vector2.SmoothStep(KidLeftPosition, KidCenterPosition, moveAmount);

                            if (moveAmount >= 1)
                            {
                                SwappingKids = false;
                                SelectedKidIndex--;
                                if (SelectedKidIndex < 1)
                                {
                                    SelectedKidIndex = 40;
                                }

                                KidHandler.SelectKid(SelectedKidType, SelectedKidIndex);
                                Kid1Position = KidCenterPosition;
                            }
                        }
                    }

                    CurrentBackground.Update(gameTime);
                    CurrentBackgroundEffect?.Update(gameTime);
                    KidHandler.Update(gameTime);
                    FormCharacterSelect.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.Begin();

            if (CurrentScreen == Screen.Logo)
            {
                LogoScreen.Draw(spriteBatch);
            }
            else if (CurrentScreen == Screen.Title)
            {
                CurrentBackground.Draw(spriteBatch);
                KidHandler.Draw(spriteBatch, (int)Kid1Position.X, (int)Kid1Position.Y);
                spriteBatch.Draw(ContentHandler.Images["TitleScreen_Foreground"], new Rectangle(0, 0, 1536, 2732), Color.White);
                FormTitle.Draw(spriteBatch);
                spriteBatch.Draw(ContentHandler.Images["IconGear"], new Vector2(1380, 40), Color.White);
                CurrentBackgroundEffect?.Draw(spriteBatch);
            }
            else if (CurrentScreen == Screen.DaySetup)
            {
                CurrentBackground.Draw(spriteBatch);
                CurrentBackgroundEffect?.Draw(spriteBatch);
                DaySetupScreen.Draw(spriteBatch);
                FormDaySetup.Draw(spriteBatch);
            }
            else if (CurrentScreen == Screen.Results)
            {
                CurrentBackground.Draw(spriteBatch);
                CurrentBackgroundEffect?.Draw(spriteBatch);
                ResultsScreen.Draw(spriteBatch);
                FormResults.Draw(spriteBatch);
            }
            else if (CurrentScreen == Screen.SupplyShop)
            {
                CurrentBackground.Draw(spriteBatch);
                CurrentBackgroundEffect?.Draw(spriteBatch);
                SupplyShopScreen.Draw(spriteBatch);
                FormSupplyShop.Draw(spriteBatch);
            }
            else if (CurrentScreen == Screen.CharacterSelect)
            {
                CurrentBackground.Draw(spriteBatch);
                KidHandler.Draw(spriteBatch, (int)Kid1Position.X, (int)Kid1Position.Y);

                if (SwappingKids)
                {
                    var nextKid = SelectedKidIndex;

                    if (SwapKidDirection == "LEFT")
                    {
                        nextKid++;
                    }
                    else
                    {
                        nextKid--;
                    }

                    if (nextKid < 1)
                        nextKid = 40;
                    else if (nextKid > 40)
                        nextKid = 1;

                    KidHandler.DrawKid(SelectedKidType, nextKid, spriteBatch, (int)Kid2Position.X, (int)Kid2Position.Y, null, false);
                }

                spriteBatch.Draw(ContentHandler.Images["CharacterSelectScreen_Foreground"], new Rectangle(0, 0, 1536, 2732), Color.White);
                spriteBatch.Draw(ContentHandler.Images["Symbol_Female"], new Rectangle(80, 700, 191, 171), Color.White);
                spriteBatch.Draw(ContentHandler.Images["Symbol_Male"], new Rectangle(1280, 700, 191, 171), Color.White);
                spriteBatch.Draw(ContentHandler.Images["ArrowLeft"], new Rectangle(140, 1145, 136, 136), Color.White);
                spriteBatch.Draw(ContentHandler.Images["ArrowRight"], new Rectangle(1330, 1145, 136, 136), Color.White);
                spriteBatch.DrawString(Defaults.Font, KidHandler.CurrentKid.Name, new Vector2(808, 1830), Defaults.Cream, 0f, Defaults.Font.MeasureString(KidHandler.CurrentKid.Name) / 2, 1f, SpriteEffects.None, 1f);

                FormCharacterSelect.Draw(spriteBatch);
                CurrentBackgroundEffect?.Draw(spriteBatch);
            }
            else if (CurrentScreen == Screen.OpenForBusiness)
            {
                CurrentBackground.Draw(spriteBatch);
                KidHandler.Draw(spriteBatch, (int)Kid1Position.X, (int)Kid1Position.Y);
                OpenForBusinessScreen.Draw(spriteBatch);
                FormOpenForBusiness.Draw(spriteBatch);
                spriteBatch.Draw(ContentHandler.Images["IconHome"], new Vector2(1380, 40), Color.White);
                CurrentBackgroundEffect?.Draw(spriteBatch);
            }

            if (Fade.ShowingFade)
            {
                Fade.Draw(spriteBatch);
            }

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
