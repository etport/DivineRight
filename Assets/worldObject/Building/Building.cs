using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : WorldObject {


    #region Override Methods
    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();	
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update(); 	
	}

    protected override void OnGUI()
    {
        base.OnGUI();
    }

    #endregion 
}
