using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraRoot;
    public GameObject cameraMain;
    public Vector3 offsetFromRoot;
    public Vector3 CameraAngles;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        RootFollowsPlayer();
        SetOffset();
        SetAngle();
    }

    // Update is called once per frame
    void Update()
    {
        RootFollowsPlayer();
        SetOffset();
        SetAngle();
    }

    public void RootFollowsPlayer()
    {
        cameraRoot.transform.position = player.transform.position;
    }

    public void SetOffset()
    {
        cameraMain.transform.localPosition = offsetFromRoot;
    }

    public void SetAngle()
    {
        cameraRoot.transform.eulerAngles = CameraAngles;
    }
}
