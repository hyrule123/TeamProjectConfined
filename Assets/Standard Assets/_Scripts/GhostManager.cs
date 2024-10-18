using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using System.Collections;



[RequireComponent(typeof(AudioSource))]
public class GhostManager : MonoBehaviour
{
    public GameObject player;

    private const float HauntingAudio_MaxPlayTime = 13.0f;
    public static float leftTime = HauntingAudio_MaxPlayTime;

    public float ghost_timer;
    RawImage blackout_img;
    public bool _caught = false;
    float delay_time = 5.0f;
    public GameObject textForTest;
    public GameObject blackout;
    public AudioClip _caughtSound;
    public AudioClip _hauntingSound;
    int caughtcount = 0;
    float blackouttimer = 0.0f;
    float fadeintimer = 0.0f;
    bool position_set = false;
    public GameObject ghos_img;
    bool firsttime = true;


    // Start is called before the first frame update
    void Start()
    {
        Reset_DelayTime();//시작시 3~7초의 랜덤 딜레이시간(귀신에게 쫓기기 전까지의 시간)
#if DEBUG
        delay_time = 180.0f;
#endif

        blackout_img = blackout.GetComponent<RawImage>();
        ghost_timer = leftTime;

    }

    // Update is called once per frame
    void Update()
    {


        Color alph = blackout.GetComponent<RawImage>().color;
        AudioSource GhostAudio = GetComponent<AudioSource>();
        bool _safe = player.GetComponent<Safe_check>()._safeCheck;//안전한지 체크한 값을 가졍옴

        if (_caught == false)//만약 잡히지 않은 상태이고
        {
            if (_safe == false||firsttime==true)//안전하지 않다면(세이프존이 아니라면)
            {
                if (delay_time > 0.0f)//딜레이시간이 아직 남아있다면
                {
                    delay_time -= Time.deltaTime;
                }
                else//딜레이 시간이 다 닳면 귀신에게 쫓기기 시작함
                {
                    if (GhostAudio.isPlaying == false)//만약 소리가 재생중이지 않다면 쫓기는 소리 시작
                    {
                        GhostAudio.clip = _hauntingSound;//쫓기는 사운드로 클립을 바꾸고
                        GhostAudio.time = HauntingAudio_MaxPlayTime - leftTime;//사운드의 시작 시점을 HauntingAudio_MaxPlayTime에서 빼준다.
                        if(GhostAudio.time < 0.0f)
                        {
                            GhostAudio.time = 0.0f;
                        }
                        GhostAudio.Play();//실행
                    }
                   // textForTest.GetComponent<Text>().text = "It's Coming";//쫓기기 시작함 테스트용 텍스트
                    if (ghost_timer > 0.0f)//만약 고스트 타이머가 남았으면
                    {
                        ghost_timer -= Time.deltaTime;//타이머감소
                    }
                    else//만약 쫓기는 타이머가 끝나면
                    {
                        ghost_timer = leftTime;//타이머 리셋
                        Reset_DelayTime();//타이머리셋

#if DEBUG
                        delay_time = 180.0f;
#endif

                        _caught = true;//잡힙 상태로 바꿈
                    }
                }
            }
            else if (_safe == true&&firsttime==false)//만약 안전한 상태면
            {
                GetComponent<AudioSource>().Stop();
               // textForTest.GetComponent<Text>().text = "Safe";
                ghost_timer = leftTime;
                Reset_DelayTime();//모두리셋
            }
        }

        else if (_caught == true)//만약 잡히면
        {


            //리스폰 지점인 206호 문 닫힘
            GameObject.Find("Door206_L").GetComponent<DoorScript>().open = false;
            GameObject.Find("Door206_R").GetComponent<DoorScript>().open = false;

            //들고있던 가스통 없어짐
            if (GameObject.Find("FirstPersonCharacter").transform.Find("HeldJerryCan").gameObject.activeSelf)
            {
                GameObject.Find("FirstPersonCharacter").transform.Find("HeldJerryCan").gameObject.SetActive(false);
            }


            if (firsttime == true)
            {
                if (position_set == false)
                {
                    blackouttimer += Time.deltaTime;
                    if (blackouttimer >= 1.0f && blackouttimer < 5.0f && alph.a < 255)
                    {
                        alph.a += 0.05f;
                        blackout_img.color = alph;

                    }
                    else if (blackouttimer >= 5.0f)
                    {

                        player.transform.position = new Vector3(-0.3049586f, 5.1f, -9.106f);
                        position_set = true;

                    }
                }
                else if (position_set == true)
                {
                    blackouttimer = 0.0f;
                    fadeintimer += Time.deltaTime;
                    if (fadeintimer >= 1.0f && alph.a > 0.0f)
                    {
                        alph.a -= 0.05f;
                        blackout_img.color = alph;
                    }
                    else if (alph.a <= 0.0f)
                    {
                        _caught = false;
                        fadeintimer = 0.0f;
                        position_set = false;
                        firsttime = false;
                    }
                }
            }
            else
            {
               // textForTest.GetComponent<Text>().text = "Caught";//잡혔다고하고
                ghos_img.SetActive(true);
                GhostAudio.clip = _caughtSound;//잡히는 소리내고
                if (GhostAudio.isPlaying == false && caughtcount == 0)
                {
                    GhostAudio.PlayOneShot(_caughtSound);
                    caughtcount = 1;//이건 이후에 잡히고 자리 리셋할 때 바꿔줄 값
                }
                if (position_set == false)
                {
                    blackouttimer += Time.deltaTime;
                    if (blackouttimer >= 1.0f && blackouttimer < 5.0f && alph.a < 255)
                    {
                        alph.a += 0.05f;
                        blackout_img.color = alph;

                    }
                    else if (blackouttimer >= 5.0f)
                    {

                        player.transform.position = new Vector3(-0.3049586f, 5.1f, -9.106f);
                        position_set = true;
                        caughtcount = 0;
                        

                    }
                }
                else if (position_set == true)
                {
                    ghos_img.SetActive(false);
                    blackouttimer = 0.0f;
                    fadeintimer += Time.deltaTime;
                    if (fadeintimer >= 1.0f && alph.a > 0.0f)
                    {
                        alph.a -= 0.05f;
                        blackout_img.color = alph;
                    }
                    else if (alph.a <= 0.0f)
                    {
                        _caught = false;
                        fadeintimer = 0.0f;
                        position_set = false;
                    }
                }
            }


            Reset_DelayTime();
        }
    }

    private void Reset_DelayTime()
    {
#if DEBUG
        delay_time = 180.0f;
#else
        delay_time = Random.Range(3.0f, 5.0f);
#endif
    }
}
