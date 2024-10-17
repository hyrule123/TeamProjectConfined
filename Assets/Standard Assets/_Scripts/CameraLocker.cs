using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
/*중요!!
스탠다드 에셋의 구성요소에 접근하기 위해서는

1.접근하려는 스크립트가 StandardAssets 폴더 안에 존재하거나
StandardAssets 폴더 안에서 필요한 구성요소를 바깥으로 꺼내와야 한다(Assets 폴더로)

2.그리고 위의 using UnityStandardAssets.Characters.FirstPerson;처럼
네임스페이스도 사용해야 한다

아니면 GetComponent 해도 접근 안되니까 주의하세요


이런 이유로 저희가 만든 Scripts 폴더를 Standard Assets 밑으로 옮겨 놨습니다 
*/

public class CameraLocker : MonoBehaviour
{
    private GameObject cameraLock;//카메라 및 위치 고정
    public GameObject puzzle;
    private bool isLocked;

    // Start is called before the first frame update
    void Start()
    {
        cameraLock = GameObject.Find("FPSController");
        puzzle = GameObject.Find("Puzzle");
        isLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        //만약 퍼즐 플레이할떄 시야를 고정하고 싶다면
        //if(puzzle.GetComponent<PuzzleScript>().isPlaying | GameObject.Find("Memo_Canvas"))
        if(GameObject.Find("Memo_Canvas"))
        {
            isLocked = true;
        }
        else
        {
            isLocked = false;
        }


        if (isLocked)
            cameraLock.GetComponent<FirstPersonController>().enabled = false;
        //메모를 보는 중에는 카메라 회전, 캐릭터 이동 기능 비활성화.
        else
            cameraLock.GetComponent<FirstPersonController>().enabled = true;
    }
}
