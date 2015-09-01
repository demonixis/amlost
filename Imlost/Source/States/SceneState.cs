using Imlost.Data;
using Imlost.Source.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using Yna.Engine;
using Yna.Engine.Graphics;
using Yna.Engine.Helpers;
using Yna.Engine.Input;

namespace Imlost.Source.States
{
    public class SceneState : YnState2D
    {
        private const int _buttonWidth = 75;
        private const int _buttonHeight = 26;
        private const int _arrowHitboxSize = 100;

        private int _menuClosedY;
        private int _menuOpenedY;

        private Rectangle _menuHitbox;

        private Rectangle _leftSceneHitbox;
        private Rectangle _topSceneHitbox;
        private Rectangle _rightSceneHitbox;
        private Rectangle _bottomSceneHitbox;

        private SceneData _sceneData;

        private YnEntity _background;
        private YnEntity _goLeft;
        private YnEntity _goUp;
        private YnEntity _goRight;
        private YnEntity _goDown;

        private YnText _narationText;
        private YnTimer _narationTimer;

        private AmbianceManager _ambianceManager;
        private YnEntity _menuIcon;
        private YnEntity _notesIcon;

        private YnEntity _menuBackground;

        private Dictionary<string, YnEntity> _itemImages;

        private bool _menuIsShown;
        private List<InventoryItem> _inventory;

        private Dictionary<string, bool> _visitedScreens;

        private bool _deadlySceenOfDeath;
        private float _deathAlpha;

        private YnEntity _splash;
        private bool _showSplash;

        private List<YnEntity> _itemsOnScreen;
        private bool _ticket1Ramasse;
        private bool _ticket2Ramasse;
        private bool _marteauRamasse;
        private bool _ampouleRamasse;
        private bool _anneauRamasse;
        private bool _diamantRamasse;
        private bool _cleRecue;

        private bool _porteOuverte;
        private bool _cadenasCasse;
        private bool _telephoneRepondu;
        private bool _ampoulePosee;
        private bool _ticketDonne;

        private YnEntity _draggedImage;
        private bool _dragging;

        private VocalSynthetizer _vocalSynthetizer;

        public SceneState(string name)
            : base(name, false)
        {
            _vocalSynthetizer = new VocalSynthetizer();

            InitParcours();
            _menuIsShown = false;
            _visitedScreens = new Dictionary<string, bool>(); // 18 écrans
            _itemsOnScreen = new List<YnEntity>();

            _narationText = new YnText("Fonts/GameText", "Dummy");
            _narationText.Color = Color.DarkBlue;
            _narationText.Scale = new Vector2(1.5f) * Yna.Engine.Helpers.ScreenHelper.GetScale().X;

            _narationTimer = new YnTimer(3500);
            _narationTimer.Completed += (s, e) => _narationText.Active = false;

            _leftSceneHitbox = new Rectangle(0, _arrowHitboxSize, _arrowHitboxSize, YnG.Height - (97 - 25) - 2 * _arrowHitboxSize);
            _topSceneHitbox = new Rectangle(_arrowHitboxSize, 0, YnG.Width - 2 * _arrowHitboxSize, _arrowHitboxSize);
            _rightSceneHitbox = new Rectangle(YnG.Width - _arrowHitboxSize, _arrowHitboxSize, _arrowHitboxSize, YnG.Height - (97 - 25) - 2 * _arrowHitboxSize);
            _bottomSceneHitbox = new Rectangle(_arrowHitboxSize, YnG.Height - (97 - 25) - _arrowHitboxSize, YnG.Width - 2 * _arrowHitboxSize, _arrowHitboxSize);

            _menuHitbox = new Rectangle(YnG.Width / 2 - _buttonWidth / 2, YnG.Height - _buttonHeight, _buttonWidth, _buttonHeight);

            int padding = 30;
            int imageHalfSize = 30;
            int imageSize = imageHalfSize * 2;

            _goLeft = new YnEntity("Textures/icone-fleche-gauche", new Vector2(padding, YnG.Height / 2 - imageHalfSize));
            _goLeft.Visible = false;
            _goLeft.Name = "go_left";
            _goLeft.MouseClicked += (s, e) => GoLeft();

            _goUp = new YnEntity("Textures/icone-fleche-haut", new Vector2(YnG.Width / 2 - imageHalfSize, padding));
            _goUp.Visible = false;
            _goUp.Name = "go_up";
            _goUp.MouseClicked += (s, e) => GoUp();

            _goRight = new YnEntity("Textures/icone-fleche-droite", new Vector2(YnG.Width - padding - imageSize, YnG.Height / 2 - imageHalfSize));
            _goRight.Visible = false;
            _goRight.Name = "go_right";
            _goRight.MouseClicked += (s, e) => GoRight();

            _goDown = new YnEntity("Textures/icone-fleche-bas", new Vector2(YnG.Width / 2 - imageHalfSize, YnG.Height - padding - imageSize - 97 + 25));
            _goDown.Visible = false;
            _goDown.Name = "go_down";
            _goDown.MouseClicked += (e, s) => GoDown();

            _menuClosedY = YnG.Height - 25;
            _menuOpenedY = YnG.Height - 97;
            _menuBackground = new YnEntity("Textures/liste-inventaire", new Vector2(0, _menuClosedY));

            int iconPadding = 15;
            int x = iconPadding;
            int y = YnG.Height - 60;

            _menuIcon = new YnEntity("Textures/btn-menu", new Vector2(x, y));
            _menuIcon.Visible = false;
            _menuIcon.MouseClicked += (e, s) =>
            {
                if (!_menuIcon.Visible) return;
            };

            y = YnG.Height - 69;
            x = YnG.Width - iconPadding - 80;
            _notesIcon = new YnEntity("Textures/btn-notes", new Vector2(x, y));
            _notesIcon.Visible = false;
            _notesIcon.MouseClicked += (e, s) =>
            {
                if (!_notesIcon.Visible) return;

                // TODO Afficher les notes
                if (ImlostGame.Debug)
                    Console.WriteLine("Clicked on NOTES");
            };

            _inventory = new List<InventoryItem>();

            _itemImages = new Dictionary<string, YnEntity>();

            foreach (InventoryItem item in ImlostGame.InventoryItems.Values)
            {
                YnEntity img = new YnEntity(item.AssetName);
                img.Visible = false;
                _itemImages.Add(item.Code, img);
            }

            _ambianceManager = new AmbianceManager();
        }

        public override void Initialize()
        {
            base.Initialize();
            _menuBackground.Initialize();
            _notesIcon.Initialize();
            _menuIcon.Initialize();
            _narationText.Initialize();
            _background.SetFullScreen();
            _vocalSynthetizer.Initialize();
            _vocalSynthetizer.Enabled = GameConfiguration.EnabledSpeechSynthesis;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _menuBackground.LoadContent();
            _menuBackground.Width = YnG.Width;
            _notesIcon.LoadContent();
            _menuIcon.LoadContent();
            _narationText.LoadContent();
            _narationText.Position = new Vector2(Yna.Engine.Helpers.ScreenHelper.GetScaleX(50), YnG.Height - _narationText.ScaledHeight - Yna.Engine.Helpers.ScreenHelper.GetScaleY(90));

            foreach (KeyValuePair<string, YnEntity> pair in _itemImages)
                pair.Value.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            _menuBackground.UnloadContent();
            _notesIcon.UnloadContent();
            _menuIcon.UnloadContent();
            _vocalSynthetizer.Close();
        }

        private void DrawRectangle(SpriteBatch spriteBatch, Rectangle rect)
        {
            Texture2D tex = YnGraphics.CreateTexture(Color.Red, 1, 1);

            Vector2 a;
            Vector2 b;

            // Top
            a = new Vector2(rect.X, rect.Y);
            b = new Vector2(rect.X + rect.Width, rect.Y);
            YnGraphics.DrawLine(spriteBatch, tex, 1F, Color.White, a, b);

            // Right
            a = new Vector2(rect.X + rect.Width, rect.Y);
            b = new Vector2(rect.X + rect.Width, rect.Y + rect.Height);
            YnGraphics.DrawLine(spriteBatch, tex, 1F, Color.White, a, b);

            // Bottom
            a = new Vector2(rect.X + rect.Width, rect.Y + rect.Height);
            b = new Vector2(rect.X, rect.Y + rect.Height);
            YnGraphics.DrawLine(spriteBatch, tex, 1F, Color.White, a, b);

            // Left
            a = new Vector2(rect.X, rect.Y + rect.Height);
            b = new Vector2(rect.X, rect.Y);
            YnGraphics.DrawLine(spriteBatch, tex, 1F, Color.White, a, b);
        }

        /// <summary>
        /// Initialise le nouvel objet de données
        /// </summary>
        /// <param name="data">Le conteneur de données</param>
        public void SetData(SceneData data)
        {
            _sceneData = data;

            // On cache le texte
            _narationText.Active = false;

            // Réinitialisation du DSOD : le Deadly Screen Of Death
            _deadlySceenOfDeath = false;

            if (_sceneData.LeftScene != String.Empty)
                _goLeft.Visible = true;
            else
                _goLeft.Visible = false;

            if (_sceneData.TopScene != String.Empty)
                _goUp.Visible = true;
            else
                _goUp.Visible = false;

            if (_sceneData.RightScene != String.Empty)
                _goRight.Visible = true;
            else
                _goRight.Visible = false;

            if (_sceneData.BottomScene != String.Empty)
                _goDown.Visible = true;
            else
                _goDown.Visible = false;

            if (!_visitedScreens.ContainsKey(data.Code))
            {
                _visitedScreens.Add(data.Code, false);
            }
            else
            {
                _visitedScreens [data.Code] = true;
            }

            if (_visitedScreens [data.Code] == false)
            {
                // Découverte de la zone : affichage d'un message TODO
                if (data.Message != String.Empty)
                {
                    string message = data.Message;
                    _narationText.Text = message;
                    _narationText.Active = true;
                    _narationTimer.Start();
                    _vocalSynthetizer.SpeakAsync(message);
                }
            }

            if (GetMemberByName("background") != null)
            {
                _background.AssetName = data.Background;

                if (data.Code == "scene_18" && _ampoulePosee)
                    _background.AssetName = data.Background + "_1";

                _background.LoadContent();
                _background.SetFullScreen();
            }
            else
            {
                _background = new YnEntity(data.Background);
                _background.Name = "background";
                Add(_background);

                _background.SetFullScreen();
            }

            if (_sceneData.LeftScene != String.Empty && GetMemberByName("go_left") == null)
                Add(_goLeft);

            if (_sceneData.TopScene != String.Empty && GetMemberByName("go_up") == null)
                Add(_goUp);

            if (_sceneData.RightScene != String.Empty && GetMemberByName("go_right") == null)
                Add(_goRight);

            if (_sceneData.BottomScene != String.Empty && GetMemberByName("go_down") == null)
                Add(_goDown);

            if (_sceneData.AmbienceZone > 0)
            {
                AmbianceManager.AmbianceZone zone = AmbianceManager.AmbianceZone.Outside;

                if (_sceneData.AmbienceZone == 2)
                    zone = AmbianceManager.AmbianceZone.Hall;
                if (_sceneData.AmbienceZone == 3)
                    zone = AmbianceManager.AmbianceZone.Bathroom;
                if (_sceneData.AmbienceZone == 4)
                    zone = AmbianceManager.AmbianceZone.Stairs;
                if (_sceneData.AmbienceZone == 5)
                    zone = AmbianceManager.AmbianceZone.Room;

                _ambianceManager.SetAmbianceZone(zone);
            }

            // 1 - on clean la scène
            _itemsOnScreen.Clear();

            foreach (SceneObject sceneObject in data.Objects)
            {
                if (sceneObject.AssetName != String.Empty)
                {
                    bool mustAddObject = true;
                    if ((ActionType)sceneObject.ActionID == ActionType.Pick)
                    {
                        // Ne pas ajouter les éléments déjà dans l'inventaire
                        foreach (InventoryItem item in _inventory)
                        {
                            if (item.Code == sceneObject.Name)
                                mustAddObject = false;
                        }
                    }

                    if (mustAddObject)
                    {
                        YnEntity imageObject = new YnEntity(sceneObject.AssetName);
                        imageObject.LoadContent();

                        imageObject.MouseClicked += (s, e) =>
                            {
                                if (sceneObject.SoundName != String.Empty)
                                {
                                    YnG.AudioManager.PlaySound(sceneObject.SoundName, 1.0f, 1.0f, 1.0f);
                                }

                                if ((ActionType)sceneObject.ActionID == ActionType.Pick)
                                {
                                    AddItem(sceneObject.Name);

                                    if (sceneObject.Name.Equals("SceneObject_1"))
                                    {
                                        _ticket1Ramasse = true;
                                        _ambianceManager.SetGuideSound(AmbianceManager.GuideSound.Carhonk);
                                    }
                                    if (sceneObject.Name.Equals("SceneObject_2"))
                                    {
                                        _marteauRamasse = true;
                                        _ambianceManager.SetGuideSound(AmbianceManager.GuideSound.None);
                                    }
                                    if (sceneObject.Name.Equals("SceneObject_3"))
                                    {
                                        _anneauRamasse = true;
                                    }
                                    if (sceneObject.Name.Equals("SceneObject_4"))
                                    {
                                        _cleRecue = true;
                                    }
                                    if (sceneObject.Name.Equals("SceneObject_5"))
                                    {
                                        _ampouleRamasse = true;
                                    }
                                    if (sceneObject.Name.Equals("SceneObject_6"))
                                    {
                                        _diamantRamasse = true;
                                    }
                                    if (sceneObject.Name.Equals("SceneObject_7"))
                                    {
                                        _diamantRamasse = true;
                                    }
                                    
                                    string temp = sceneObject.Name.Split(new char[] { '_' })[1];
                                    bool valid = true;
                                    
                                    try
                                    {
                                        int c = int.Parse(temp);
                                    }
                                    catch (Exception ex)
                                    {
                                        valid = false;
                                        Console.WriteLine(ex.Message);
                                    }

                                    if (valid)
                                    {
                                        showSplash(sceneObject.Name);

                                        // Suppression de l'item de la scène
                                        YnEntity imgToDelete = null;

                                        foreach (YnEntity i in _itemsOnScreen)
                                        {
                                            if (i.AssetName == sceneObject.AssetName)
                                                imgToDelete = i;
                                        }

                                        _itemsOnScreen.Remove(imgToDelete);
                                    }
                                }
                            };
                        _itemsOnScreen.Add(imageObject);

                        imageObject.X = (int)ScreenHelper.GetScaleX(sceneObject.X);
                        imageObject.Y = (int)ScreenHelper.GetScaleY(sceneObject.Y);
                    }
                }
            }

            // Si c'est une scène "mortelle" on passe un flag à true
            // TODO Ajouter ici les scènes mortelles
            if (data.Code == "scene_4")
            {
                _deadlySceenOfDeath = true;
                _ambianceManager.PlayDeath(AmbianceManager.TypeOfDeath.Ghost);
                _deathAlpha = 0.0F;
            }
        }

        private bool MouseInRectangle(Rectangle rectangle)
        {
            int mx = YnG.Mouse.X;
            int my = YnG.Mouse.Y;
            return mx >= rectangle.X && mx <= rectangle.X + rectangle.Width && my >= rectangle.Y && my <= rectangle.Y + rectangle.Height;
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
                    position = touches [0].Position;
                }

                return position.X >= rectangle.X && position.X <= rectangle.X + rectangle.Width && position.Y >= rectangle.Y && position.Y <= rectangle.Y + rectangle.Height;
            }
            return false;
        }

        private bool GetTouchState()
        {
            TouchPanelCapabilities touchCap = TouchPanel.GetCapabilities();

            if (touchCap.IsConnected)
            {
                TouchCollection touches = TouchPanel.GetState();

                if (touches.Count > 0)
                {
                    return touches [0].State == TouchLocationState.Pressed;
                }

                return false;
            }

            return false;
        }

        private void GoDown()
        {
            if (ImlostGame.Debug)
                Console.WriteLine("Go DOWN");

            if (ImlostGame.Scenes.Keys.Contains(_sceneData.BottomScene))
                SetData(ImlostGame.Scenes [_sceneData.BottomScene]);
        }

        private void GoUp()
        {
            if (ImlostGame.Debug)
                Console.WriteLine("Go UP");


            if (_sceneData.Code.Equals("scene_1") && !_ticket1Ramasse)
            {
                //on est bloqué
            }

            else if (_sceneData.Code.Equals("scene_8") && _marteauRamasse)
            {
                //on charge la scene 9 alternative
            }

            else if (_sceneData.Code.Equals("scene_14") && _ampouleRamasse)
            {
                //on charge la scene 15 alternative
            }
            else if (_sceneData.Code.Equals("scene_17") && _ampoulePosee)
            {
                //on charge la scene 18 alternative
            }

            else if (ImlostGame.Scenes.Keys.Contains(_sceneData.TopScene))
                SetData(ImlostGame.Scenes [_sceneData.TopScene]);
        }


        private void GoLeft()
        {
            if (ImlostGame.Debug)
                Console.WriteLine("Go LEFT");
            if (_sceneData.Code.Equals("scene_8") && !_cadenasCasse)
            {
                _narationText.Text = "Je ne pourrais jamais ouvrir ça";
                _narationText.Active = true;
                _narationTimer.Start();
            }

            else if (ImlostGame.Scenes.Keys.Contains(_sceneData.LeftScene))
                SetData(ImlostGame.Scenes [_sceneData.LeftScene]);
        }

        private void GoRight()
        {
            if (ImlostGame.Debug)
                Console.WriteLine("Go RIGHT");

            if (_sceneData.Code.Equals("scene_13") && !_porteOuverte)
            {
                _narationText.Text = "Rien à faire, c'est bien fermé.";
                _narationText.Active = true;
                _narationTimer.Start();
            }

            else if (ImlostGame.Scenes.Keys.Contains(_sceneData.RightScene))
                SetData(ImlostGame.Scenes [_sceneData.RightScene]);
        }

        private void AddItem(string code)
        {
            if (code != "SceneObject_b")
                _inventory.Add(ImlostGame.InventoryItems [code]);
        }

        public void showSplash(string assetName)
        {
            _showSplash = true;

            if (_splash == null)
            {
                _splash = new YnEntity("Splashes/" + assetName);
            }
            else
            {
                _splash.AssetName = "Splashes/" + assetName;
            }

            _splash.LoadContent();
            _splash.SetFullScreen();
        }

        private void InitParcours()
        {
            _ticket1Ramasse = false;
            _ticket2Ramasse = false;
            _marteauRamasse = false;
            _ampouleRamasse = false;
            _anneauRamasse = false;
            _diamantRamasse = false;
            _cleRecue = false;

            _cadenasCasse = false;
            _telephoneRepondu = false;
            _porteOuverte = false;
            _ampoulePosee = false;
            _ticketDonne = false;
        }

        private void DoDrop(YnEntity dropped, YnEntity reciever)
        {
            string code = reciever.AssetName;
            string dcode = dropped.AssetName;
            Console.WriteLine(code + "-" + dcode);

            if (code == "Items/cadenas" && dcode == "Items/item_3")
            {
                // Supprimer le cadenas
                _cadenasCasse = true;
                YnEntity todelete = null;
                foreach (YnEntity img in _itemsOnScreen)
                {
                    if (img.AssetName == "Items/cadenas")
                        todelete = img;
                }
                _itemsOnScreen.Remove(todelete);
            }
            else if (code == "Items/masqueporte" && dcode == "Items/item_7")
            {
                _porteOuverte = true;
            }
            else if (code == "Items/douille" && dcode == "Items/item_6")
            {
                _ampoulePosee = true;
                SetData(ImlostGame.Scenes ["scene_18"]);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _ambianceManager.Update(gameTime);

            _narationTimer.Update(gameTime);

            if (_deadlySceenOfDeath)
            {
                // Le joueur est là où il ne devrait pas!
                // Fondu qui va bien
                _deathAlpha += 0.01F;

                if (_deathAlpha >= 1.0F && _ambianceManager.TransitionDone())
                {
                    // Le joueur est mort : TP sur le banc
                    _deathAlpha = 0.0F;
                    SetData(ImlostGame.Scenes ["scene_1"]);
                }
            }
            else
            {
                if (_showSplash)
                {
                    // Splash affiché, on le masque si on clique n'importe où
                    if (YnG.Mouse.JustClicked(MouseButton.Left) || GetTouchState()
                        || YnG.Keys.JustPressed(Keys.C) || YnG.Keys.JustPressed(Keys.NumPad0) || YnG.Gamepad.JustPressed(PlayerIndex.One, Buttons.Y))
                    {
                        _showSplash = false;
                    }
                }
                else
                {
                    _menuIcon.Update(gameTime);
                    _notesIcon.Update(gameTime);

                    //Gestion du curseur par manette xbox360
                    if (GamePad.GetState(PlayerIndex.One).IsConnected)
                    {
#if !LINUX
                        Mouse.SetPosition(YnG.Mouse.X + (int)YnG.Gamepad.RightStickValue(PlayerIndex.One).X * 20, YnG.Mouse.Y - (int)YnG.Gamepad.RightStickValue(PlayerIndex.One).Y * 20);
#endif
                    }

                    if (MouseInRectangle(_menuIcon.Rectangle) && YnG.Mouse.JustClicked(MouseButton.Left) || GetTouchState()
                        || YnG.Keys.JustPressed(Keys.C) || YnG.Keys.JustPressed(Keys.NumPad0) || YnG.Gamepad.JustPressed(PlayerIndex.One, Buttons.Y))
                    {
                        // Click sur le menu
                        // TODO
                    }

                    // Images sur la scène
                    List<YnEntity> safeList = new List<YnEntity>(_itemsOnScreen);
                    foreach (YnEntity img in safeList)
                    {
                        img.Update(gameTime);
                    }

                    if (YnG.Mouse.MouseState.LeftButton == ButtonState.Pressed && !_dragging)
                    {
                        foreach (YnEntity img in _itemImages.Values)
                        {
                            if (MouseInRectangle(img.Rectangle))
                            {
                                _dragging = true;

                                _draggedImage = new YnEntity(img.AssetName);
                                _draggedImage.LoadContent();
                            }
                        }
                    }

                    if (YnG.Mouse.Released(MouseButton.Left) && _dragging)
                    {
                        _draggedImage.Position = new Vector2(YnG.Mouse.X - _draggedImage.Width / 2, YnG.Mouse.Y - _draggedImage.Height / 2);

                        if (YnG.Mouse.Released(MouseButton.Left))
                        {
                            // Drop
                            List<YnEntity> safeList2 = new List<YnEntity>(_itemsOnScreen);
                            foreach (YnEntity img in safeList2)
                            {
                                if (img.Rectangle.Intersects(_draggedImage.Rectangle))
                                {
                                    // Intersection
                                    DoDrop(_draggedImage, img);
                                }
                            }
                        }
                    }

                    if (_dragging)
                    {
                        _draggedImage.Position = new Vector2(YnG.Mouse.X - _draggedImage.Width / 2, YnG.Mouse.Y - _draggedImage.Height / 2);
                    }

                    if (YnG.Mouse.Released(MouseButton.Left))
                    {
                        _dragging = false;
                    }

                    // Déplacement au clavier, et manette Xbox360
                    if ((YnG.Keys.JustPressed(Keys.Z) || YnG.Keys.JustPressed(Keys.Up) || YnG.Gamepad.JustPressed(PlayerIndex.One, Buttons.LeftThumbstickUp))
                        && _sceneData.TopScene != String.Empty)
                    {
                        GoUp();
                    }
                    else if ((YnG.Keys.JustPressed(Keys.S) || YnG.Keys.JustPressed(Keys.Down) || YnG.Gamepad.JustPressed(PlayerIndex.One, Buttons.LeftThumbstickDown))
                        && _sceneData.BottomScene != String.Empty)
                    {
                        GoDown();
                    }
                    else if ((YnG.Keys.JustPressed(Keys.Q) || YnG.Keys.JustPressed(Keys.Left) || YnG.Gamepad.JustPressed(PlayerIndex.One, Buttons.LeftThumbstickLeft))
                        && _sceneData.LeftScene != String.Empty)
                    {
                        GoLeft();
                    }
                    else if ((YnG.Keys.JustPressed(Keys.D) || YnG.Keys.JustPressed(Keys.Right) || YnG.Gamepad.JustPressed(PlayerIndex.One, Buttons.LeftThumbstickRight))
                        && _sceneData.RightScene != String.Empty)
                    {
                        GoRight();
                    }
                    else if (YnG.Keys.JustPressed(Keys.Escape) || YnG.Gamepad.JustPressed(PlayerIndex.One, Buttons.Back))
                    {
                        YnG.Exit();
                    }

                    if (YnG.Mouse.JustClicked(MouseButton.Left) || GetTouchState()
                        || YnG.Keys.JustPressed(Keys.C) || YnG.Keys.JustPressed(Keys.NumPad0) || YnG.Gamepad.JustPressed(PlayerIndex.One, Buttons.Y))
                    {
                        // Click sur le bouton de menu
                        int mx = YnG.Mouse.X;
                        int my = YnG.Mouse.Y;
                        if (MouseInRectangle(_menuHitbox) || TouchInRectangle(_menuHitbox)
                            || YnG.Keys.JustPressed(Keys.C) || YnG.Keys.JustPressed(Keys.NumPad0) || YnG.Gamepad.JustPressed(PlayerIndex.One, Buttons.Y))
                        {
                            // Afficher ou masquer le menu
                            // 97 => hauteur du menu
                            // 25 => hauteur de la poignée
                            int delta = 97 - 25;
                            if (_menuIsShown)
                            {
                                // Déplacement de la hitbox vers le haut
                                _menuHitbox.Y += delta;

                                // Déplacement du background vers le haut
                                _menuBackground.Y += delta;
                            }
                            else
                            {
                                // Déplacement de la hitbox vers le bas
                                _menuHitbox.Y -= delta;

                                // Déplacement du background vers le bas
                                _menuBackground.Y -= delta;
                            }
                            _menuIsShown = !_menuIsShown;

                            _menuIcon.Visible = _menuIsShown;
                            _notesIcon.Visible = _menuIsShown;

                            // Un peu brutasse comme méthode...
                            // On cache tout
                            foreach (KeyValuePair<string, YnEntity> pair in _itemImages)
                                pair.Value.Visible = false;

                            // Et on ne raffiche que ce qui est dans l'inventaire du joueur
                            int itemPadding = 20;
                            int nbItems = _inventory.Count;
                            int x = YnG.Width / 2 - (56 * nbItems) / 2;
                            if (nbItems > 1)
                                x -= ((nbItems - 1) * itemPadding) / 2;

                            foreach (InventoryItem item in _inventory)
                            {
                                if (_menuIsShown)
                                {
                                    _itemImages [item.Code].Visible = true;

                                    // Replacement de l'élément, centré en bas, de gauche à droite
                                    Vector2 pos = new Vector2(x, YnG.Height - 63);
                                    _itemImages [item.Code].Position = pos;

                                    x += 56 + itemPadding;
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();

            // Interface
            _menuBackground.Draw(gameTime, spriteBatch);
            _notesIcon.Draw(gameTime, spriteBatch);
            _menuIcon.Draw(gameTime, spriteBatch);

            foreach (InventoryItem item in _inventory)
                _itemImages [item.Code].Draw(gameTime, spriteBatch);

            if (ImlostGame.Debug)
            {
                DrawRectangle(spriteBatch, _menuHitbox);
                DrawRectangle(spriteBatch, _leftSceneHitbox);
                DrawRectangle(spriteBatch, _topSceneHitbox);
                DrawRectangle(spriteBatch, _rightSceneHitbox);
                DrawRectangle(spriteBatch, _bottomSceneHitbox);
            }

            if (_deadlySceenOfDeath)
            {
                Texture2D tex = YnGraphics.CreateTexture(new Color(0, 0, 0, _deathAlpha), 1, 1);
                Rectangle rect = new Rectangle(0, 0, YnG.Width, YnG.Height);
                spriteBatch.Draw(tex, rect, Color.White);
            }

            // Images sur la scène
            List<YnEntity> safeList = new List<YnEntity>(_itemsOnScreen);
            foreach (YnEntity img in safeList)
            {
                img.Draw(gameTime, spriteBatch);
            }

            if (_dragging)
            {
                _draggedImage.Draw(gameTime, spriteBatch);
            }

            _narationText.Draw(gameTime, spriteBatch);

            // Par dessus tout, on dessine le splash
            if (_showSplash)
            {
                _splash.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }
    }

#if WINDOW_PHONE || NETFX_CORE
    public static class Console
    {
        public static string WriteLine(string s) { return ""; }
    }
#endif
}
