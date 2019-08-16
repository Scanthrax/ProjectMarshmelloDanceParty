//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

///// <summary>
///// Val Script: Camera Zoom and Follow player
///// </summary>
//public class CameraFollow : MonoBehaviour
//{

//    public static CameraFollow instance;

//    private void Awake()
//    {
//        instance = this;
//    }

//    //Follow Camera  
//    public float dampTime = 0.15f;
//    private Vector3 velocity = Vector3.zero;
//    private float offset = 0f; 
//    public List<Transform> target;


//    //Zoom feature
//    Camera cam;
//    public float zoomSpeed = 4f;
//    private float currentZoom = 1f;
//    public float minZoom = 3f;
//    public float maxZoom = 7f;


//    public Transform cameraBounds;
//    public List<RectTransform> currentBounds;

//    Rigidbody2D rigidbody;

//    public void Start()
//    {
//        cam = this.GetComponent<Camera>();
//        rigidbody = GetComponent<Rigidbody2D>();
//    }

//    void Update()
//    {
//        //Gets input from scroll wheel
//        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
//        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);



//        //if (target.Count != 0)
//        //{
//        //    Vector3 accumulatePos = Vector3.zero;
//        //    float minX = target[0].position.x, maxX = target[0].position.x;
//        //    foreach (var character in target)
//        //    {
//        //        accumulatePos += character.position;

//        //        if (character.position.x < minX)
//        //            minX = character.position.x;
//        //        else if (character.position.x > maxX)
//        //            maxX = character.position.x;
//        //    }

//        //    Vector3 averagePos = accumulatePos / target.Count;

//        //    //Follow Character
//        //    Vector3 point = GetComponent<Camera>().WorldToViewportPoint(averagePos);
//        //    Vector3 delta = averagePos - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
//        //    Vector3 destination = transform.position + delta;
//        //    rigidbody.MovePosition(Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime));
//        //    //Zoom update
//        //    //cam.orthographicSize = offset + currentZoom;

//        //    var desiredZoom = Mathf.Abs(maxX - minX);

//        //    cam.orthographicSize = Mathf.Clamp(desiredZoom, minZoom, maxZoom);

//        //    foreach (var item in target)
//        //    {
//        //        item.position = new Vector3(Mathf.Clamp(item.position.x, transform.position.x - (cam.orthographicSize * (16f / 9f)), transform.position.x + (cam.orthographicSize * (16f / 9f))), item.position.y, item.position.z);
//        //    }


//        //    //ClampCamera();
//        //}


//    }





//    public void ClampCamera()
//    {
//        //Vector2 pos = target[0].transform.position;

//        //float orthoOffset = cam.orthographicSize;
//        //float aspect = 16f/9f;

//        //float leftBound, rightBound, topBound, botBound;

//        //foreach (RectTransform item in cameraBounds)
//        //{
//        //    leftBound = item.transform.position.x + item.rect.xMin;
//        //    rightBound = item.transform.position.x + item.rect.xMax;
//        //    botBound = item.transform.position.y + item.rect.yMin;
//        //    topBound = item.transform.position.y + item.rect.yMax;

//        //    if (pos.x > leftBound &&
//        //        pos.x < rightBound &&
//        //        pos.y > botBound &&
//        //        pos.y < topBound)
//        //    {
//        //        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBound + orthoOffset * aspect, rightBound - orthoOffset * aspect), Mathf.Clamp(transform.position.y, botBound + orthoOffset, topBound - orthoOffset), -10f);
                
//        //    }
//        //}




//    }

//}
