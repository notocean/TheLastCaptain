using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/PlayerDataScriptableObject", order = 1)]
public class PlayerDataScriptableObject : ScriptableObject
{
    public int ironResource;
    public int damageUpgradeLevel;
    public int healthUpgradeLevel;
    public int speedUpgradeLevel;
    public int ironResourceOfLevel;
    public bool isAudio;
    public bool isMusic;
    public bool isTutorial;
}