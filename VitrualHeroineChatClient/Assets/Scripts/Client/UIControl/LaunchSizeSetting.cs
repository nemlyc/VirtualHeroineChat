using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSizeSetting : MonoBehaviour
{
    [Tooltip("WindowSize")]
    public int width = 400;
    public int height = 800;

    private void Awake() {
        Screen.SetResolution(CheckZero(width), CheckZero(height), false);
    }

    int CheckZero(int val) {
        if(val == 0) {
            val = 800;
        }
        return val;
    }

}
