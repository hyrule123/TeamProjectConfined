using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class ImageManager : MonoBehaviour
{
    public GameObject img_01;
    public GameObject img_02;
    public GameObject img_03;
    public GameObject img_04;
    public GameObject img_05;
    public GameObject img_06;
    private int count = 1;
    float timer = 0.0f;
    int trigger = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            if (trigger == 0) trigger = 1;
           /* while (timer < 0.6f)
            {
                
                if (trigger == 1) timer += Time.deltaTime;*/
                switch (count)
                {
                    case 1:
                        count++;
                        img_01.gameObject.SetActive(true);
                        break;
                    case 2:
                        count++;
                        img_02.gameObject.SetActive(true);
                        break;
                    case 3:
                        count++;
                        img_03.gameObject.SetActive(true);
                        break;
                    case 4:
                        img_01.gameObject.SetActive(false);
                        img_03.gameObject.SetActive(false);
                        img_02.gameObject.SetActive(false);
                        count++;
                        img_04.gameObject.SetActive(true);
                        break;
                    case 5:
                        count++;
                        img_05.gameObject.SetActive(true);
                        break;
                    case 6:
                        count++;
                        img_06.gameObject.SetActive(true);
                        break;
                case 7:
                    SceneManager.LoadScene("Show");
                    break;
                }

            //}
        }
    }
}
