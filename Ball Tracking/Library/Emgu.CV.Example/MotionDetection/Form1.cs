//----------------------------------------------------------------------------
//  Copyright (C) 2004-2016 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.VideoSurveillance;
using Emgu.Util;

namespace MotionDetection
{
    public partial class Form1 : Form
    {
        private Capture _capture;
        private MotionHistory _motionHistory;

        public Form1()
        {
            InitializeComponent();

            //try to create the capture
            if (_capture == null)
            {
                try
                {
                    _capture = new Capture();
                }
                catch (NullReferenceException excpt)
                {   //show errors if there is any
                    MessageBox.Show(excpt.Message);
                }
            }

            if (_capture != null) //if camera capture has been successfully created
            {
                _motionHistory = new MotionHistory(
                    1.0, //in second, the duration of motion history you wants to keep
                    0.05, //in second, maxDelta for cvCalcMotionGradient
                    0.5); //in second, minDelta for cvCalcMotionGradient

                _capture.ImageGrabbed += ProcessFrame;
                _capture.Start();
            }

            
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            Mat image = new Mat();

            _capture.Retrieve(image);

            if (highlightBody.Checked)
            {
                image = detectBody(image);
            }
            else
            {
                image = detectFace(image);
            }

            if (this.Disposing || this.IsDisposed)
                return;

            afterProcesImage.Image = image;
        }

        private Mat detectBody(Mat image)
        {
            long procesingtime;
            Rectangle[] results = FindPedestrian.Find(image, true, out procesingtime);
            foreach (Rectangle rect in results)
            {
                CvInvoke.Rectangle(image, rect, new Bgr(Color.Red).MCvScalar);
            }

            return image;
        }

        private Mat detectFace(Mat image)
        {
            long detectionTime;
            List<Rectangle> faces = new List<Rectangle>();
            bool tryUseCuda = true;

            DetectFace.Detect(image, "haarcascade_frontalface_default.xml", faces, tryUseCuda, out detectionTime);

            foreach (Rectangle face in faces)
            {
                CvInvoke.Rectangle(image, face, new Bgr(Color.Red).MCvScalar, 2);
            }
            return image;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {

            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _capture.Stop();
        }
    }
}
