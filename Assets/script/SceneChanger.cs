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
            if(test)
            {
                if(playerHuman.GetComponent<MainPlayerController>().GetMode())
                {
                    player.SetActive(false);
                    playerHuman.GetComponent<CharacterController>().enabled = false;
                    playerHuman.transform.localScale = new Vector3(1,1,1);
                    playerHuman.transform.position = player.transform.position;

                    playerHuman.GetComponent<CharacterController>().enabled = true;
                    playerHuman.GetComponent<MainPlayerController>().SetMode(false);
                    test = false;
                }else
                {
                    if(!playerHuman.GetComponent<MainPlayerController>().GetMode() && playerHuman.GetComponent<MainPlayerController>().IsNearAutel())
                    {
                        player.SetActive(true);
                        playerHuman.GetComponent<CharacterController>().enabled = false;
                        playerHuman.transform.localScale = new Vector3(10,10,10);
                        playerHuman.transform.position = god.transform.position;

                        playerHuman.GetComponent<CharacterController>().enabled = true;
                        playerHuman.GetComponent<MainPlayerController>().SetMode(true);
                        test = false;
                        playerHuman.GetComponent<MainPlayerController>().SetNearAutel(false);
                    }
                }
            }
        }else
        {
            test = true;
        }
    }
}
