using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepTrigger : MonoBehaviour
{
    public bool triggered;

    private void Start()
    {
        triggered = false;
    }

    //if something enters the objects Collider, this method is called
    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
    }

    //if something exits the objects Collider, this method is called
    private void OnTriggerExit(Collider other)
    {
        triggered = false;
    }
}
