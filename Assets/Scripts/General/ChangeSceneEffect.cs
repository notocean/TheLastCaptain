using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneEffect: MonoBehaviour
{
    [SerializeField] bool isOpen;
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
        if (isOpen)
            animator.SetTrigger("Open");
    }

    public void Destroy() {
        Destroy(gameObject);
    }

    public void LoadScene() {
        GameManager.Instance.LoadScene();
    }
}
