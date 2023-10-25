using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private PlatformEffector2D effector;
    public bool leavable = true;
    void Start() {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update() {
        float dirY = Input.GetAxis("Vertical");
        bool pressedDown = dirY < 0f;
        if (pressedDown && leavable) {
            effector.rotationalOffset = 180;
            
        } else {
            effector.rotationalOffset = 0;
        }
    }
    
    
}
