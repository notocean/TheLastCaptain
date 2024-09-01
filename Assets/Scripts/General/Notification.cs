using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class Notification
{
    public static void Show(Canvas canvas, GameObject notificationPrefab, string text) {
        GameObject gObj = GameObject.Instantiate(notificationPrefab, canvas.transform);
        TMP_Text _text = gObj.GetComponentInChildren<TMP_Text>();
        _text.text = text;
    }
}
