using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField] float xSensitivity = 1.0f;
    [SerializeField] float ySensitivity = 1.0f;
    float xRotation = 0f;
    float yRotation = 0f;
    float zRotation = 0f;

    float startTimeClick = 0f;

    [SerializeField] MapMaterialScriptableObject _mapSO;
    [SerializeField] List<MeshRenderer> mapMeshRenderers = new List<MeshRenderer>();
    [SerializeField] CustomButton startButton;
    [SerializeField] TMP_Text startText;
    GameObject selectedMapObj;
    const string lockStr = "KHÓA";
    const string startStr = "BẮT ĐẦU";

    private void Start() {
        zRotation = transform.localRotation.eulerAngles.z;
        LoadMapData();
    }

    private void Update() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
                RotateMap(touch);

            switch (touch.phase) {
                case TouchPhase.Began:
                    startTimeClick = Time.time;
                    break;
                case TouchPhase.Ended:
                    if (Time.time - startTimeClick <= 0.15f) {
                        SelectMap(touch);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    // Rotate world map by finger
    private void RotateMap(Touch touch) {
        xRotation = Mathf.Repeat(xRotation + xSensitivity * touch.deltaPosition.x, 360);
        yRotation = Mathf.Clamp(yRotation + ySensitivity * touch.deltaPosition.y, -60, 60);
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
    }

    // Select world map to start
    private void SelectMap(Touch touch) {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f)) {
            if (hit.collider != null) {
                GameObject mapObj = hit.collider.gameObject;
                if (mapObj != null) {
                    AudioManager.Instance.PlayAudio(AudioType.ClickMap);
                    SetSelectedMapMaterial();
                    Material targetMaterial;

                    // check if map is hidden or other
                    if (mapObj.GetComponent<MeshRenderer>().sharedMaterial == _mapSO.hiddenMapMaterial) {
                        targetMaterial = _mapSO.selectedHiddenMapMaterial;
                        startButton.canClicked = false;
                        startButton.GetComponent<Button>().transition = Selectable.Transition.None;
                        startText.text = lockStr;
                    }
                    else {
                        targetMaterial = _mapSO.selectedMapMaterial;
                        startButton.canClicked = true;
                        startButton.GetComponent<Button>().transition = Selectable.Transition.ColorTint;
                        startText.text = startStr;
                    }
                    selectedMapObj = mapObj; 
                    mapObj.GetComponent<MeshRenderer>().material = targetMaterial;

                    GameManager.Instance.SetNextSceneIndex(int.Parse(mapObj.name.Remove(0, 4)));
                }
            }
        }
    }

    private void SetSelectedMapMaterial() {
        if (selectedMapObj != null) {
            int index = int.Parse(selectedMapObj.name.Remove(0, 4));
            selectedMapObj.GetComponent<MeshRenderer>().material = _mapSO.originalMapMaterials[index - 1];
        }
    }

    private void LoadMapData() {
        for (int i = 0; i < mapMeshRenderers.Count; i++) {
            mapMeshRenderers[i].material = _mapSO.originalMapMaterials[i];
        }

        mapMeshRenderers[0].material = _mapSO.selectedMapMaterial;
        selectedMapObj = mapMeshRenderers[0].gameObject;
        GameManager.Instance.SetNextSceneIndex(1);
    }
}
