using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/LevelUpgradeScriptableObject", order = 1)]
public class LevelUpgradeScriptableObject : ScriptableObject
{
    public int basicAttack;
    public int basicHealth;
    public float basicSpeed;

    public int attackAddedPerLevel;
    public int healthAddedPerLevel;
    public float speedAddedPerLevel;
}