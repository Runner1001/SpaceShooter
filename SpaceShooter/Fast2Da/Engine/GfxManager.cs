using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Fast2Da
{
    static class GfxManager
    {
        static Dictionary<string, Texture> textures;
        static Dictionary<string, Tuple<Texture, Animation>> spritesheets;

        static GfxManager()
        {
            textures = new Dictionary<string, Texture>();
            spritesheets = new Dictionary<string, Tuple<Texture, Animation>>();
        }

        private static Animation LoadAnimation(
            XmlNode animationNode, int width, int height)
        {
            XmlNode currNode = animationNode.FirstChild;
            bool loop = bool.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            float fps = float.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int rows = int.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int cols = int.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int startX = int.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int startY = int.Parse(currNode.InnerText);

            return new Animation(width, height, cols, rows, fps, loop, startX, startY);
        }

        public static void LoadSpritesheet(XmlNode spritesheetNode)
        {
            XmlNode nameNode = spritesheetNode.FirstChild;

            string name = nameNode.InnerText;
            XmlNode filenameNode = nameNode.NextSibling;
            Texture texteure = new Texture(filenameNode.InnerText);

            XmlNode frameNode = filenameNode.NextSibling;

            Animation animation;

            if (frameNode.HasChildNodes)
            {
                int width = int.Parse(frameNode.FirstChild.InnerText);
                int height = int.Parse(frameNode.LastChild.InnerText);
                XmlNode animationsNode = frameNode.NextSibling;
                animation = LoadAnimation(
                    animationsNode.FirstChild, width, height);
            }
            else
            {
                animation = new Animation(texteure.Width, texteure.Height);
            }
            AddSpritesheet(name, texteure, animation);
        }

        public static void Load()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Assets/SpriteSheetConfig.xml");

            XmlNode root = doc.DocumentElement;

            foreach(XmlNode spritesheetNode in root.ChildNodes)
            {
                LoadSpritesheet(spritesheetNode);
            }
           
        }
        public static void AddTexture(string name, string filePath)
        {
            textures.Add(name, new Texture(filePath));
        }

        public static Texture GetTexture(string name)
        {
            if (textures.ContainsKey(name))
            {
                return textures[name];
            }
            return null;
        }

        public static void AddSpritesheet(string name, Texture t, Animation a)
        {
            spritesheets.Add(name, new Tuple<Texture, Animation>(t, a));
        }

        public static Tuple<Texture, Animation> GetSpritesheet(string name)
        {
            if (spritesheets.ContainsKey(name))
            {
                return spritesheets[name];
            }
            return null;
        }
    }
}
