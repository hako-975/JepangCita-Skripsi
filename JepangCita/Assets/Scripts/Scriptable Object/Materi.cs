using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewMateri", menuName = "Data/New Materi")]
[System.Serializable]
public class Materi : ScriptableObject
{
    public string titleMateri;
    public List<MaterialData> materialsData = new List<MaterialData>();
}

[Serializable]
public class MaterialData
{
    [TextArea(8, 20)]
    public string materi;
    public int intervalTimes;
}
