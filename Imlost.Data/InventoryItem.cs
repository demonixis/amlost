using System;

namespace Imlost.Data
{
    [Serializable]
    public class InventoryItem
    {
        private string _code;
        private string _name;
        private string _assetName;
        private string _pickSound;
        private string _useSound;


        public string PickSound
        {
            get { return _pickSound; }
            set { _pickSound = value; }
        }

        public string UseSound
        {
            get { return _useSound; }
            set { _useSound = value; }
        }

        /// <summary>
        /// Code unique de l'objet
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        /// Nom de l'objet (libellé affiché)
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Nom de l'asset représentant l'objet
        /// </summary>
        public string AssetName
        {
            get { return _assetName; }
            set { _assetName = value; }
        }
    }
}
