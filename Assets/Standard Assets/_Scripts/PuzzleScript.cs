using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PuzzleScript : MonoBehaviour
{
    public bool isPlaying;//플레이중인지 여부 확인. 카메라 회전 불가 및 마우스 커서 보이기 활성화해줘야 함.
    public GameObject puzzlePrefab;//인스펙터에서 프리팹 등록 예정


    public void ChangePlaying(bool status)
    {
        isPlaying = status;
        if(isPlaying && !GameObject.Find("puzzle33"))
        {
            GameObject puzzle33 = Instantiate(puzzlePrefab);
            puzzle33.transform.SetParent(this.transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isPlaying)
        {
            
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isPlaying)
            {
                ChangePlaying();
            }
        }
        */

    }
        
}

