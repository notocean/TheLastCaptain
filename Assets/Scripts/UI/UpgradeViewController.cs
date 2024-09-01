using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeViewController : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject notificationPrefab;
    [SerializeField] PlayerDataScriptableObject _playerDataSO;
    [SerializeField] TMP_Text ironResourceText;
    [SerializeField] GameObject upgradePrefab;

    [Header("Damage Upgrade")]
    [SerializeField] Transform damageResource;
    [SerializeField] TMP_Text damageIronResourceText;
    [SerializeField] Transform damageUpgradeBar;
    [SerializeField] CustomButton damageUpgradeButton;

    [Header("Health Upgrade")]
    [SerializeField] Transform healthResource;
    [SerializeField] TMP_Text healthIronResourceText;
    [SerializeField] Transform healthUpgradeBar;
    [SerializeField] CustomButton healthUpgradeButton;

    [Header("Speed Upgrade")]
    [SerializeField] Transform speedResource;
    [SerializeField] TMP_Text speedIronResourceText;
    [SerializeField] Transform speedUpgradeBar;
    [SerializeField] CustomButton speedUpgradeButton;

    int ironResource;
    int damageUpgradeLevel;
    int healthUpgradeLevel;
    int speedUpgradeLevel;
    int ironResourceOfLevel;

    const string maxStr = "TỐI ĐA";

    private void Start() {
        LoadUpgradeView();

        damageUpgradeButton.clickedEvent.AddListener(UpgradeDamage);
        healthUpgradeButton.clickedEvent.AddListener(UpgradeHealth);
        speedUpgradeButton.clickedEvent.AddListener(UpgradeSpeed);
    }

    private void LoadUpgradeView() {
        ironResourceOfLevel = _playerDataSO.ironResourceOfLevel;
        ironResource = _playerDataSO.ironResource;
        ironResourceText.text = ironResource.ToString();

        LoadDamageUpgrade();
        LoadHealthUpgrade();
        LoadSpeedUpgrade();
    }

    private void LoadDamageUpgrade() {
        damageUpgradeLevel = _playerDataSO.damageUpgradeLevel;
        foreach (Transform child in damageUpgradeBar) {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < damageUpgradeLevel; i++) {
            Instantiate(upgradePrefab, damageUpgradeBar);
        }
        damageIronResourceText.text = (ironResourceOfLevel * (damageUpgradeLevel + 1)).ToString();

        if (damageUpgradeLevel >= 10) {
            Destroy(damageResource.gameObject);
            damageUpgradeButton.GetComponentInChildren<TMP_Text>().text = maxStr;
            damageUpgradeButton.GetComponent<Button>().transition = Selectable.Transition.None;
            damageUpgradeButton.canClicked = false;
        }
    }

    private void LoadHealthUpgrade() {
        healthUpgradeLevel = _playerDataSO.healthUpgradeLevel;
        foreach (Transform child in healthUpgradeBar) {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < healthUpgradeLevel; i++) {
            Instantiate(upgradePrefab, healthUpgradeBar);
        }
        healthIronResourceText.text = (ironResourceOfLevel * (healthUpgradeLevel + 1)).ToString();

        if (healthUpgradeLevel >= 10) {
            Destroy(healthResource.gameObject);
            healthUpgradeButton.GetComponentInChildren<TMP_Text>().text = maxStr;
            healthUpgradeButton.GetComponent<Button>().transition = Selectable.Transition.None;
            healthUpgradeButton.canClicked = false;
        }
    }

    private void LoadSpeedUpgrade() {
        speedUpgradeLevel = _playerDataSO.speedUpgradeLevel;
        foreach (Transform child in speedUpgradeBar) {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < speedUpgradeLevel; i++) {
            Instantiate(upgradePrefab, speedUpgradeBar);
        }
        speedIronResourceText.text = (ironResourceOfLevel * (speedUpgradeLevel + 1)).ToString();

        if (speedUpgradeLevel >= 10) {
            Destroy(speedResource.gameObject);
            speedUpgradeButton.GetComponentInChildren<TMP_Text>().text = maxStr;
            speedUpgradeButton.GetComponent<Button>().transition = Selectable.Transition.None;
            speedUpgradeButton.canClicked = false;
        }
    }

    private void UpgradeDamage(CustomButton customButton) {
        if (ironResource >= (ironResourceOfLevel * (damageUpgradeLevel + 1))) {
            ironResource -= (ironResourceOfLevel * (damageUpgradeLevel + 1));
            damageUpgradeLevel++;
            SaveUpgradeDataSO();
            LoadDamageUpgrade();
            GameManager.Instance.SaveData();
        }
        else {
            Notification.Show(canvas, notificationPrefab, "TÀI NGUYÊN KHÔNG ĐỦ");
        }
    }

    private void UpgradeHealth(CustomButton customButton) {
        if (ironResource >= (ironResourceOfLevel * (healthUpgradeLevel + 1))) {
            ironResource -= (ironResourceOfLevel * (healthUpgradeLevel + 1));
            healthUpgradeLevel++;
            SaveUpgradeDataSO();
            LoadHealthUpgrade();
            GameManager.Instance.SaveData();
        }
        else {
            Notification.Show(canvas, notificationPrefab, "TÀI NGUYÊN KHÔNG ĐỦ");
        }
    }

    private void UpgradeSpeed(CustomButton customButton) {
        if (ironResource >= (ironResourceOfLevel * (speedUpgradeLevel + 1))) {
            ironResource -= (ironResourceOfLevel * (speedUpgradeLevel + 1));
            speedUpgradeLevel++;
            SaveUpgradeDataSO();
            LoadSpeedUpgrade();
            GameManager.Instance.SaveData();
        }
        else {
            Notification.Show(canvas, notificationPrefab, "TÀI NGUYÊN KHÔNG ĐỦ");
        }
    }

    private void SaveUpgradeDataSO() {
        _playerDataSO.ironResource = ironResource;
        _playerDataSO.damageUpgradeLevel = damageUpgradeLevel;
        _playerDataSO.healthUpgradeLevel = healthUpgradeLevel;
        _playerDataSO.speedUpgradeLevel = speedUpgradeLevel;
        ironResourceText.text = ironResource.ToString();
    }
}
