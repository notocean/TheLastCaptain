using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneBtn : CustomButton
{
    [SerializeField] bool isHomeBtn = false;
    public override void Clicked() {
        if (isHomeBtn)
            GameManager.Instance.SetNextSceneIndex(0);
        base.Clicked();
        if (canClicked)
            GameManager.Instance.SceneChangeEffect();
    }
}
