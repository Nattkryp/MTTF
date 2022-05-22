using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    Camera cam;
    public Text gameSpeed;
    public float baseScrollSpeed = 4f;
    Transform audiolistener;

    private void Start()
    {
        cam= GetComponent<Camera>();
        audiolistener = transform.Find("Listener");
    }

    public void SetGameSpeed(int x) {
    Time.timeScale = x;
    }

    void Update()
    {
        CameraMovement();
        gameSpeed.text = Time.timeScale.ToString() + "x";

        Vector3 newAudiolistenerPosition = new Vector3(audiolistener.transform.position.x, audiolistener.transform.position.y, cam.orthographicSize * -1);
        audiolistener.transform.position = newAudiolistenerPosition;
    }

    public void CameraMovement()
    {
        //Camera Movement
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x + -baseScrollSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + baseScrollSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + baseScrollSpeed * Time.deltaTime, transform.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + -baseScrollSpeed * Time.deltaTime, transform.position.z);
        }

        //Camera Zoom
        if (Input.GetKeyDown(KeyCode.Z))
        {
            cam.orthographicSize = cam.orthographicSize + 10 * baseScrollSpeed / 3f * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            cam.orthographicSize = cam.orthographicSize - 10 * baseScrollSpeed / 3f * Time.deltaTime;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 0.2f, 30f);
        }

        //Manage time (Maybe not in this script in the future but would be good to have everything about player input in the same place)
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Time.timeScale += 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale -= 0.5f;
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            Time.timeScale = 1.0f;
        }
    }
}
