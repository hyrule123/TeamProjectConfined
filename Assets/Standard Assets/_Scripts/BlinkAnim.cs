using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnim : MonoBehaviour
{
    float time;

    // Update is called once per frame
    void Update()
    {
        if (time < 0.04f)
        {
            this.GetComponent<RawImage>().color = new Color(1, 1, 1, 1 - time*12);
        }
        else
        {
            this.GetComponent<RawImage>().color = new Color(1, 1, 1, time*12) ;
            if(time > 0.14f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;
        
    }
}
