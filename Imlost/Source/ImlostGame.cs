using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Yna.Engine;
using Yna.Engine.Helpers;
using Imlost.Source.States;
using Imlost.Source;
using Imlost.Data;
using Yna.Engine.Content;

namespace Imlost
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ImlostGame : YnGame
    {
        public MenuState menuState;
        public SceneState sceneState; // TODO ajouter un state par screen ;) avec un petit XML 

        public static bool Debug;

        //public static List<SceneData> Scenes;
        public static Dictionary<string, SceneData> Scenes;
        public static Dictionary<string, InventoryItem> InventoryItems;

#if !WINDOWS_PHONE && !NETFX_CORE
        public ImlostGame()
            : base(GameConfiguration.ScreenWidth, GameConfiguration.ScreenHeight, GameConfiguration.GameTitle)
        {
            if (GameConfiguration.DetermineBestResolution)
                DetermineBestResolution(true);
            else
            {
                if (GameConfiguration.EnabledFullScreen && !graphics.IsFullScreen)
                    graphics.ToggleFullScreen();
            }

            YnG.AudioManager.SoundEnabled = GameConfiguration.EnabledSound;
            YnG.AudioManager.MusicEnabled = GameConfiguration.EnabledMusic;
#else
        public ImlostGame()
            : base()
        {
#endif
            ScreenHelper.ScreenWidthReference = GameConfiguration.ReferenceWidth;
            ScreenHelper.ScreenHeightReference = GameConfiguration.ReferenceHeight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            menuState = new MenuState("menu");
            sceneState = new SceneState("scene");
            sceneState.SetData(Scenes["scene_1"]);

            stateManager.Add(menuState, true);
            stateManager.Add(sceneState, false);
            stateManager.SetActive("menu", true);

            //stateManager.SetScreenActive("scene", true);
            //AmbianceManager am = new AmbianceManager();

            YnG.ShowMouse = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var liste = Content.Load<SceneData[]>("database");

            Scenes = new Dictionary<string, SceneData>();

            foreach (SceneData data in liste)
                Scenes.Add(data.Code, data);

            // Chargement des objets d'inventaire
            List<InventoryItem> items = Content.Load<List<InventoryItem>>("items");
            InventoryItems = new Dictionary<string, InventoryItem>();
            foreach (InventoryItem item in items)
            {
                InventoryItems.Add(item.Code, item);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

    }
}
