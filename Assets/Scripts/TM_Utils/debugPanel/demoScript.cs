using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TM.Utils;

public class demoScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        debugLog.instance.Log("Input", "Delta Time", Time.deltaTime);
        debugLog.instance.Log("System", "Time", Time.time);
        debugLog.instance.Log("Input", "hello", "hello");
        debugLog.instance.Log("Objects", "sphere", this.transform.position);  
#endif
    }
}
