using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Yna.Engine;
using Yna.Engine.Graphics;
using Yna.Engine.Graphics.Event;
using Yna.Engine.Helpers;
using Yna.Engine.Input;

namespace Imlost.Source.States
{
    public class MenuState : YnState2D
    {
        private YnEntity _background;
        private Rectangle playRectangle;
        private Rectangle quitRectangle;

        public MenuState(string name)
            : base(name, true)
        {
            _background = new YnEntity("Backgrounds/Accueil");
            Add(_background);

            playRectangle = new Rectangle(
                (int)ScreenHelper.GetScaleX(135),
                (int)ScreenHelper.GetScaleY(265),
                (int)(ScreenHelper.GetScale().X * 226),
                (int)(ScreenHelper.GetScale().Y * 65));

            quitRectangle = new Rectangle(
                (int)ScreenHelper.GetScaleX(135),
                (int)ScreenHelper.GetScaleY(420),
                (int)(ScreenHelper.GetScale().X * 226),
                (int)(ScreenHelper.GetScale().Y * 65));
        }

        public override void Initialize()
        {
            base.Initialize();

            _background.SetFullScreen();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (YnG.Keys.JustPressed(Keys.Enter) || MouseInRectangle(playRectangle) || TouchInRectangle(playRectangle))
                YnG.StateManager.SetActive("scene", true);

            else if (YnG.Keys.JustPressed(Keys.Escape) || YnG.Gamepad.JustPressed(PlayerIndex.One, Buttons.Back) || MouseInRectangle(quitRectangle) || TouchInRectangle(quitRectangle))
                YnG.Exit();
        }

        public bool MouseInRectangle(Rectangle rectangle)
        {
            if (YnG.Mouse.JustClicked(MouseButton.Left))
                return rectangle.Contains(YnG.Mouse.X, YnG.Mouse.Y);
            return false;
        }

        private bool TouchInRectangle(Rectangle rectangle)
        {
            TouchPanelCapabilities touchCap = TouchPanel.GetCapabilities();

            if (touchCap.IsConnected)
            {
                TouchCollection touches = TouchPanel.GetState();
                Vector2 position = Vector2.Zero;

                if (touches.Count > 0)
                {
                    position = touches[0].Position;
                }

                return position.X >= rectangle.X && position.X <= rectangle.X + rectangle.Width && position.Y >= rectangle.Y && position.Y <= rectangle.Y + rectangle.Height;
            }
            return false;
        }
    }
}
