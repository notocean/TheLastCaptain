using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectIronBtn : CustomButton
{
    [SerializeField] int ironResource;

    public override void Clicked() {
        base.Clicked();
        GameManager.Instance.ChangeIronResource(ironResource);
        TMP_Text ironResourceText = GameObject.Find("IronResource").GetComponent<TMP_Text>();
        ironResourceText.text = GameManager.Instance.GetIronResource().ToString();

        Destroy(gameObject);
    }
}
