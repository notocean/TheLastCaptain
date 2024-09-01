using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    protected override void Start() {
        base.Start();
        input = Vector2.up;
        handle.anchoredPosition = Vector2.up * (background.sizeDelta / 2) * handleRange;
    }

    public override void OnDrag(PointerEventData eventData) {
        base.OnDrag(eventData);
        input = input.normalized;
        handle.anchoredPosition = input * (background.sizeDelta / 2) * handleRange;
    }

    public override void OnPointerUp(PointerEventData eventData) {

    }
}
