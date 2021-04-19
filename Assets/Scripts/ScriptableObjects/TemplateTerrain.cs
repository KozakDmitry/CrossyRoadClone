using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="TemplateTerrain",menuName = "TemplateTerrain")]
public class TemplateTerrain : ScriptableObject
{
    public List<GameObject> terrainType;
    public int maxTerrainInOneTime;
}
