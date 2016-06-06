using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public static CameraControl instance;

    public float speed;
    public Light GameLight;

	// Use this for initialization
	void Awake ()
    {
        instance = this;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= new Vector3(speed * Time.deltaTime,0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }

        GameLight.transform.position = this.transform.position;
    }

    public void setCameraPositionForPlayer(int player)
    {
        switch (player)
        {
            case 1: //camera position player 1
                this.transform.position = new Vector3(-21, 5, -15);
                break;
            case 2: //camera position player 2
                this.transform.position = new Vector3(8, 5, -15);
                break;
            case 3: //camera position player 3
                this.transform.position = new Vector3(-21, 5, 15);
                break;
            case 4: //camera position player 4
                this.transform.position = new Vector3(8, 5, 15);
                break;
            default:
                break;
        }
    }

    public void setCameraPositionForGameOver()
    {
        this.transform.position = new Vector3(0, 0, 0); //TO DO
    }
}
