using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfor : ObjectInfor 
{
    bool isAlive = true;
    private void Update() {
        if (isAlive && health <= 0) {
            GameManager.Instance.EndGame(false);
            isAlive = false;
        }
    }
}
