using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyAfterTime : MonoBehaviour
{
    [SerializeField] float time;

    private void Start() {
        Destroy(gameObject, time);
    }
}
