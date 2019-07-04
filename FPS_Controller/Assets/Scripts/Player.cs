using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



//extra
[RequireComponent(typeof(PlayerSetup))]

public class Player : NetworkBehaviour {


    private bool isDead = false;

    public bool IsDead { get { return isDead;} protected set { isDead = value; } }


   



    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private GameObject deathEffect;

    [SyncVar]
    private int currentHealth = 100;
    
    public float GetHealthPct()
    {
        return (float)currentHealth / maxHealth;
    }

    private void Start()
    {
        SetDefaultValues();
    }
    public void SetDefaultValues()
    {
       // isDead = false;
        currentHealth = maxHealth;
    }

    public void SetupPlayer()
    {
        if (isLocalPlayer)
        {
            
            //Switch cameras
           // GameManager1.instance.SetSceneCameraActive(false);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);
        }

        
    }



    [ClientRpc]
    public void RpcTakeDamage(int amount,GameObject p)
    {


        if (isDead)
        {
            return;
        }
        currentHealth -= amount;
        Debug.Log(transform.name+"has"+currentHealth+"health");

        //float c;
        //c = (float) currentHealth / maxHealth;
        ////GetComponent<PlayerUI>()
        //healthBarFill.localScale = new Vector3(1f, c, 1f);



        if (currentHealth<=0)
        {
            Die(p);
        }
    }
    
    private void Die(GameObject p)

    {
        IsDead = true;

        GameObject gfxDeath=(GameObject)Instantiate(deathEffect,transform.position,Quaternion.identity);
        Destroy(gfxDeath,3f);

        Debug.Log(transform.name +"is dead");


       // GameObject ob= GetComponent<PlayerSetup>().playerUIInstance;
        //NetworkServer.Destroy(ob);

        NetworkServer.Destroy(p);

        Debug.Log(transform.name+"is DEAD");

    }
    

    

}
