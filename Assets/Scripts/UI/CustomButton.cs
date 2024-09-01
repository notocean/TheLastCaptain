using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomButton : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float timeClickEffect = 0.2f;

    private float currentScale = 1f;
    protected bool isClickedEffect = false;
    [HideInInspector] public bool canClicked = true;
    [HideInInspector] public UnityEvent<CustomButton> clickedEvent = new UnityEvent<CustomButton>();

    public virtual void Clicked() {
        if (!isClickedEffect && canClicked) {
            AudioManager.Instance.PlayAudio(AudioType.ClickBtn);
            StartCoroutine(ClickedCoroutine());
            clickedEvent.Invoke(this);
        }
    }

    IEnumerator ClickedCoroutine() {
        isClickedEffect = true;
        RectTransform rectTransform = GetComponent<RectTransform>();
        float timer = 0f;

        while (timer <= 1) {
            currentScale = curve.Evaluate(timer);
            rectTransform.localScale = new Vector3(currentScale, currentScale, currentScale);
            timer += Time.unscaledDeltaTime / timeClickEffect;
            yield return null;
        }

        currentScale = curve.Evaluate(1);
        rectTransform.localScale = new Vector3(currentScale, currentScale, currentScale);
        isClickedEffect = false;
    }
}
