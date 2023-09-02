using Cinemachine;
using UnityEngine;

public class CinemachineCoreInput : MonoBehaviour
{
    public float touchSensitivity = 20f;

    TouchField touchField;

    // Start is called before the first frame update
    void Start()
    {
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
        touchField = FindAnyObjectByType<TouchField>();
    }

    float HandleAxisInputDelegate(string axisName)
    {
        switch (axisName)
        {
            case "Touch X":
                return touchField.touchDist.x / touchSensitivity;
            case "Touch Y":
                return touchField.touchDist.y / touchSensitivity;
            default:
                Debug.LogError("Input <" + axisName + "> Tidak ada.", this);
                break;
        }

        return 0f;
    }
}