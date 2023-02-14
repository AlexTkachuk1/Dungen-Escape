using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    private Spider script;
    private void Start()
    {
        script = GetComponentInParent<Spider>();
    }
    public void Fier()
    {
        script.CreateAcidBoll();
    }
}
