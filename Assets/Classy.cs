using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classy : MonoBehaviour {

    public int myInt = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void YellFoo()
    {
        Debug.Log("Foo");
    }

    public void Set10()
    {
        myInt = 10;
    }

    public void set20()
    {
        myInt = 20;
    }


}
