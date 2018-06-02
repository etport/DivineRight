using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System; 

///<summary>
///
///</summary>

public class ConfigData 
{

    #region Fields

    const string ConfigDataFileName = "ConfigurationData.csv";

    #endregion

    #region Defaults 
    static float cameraScrollSpeed = 25;
    static float cameraRotateSpeed = 100;
    static int edgeScrollTolerance = 15;
    static float minCameraHeight = 10;
    static float maxCameraHeight = 40; 

    #endregion 

    //public accessors 
    #region Properties

    public float CameraScrollSpeed
    {
        get { return cameraScrollSpeed; }
    }

    public float CameraRotateSpeed
    {
        get { return cameraRotateSpeed;  }
    }

    public int EdgeScrollTolerance
    {
        get { return edgeScrollTolerance;  }
    }

    public float MinCameraHeight
    {
        get { return minCameraHeight;  }
    }
    
    public float MaxCameraHeight
    {
        get { return maxCameraHeight;  }
    }

    #endregion

    #region Constructor

    public ConfigData()
    {
        StreamReader input = null; 

        try
        {
            input = File.OpenText(Path.Combine(Application.streamingAssetsPath, ConfigDataFileName));
            input.ReadLine(); //this line is the var names -- discard
            string cameraValues = input.ReadLine();
            SetConfigValues(cameraValues); 
        }

        catch (Exception)
        {
            
        }
        finally
        {
            if (input != null)
            {
                input.Close(); 
            }
        }
    }
    #endregion

    #region Methods

    void SetConfigValues(string values)
    {
        string[] valueString = values.Split(',');
        cameraScrollSpeed = float.Parse(valueString[0]);
        cameraRotateSpeed = float.Parse(valueString[1]);
        edgeScrollTolerance = int.Parse(valueString[2]);
        minCameraHeight = float.Parse(valueString[3]);
        maxCameraHeight = float.Parse(valueString[4]); 

    }
    #endregion
}

