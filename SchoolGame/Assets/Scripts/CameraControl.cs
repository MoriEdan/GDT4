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
            //transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            //transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= new Vector3(speed * Time.deltaTime,0, 0);
            //transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            //transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }

        GameLight.transform.position = this.transform.position;
    }
}
