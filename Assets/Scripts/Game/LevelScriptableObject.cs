using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "level", menuName = "Scriptable Objects/Level", order = 1)]
public class LevelScriptableObject : ScriptableObject
{
    public List<Vector3> lstTransformCat = new List<Vector3>();
    public List<Vector3> lstTransformObstacle = new List<Vector3>();
}
