using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBullet : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    Vector3 startPos;
    Vector3 dir;
    float maxDistance;
    int damage;

    private void Update() {
        if (Vector3.Distance(transform.position, startPos) < maxDistance) {
            transform.position += speed * dir * Time.deltaTime;
        }
        else Destroy(gameObject);
    }

    public void Launch(Vector3 dir, float maxDistance, int damage) {
        startPos = transform.position;
        this.dir = dir;
        this.maxDistance = maxDistance;
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            PlayerInfor playerInfor = other.GetComponent<PlayerInfor>();
            playerInfor.TakeDamage(damage);
            AudioManager.Instance.PlayAudio(AudioType.Explosion);
        }
    }
}
