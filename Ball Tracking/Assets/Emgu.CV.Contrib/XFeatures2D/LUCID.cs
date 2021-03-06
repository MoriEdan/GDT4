﻿//----------------------------------------------------------------------------
//  Copyright (C) 2004-2016 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.Util;
using Emgu.CV.Features2D;

namespace Emgu.CV.XFeatures2D
{
   /// <summary>
   /// The locally uniform comparison image descriptor:
   /// An image descriptor that can be computed very fast, while being
   /// about as robust as, for example, SURF or BRIEF.
   /// </summary>
   public class LUCID : Feature2D
   {
      /// <summary>
      /// Create a locally uniform comparison image descriptor.
      /// </summary>
      /// <param name="lucidKernel">Kernel for descriptor construction, where 1=3x3, 2=5x5, 3=7x7 and so forth</param>
      /// <param name="blurKernel">kernel for blurring image prior to descriptor construction, where 1=3x3, 2=5x5, 3=7x7 and so forth</param>
      public LUCID(int lucidKernel = 1, int blurKernel = 2)
      {
         _ptr = ContribInvoke.cveLUCIDCreate(lucidKernel, blurKernel, ref _feature2D);
      }

      /// <summary>
      /// Release all the unmanaged resource associated with BRIEF
      /// </summary>
      protected override void DisposeObject()
      {
         if (_ptr != IntPtr.Zero)
         {
            ContribInvoke.cveLUCIDRelease(ref _ptr);
         }
         base.DisposeObject();
      }
   }

   public static partial class ContribInvoke
   {

      [DllImport(CvInvoke.ExternLibrary, CallingConvention = CvInvoke.CvCallingConvention)]
      internal extern static IntPtr cveLUCIDCreate(int lucidKernel, int blurKernel, ref IntPtr feature2D);

      [DllImport(CvInvoke.ExternLibrary, CallingConvention = CvInvoke.CvCallingConvention)]
      internal extern static void cveLUCIDRelease(ref IntPtr extractor);
   }
}