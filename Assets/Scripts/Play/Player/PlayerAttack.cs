using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInfor))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] List<Transform> cannonLeftTrans = new List<Transform>();
    [SerializeField] List<Transform> cannonRightTrans = new List<Transform>();
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject explosionPrefab;

    int attackDamage;

    private void Start() {
        attackDamage = GetComponent<PlayerInfor>().GetAttackDamage();
    }

    public void Shoot(ShootType shootType) {
        AudioManager.Instance.PlayAudio(AudioType.Shoot);
        if (shootType == ShootType.Left) {
            foreach (Transform t in cannonLeftTrans) {
                Vector3 dir = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * -Vector3.right;
                Bullet bullet = Instantiate(bulletPrefab, t.position, Quaternion.LookRotation(dir)).GetComponent<Bullet>();
                Instantiate(explosionPrefab, t.position, Quaternion.identity);
                bullet.Launch(dir, 10f, attackDamage);
            }
        }
        else {
            foreach (Transform t in cannonRightTrans) {
                Vector3 dir = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * Vector3.right;
                Bullet bullet = Instantiate(bulletPrefab, t.position, Quaternion.LookRotation(dir)).GetComponent<Bullet>();
                Instantiate(explosionPrefab, t.position, Quaternion.identity);
                bullet.Launch(dir, 10f, attackDamage);
            }
        }
    }
}
