using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : MonoBehaviour
{
    public Camera m_Camera;
    [SerializeField] private MouseLook m_MouseLook;
    private void Start()
    {
        m_MouseLook.Init(transform, m_Camera.transform);
    }
    void Update()
    {
        if (!UIManager.Instance.isPaused)
        {
            RotateView();
        }
    }

    private void RotateView()
    {
        m_MouseLook.LookRotation(transform, m_Camera.transform);
    }
}
