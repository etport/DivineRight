using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS; 

public class WorldObject : MonoBehaviour {

    #region Public Fields

    public string objectName;
    public Texture2D buildImage;
    public int cost;
    public int sellValue;
    public int hitPoints;
    public int maxHitPoints;

    #endregion


    #region Protected Fields

    protected Player player;
    protected string[] actions = { };
    protected bool currentlySelected = false;
    protected Bounds selectionBounds;
    protected Rect playingArea = new Rect(0, 0, 0, 0); 

    #endregion
    

    #region Protected Methods

    protected virtual void Awake()
    {
        selectionBounds = ResourceManager.InvalidBounds;
        CalculateBounds(); 
    }
    // Use this for initialization
    protected virtual void Start ()
    {
        player = transform.root.GetComponentInChildren<Player>();
	}
	
	// Update is called once per frame
	protected virtual void Update ()
    {
		
	}

    protected virtual void OnGUI()
    {
        if (currentlySelected)
        {
            DrawSelection(); 
        }
    }

    protected virtual void DrawSelectionBox(Rect selectBox)
    {
        GUI.Box(selectBox, ""); 
    }
    #endregion

    #region Public Methods

    public void SetSelected (bool selected, Rect playingArea)
    {
        currentlySelected = selected; 
        if (selected)
        {
            this.playingArea = playingArea; 
        }
    }

    public string[] GetActions()
    {
        return actions; 
    }

    public virtual void PerformAction (string actionToPerform)
    {
        //implemented in child classes
    }

    public virtual void MouseClick(GameObject hitObject, Vector3 hitPoint, Player controller)
    {
        //handle input if currently selected 
        if (currentlySelected && hitObject && hitObject.name != "Ground")
        {
            WorldObject worldObject = hitObject.transform.root.GetComponent<WorldObject>(); 
            //clicked on another selectable object 
            if (worldObject)
            {
                ChangeSelection(worldObject, controller); 
            }
        }
    }


    public void CalculateBounds()
    {
        selectionBounds = new Bounds(transform.position, Vector3.zero);
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            selectionBounds.Encapsulate(r.bounds);
        }
    }

    #endregion

    #region Private Methods

    void ChangeSelection(WorldObject worldObject, Player controller)
    {
        SetSelected(false, playingArea); 
        if (controller.SelectedObject)
        {
            controller.SelectedObject.SetSelected(false, playingArea);
            controller.SelectedObject = worldObject;
            worldObject.SetSelected(true, playingArea); 
        }
    }

    void DrawSelection ()
    {
        GUI.skin = ResourceManager.SelectBoxSkin;
        Rect selectBox = WorkManager.CalculateSelectionBox(selectionBounds, playingArea);

        GUI.BeginGroup(playingArea);
        DrawSelectionBox(selectBox);
        GUI.EndGroup();
    }

    #endregion

}
