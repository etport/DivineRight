using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RTS
{
    
    
    public static class ResourceManager
    {
        #region Fields

        static ConfigData configData;
        static Vector3 invalidPosition = new Vector3(-9999, -9999, -9999);
        static GUISkin selectBoxSkin;
        static Bounds invalidBounds = new Bounds(new Vector3(-9999, -9999, -9999), new Vector3(0, 0, 0)); 

        #endregion

        #region Properties
        //Camera Properties
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

        //Selection Properties 
        public static Vector3 InvalidPosition
        {
            get { return invalidPosition;  }
        }

        public static GUISkin SelectBoxSkin
        {
            get { return selectBoxSkin;  }
        }

        public static Bounds InvalidBounds
        {
            get { return invalidBounds;  }
        }
        #endregion

        #region Methods
        public static void Initialize()
        {
            configData = new ConfigData(); 
        }

        public static void StoreSelectBoxItems(GUISkin skin)
        {
            selectBoxSkin = skin; 
        }
        #endregion 
    }


}