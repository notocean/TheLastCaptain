using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFolow : MonoBehaviour
{
    Transform target;
    private Vector3 target2This;

    private void Start() {
        //target = GameManager.Instance.player.transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        target2This = transform.position - target.position;
    }

    private void Update() {
        transform.position = target.position + target2This;
        transform.rotation = Quaternion.LookRotation(-target2This);
    }
}
