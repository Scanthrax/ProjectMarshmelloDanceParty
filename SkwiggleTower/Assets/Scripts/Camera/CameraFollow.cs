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
    public List<Transform> target;


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

        if (target.Count != 0)
        {
            Vector3 accumulatePos = Vector3.zero;
            float minX = target[0].position.x, maxX = target[0].position.x;
            foreach (var character in target)
            {
                accumulatePos += character.position;

                if (character.position.x < minX)
                    minX = character.position.x;
                else if(character.position.x > maxX)
                    maxX = character.position.x;
            }

            Vector3 averagePos = accumulatePos / target.Count;

            //Follow Character
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(averagePos);
            Vector3 delta = averagePos - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            //Zoom update
            //cam.orthographicSize = offset + currentZoom;

            var desiredZoom = Mathf.Abs(maxX - minX);

            cam.orthographicSize = Mathf.Clamp(desiredZoom, minZoom, maxZoom);

            foreach (var item in target)
            {
                item.position = new Vector3(Mathf.Clamp(item.position.x,transform.position.x - (cam.orthographicSize * (16f/9f)), transform.position.x + (cam.orthographicSize * (16f / 9f))),item.position.y,item.position.z);
            }
        }
    }

}
