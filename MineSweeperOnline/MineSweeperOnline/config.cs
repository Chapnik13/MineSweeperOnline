using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeperOnline
{
    public static class config
    {
        private static Properties.Settings sett = new Properties.Settings();
        public static int WIDTH = 750;
        public static int HEIGHT = 500;

        public static int BUTTON_WIDTH = 200;
        public static int BUTTON_HEIGHT = 50;

        public static int SLIDER_WIDTH = 200;
        public static int SLIDER_HEIGHT = 50;

        public static int BOARD = sett.Size;
        public static int BOARD_BOMBS = sett.Bombs;

        public static bool FULLSCREEN = false;

        public static void refresh()
        {
            Properties.Settings sett = new Properties.Settings();
            BOARD = sett.Size;
            BOARD_BOMBS = sett.Bombs;
        }
    }
}
