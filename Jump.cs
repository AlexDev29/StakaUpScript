using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    private GameObject player;
    private bool droite;
    private bool gauche;

    public float timeClic = 0; //detecter le temps des clics
    public float timeEntreClic = 0; //detecter le temps entre les clics


    public float jumpX;

    public float jumpY1;
    public float jumpY2;
    public float jumpY3;

    public bool doubleJump = false;
    public bool isJumping = true;
    public bool missDoubleJump = false;

    public bool needJumpPetit = false;
    public bool needJumpMoyen = false;
    public bool needJumpGrand = false;


    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");

        jumpX = 1.23f;
        jumpY2 = 5f;

        needJumpPetit = false;
        needJumpMoyen = false;
        needJumpGrand = false;
        doubleJump = false;
    }

    // Update is called once per frame
    void Update () {        
    
        if (Input.touchCount > 0)
        {
            timeClic++;
            timeEntreClic = 0;
            if (needJumpPetit)
            {
                jumping(timeClic);
                isJumping = true;
            }
            if (needJumpMoyen)
            {
                jumping(timeClic);
            }
            if (needJumpGrand)
            {
                jumping(timeClic);
            }


            if (timeClic > 30)
            {
                timeClic = 0;
                missDoubleJump = true;
            }

            if (doubleJump && !missDoubleJump)
            {
                jumping(timeClic);
                doubleJump = false;
                missDoubleJump = true;
            }
        }


        if (Input.touchCount == 0)
        {
            timeClic = 0;
            needJumpMoyen = false;
            needJumpGrand = false;
            if (needJumpPetit && !isJumping)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(jumpX, 0);
            }
            if (Input.touchCount == 0 && !needJumpPetit && !isJumping)
            {
                Lose();
            }

            if (isJumping && !needJumpPetit && !missDoubleJump)   //double saut
            {     
                if(timeEntreClic > 10)
                {
                    timeEntreClic = 0;
                    doubleJump = false;
                    missDoubleJump = true ;
                }
                else
                {
                    timeEntreClic++;
                    doubleJump = true;
                }
            }


        }


    }
    

    //Definit la velocité du joueur en fonction du temps de clique
    void jumping(float time)
    {

        if(time <= 5)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(jumpX, 2);
            needJumpMoyen = true;
        }
        else if (time <= 20)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(jumpX, 4);
            needJumpGrand = true;
            needJumpMoyen = false;
        }
        else if (time < 30)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(jumpX, 5);
            needJumpGrand = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "barreDroite" && !droite)  //evite d'activer 2 fois la barre droite ou gauche
        {
            jumpX = -jumpX;
            missDoubleJump = false;
            needJumpPetit = true;
            changerHauteur();
            droite = true;
            gauche = false;
        }
        if (collision.tag == "barreGauche" && !gauche)
        {
            jumpX = -jumpX;
            missDoubleJump = false;
            needJumpPetit = true;
            changerHauteur();
            droite = false;
            gauche = true;
        }

    }

   

    private void OnTriggerStay2D(Collider2D collision)    //Peut sauter quand il est au dessus de la barre
    {
        if (collision.tag == "barre")
        {
            needJumpPetit = true;
            missDoubleJump = false;
            isJumping = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)    //Ne peut plus sauter quand quitte la barre
    {
        if (collision.tag == "barre" || collision.tag == "barreGauche" || collision.tag == "barreDroite")
        {
            needJumpPetit = false;
        }
    }

    //Change la hauteur des barres de manière aléatoire parmis les 4 différentes valeurs possibles
    void changerHauteur()
    {
        GameObject[] tableBarre = GameObject.FindGameObjectsWithTag("barre");
        for (int i = 0; i <= 3; i++)
        {
            float random = Random.value;
            if(random < 0.25)
            {
                tableBarre[i].transform.localPosition = new Vector2(tableBarre[i].transform.localPosition.x, -0.5f);
            }
            else if(random < 0.5)
            {
                tableBarre[i].transform.localPosition = new Vector2(tableBarre[i].transform.localPosition.x, -0.3f);
            }
            else if(random < 0.75)
            {
                tableBarre[i].transform.localPosition = new Vector2(tableBarre[i].transform.localPosition.x, 0.1f);
            }
            else
            {
                tableBarre[i].transform.localPosition = new Vector2(tableBarre[i].transform.localPosition.x, 0.4f);
            }
        }
    }


    void Lose()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(jumpX, -5);
    }

}
