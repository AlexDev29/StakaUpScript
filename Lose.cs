using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour {


    private bool menuLose = false;
    public GameObject menu;
    public GameObject pause;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (menuLose)
        {
            menu.SetActive(true);
            pause.SetActive(false);
        }
        else
        {
            menu.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            menuLose = true;
        }
    }
}
