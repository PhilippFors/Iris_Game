using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    //Input for the First Person Camera
    public Camera fpsCamera;
    public Camera bedCamera;
    //These two lines show how to make an event, parameters need to match the events that are going to subscribe
    public delegate void Progress(GameObject o);
    public static event Progress progress;

    void Update()
    {
        if (!UIManager.Instance.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnClick();
                UIManager.Instance.interactCircle.Play();
            }
        }
    }

    void OnClick()
    {
        var ray = fpsCamera.ScreenPointToRay(Input.mousePosition);
        //if e is pressed, a ray is sent out.
        if (fpsCamera.enabled)
        {
            ray = fpsCamera.ScreenPointToRay(Input.mousePosition);

        }
        else if (bedCamera.enabled)
        {
            ray = bedCamera.ScreenPointToRay(Input.mousePosition);

        }
        //hit contains the object that is hit by the ray
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2f))
        {
            //Converting the selction into a GameObject so it can be properly used later (variables of type "var" can not be a parameter)
            GameObject o = hit.transform.gameObject;

            //The event is called here and every method suscribed to the event will be called
            progress(o);
        }
    }
}





