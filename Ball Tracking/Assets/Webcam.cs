using UnityEngine;
using UnityEngine.UI;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
#if !(__IOS__ || NETFX_CORE)
using Emgu.CV.Cuda;
#endif

public class Webcam : MonoBehaviour
{
    public RawImage rawImage;
    private WebCamTexture webcam;
    public Color32[] data;

    // Use this for initialization
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
            Debug.Log(devices[i].name);

        webcam = new WebCamTexture(devices[0].name);
        webcam.Play();

        data = new Color32[webcam.width * webcam.height];
    }

    // Update is called once per frame
    void Update()
    {
        if(webcam.didUpdateThisFrame)
        {
            Debug.Log(data.Length);
            rawImage = this.GetComponent<RawImage>();
            rawImage.material.mainTexture = webcam;
        }
    }

    private Texture2D ProcesFrame()
    {
        data = webcam.GetPixels32();
        Texture2D newImage = new Texture2D(webcam.width, webcam.height);
        newImage.SetPixels32(data);

        // colors used to tint the first 3 mip levels
        var colors = new Color32[3];
        colors[0] = Color.red;
        colors[1] = Color.green;
        colors[2] = Color.blue;
        var mipCount = Mathf.Min(3, newImage.mipmapCount);

        // tint each mip level
        for (var mip = 0; mip < mipCount; ++mip)
        {
            var cols = newImage.GetPixels32(mip);
            for (var i = 0; i < cols.Length; ++i)
            {
                cols[i] = Color32.Lerp(cols[i], colors[mip], 0.33f);
            }
            newImage.SetPixels32(cols, mip);
        }

        // actually apply all SetPixels32, don't recalculate mip levels
        newImage.Apply(false);

        return newImage;
    }
}
