using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollapsibleUIController : MonoBehaviour
{
    [SerializeField] CustomButton showUIButton;
    [SerializeField] List<CustomButton> hideUIButton;
    [SerializeField] List<GameObject> UIObject;
    [SerializeField] AnimationCurve scaleCurve;
    [SerializeField] float timeGapShow = 0.1f;

    bool isCoroutineRunning = false;
    float currentScale = 1f;
    bool isOpen = false;

    private void Start() {
        showUIButton.clickedEvent.AddListener(ShowUI);
        foreach (CustomButton customButton in hideUIButton) {
            customButton.clickedEvent.AddListener(HideUI);
        }
    }

    private void ShowUI(CustomButton customButton) {
        if (!isOpen && !isCoroutineRunning) {
            isOpen = true;
            StartCoroutine(ShowUICoroutine(customButton));
        }
    }

    private void HideUI(CustomButton customButton) {
        if (isOpen && !isCoroutineRunning) {
            isOpen = false;
            StartCoroutine(ShowUICoroutine(customButton));
        }
    }

    IEnumerator ShowUICoroutine(CustomButton customButton) {
        isCoroutineRunning = true;
        customButton.canClicked = false;
        float timer;
        int uiIndex, directionTime;
        if (isOpen) {
            timer = 0;
            uiIndex = 0;
            directionTime = 1;
        }
        else {
            timer = UIObject.Count * timeGapShow;
            uiIndex = UIObject.Count - 1;
            directionTime = -1;
            foreach (GameObject gObj in UIObject) {
                Button button = gObj.GetComponent<Button>();
                if (button != null)
                    button.interactable = false;
            }
        }

        while (timer >= 0 && timer <= UIObject.Count * timeGapShow) {
            currentScale = scaleCurve.Evaluate(timer / timeGapShow - uiIndex);
            UIObject[uiIndex].GetComponent<RectTransform>().localScale = new Vector3(currentScale, currentScale, currentScale);
            timer += directionTime * Time.unscaledDeltaTime / timeGapShow;
            if (isOpen) {
                if (timer > (uiIndex + 1) * timeGapShow) {
                    currentScale = scaleCurve.Evaluate(1);
                    UIObject[uiIndex].GetComponent<RectTransform>().localScale = new Vector3(currentScale, currentScale, currentScale);
                    uiIndex++;
                }
            }
            else {
                if (timer < uiIndex * timeGapShow) {
                    currentScale = scaleCurve.Evaluate(0);
                    UIObject[uiIndex].GetComponent<RectTransform>().localScale = new Vector3(currentScale, currentScale, currentScale);
                    uiIndex--;
                }
            }

            yield return null;
        }

        if (isOpen) {
            foreach (GameObject gObj in UIObject) {
                Button button = gObj.GetComponent<Button>();
                if (button != null)
                    button.interactable = true;
            }
        }

        customButton.canClicked = true;
        isCoroutineRunning = false;
    }
}
