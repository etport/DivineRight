using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS; 

///<summary>
///
///</summary>

public class HUD : MonoBehaviour 
{
    #region Public Fields
    public GUISkin resourceSkin;
    public GUISkin ordersSkin;
    public GUISkin selectBoxSkin; 
    #endregion

    #region Private Fields

    //TODO load from csv
    const int OrdersBarWidth = 100;
    const int ResourceBarHeight = 40;
    const int SelectionNameHeight = 15;

    Player player;

    #endregion

   

    #region Private Methods

    ///<summary>
    /// Use this for initialization
    ///</summary>
    void Start ()
	{
        player = transform.root.GetComponent<Player>();
        ResourceManager.StoreSelectBoxItems(selectBoxSkin); 
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
        string selectionName = ""; 
        if (player.SelectedObject)
        {
            selectionName = player.SelectedObject.objectName;
        }
        if (!selectionName.Equals(""))
        {
            GUI.Label(new Rect(0, 10, OrdersBarWidth, SelectionNameHeight), selectionName); 
        }
        GUI.EndGroup(); 
    }
    void DrawResourceBar()
    {
        GUI.skin = resourceSkin;
        GUI.BeginGroup(new Rect(0, 0, Screen.width, ResourceBarHeight));
        GUI.Box(new Rect(0, 0, Screen.width, ResourceBarHeight), "");
        GUI.EndGroup(); 
    }

    #endregion

    #region Public Methods

    public bool MouseInBounds()
    {
        Vector3 mousePosition = Input.mousePosition;
        bool insideWidth = mousePosition.x >= 0 && mousePosition.x <= (Screen.width - OrdersBarWidth);
        bool insideHeight = mousePosition.y >= 0 && mousePosition.y <= (Screen.height - ResourceBarHeight);
        return insideWidth && insideHeight; 
    }

    public Rect GetPlayingArea()
    {
        return new Rect(0, ResourceBarHeight, (Screen.width - OrdersBarWidth), (Screen.height - ResourceBarHeight)); 
    }
    #endregion 
}
