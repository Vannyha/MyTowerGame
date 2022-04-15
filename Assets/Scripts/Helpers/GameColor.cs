using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public static class ColorHelper
    {
        public static Dictionary<GameColor, Color> GameColorToRGB = new Dictionary<GameColor, Color>
        {
            {GameColor.None, Color.white},
            {GameColor.Red, Color.red},
            {GameColor.Green, Color.green},
            {GameColor.Blue, Color.blue},
        };
        
        public static Color GetRGBFromGameColor(GameColor color)
        {
            return GameColorToRGB[color];
        }
    }
    public enum GameColor
    {
        None = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
    }
}