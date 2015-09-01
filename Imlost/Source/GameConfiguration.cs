using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imlost.Source
{
    public class GameConfiguration
    {
        // Audio
        public static bool EnabledSound = true;
        public static bool EnabledMusic = true;
        public static bool EnabledGamepad = true;
        public static bool EnabledSpeechSynthesis = false;

        // Affichage
        public static int ReferenceWidth = 1280;
        public static int ReferenceHeight = 720;
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;
        public static bool EnabledFullScreen = false;
        public static bool DetermineBestResolution = false;
        public static string GameTitle = "A.M. Lost";
    }
}
