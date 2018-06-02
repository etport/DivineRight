using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RTS
{
    
    
    public static class ResourceManager
    {
        #region Fields

        static ConfigData configData;

        #endregion

        #region Properties

        public static float ScrollSpeed
        {
            get { return configData.CameraScrollSpeed; }
        }

        public static float RotateSpeed
        {
            get { return configData.CameraRotateSpeed;  }
        }

        public static int ScrollWidth
        {
            get { return configData.EdgeScrollTolerance; }
        }

        public static float MinCameraHeight
        {
            get { return configData.MinCameraHeight; }
        }

        public static float MaxCameraHeight
        {
            get { return configData.MaxCameraHeight;  }
        }
        #endregion

        #region Methods
        public static void Initialize()
        {
            configData = new ConfigData(); 
        }
        #endregion 
    }


}