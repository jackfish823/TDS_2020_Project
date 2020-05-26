#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion

namespace TopDownShooterProject2020
{
    public class Save
    {
        public int gameId, userId;
        public string gameName, baseFolder, backupFolder, backupPath;
        public bool loadingID = true;
        public XDocument saveFile;  

        public Save(int gameId, string gameName)
        {
            this.gameId = gameId;
            this.gameName = gameName;
            //heroLevel = 1;

            //LoadGame();
            backupFolder = "asdgvqwex";
            backupPath = "bath";

            baseFolder = Globals.appDataFilePath + "\\" + gameName + "";

            CreateBaseFolders();

        }

        
        public void CreateBaseFolders() // Adding the folders if they dont exist
        {
            CreateFolder(Globals.appDataFilePath + "\\" + gameName + "");
            CreateFolder(Globals.appDataFilePath + "\\" + gameName + "\\XML");
            CreateFolder(Globals.appDataFilePath + "\\" + gameName + "\\XML\\SavedGames");
        }     
        public void CreateFolder(string str) // Creates folders
        {
            DirectoryInfo CreateSiteDirectory = new DirectoryInfo(str);
            if (!CreateSiteDirectory.Exists)
            {
                CreateSiteDirectory.Create();
            }
        }
        public virtual bool CheckIfFileExists(string path)
        {
            bool fileExists;

            fileExists = File.Exists(Globals.appDataFilePath + "\\" + gameName + "\\" + path);


            return fileExists;
            //return true;
        }
        public virtual void DeleteFile(string PATH)
        {
            File.Delete(PATH);
        }
        public XDocument GetFile(string file)  // Returns only xml files
        {
            if (CheckIfFileExists(file))
            {
                return XDocument.Load(Globals.appDataFilePath + "\\" + gameName + "\\" + file);
            }

            return null;
        }
        public virtual XDocument LoadFile(string FILEPATH, bool CHECKKEY = true) // Loading any xml file
        {
            XDocument xml = GetFile(FILEPATH);


            return xml;
        }
        public virtual void HandleSaveFormates(XDocument xml) // Write any file in bytes
        {

            byte[] compress = Encoding.ASCII.GetBytes(StringToBinary(xml.ToString()));
            File.WriteAllBytes(Globals.appDataFilePath + "\\" + gameName + "\\XML\\SavedGames\\" + Convert.ToString(gameId, Globals.culture), compress);



        }
        public virtual void HandleSaveFormates(XDocument xml, string PATH) // Writes the files in xml
        {

            xml.Save(Globals.appDataFilePath + "\\" + gameName + "\\XML\\" + PATH);


        }


        #region Converting to Binary and back

        public static string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }

            return Encoding.ASCII.GetString(byteList.ToArray());
        }
        #endregion

    }
}
