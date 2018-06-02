using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///</summary>

public class Player : MonoBehaviour 
{
    #region Public Fields
    public string username;
    public bool human;
    public HUD hud;
    public WorldObject SelectedObject { get; set;  }
    #endregion

    ///<summary>
    /// Use this for initialization
    ///</summary>
    void Start ()
	{
        hud = GetComponentInChildren<HUD>();
	}
	
	///<summary>
	/// Update is called once per frame
	///</summary>
	void Update ()
	{
		
	}
}
