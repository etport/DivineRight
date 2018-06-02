using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour {

    #region Fields
    public string objectName;
    public Texture2D buildImage;
    public int cost;
    public int sellValue;
    public int hitPoints;
    public int maxHitPoints;

    protected Player player;
    protected string[] actions = { };
    protected bool currentlySelected = false;
    #endregion

    #region Protected Methods

    protected virtual void Awake()
    {

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

    }
    #endregion

    #region Public Methods

    public void SetSelected (bool selected)
    {
        currentlySelected = selected; 
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

    #endregion

    #region Private Methods
    
    void ChangeSelection(WorldObject worldObject, Player controller)
    {
        SetSelected(false); 
        if (controller.SelectedObject)
        {
            controller.SelectedObject.SetSelected(false);
            controller.SelectedObject = worldObject;
            worldObject.SetSelected(true); 
        }
    }
    #endregion

}
