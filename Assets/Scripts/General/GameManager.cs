using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] PlayerDataScriptableObject _playerDataSO;
    [SerializeField] MapMaterialScriptableObject _mapMaterialSO;
    [SerializeField] LevelUpgradeScriptableObject _levelUpgradeSO;
    [SerializeField] GameObject endgamePrefab;
    [SerializeField] GameObject changeScenePrefab;
    [SerializeField] int maxLevel;
    [SerializeField] GameObject tutorialPrefab;

    GameObject player;
    int sceneIndex = 0;
    int nextSceneIndex = 1;
    const string playerTagStr = "Player";

    private void Awake() {
        if (Instance == null) {
            Instance = FindObjectOfType<GameManager>();
            if (Instance == null ) {
                GameObject gameManager = new GameObject("Game Manager");
                Instance = gameManager.AddComponent<GameManager>();
            }
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        SaveData();
        LoadData();
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    public void LoadData() {
        PlayerData playerData = SaveLoadController.LoadData();

        for (int i = 0; i < playerData.originalMapMaterialIndexes.Count; i++) {
            if (playerData.originalMapMaterialIndexes[i] == 1) {
                _mapMaterialSO.originalMapMaterials[i] = _mapMaterialSO.originalMapMaterial;
            }
            else if (playerData.originalMapMaterialIndexes[i] == 2) {
                _mapMaterialSO.originalMapMaterials[i] = _mapMaterialSO.completedMapMaterial;
            }
            else {
                _mapMaterialSO.originalMapMaterials[i] = _mapMaterialSO.hiddenMapMaterial;
            }
        }
        _playerDataSO.ironResource = playerData.ironResource;
        _playerDataSO.damageUpgradeLevel = playerData.damageUpgradeLevel;
        _playerDataSO.healthUpgradeLevel = playerData.healthUpgradeLevel;
        _playerDataSO.speedUpgradeLevel = playerData.speedUpgradeLevel;
        _playerDataSO.ironResourceOfLevel = playerData.ironResourceOfLevel;
        _playerDataSO.isAudio = playerData.isAudio;
        _playerDataSO.isMusic = playerData.isMusic;
        _playerDataSO.isTutorial = playerData.isTutorial;
    }

    public void SaveData() {
        List<int> originalMapMaterialIndexes = new List<int>();
        for (int i = 0; i < _mapMaterialSO.originalMapMaterials.Count; i++) {
            if (_mapMaterialSO.originalMapMaterials[i] == _mapMaterialSO.originalMapMaterial) {
                originalMapMaterialIndexes.Add(1);
            }
            else if (_mapMaterialSO.originalMapMaterials[i] == _mapMaterialSO.completedMapMaterial) {
                originalMapMaterialIndexes.Add(2);
            }
            else {
                originalMapMaterialIndexes.Add(3);
            }
        }
        int ironResource = _playerDataSO.ironResource;
        int damageUpgradeLevel = _playerDataSO.damageUpgradeLevel;
        int healthUpgradeLevel = _playerDataSO.healthUpgradeLevel;
        int speedUpgradeLevel = _playerDataSO.speedUpgradeLevel;
        int ironResourceOfLevel = _playerDataSO.ironResourceOfLevel;
        bool isAudio = _playerDataSO.isAudio;
        bool isMusic = _playerDataSO.isMusic;
        bool isTutorial = _playerDataSO.isTutorial;

        PlayerData playerData = new PlayerData(originalMapMaterialIndexes, ironResource, damageUpgradeLevel, healthUpgradeLevel, speedUpgradeLevel, ironResourceOfLevel, isAudio, isMusic, isTutorial);
        SaveLoadController.SaveData(playerData);
    }

    public void SceneChangeEffect() {
        Time.timeScale = 1;
        GameObject mainCanvas = GameObject.Find("MainCanvas");
        if (mainCanvas != null) {
            Instantiate(changeScenePrefab, mainCanvas.transform);
        }
    }

    public void LoadScene() {
        SceneManager.LoadScene(nextSceneIndex);
        sceneIndex = nextSceneIndex;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
        player = GameObject.FindGameObjectWithTag(playerTagStr);
        if (player != null) {
            int attackDamage = _levelUpgradeSO.basicAttack + _playerDataSO.damageUpgradeLevel * _levelUpgradeSO.attackAddedPerLevel;
            int maxHealth = _levelUpgradeSO.basicHealth + _playerDataSO.healthUpgradeLevel * _levelUpgradeSO.healthAddedPerLevel;
            float speed = _levelUpgradeSO.basicSpeed + _playerDataSO.speedUpgradeLevel * _levelUpgradeSO.speedAddedPerLevel;
            player.GetComponent<PlayerInfor>().SetInfor(attackDamage, maxHealth, speed);
        }

        if (_playerDataSO.isTutorial)
            OpenTutorial();

        if (sceneIndex != 0) {
            TMP_Text ironResource = GameObject.Find("IronResource").GetComponent<TMP_Text>();
            ironResource.text = _playerDataSO.ironResource.ToString();

            _playerDataSO.isTutorial = false;
        }
    }

    public void EndGame(bool isWin) {
        Time.timeScale = 0;
        GameObject mainCanvas = GameObject.Find("MainCanvas");
        GameObject endgameObj = Instantiate(endgamePrefab, mainCanvas.transform);
        TMP_Text text = endgameObj.GetComponentInChildren<TMP_Text>();
        if (isWin) {
            CompletedMap();
            text.text = "CHIẾN THẮNG";
        }
        else text.text = "THẤT BẠI";
        SaveData();
    }

    public void SetNextSceneIndex(int index) {
        nextSceneIndex = Mathf.Clamp(index, 0, maxLevel);
    }

    public GameObject GetPlayerObj() {
        return player;
    }

    public int GetIronResource() {
        return _playerDataSO.ironResource;
    }

    public void ChangeIronResource(int amount) {
        _playerDataSO.ironResource += amount;
        if (_playerDataSO.ironResource < 0)
            _playerDataSO.ironResource = 0;
    }

    public void CompletedMap() {
        _mapMaterialSO.originalMapMaterials[sceneIndex - 1] = _mapMaterialSO.completedMapMaterial;
    }

    public void OpenTutorial() {
        TutorialManager tutorialManager = Instantiate(tutorialPrefab).GetComponent<TutorialManager>();
        if (sceneIndex == 0)
            tutorialManager.SetTutorialIndex(0, 9);
        else tutorialManager.SetTutorialIndex(10, 16);
    }
}
