using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Imlost.Data;

namespace Imlost.Serializer
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };

            using (XmlWriter writer = XmlWriter.Create("database.xml", settings))
            {
                IntermediateSerializer.Serialize<List<SceneData>>(writer, PrepareSceneDatas(), null);
            }
            
            using (XmlWriter writer = XmlWriter.Create("items.xml", settings))
            {
                IntermediateSerializer.Serialize<List<InventoryItem>>(writer, PrepareInventory(), null);
            }
        }

        public static List<InventoryItem> PrepareInventory()
        {
            List<InventoryItem> items = new List<InventoryItem>(6);

            InventoryItem item;

            string[] names = 
            {
                "Ticket 1/2", 
                "Ticket 2/2", 
                "Marteaux", 
                "Anneau vide",
                "Diamand", 
                "Ampoule",
                "Clé local technique"
            };

            for (int i = 0; i < names.Length; i++)
            {
                item = new InventoryItem();
                item.AssetName = "Items/item_" + (i + 1);
                item.Code = String.Format("{0}_{1}", names[i].Trim(), (i + 1));
                item.Name = names[i];
                item.PickSound = "Audio/Sound";
                item.UseSound = "Audio/Sound";

                items.Add(item);
            }

            return items;
        }

        public static List<SceneData> PrepareSceneDatas()
        {
            List<SceneData> datas = new List<SceneData>(16);

            SceneData data;

            for (int i = 0; i < 16; i++)
            {
                data = new SceneData();
                data.Background = "Backgrounds/Background_" + (i + 1);
                data.BottomScene = "scene_1";
                data.RightScene = "scene_2";
                data.LeftScene = "scene_3";
                data.TopScene = "scene_4";
                data.AmbienceZone = 1;
                data.Message = "Message perso";
                data.Code = "scene_" + (i + 1);
                data.Objects = PrepareObjects();
                datas.Add(data);
            }

            return datas;
        }

        public static List<SceneObject> PrepareObjects()
        {
            List<SceneObject> sceneObjects = new List<SceneObject>(5);

            SceneObject sceneObject;

            for (int i = 0; i < 5; i++)
            {
                sceneObject = new SceneObject();
                sceneObject.Name = "SceneObject_" + (i + 1);
                sceneObjects.Add(sceneObject);
                sceneObject.ActionID = 1;
                sceneObject.AssetName = "Items/item_" + (i + 1);
                sceneObject.SoundName = String.Empty;
            }

            return sceneObjects;
        }
    }
}
