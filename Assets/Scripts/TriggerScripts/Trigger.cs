using UnityEngine;

public class Trigger : MonoBehaviour
{
    //Scripts that check BoxColliders must be applied to the Object on which the Collider is applied

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
