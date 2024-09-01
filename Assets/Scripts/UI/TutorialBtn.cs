using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBtn : CustomButton
{
    public override void Clicked() {
        base.Clicked();
        GameManager.Instance.OpenTutorial();
    }
}
