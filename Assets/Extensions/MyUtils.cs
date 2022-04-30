using UnityEditor;
using UnityEngine;

namespace Extensions
{
    public static class MyUtils
    {
        [MenuItem ("Tools/Clear PlayerPrefs")]
        static void ClearPlayerPrefs() {
            PlayerPrefs.DeleteAll();
        }
    }
}