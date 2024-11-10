using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionStimuli : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SenseComp.RegisterStimuli(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        SenseComp.UnRigisterStimuli(this);
    }
}
