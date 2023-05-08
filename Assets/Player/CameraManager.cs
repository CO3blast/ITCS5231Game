using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{   
    public Transform cameraTransform;
    [SerializeField] private Camera cam;
     float mouseX=0;
     float mouseY=0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
         mouseX= mouseX+Input.GetAxis("Mouse Y")*2;
         mouseY= mouseY+Input.GetAxis("Mouse X")*2;
         mouseX=Mathf.Clamp(mouseX,-90,90);
        gameObject.transform.rotation=Quaternion.Euler(0,mouseY,0);;
        cameraTransform.rotation=Quaternion.Euler(-mouseX,mouseY,0);;
    }
}
