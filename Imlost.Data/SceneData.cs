using System;
using System.Collections.Generic;

namespace Imlost.Data
{
    [Serializable]
    public class SceneData
    {
        private string _background;
        private string _code;
        private List<SceneObject>  _objects;
        private string _leftScene;
        private string _topScene;
        private string _rightScene;
        private string _bottomScene;
        private int _ambienceZone;
        private string _message;

        public SceneData() 
        {
            _background = String.Empty;
            _code = String.Empty;
            _objects = new List<SceneObject>(0);
            _leftScene = String.Empty;
            _topScene = String.Empty;
            _rightScene = String.Empty;
            _bottomScene = String.Empty;
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public int AmbienceZone
        {
            get { return _ambienceZone; }
            set { _ambienceZone = value; }
        }

        /// <summary>
        /// Image de fond
        /// </summary>
        public string Background
        {
            get { return _background; }
            set { _background = value; }
        }

        /// <summary>
        /// Code de la scene
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        /// Collection des objets de la scece
        /// </summary>
        public List<SceneObject> Objects
        {
            get { return _objects; }
            set { _objects = value; }
        }

        /// <summary>
        /// Lien vers la scène à gauche
        /// </summary>
        public string LeftScene
        {
            get { return _leftScene; }
            set { _leftScene = value; }
        }

        /// <summary>
        /// Lien vers la scene en haut
        /// </summary>
        public string TopScene
        {
            get { return _topScene; }
            set { _topScene = value; }
        }

        /// <summary>
        /// Lien vers la scene à droite
        /// </summary>
        public string RightScene
        {
            get { return _rightScene; }
            set { _rightScene = value; }
        }

        /// <summary>
        /// Lien vers la scene en bas
        /// </summary>
        public string BottomScene
        {
            get { return _bottomScene; }
            set { _bottomScene = value; }
        }
    }
}
