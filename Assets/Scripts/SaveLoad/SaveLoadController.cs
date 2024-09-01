using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveLoadController
{
    static string saveFilePath = Application.persistentDataPath + "/PlayerData.json";
    static string savePlayerData;

    public static void SaveData(PlayerData playerData) {
        savePlayerData = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, savePlayerData);
    }

    public static PlayerData LoadData() {
        savePlayerData = File.ReadAllText(saveFilePath);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(savePlayerData);
        return playerData;
    }
}
