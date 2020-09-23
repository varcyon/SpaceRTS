using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{

    public static CameraControl i;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minZoomDist = 10f;
    [SerializeField] private float maxZoomDist = 50f;
    [SerializeField] private float panSpeed = 10f;
    [SerializeField] private float panEdgeBoarder = 25f;
    [SerializeField] private CinemachineVirtualCamera panCam;
    Vector3 pcam;
    private void Start()
    {
        if (i == null)
        {
            i = this;
        }


        panCam.transform.position = new Vector3(GameSetupController.startPosistions[Persistant.team - 1].transform.position.x, 15, GameSetupController.startPosistions[Persistant.team - 1].transform.position.z);
    }
    private void Update()
    {
        PanCamera();
        // EdgePan();
        Zoom();
        FocusCamra();
    }
    public void PanCamera()
    {
        //pans camera
        if (Input.GetMouseButton(2))
        {
            panCam.transform.position -= new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = transform.forward * z + transform.right * x;
        panCam.transform.position += dir * panSpeed * Time.deltaTime;
    }

    public void Zoom()
    {   //zooms the camera on its forward vector
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        // finds the distance from the center of everything to the camera
        float zoomDist = Vector3.Distance(new Vector3(0, 0, 0), panCam.transform.position);
        //returns ( stops ) zooming if its the min distance or max distance
        if (zoomDist < minZoomDist && scrollInput > 0.0f)
            return;
        else if (zoomDist > maxZoomDist && scrollInput < 0.0f)
            return;
        // actuall moves the camera
        panCam.transform.position += panCam.transform.forward * scrollInput * zoomSpeed;
    }



    public void EdgePan()
    {   // if mouse position is panEdgeBoarder( 25 ) from the top , pans up, down, left, right
        if (Input.mousePosition.y <= Screen.height - panEdgeBoarder)
        {
            panCam.transform.position += new Vector3(0, 0, panSpeed * Time.deltaTime);
        }

        if (Input.mousePosition.y >= panEdgeBoarder)
        {
            panCam.transform.position -= new Vector3(0, 0, panSpeed * Time.deltaTime);
        }

        if (Input.mousePosition.x <= Screen.width - panEdgeBoarder)
        {
            panCam.transform.position -= new Vector3(panSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.mousePosition.x >= panEdgeBoarder)
        {
            panCam.transform.position += new Vector3(panSpeed * Time.deltaTime, 0, 0);
        }

    }
    public void FocusCamra()
    {
        if (Input.GetKey(KeyCode.Space) && UnitManager.UM.selectedStructures.Count != 0)
        {
            FocusPos(UnitManager.UM.selectedStructures[0].transform.position);
        }
    }
    public void FocusPos(Vector3 uPos)
    {
        Vector3 camDiff = new Vector3(0, 15, -15);
        panCam.transform.position = uPos + camDiff;
    }

    public void SetCam()
    {
        panCam.Follow = GameSetupController.startPosistions[Persistant.team - 1].transform;
        StartCoroutine(FollowToNull()); 
    }

    public IEnumerator FollowToNull()
    {
        yield return new WaitForSeconds(1f);
        panCam.Follow = null;
       
    }
}
