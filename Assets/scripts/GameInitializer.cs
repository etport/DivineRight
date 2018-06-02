using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS; 

///<summary>
///
///</summary>

public class GameInitializer : MonoBehaviour 
{

    void Awake()
    {
        ResourceManager.Initialize(); 
    }
}
