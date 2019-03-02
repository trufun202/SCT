﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using SnowConeTycoon.Shared.Animations;
using SnowConeTycoon.Shared.Backgrounds;
using SnowConeTycoon.Shared.Backgrounds.Effects;
using SnowConeTycoon.Shared.Backgrounds.Effects.Components;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Forms;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Kids;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Particles;
using SnowConeTycoon.Shared.Screens;
using SnowConeTycoon.Shared.ScreenTransitions;
using SnowConeTycoon.Shared.Services;
using SnowConeTycoon.Shared.Utils;
using XnaMediaPlayer = Microsoft.Xna.Framework.Media.MediaPlayer;

#endregion

namespace SnowConeTycoon.Shared
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SnowConeTycoonGame
    {
        DeviceStorageService storageService = new DeviceStorageService();
        ContentManager Content;
        public Screen CurrentScreen = Screen.Loading;
        RenderTarget2D renderTarget;
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
        Form FormDailyBonus;

        LoadingScreen LoadingScreen;
        LogoScreen LogoScreen;
        DailyBonusScreen DailyBonusScreen;
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
        TimedEvent LoadingScreenEvent;
        TimedEvent LogoScreenEvent;
        TimedEvent DailyBonusEvent;
        TimedEvent DailyBonusIceEarnedEvent;
        bool ContentLoaded = false;
        IBusinessDayService businessDayService;
        IWeatherService weatherService;
        bool ShowingDailyBonus = false;
        ParticleEmitter IceParticleEmitter;
        TimedEvent IceParticleTimedEvent;
        PulseImage IceIcon;

        public SnowConeTycoonGame()
        {
            businessDayService = new BusinessDayService();
            weatherService = new WeatherService();
        }

        public void GoToResultsScreen(BusinessDayResult results)
        {
            Fade.Reset(() =>
            {
                ResultsScreen.ResetAndSetResults(results);
                CurrentScreen = Screen.Results;
                //CurrentScreen = Screen.SupplyShop;
            });
        }

        public void SetWeather(DayForecast dayForecast)
        {
            DaySetupScreen.CurrentForecast = dayForecast.Forecast;
            ResultsScreen.CurrentForecast = dayForecast.Forecast;
            ResultsScreen.CurrentTemperature = dayForecast.Temperature;

            switch (dayForecast.Forecast)
            {
                case Forecast.Sunny:
                    CurrentBackground = Backgrounds["sunny"];
                    CurrentBackgroundEffect = null;
                    break;
                case Forecast.Cloudy:
                    CurrentBackground = Backgrounds["cloudy"];
                    CurrentBackgroundEffect = null;
                    break;
                case Forecast.PartlyCloudy:
                    CurrentBackground = Backgrounds["partlycloudy"];
                    CurrentBackgroundEffect = null;
                    break;
                case Forecast.Rain:
                    CurrentBackground = Backgrounds["rainy"];
                    CurrentBackgroundEffect = BackgroundEffects["rain"];
                    break;
                case Forecast.Snow:
                    CurrentBackground = Backgrounds["snowing"];
                    CurrentBackgroundEffect = BackgroundEffects["snow"];
                    break;
            }
        }

        public void Initialize(GraphicsDeviceManager graphics)
        {
            ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.PreferredBackBufferWidth = ScreenWidth;

            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            renderTarget = new RenderTarget2D(
                graphics.GraphicsDevice,
                Defaults.GraphicsWidth,
                Defaults.GraphicsHeight,
                false,
                graphics.GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            Player.Reset();

            if (storageService.SaveFileExists())
            {
                storageService.Load();
            }

            storageService.Save();

            var scaleX = (double)ScreenWidth / (double)Defaults.GraphicsWidth;
            var scaleY = (double)ScreenHeight / (double)Defaults.GraphicsHeight;

            LoadingScreenEvent = new TimedEvent(3000,
            () =>
            {
                Fade.Reset(() =>
                {
                    if (ContentLoaded)
                    {
                        CurrentScreen = Screen.Logo;
                    }
                });
            },
            -1);

            LogoScreenEvent = new TimedEvent(5500,
            () =>
            {
                Fade.Reset(() =>
                {
                    CurrentScreen = Screen.Title;
                    //XnaMediaPlayer.Play(ContentHandler.Songs["Song1"]);
                    //XnaMediaPlayer.IsRepeating = true;
                });
            },
            1);

            DailyBonusEvent = new TimedEvent(500,
            () =>
            {
                var ts = DateTime.Now - Player.DailyBonusLastReceived;

                if (ts.Days >= 0)
                {
                    DailyBonusScreen.Reset();
                    ShowingDailyBonus = true;
                }
            },
            1);

            Fade = new FadeTransition(Color.White, null);

            FormTitle = new Form(0, 0);
            /////////////////////////
            //NEW GAME
            /////////////////////////
            FormTitle.Controls.Add(new Button(new Rectangle(30, 1738, 1485, 205), () =>
            {
                Fade.Reset(() =>
                {
                    DaySetupScreen.Reset(100);
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
                CurrentScreen = Screen.RewardAd;

                //Fade.Reset(() =>
                //{
                //   ResultsScreen.ResetAndSetResults(businessDayService.CalculateDay(Forecast.Sunny, 0, 0, 0, 2));
                //    CurrentScreen = Screen.Results;
                //    //CurrentScreen = Screen.SupplyShop;
                //});

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

            FormCharacterSelect = new Form(0, 0);
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
                if (KidHandler.CurrentKid.IsLocked)
                {
                    KidHandler.CurrentKid.Unlock();
                    //TODO play sound
                    return false; //so the default sound doesn't play
                }
                else
                {
                    Fade.Reset(() =>
                    {
                        SelectedKidIndex = KidHandler.SelectedKidIndex;
                        CurrentScreen = Screen.Title;
                    });
                }
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
            FormDaySetup.Controls.Add(new Button(new Rectangle(1370, 30, 151, 152), () =>
            {
                Fade.Reset(() =>
                {
                    SupplyShopScreen.Reset();
                    CurrentScreen = Screen.SupplyShop;
                });

                return true;
            }, "pop", scaleX, scaleY));

            FormDaySetup.Controls.Add(new Button(new Rectangle(50, 2335, 592, 250), () =>
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
                    OpenForBusinessScreen.Reset(businessDayService.CalculateDay(weatherService.GetForecast(Player.CurrentDay), Player.ConeCount, DaySetupScreen.SyrupCount, DaySetupScreen.FlyerCount, DaySetupScreen.Price));
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
                    Player.CurrentDay++;
                    var dayForecast = weatherService.GetForecast(Player.CurrentDay);
                    SetWeather(dayForecast);
                    DaySetupScreen.Reset(dayForecast.Temperature);
                    CurrentScreen = Screen.DaySetup;
                });

                return true;
            }, "pop", scaleX, scaleY));

            FormSupplyShop = new Form(0, 0);
            FormSupplyShop.Controls.Add(new Button(new Rectangle(50, 2335, 592, 250), () =>
            {
                Fade.Reset(() =>
                {
                    CurrentScreen = Screen.DaySetup;
                });

                return true;
            }, "pop", scaleX, scaleY));
            FormSupplyShop.Controls.Add(new Button(new Rectangle(925, 2375, 592, 250), () =>
            {
                Fade.Reset(() =>
                {
                    //TODO checkout items purchased on supply screen
                    CurrentScreen = Screen.DaySetup;
                });

                return true;
            }, "pop", scaleX, scaleY));

            FormDailyBonus = new Form(0, 0);
            FormDailyBonus.Controls.Add(new Button(new Rectangle(850, 2000, 592, 250), () =>
            {
                if (DailyBonusScreen.ScreenHasLoaded())
                {
                    ShowingDailyBonus = false;

                    DailyBonusIceEarnedEvent = new TimedEvent(250,
                    () =>
                        {
                            Player.AddIce(1);
                            ContentHandler.Sounds["Ice_Cube"].Play();
                            IceParticleEmitter.FlowOn = true;
                            IceIcon.Reset();

                            IceParticleTimedEvent = new TimedEvent(200,
                            () =>
                            {
                                IceParticleEmitter.FlowOn = false;
                            },
                            1);

                        },
                        Player.GetIceEarned());

                    return true;
                }

                return false;
            }, "pop", scaleX, scaleY));

            previousTouchCollection = TouchPanel.GetState();
        }

        public void LoadContent(ContentManager content)
        {
            Content = content;
            Defaults.Font = Content.Load<SpriteFont>("cooper-black-80");
            ContentHandler.PreInit(Content);
            LoadingScreen = new LoadingScreen();
        }

        private void LoadStuff()
        {
            var scaleX = (double)ScreenWidth / (double)Defaults.GraphicsWidth;
            var scaleY = (double)ScreenHeight / (double)Defaults.GraphicsHeight;

            ContentHandler.Init(Content);
            KidHandler.Init();

            Backgrounds = new Dictionary<string, IBackground>();
            Backgrounds.Add("cloudy", new BackgroundCloudy());
            Backgrounds.Add("sunny", new BackgroundSunny());
            Backgrounds.Add("partlycloudy", new BackgroundPartlyCloudy());
            Backgrounds.Add("rainy", new BackgroundRainy());
            Backgrounds.Add("snowing", new BackgroundSnowing());
            CurrentBackground = Backgrounds["sunny"];

            BackgroundEffects = new Dictionary<string, IBackgroundEffect>();
            BackgroundEffects.Add("rain", new Rain(100));
            BackgroundEffects.Add("snow", new Snow(100));
            //CurrentBackgroundEffect = BackgroundEffects["snow"];

            LogoScreen = new LogoScreen();
            DaySetupScreen = new DaySetupScreen(scaleX, scaleY);
            OpenForBusinessScreen = new OpenForBusinessScreen(this, scaleX, scaleY);
            ResultsScreen = new ResultsScreen(scaleX, scaleY);
            SupplyShopScreen = new SupplyShopScreen(scaleX, scaleY);
            DailyBonusScreen = new DailyBonusScreen();

            IceParticleEmitter = new ParticleEmitter(100, 1375, 110, 40, 2000, "particle_ice", 3.25f);
            IceParticleEmitter.Gravity = 30f;
            IceParticleEmitter.Velocity = new Vector2(1350, 1350);
            IceParticleEmitter.SetCircularPath(30);

            IceIcon = new PulseImage("TitleScreen_Ice", new Vector2(1400, 125), 1f, 1.5f, 1f);
        }

        public void OnDeactivated()
        {
            storageService.Save();
        }

        public void Update(GameTime gameTime)
        {
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
                if (CurrentScreen == Screen.Loading)
                {
                    LoadingScreen.HandleInput(previousTouchCollection, currentTouchCollection);
                }
                else if (CurrentScreen == Screen.Logo)
                {
                    LogoScreen.HandleInput(previousTouchCollection, currentTouchCollection);
                }
                else if (CurrentScreen == Screen.Title)
                {
                    if (ShowingDailyBonus)
                    {
                        FormDailyBonus.HandleInput(previousTouchCollection, currentTouchCollection);
                    }
                    else
                    {
                        FormTitle.HandleInput(previousTouchCollection, currentTouchCollection);
                    }
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

                if (CurrentScreen == Screen.Loading)
                {
                    if (!ContentLoaded)
                    {
                        LoadStuff();
                        ContentLoaded = true;
                    }

                    LoadingScreenEvent.Update(gameTime);
                    LoadingScreen.Update(gameTime);
                }
                else if (CurrentScreen == Screen.Logo)
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
                    DailyBonusEvent.Update(gameTime);
                    DailyBonusIceEarnedEvent?.Update(gameTime);
                    IceParticleEmitter.Update(gameTime);
                    IceParticleTimedEvent?.Update(gameTime);
                    IceIcon?.Update(gameTime);

                    if (ShowingDailyBonus)
                    {
                        DailyBonusScreen.Update(gameTime);
                    }
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
                    //CurrentBackground.Update(gameTime);
                    //CurrentBackgroundEffect?.Update(gameTime);
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
        }

        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
            graphics.GraphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.Begin();

            if (CurrentScreen == Screen.Loading)
            {
                LoadingScreen.Draw(spriteBatch);
            }
            else if (CurrentScreen == Screen.Logo)
            {
                LogoScreen.Draw(spriteBatch);
            }
            else if (CurrentScreen == Screen.Title)
            {
                CurrentBackground.Draw(spriteBatch);
                KidHandler.Draw(spriteBatch, (int)Kid1Position.X, (int)Kid1Position.Y);
                spriteBatch.Draw(ContentHandler.Images["TitleScreen_Foreground"], new Rectangle(0, 0, 1536, 2732), Color.White);
                FormTitle.Draw(spriteBatch);
                spriteBatch.DrawString(Defaults.Font, Player.IceCount.ToString(), new Vector2(1268, 43), Defaults.Brown, 0f, new Vector2(Defaults.Font.MeasureString(Player.IceCount.ToString()).X, 0), 1f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, Player.IceCount.ToString(), new Vector2(1268, 47), Defaults.Brown, 0f, new Vector2(Defaults.Font.MeasureString(Player.IceCount.ToString()).X, 0), 1f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, Player.IceCount.ToString(), new Vector2(1272, 43), Defaults.Brown, 0f, new Vector2(Defaults.Font.MeasureString(Player.IceCount.ToString()).X, 0), 1f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, Player.IceCount.ToString(), new Vector2(1272, 47), Defaults.Brown, 0f, new Vector2(Defaults.Font.MeasureString(Player.IceCount.ToString()).X, 0), 1f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, Player.IceCount.ToString(), new Vector2(1270, 45), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString(Player.IceCount.ToString()).X, 0), 1f, SpriteEffects.None, 1f);
                IceIcon?.Draw(spriteBatch);
                CurrentBackgroundEffect?.Draw(spriteBatch);
                IceParticleEmitter.Draw(spriteBatch);

                if (ShowingDailyBonus)
                {
                    DailyBonusScreen.Draw(spriteBatch);
                    FormDailyBonus.Draw(spriteBatch);
                }
            }
            else if (CurrentScreen == Screen.DaySetup)
            {
                CurrentBackground.Draw(spriteBatch);
                CurrentBackgroundEffect?.Draw(spriteBatch);
                DaySetupScreen.Draw(spriteBatch);
                FormDaySetup.Draw(spriteBatch);
                spriteBatch.Draw(ContentHandler.Images["DaySetup_IconShop"], new Vector2(1380, 40), Color.White);
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
                //CurrentBackground.Draw(spriteBatch);
                //CurrentBackgroundEffect?.Draw(spriteBatch);
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

                spriteBatch.Draw(ContentHandler.Images["DaySetup_IconPrice"], new Vector2(40, -15), Color.White);

                spriteBatch.DrawString(Defaults.Font, Player.CoinCount.ToString(), new Vector2(218, 33), Defaults.Brown);
                spriteBatch.DrawString(Defaults.Font, Player.CoinCount.ToString(), new Vector2(218, 37), Defaults.Brown);
                spriteBatch.DrawString(Defaults.Font, Player.CoinCount.ToString(), new Vector2(222, 33), Defaults.Brown);
                spriteBatch.DrawString(Defaults.Font, Player.CoinCount.ToString(), new Vector2(222, 37), Defaults.Brown);
                spriteBatch.DrawString(Defaults.Font, Player.CoinCount.ToString(), new Vector2(220, 35), Defaults.Cream);

                spriteBatch.Draw(ContentHandler.Images["CharacterSelectScreen_Foreground"], new Rectangle(0, 0, 1536, 2732), Color.White);
                spriteBatch.Draw(ContentHandler.Images["Symbol_Female"], new Rectangle(80, 700, 191, 171), Color.White);
                spriteBatch.Draw(ContentHandler.Images["Symbol_Male"], new Rectangle(1280, 700, 191, 171), Color.White);
                spriteBatch.Draw(ContentHandler.Images["ArrowLeft"], new Rectangle(140, 1145, 136, 136), Color.White);
                spriteBatch.Draw(ContentHandler.Images["ArrowRight"], new Rectangle(1330, 1145, 136, 136), Color.White);
                spriteBatch.DrawString(Defaults.Font, KidHandler.CurrentKid.GetName(), new Vector2(808, 1830), Defaults.Cream, 0f, Defaults.Font.MeasureString(KidHandler.CurrentKid.GetName()) / 2, 1.2f, SpriteEffects.None, 1f);

                if (KidHandler.CurrentKid.IsLocked)
                {
                    if (KidHandler.CurrentKid.UnlockMechanism == UnlockMechanism.Purchase && Player.CoinCount >= 0)//KidHandler.CurrentKid.UnlockPrice)
                    {
                        spriteBatch.Draw(ContentHandler.Images["button_unlock"], new Vector2(30, 1926), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(ContentHandler.Images["button_locked"], new Vector2(30, 1926), Color.White);
                    }
                }

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

            graphics.GraphicsDevice.SetRenderTarget(null);
            graphics.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
            spriteBatch.End();
        }
    }
}
