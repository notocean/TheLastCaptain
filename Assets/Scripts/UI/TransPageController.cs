using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransPageController : MonoBehaviour
{
    [SerializeField] List<GameObject> canvas;
    [SerializeField] List<GameObject> pageUI;
    [SerializeField] RectTransform sliderRect;
    [SerializeField] AnimationCurve scaleSliderCurve;

    [SerializeField] float timeTransition = 0.5f;
    bool isCoroutineRunning = false;
    int pageIndex = 1;

    private void Start() {
        for (int i = 0; i < pageUI.Count; i++) {
            pageUI[i].GetComponent<PageButton>().clickedEvent.AddListener(TransitionPage);
        }
    }

    public void TransitionPage(CustomButton customButton) {
        int newPageIndex = ((PageButton)customButton).pageIndex;
        if (!isCoroutineRunning) {
            StartCoroutine(TransPageCoroutine(newPageIndex));
        }
    }

    IEnumerator TransPageCoroutine(int newPageIndex) {
        if (pageIndex != newPageIndex) {
            isCoroutineRunning = true;
            float timer = 0f;
            float startSliderPos = pageUI[pageIndex].GetComponent<RectTransform>().anchoredPosition.x;
            float endSliderPos = pageUI[newPageIndex].GetComponent<RectTransform>().anchoredPosition.x;
            float startCameraPos = canvas[pageIndex].transform.position.x;
            float endCameraPos = canvas[newPageIndex].transform.position.x;
            Vector3 cameraPos = Camera.main.transform.position;

            while (timer <= 1) {
                float xSlider = Mathf.Lerp(startSliderPos, endSliderPos, timer);
                sliderRect.anchoredPosition = new Vector2(xSlider, sliderRect.anchoredPosition.y);

                float scaleX = scaleSliderCurve.Evaluate(timer);
                sliderRect.localScale = new Vector3(scaleX, 1, 1);

                float xCamera = Mathf.Lerp(startCameraPos, endCameraPos, timer);
                Camera.main.transform.position = new Vector3(xCamera, cameraPos.y, cameraPos.z);

                timer += Time.unscaledDeltaTime / timeTransition;

                yield return null;
            }


            sliderRect.anchoredPosition = new Vector2(endSliderPos, sliderRect.anchoredPosition.y);
            Camera.main.transform.position = new Vector3(endCameraPos, cameraPos.y, cameraPos.z);
            pageIndex = newPageIndex;
            isCoroutineRunning = false;
        }
    }
}
