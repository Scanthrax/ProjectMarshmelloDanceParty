using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Val Script: Camera Zoom and Follow player
/// </summary>
public class CameraFollow : MonoBehaviour
{
    //Follow Camera  
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    private float offset = 0f; 
    public Transform target;


    //Zoom feature
    Camera cam;
    public float zoomSpeed = 4f;
    private float currentZoom = 1f;
    public float minZoom = 3f;
    public float maxZoom = 7f;


    public void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    void Update()
    {
        //Gets input from scroll wheel
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            //Follow Character
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            //Zoom update
            cam.orthographicSize = offset + currentZoom;
        }
    }

}
