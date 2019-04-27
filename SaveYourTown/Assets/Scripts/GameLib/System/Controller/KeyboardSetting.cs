using UnityEngine;
using System;

namespace GameLib.System.Controller
{
    [CreateAssetMenu(fileName = "Data", menuName = "GameLib/KeyboardSetting", order = 1)]
    [Serializable]
    public class KeyboardSetting : ScriptableObject
    {
        public KeyCode jumpKey;
        public KeyCode attackKey;
        public KeyCode actionKeyOne;
        public KeyCode actionKeyTwo;
        public KeyCode actionKeyThree;
        public KeyCode upKey;
        public KeyCode downKey;
        public KeyCode rightKey;
        public KeyCode leftKey;
    }
}