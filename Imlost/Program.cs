using System;
using Imlost.Source;

namespace Imlost
{
#if WINDOWS || XBOX || LINUX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            int size = args.Length;

            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                    PrepareParams(args[i]);
            }

            using (ImlostGame game = new ImlostGame())
            {
                game.Run();
            }
        }

        static void PrepareParams(string param)
        {
            string[] temp = param.Split(new char[] { '=' });
            string name = temp[0];
            string value = temp[1];

            switch (name)
            {
                case "width": GameConfiguration.ScreenWidth = int.Parse(value); break;
                case "height": GameConfiguration.ScreenHeight = int.Parse(value); break;
                case "fullscreen": GameConfiguration.EnabledFullScreen = bool.Parse(value); break;
                case "detect": GameConfiguration.DetermineBestResolution = bool.Parse(value); break;
                case "sound": GameConfiguration.EnabledSound = bool.Parse(value); break;
                case "speech": GameConfiguration.EnabledSpeechSynthesis = bool.Parse(value); break;
            }
        }
    }
#endif
}

