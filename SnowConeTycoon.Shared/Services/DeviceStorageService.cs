using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml;
using System.Xml.Serialization;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Services.Interfaces;

namespace SnowConeTycoon.Shared.Services
{
    public class DeviceStorageService : IStorageService
    {
        private const string SAVEFILENAME = "SC.sav";

        public DeviceStorageService()
        {
        }

        public void Save()
        {
            try
            {
                // Save the game state (in this case, the typed text).
                IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication();

                // open isolated storage, and write the savefile.
                IsolatedStorageFileStream fs = null;
                fs = savegameStorage.OpenFile(SAVEFILENAME, FileMode.Create);

                if (fs != null)
                {
                    var gameData = Player.ToGameData();

                    var settings = new XmlWriterSettings();
                    settings.OmitXmlDeclaration = true;
                    XmlSerializer serializer = new XmlSerializer(typeof(GameData));
                    using (XmlWriter writer = XmlWriter.Create(fs, settings))
                    {
                        serializer.Serialize(writer, gameData);
                    }
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                var a = ex.ToString();
            }
        }

        public bool SaveFileExists()
        {
            try
            {
                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    return myIsolatedStorage.FileExists(SAVEFILENAME);
                }
            }
            catch (Exception ex)
            {
                var a = ex.ToString();
            }

            return false;
        }

        public void Load()
        {
            var gameData = new GameData();
            try
            {
                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile(SAVEFILENAME, FileMode.Open))
                    {
                        using (TextReader textReader = new StreamReader(stream))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
                            gameData = (GameData)serializer.Deserialize(textReader);
                        }

                        stream.Close();
                    }
                }

                Player.SetCoins(gameData.CoinCount);
                Player.SetCones(gameData.ConeCount);
                Player.SetIce(gameData.IceCount);
                Player.SetFlyer(gameData.FlyerCount);
                Player.SetSyrup(gameData.SyrupCount);

                var ts = DateTime.Now - gameData.LastPlayed;
                var days = ts.Days;

                if (days < 1)
                {
                    days = 1;
                }

                Player.SetConsecutiveDays(days);
            }
            catch (Exception ex)
            {
                var a = ex.ToString();
                Player.Reset();
            }
        }
    }
}
