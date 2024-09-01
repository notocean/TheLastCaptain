using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AddShipBtn : CustomButton
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject notificationPrefab;
    [SerializeField] int shipPrice;
    [SerializeField] GameObject shipPrefab;
    [SerializeField] float speed;
    FixedJoystick fixedJoystick;

    private void Start() {
        fixedJoystick = GameObject.Find("FixedJoystick").GetComponent<FixedJoystick>();
    }

    public override void Clicked() {
        if (GameManager.Instance.GetIronResource() > shipPrice) {
            base.Clicked();
            GameManager.Instance.ChangeIronResource(-shipPrice);
            Vector3 startPos = GameManager.Instance.GetPlayerObj().transform.position + 1.5f * fixedJoystick.Direction;
            ExplorationShip explorationShip = Instantiate(shipPrefab, startPos, Quaternion.identity).GetComponent<ExplorationShip>();
            explorationShip.Move(new Vector3(fixedJoystick.Direction.x, 0, fixedJoystick.Direction.z), speed);

            TMP_Text ironResource = GameObject.Find("IronResource").GetComponent<TMP_Text>();
            ironResource.text = GameManager.Instance.GetIronResource().ToString();
        }
        else {
            Notification.Show(canvas, notificationPrefab, "TÀI NGUYÊN KHÔNG ĐỦ");
        }
    }
}
