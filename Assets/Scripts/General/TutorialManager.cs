using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] List<GameObject> tutorialPrefabs = new List<GameObject>();
    
    Transform canvasTrans;
    int tutorialIndex;
    int endTutorialIndex;
    GameObject currentTutorial;

    private void Start() {
        canvasTrans = GameObject.Find("MainCanvas").transform;
        OpenTutorial(null);
    }

    public void SetTutorialIndex(int index, int endIndex) {
        tutorialIndex = index;
        endTutorialIndex = endIndex;
    }

    private void OpenTutorial(CustomButton cusBtn) {
        if (currentTutorial != null)
            Destroy(currentTutorial);

        if (tutorialIndex <= endTutorialIndex) {
            GameObject obj = Instantiate(tutorialPrefabs[tutorialIndex], canvasTrans);
            CustomButton customButton = obj.GetComponentInChildren<CustomButton>();
            customButton.clickedEvent.AddListener(OpenTutorial);

            switch (tutorialIndex) {
                case 0:
                    GameObject battleViewBtn = GameObject.Find("BattleViewBtn");
                    PageButton pageButton1 = battleViewBtn.GetComponent<PageButton>();
                    pageButton1.Clicked();
                    break;
                case 1:
                    GameObject settingBtn = GameObject.Find("SettingBtn");
                    ToggleButton toggleButton = settingBtn.GetComponent<ToggleButton>();
                    if (!toggleButton.isOn)
                        toggleButton.Clicked();
                    break;
                case 7:
                    GameObject upgradeViewBtn = GameObject.Find("UpgradeViewBtn");
                    PageButton pageButton2 = upgradeViewBtn.GetComponent<PageButton>();
                    pageButton2.Clicked();
                    break;
                default: break;
            }

            tutorialIndex++;
            currentTutorial = obj;
        }
    }
}
