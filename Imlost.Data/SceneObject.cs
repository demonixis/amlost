using System;

namespace Imlost.Data
{
    [Serializable]
    public class Rectangle4
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle4()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
        }

        public Rectangle4(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;                
        }
    }

    [Serializable]
    public class SceneObject
    {
        private Rectangle4 _rectangle;
        private string _name;
        private int _actionID;
        private string _assetName;
        private string _soundName;

        public SceneObject() 
        {
            _rectangle = new Rectangle4();
            _name = "SceneObject";
        }

        public string SoundName
        {
            get { return _soundName; }
            set { _soundName = value; }
        }

        public string AssetName
        {
            get { return _assetName; }
            set { _assetName = value; }
        }

        public int ActionID
        {
            get { return _actionID; }
            set { _actionID = value; }
        }

        /// <summary>
        /// Position de l'objet sur la scène : coordonnées relatives au viewport!
        /// </summary>
        public Rectangle4 Rectangle 
        { 
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        /// <summary>
        /// Position du centre de l'objet (utile pour poser le point de vision)
        /// </summary>
        public int X
        {
            get { return _rectangle.X; }
            set { _rectangle.X = value; }
        }

        public int Y
        {
            get { return _rectangle.Y; }
            set { _rectangle.Y = value; }
        }

        public int Width
        {
            get { return _rectangle.Width; }
            set { _rectangle.Width = value; }
        }

        public int Height
        {
            get { return _rectangle.Height; }
            set { _rectangle.Height = value; }
        }

        /// <summary>
        /// Nom de l'objet
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
