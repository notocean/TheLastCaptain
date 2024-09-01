using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookToCamera : MonoBehaviour
{
    private void Start() {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void LateUpdate() {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
