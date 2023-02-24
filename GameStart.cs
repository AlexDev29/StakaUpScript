using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.transform.position = new Vector2(-2.2f, 2);
    }
	
	// Update is called once per frame
	void Update () {
        if(this.gameObject.transform.position.x >= -3.7)
        {
            this.gameObject.transform.Translate(-0.03f, 0, 0);
        }
    }
}
