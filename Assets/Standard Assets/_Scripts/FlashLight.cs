using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{

    private bool isFlashOn=false;
    public Light flash;
    private AudioSource flashAudio;
    public AudioClip[] flashSound;
    //켤때 끌때 2개니까 배열로 해봄
    //끌때소리[0], 킬때소리[1] 지정용
    private float smooth = 5.0f;//손전등 회전 속도

    private float obstacleDist = 10.0f;//손전등 비추는 방향 보정을 위한 장애물과의 거리(PlayerInteract 스크립트에서 getObstacleDist함수를 통해 값을 넘겨받을 예정)
    private float correction = 0.4f;//위치 보정 배율
    Vector3 dir;


    public void getObstacleDist(float dist)
    {
        obstacleDist = Mathf.Abs(dist);
    }

    // Start is called before the first frame update
    void Start()
    {
        flash = GetComponent<Light>();
        flashAudio = GetComponent<AudioSource>();


        

    }

    public void FlashOn()//다른 스크립트에서 플래시 접근할때 쓸수있는 함수
    {
        //isFlashOn = !isFlashOn;

        if (isFlashOn)
        {
            isFlashOn = !isFlashOn;
            flash.enabled = false;
            flashAudio.clip = flashSound[0];
            flashAudio.Play();
            //FlashOn();
        }
        else
        {
            isFlashOn = !isFlashOn;
            flash.enabled = true;
            flashAudio.clip = flashSound[1];
            flashAudio.Play();
            //FlashOn();


        }
    }


    // Update is called once per frame
    void Update()
    {

        //obstacleDist의 값이 일정 각도 아래를 바라보면 갑자기 크게 줄어들음 -> 수동 보정이 필요(2를 더해주면 부드럽게 전환됨)
        if(obstacleDist < 1.0f && GameObject.Find("FirstPersonCharacter").transform.localRotation.eulerAngles.x < 345f)
        {
            float pinDist = obstacleDist + 2.0f;
            dir = (Camera.main.transform.forward + (Camera.main.transform.right * (-1 / pinDist) * correction) + (Camera.main.transform.up * (1 / pinDist) * correction * 2f));
        }
        else
        {
            
            dir = (Camera.main.transform.forward + (Camera.main.transform.right * (-1 / obstacleDist) * correction) + (Camera.main.transform.up * (1 / obstacleDist) * correction * 1.5f));

        }




        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, smooth * Time.deltaTime);

        //gameObject.transform.rotation = rot;
        //카메라가 바라보는 방향으로 손전등 비추기. 손전등이 사실감때문에 약간 오른쪽 아래에 있어서 가운데로 오게 하기위해 약간 보정하였음.








        if (Input.GetKeyDown(KeyCode.V))
        {
            FlashOn();



        }
    }
}
