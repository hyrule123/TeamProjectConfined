using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoReader : MonoBehaviour
{
    Vector3 dir;

    public int reading_trig=0;
    private GameObject fpsControl;



    public void read()
    {
        Debug.Log("메모함수는들어가짐");
        GameObject canvas = transform.GetChild(0).gameObject;
        Debug.Log("자식오브젝트불러오기");//자식 오브젝트 canvas불러옴
        if (reading_trig == 0)//만약 메모가 안 켜져있으면
        {
            canvas.SetActive(true);
            reading_trig = 1;//메모를 키고 트리거 활성화
            Debug.Log("캔버스 키고 리딩트리거1");
        }
        else
        {
            canvas.SetActive(false);
            reading_trig = 0;//메모를 닫고 트리거도 비활성화
            Debug.Log("열린거확인하고닫기");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        fpsControl = GameObject.Find("FirstPersonCharacter");
    }

    // Update is called once per frame
    void Update()
    {
/*
        if (reading_trig == 1)
            
        //메모를 보는 중에는 카메라 회전, 캐릭터 이동 기능 비활성화.
        else
         */   

        /*if (Input.GetKeyDown(KeyCode.F))
        {
            if (reading_trig == 1)//f를 눌렀을 때 메모가 켜져있어도
            {
                GameObject canvas = transform.GetChild(0).gameObject;//자식 오브젝트 canvas불러옴
                canvas.SetActive(false);//메모 제거
            }
        }*/


    }
}
