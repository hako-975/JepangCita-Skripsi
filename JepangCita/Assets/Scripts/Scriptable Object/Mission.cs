using UnityEngine;

[CreateAssetMenu(fileName = "NewMission", menuName = "Data/New Mission")]
[System.Serializable]
public class Mission : ScriptableObject
{
    public string titleMission;
    [TextArea(8,8)]
    public string detailMission;
}
