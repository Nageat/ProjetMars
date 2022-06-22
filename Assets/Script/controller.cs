using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    private Vector3 targetPosition;
    private Plane plane;
    private int ScreenNB = 0;
    public float moveSpeed = 3f;
    public float turnSpeed = 3f;
    public float angleLimit = 5f;
    public float height = 1f;
    public bool waitForRotation = true;
    public bool HavetheMastCam = false;
    public SolarEnergy SolarEnergy;
    public GameObject CamTPS;
    public GameObject CamFPS;
    public GameObject NotMastCam;
    public Animator animScreenShot;

    public enum Cam {TPS, FPS }
    public Cam CurentView;
    public Renderer Renderer_Gale;
    public GameObject Panel_Gale;
    public Renderer Renderer_Olympus;
    public GameObject Panel_Olympus;
    public Renderer Renderer_Asteroid;
    public GameObject Panel_Asteroid;
    void Start()
    {
        CurentView = Cam.TPS;
        targetPosition = transform.position;
        plane = new Plane(Vector3.up, new Vector3(0, height, 0));
    }

    void Update()
    {
        if (Input.GetKeyDown("space")){
            if (CurentView == Cam.TPS)
            {
                CurentView = Cam.FPS;
                CamFPS.SetActive(true);
                CamTPS.SetActive(false);
                if (HavetheMastCam == false)
                {
                    NotMastCam.SetActive(true);
                }
                else
                {
                    NotMastCam.SetActive(false);
                }
            }
            else if (CurentView == Cam.FPS)
            {
                CurentView = Cam.TPS;
                NotMastCam.SetActive(false);
                CamFPS.SetActive(false);
                CamTPS.SetActive(true);
            }
        }

        if (Input.GetMouseButtonDown(0) && CurentView == Cam.TPS)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float distance))
            {
                targetPosition = ray.GetPoint(distance);
                SolarEnergy.CurrentEnergy -= 5;

            }

        }
        else if (Input.GetMouseButtonDown(0) && CurentView == Cam.FPS && HavetheMastCam == true)
        {
            animScreenShot.Play("TakeScreen", 0, 0);

            ScreenCapture.CaptureScreenshot("Screen"+ScreenNB+".png");
            ScreenNB++;
            SolarEnergy.CurrentEnergy -= 15;
            if (Renderer_Gale.isVisible)
            {
                Panel_Gale.SetActive(true);
                Debug.Log("SCREEN OF GALE CRATER");
            }
            else if (Renderer_Olympus.isVisible)
            {
                Panel_Olympus.SetActive(true);
                Debug.Log("SCREEN OF Olympus");

            }
            else if (Renderer_Asteroid.isVisible)
            {
                Panel_Asteroid.SetActive(true);
                Debug.Log("SCREEN OF Asteroid");

            }
        }

        if (targetPosition != transform.position)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            if (!waitForRotation || Quaternion.Angle(targetRotation, transform.rotation) < angleLimit)
            {
                float distanceToDestination = Vector3.Distance(transform.position, targetPosition);

                if (distanceToDestination > (Time.deltaTime * moveSpeed))
                {
                    transform.position += (targetPosition - transform.position).normalized * moveSpeed * Time.deltaTime;
                }
                else
                {
                    transform.position = targetPosition;
                }
            }
        }


        /*
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float distance))
            {
                targetPosition = ray.GetPoint(distance);
                Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                difference.Normalize();
                float rotation_z = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, rotation_z, 0f);
            }
        }

        float distanceToDestination = Vector3.Distance(transform.position, targetPosition);
        if (distanceToDestination > (Time.deltaTime * speed))
        { //We won't reach destination this frame
          //Move toward the destination
            transform.position += (targetPosition - transform.position).normalized * speed * Time.deltaTime;
        }
        else
        { //We will reach destination this frame
            transform.position = targetPosition;
        }*/
    }

    public void SwitchCam()
    {
        if (CurentView == Cam.TPS)
        {
            CurentView = Cam.FPS;
            CamFPS.SetActive(true);
            CamTPS.SetActive(false);
            if (HavetheMastCam == false)
            {
                NotMastCam.SetActive(true);
            }
            else
            {
                NotMastCam.SetActive(false);
            }
        }
        else if (CurentView == Cam.FPS)
        {
            CurentView = Cam.TPS;
            NotMastCam.SetActive(false);
            CamFPS.SetActive(false);
            CamTPS.SetActive(true);
        }
    }
    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}
