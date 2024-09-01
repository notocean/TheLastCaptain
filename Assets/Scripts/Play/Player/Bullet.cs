using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] GameObject explosionPrefab;
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
        if (other.CompareTag("Enemy")) {
            EnemyInfor enemyInfor = other.GetComponentInParent<EnemyInfor>();
            enemyInfor.TakeDamage(damage);
            GameObject explosionObj = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosionObj.transform.localScale *= 4;
            AudioManager.Instance.PlayAudio(AudioType.Explosion);
            Destroy(gameObject);
        }
    }
}
