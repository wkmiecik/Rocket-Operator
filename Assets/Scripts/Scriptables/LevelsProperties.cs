using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Levels", menuName = "Scriptable Objects/Levels")]
public class LevelsProperties : ScriptableObject
{
    public Properties[] levels;

    [Serializable]
    public class Properties
    {
        public int goldTimeLimit;
        public Sprite icon;
    }
}
