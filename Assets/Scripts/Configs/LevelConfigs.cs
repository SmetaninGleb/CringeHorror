using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfigs", menuName = "Configs/LevelConfigs")]
public class LevelConfigs : ScriptableObject
{
    [SerializeField] private int CollectableToFoundAfterAll = 1;
    [SerializeField] private int CollectableToSpawnAfterAll = 1;
    [SerializeField] private List<LevelConfig> _configList;

    public int GetCollectableToFound(int level)
    {
        foreach (LevelConfig config in _configList)
        {
            if (level == config.Level)
            {
                return config.CollectableToFound;
            }
        }
        return CollectableToFoundAfterAll;
    }

    public int GetCollectableToSpawn(int level)
    {
        foreach (LevelConfig config in _configList)
        {
            if (level == config.Level)
            {
                return config.CollectableToSpawn;
            }
        }
        return CollectableToSpawnAfterAll;
    }

    [Serializable]
    private class LevelConfig
    {
        public int Level;
        public int CollectableToFound;
        public int CollectableToSpawn;
    }
}
