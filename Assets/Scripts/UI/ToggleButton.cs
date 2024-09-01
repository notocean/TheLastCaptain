using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : CustomButton 
{
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;
    [SerializeField] Image image;

    [HideInInspector] public bool isOn;

    private void Awake() {
        isOn = false;
    }

    public override void Clicked() {
        if (!isClickedEffect && canClicked) {
            SetOnOff(!isOn);
        }
        base.Clicked();
    }

    public void SetOnOff(bool isOn) {
        this.isOn = isOn;
        image.sprite = isOn ? onSprite : offSprite;
    }
}
