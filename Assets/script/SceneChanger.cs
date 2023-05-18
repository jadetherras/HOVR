using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("Player perso")]
    public GameObject player;
    public GameObject god;

    [Header("Player cameras")]
    public GameObject playerHuman;

    protected bool test = false;

    // Update is called once per frame
    void Update()
    {   
        bool b_4 = OVRInput.Get(OVRInput.Button.Four, OVRInput.Controller.Touch);

        if(b_4)
        {
            if(playerHuman.GetComponent<MainPlayerController>().GetB4())
            {
                if(playerHuman.GetComponent<MainPlayerController>().GetMode())
                {
                    player.transform.SetParent(null);
                    player.SetActive(false);
                    playerHuman.GetComponent<CharacterController>().enabled = false;
                    playerHuman.transform.localScale = new Vector3(1,1,1);
                    playerHuman.transform.position = player.transform.position;

                    playerHuman.GetComponent<CharacterController>().enabled = true;
                    playerHuman.GetComponent<MainPlayerController>().SetMode(false);
                    playerHuman.GetComponent<MainPlayerController>().SetB4(false);
                }else
                {
                    if(!playerHuman.GetComponent<MainPlayerController>().GetMode() && playerHuman.GetComponent<MainPlayerController>().IsNearAutel())
                    {
                        player.transform.position = playerHuman.transform.position;
                        player.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                        player.SetActive(true);
                        playerHuman.GetComponent<CharacterController>().enabled = false;
                        playerHuman.transform.localScale = new Vector3(15,15,15);
                        playerHuman.transform.position = god.transform.position;

                        playerHuman.GetComponent<CharacterController>().enabled = true;
                        playerHuman.GetComponent<MainPlayerController>().SetMode(true);
                        playerHuman.GetComponent<MainPlayerController>().SetB4(false);
                        playerHuman.GetComponent<MainPlayerController>().SetNearAutel(false);
                    }
                }
            }
        }else
        {
            playerHuman.GetComponent<MainPlayerController>().SetB4(true);
        }
    }
}
