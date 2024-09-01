using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationShip : MonoBehaviour
{
    Canvas canvas;
    [SerializeField] GameObject notificationPrefab;
    [SerializeField] LayerMask unexplorerOrWall;
    Vector3 dir;
    float speed;

    private void Start() {
        canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
    }

    private void FixedUpdate() {
        transform.position += speed * dir * Time.deltaTime;
    }

    public void Move(Vector3 dir, float speed) {
        this.dir = dir;
        this.speed = speed;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void OnTriggerEnter(Collider other) {
        if (((1 << other.gameObject.layer) & unexplorerOrWall) != 0) {
            FOVManager fovManager = other.GetComponent<FOVManager>();
            if (fovManager != null) {
                fovManager.EnableFOV();
            }
            if (other.CompareTag("Enemy")) {
                Notification.Show(canvas, notificationPrefab, "PHÁT HIỆN QUÁI VẬT");
            }
            else if (other.CompareTag("Island")) {
                Notification.Show(canvas, notificationPrefab, "PHÁT HIỆN ĐẢO");
            }
            Destroy(gameObject);
        }
    }
}
