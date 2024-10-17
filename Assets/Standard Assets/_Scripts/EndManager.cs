using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndManager : MonoBehaviour
{
    public GameObject Ghost_Manager;
    public GameObject player;
    public GameObject inventory;
    public GameObject blackout;
    RawImage blackout_img;
    bool inEZone = false;
    bool finalKey = false;
    public bool end_trig = false;
    public AudioClip ender;
    float bltimer = 0.0f;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        blackout_img = blackout.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        Color alph = blackout.GetComponent<RawImage>().color;
        inEZone = player.GetComponent<Safe_check>()._inEndZone;
        finalKey = Inventory.keys[30];
        if (inEZone == true && finalKey == true)
        {
            end_trig = true;
        }
        if (end_trig == true)
        {
            Ghost_Manager.SetActive(false);
            if (audio.isPlaying == false)
            {
                audio.Play();
            }
            bltimer += Time.deltaTime;
            if (bltimer<=10.0f&&alph.a<255.0f)
            {
                alph.a += 0.001f;
                blackout_img.color = alph;

            }
            else if(bltimer > 10.0f){ 
                SceneManager.LoadScene("Ending");
            }


        }

    }
}
