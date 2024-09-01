using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public List<int> originalMapMaterialIndexes;
    public int ironResource;
    public int damageUpgradeLevel;
    public int healthUpgradeLevel;
    public int speedUpgradeLevel;
    public int ironResourceOfLevel;
    public bool isAudio;
    public bool isMusic;
    public bool isTutorial;

    public PlayerData() { }

    public PlayerData(List<int> originalMapMaterialIndexes, int ironResource, int damageUpgradeLevel, int healthUpgradeLevel, int speedUpgradeLevel, int ironResourceOfLevel, bool isAudio, bool isMusic, bool isTutorial) {
        this.originalMapMaterialIndexes = originalMapMaterialIndexes;
        this.ironResource = ironResource;
        this.damageUpgradeLevel = damageUpgradeLevel;
        this.healthUpgradeLevel = healthUpgradeLevel;
        this.speedUpgradeLevel = speedUpgradeLevel;
        this.ironResourceOfLevel = ironResourceOfLevel;
        this.isAudio = isAudio;
        this.isMusic = isMusic;
        this.isTutorial = isTutorial;
    } 
}
