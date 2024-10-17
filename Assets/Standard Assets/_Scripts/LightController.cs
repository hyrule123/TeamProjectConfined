using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private float deltatime;
    [SerializeField] private float duration;//제한시간
    private bool isOn;//PlayerInteract로부터 받아와서 정보를 저장
    private GameObject asylum;//어사일럼에서 자식 오브젝트를 찾는 형태로 접근
    [SerializeField] private AudioClip[] genSound;//발전기 On/Off 사운드 등록용. 인스펙터에서 등록해야함.(0=off,1=on)
    private AudioSource genAudio;

    //104호 조명 이벤트 관련 변수들
    //keyStatue=true 되면 104호 영사기의 조명을 킴 -> 이후 열쇠를 주우면 104호의 빨간 조명을 킴과 동시에 스크린에 귀신 화면 투영
    [SerializeField] private AudioClip[] lightSound;//104호(Theater) 사운드 등록용.
    private AudioSource lightAudio;
    [SerializeField] private Inventory haveKeys;
    [SerializeField] private AudioClip screamSound;//genAudio에 등록된 AudioSource를 통해 재생할 예정.
    private bool isHorrorSoundPlayed;



    public void GeneratorOn()
    {
        isOn = true;
        deltatime = 0f;
    }


    // Start is called before the first frame update
    void Start()
    {
        asylum = GameObject.Find("Asylum");
        genAudio = gameObject.GetComponent<AudioSource>();
        duration = 120.0f;

        haveKeys = GameObject.Find("Inventory").GetComponent<Inventory>();
        isHorrorSoundPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn)
        {
            deltatime += Time.deltaTime;//시간 추가해주고
            if(!asylum.transform.Find("Lights_All").gameObject.activeSelf)//불이 꺼져 있다면
            {
                asylum.transform.Find("Lights_All").gameObject.SetActive(true);//불을 켜준다

                GameObject.Find("FlashLight").GetComponent<FlashLight>().FlashOn();

                genAudio.clip = genSound[1];//발전기 돌아가는 소리
                genAudio.Play();
                
            }

            if(deltatime > duration)//제한시간을 초과하면
            {
                asylum.transform.Find("Lights_All").gameObject.SetActive(false);//다시 불을 꺼준다.

                genAudio.clip = genSound[0];//발전기 꺼지는 소리
                genAudio.Play();
                deltatime = 0;
                isOn = false;
            }

        }

        if(Inventory.keys[14] == true)//기둥 부근 열쇠를 먹으면 극장쪽 영사기 불이 들어옴
        {
            asylum.transform.Find("TheaterLights").GetChild(3).gameObject.SetActive(true);
            
        }

        if(Inventory.keys[30] == true)//극장 열쇠를 습득 시 놀래키기
        {
            asylum.transform.Find("TheaterLights").GetChild(0).gameObject.SetActive(true);
            asylum.transform.Find("TheaterLights").GetChild(1).gameObject.SetActive(true);
            asylum.transform.Find("TheaterLights").GetChild(2).gameObject.SetActive(true);


            if(isHorrorSoundPlayed == false)
            {
                genAudio.clip = screamSound;
                genAudio.Play();
                isHorrorSoundPlayed = true;
            }


        }

    }
}
