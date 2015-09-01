using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yna.Engine;
using Yna.Engine.Graphics;
using Yna.Engine.Graphics.Gui;
using Yna.Engine.Graphics.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Yna.Engine.Graphics.Gui.Widgets;

namespace Imlost.Source.States
{
    public class MainMenuState : YnState2D
    {
        private YnGui _gui;

        public MainMenuState(string name)
            : base(name, false)
        {
            _gui = new YnGui();
            YnGui.RegisterSkin("GAME", YnSkinGenerator.Generate(Color.Aquamarine, "Fonts/MainMenuFont"));
        }

        public override void Initialize()
        {
            base.Initialize();
            _gui.Initialize();
        }

        public void BuildGui()
        {
            YnLabel title = new YnLabel();
            title.Text = "I'm lost";
            title.CustomFont = YnG.Content.Load<SpriteFont>("Fonts/TitleFont");
            //title.Layout(); // Forçage du layout pour avoir la taille du texte
            title.Position = new Vector2(YnG.Width / 2 - title.Width / 2, title.Height);
            _gui.Add(title);

            YnPanel menu = new YnPanel();
            menu.HasBackground = false;
            menu.Position = new Vector2(YnG.Width / 2 - 170 / 2, title.Position.Y + title.Height * 1.5F);
            menu.Padding = 10;
            _gui.Add(menu);

            const int buttonWidth = 150;
            const int buttonHeight = 50;

            SpriteFont menuFont = YnG.Content.Load<SpriteFont>("Fonts/MainMenuFont");
            YnTextButton playButton = menu.Add(new YnTextButton());
            playButton.Text = "Jouer";
            playButton.Width = buttonHeight;
            playButton.Width = buttonWidth;
            playButton.CustomFont = menuFont;
            playButton.MouseClick += delegate(object o, MouseClickEntityEventArgs e)
            {
                // TODO
            };

            YnTextButton settingsButton = menu.Add(new YnTextButton());
            settingsButton.Text = "Options";
            settingsButton.Width = buttonHeight;
            settingsButton.Width = buttonWidth;
            settingsButton.CustomFont = menuFont;
            settingsButton.MouseClick += delegate(object o, MouseClickEntityEventArgs e)
            {
                // TODO
            };

            YnTextButton exitButton = menu.Add(new YnTextButton());
            exitButton.Text = "Quitter";
            exitButton.Width = buttonHeight;
            exitButton.Width = buttonWidth;
            exitButton.CustomFont = menuFont;
            exitButton.MouseClick += delegate(object o, MouseClickEntityEventArgs e)
            {
                YnG.Exit();
            };


            _gui.Initialize();
            //_gui.PrepareWidgets();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _gui.Update(gameTime);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _gui.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            _gui.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
