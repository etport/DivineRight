using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS; 

///<summary>
///
///</summary>

public class HUD : MonoBehaviour 
{
    public GUISkin resourceSkin;
    public GUISkin ordersSkin;

    //TODO load from csv
    const int OrdersBarWidth = 150;
    const int ResourceBarHeight = 40;

    Player player; 

	///<summary>
	/// Use this for initialization
	///</summary>
	void Start ()
	{
        player = transform.root.GetComponent<Player>();
	}
	
	void OnGUI()
    {
        if (player && player.human)
        {
            DrawOrdersBar();
            DrawResourceBar(); 
        }
    }
    void DrawOrdersBar()
    {
        GUI.skin = ordersSkin;
        GUI.BeginGroup(new Rect(Screen.width - OrdersBarWidth, ResourceBarHeight, OrdersBarWidth, Screen.height - ResourceBarHeight));
        GUI.Box(new Rect(0, 0, OrdersBarWidth, Screen.height - ResourceBarHeight), "");
        GUI.EndGroup(); 
    }
    void DrawResourceBar()
    {
        GUI.skin = resourceSkin;
        GUI.BeginGroup(new Rect(0, 0, Screen.width, ResourceBarHeight));
        GUI.Box(new Rect(0, 0, Screen.width, ResourceBarHeight), "");
        GUI.EndGroup(); 
    }
}
