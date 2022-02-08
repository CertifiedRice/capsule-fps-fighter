using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouch : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.C))
        {
            transform.localScale = new Vector3(1.2f, 0.9f, 1.2f);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            transform.localScale = new Vector3(1.2f, 1.8f, 1.2f);
        }
    }
}