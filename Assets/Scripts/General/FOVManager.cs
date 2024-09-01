using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVManager : MonoBehaviour
{
    [SerializeField] GameObject fov;

    public void EnableFOV() {
        fov.SetActive(true);
        Destroy(gameObject.GetComponent<Collider>());
        Destroy(fov, 0.5f);
    }
}
