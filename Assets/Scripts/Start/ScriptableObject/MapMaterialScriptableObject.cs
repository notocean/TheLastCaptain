using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/MapManagerScriptableObject", order = 1)]
public class MapMaterialScriptableObject : ScriptableObject
{
    public Material hiddenMapMaterial;
    public Material selectedHiddenMapMaterial;
    public Material originalMapMaterial;
    public Material selectedMapMaterial;
    public Material completedMapMaterial;

    public List<Material> originalMapMaterials;
}