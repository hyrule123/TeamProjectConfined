using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*문 스크립트 사용하는법
1.스크립트를 문에 등록한다
2.문짝 태그를 'Door'로 변경한다
3.인스펙터에서 'Door Sound'의 사이즈를 2로 올린 뒤 나오는 빈칸 두개에 순서대로 Sound 폴더에 있는 'DoorClose', 'DoorOpen' 사운드를 등록한다.
4.Add Component로 Box Collider을 추가하고 isTrigger 체크박스에 체크한다(문열면 안보이는 광선이 발사되고 이 광선이 여기에 부딪히면 문열림 이벤트가 발생)
5.Add Componend로 AudioSource를 추가하고, Play On Awake 체크박스를 해제한다.
6.잘 작동하는지 확인하고, 문열리는 각도가 안 맞을 시 인스펙터에서 각도를 조정한다.
*/

public class DoorScript : MonoBehaviour
{
    public int index = 0;
    public bool open = false;
    [SerializeField] private float doorOpenAngle = -90f;//문 열리는 각도가 안맞으면 인스펙터에서 조절할 것
    [SerializeField] private float doorCloseAngle = 0f;
    public float smoot = 3f;//문 열리는 속도
    private AudioSource doorAudio;
    public AudioClip[] doorSound;//인스펙터에서 직접 등록할 예정. 0=닫힘, 1=열림

    private float x, z;//오일러앵글 값을 받아올 변수(x와 z값은 무조건 원본 값 그대로 가지고 있어야 함. 안그러면 에러남.

    public GameObject otherSide = null; //양문일 경우 인스펙터에서 같이 열릴 반대쪽 문 등록할 것. 같이 열림
    private bool otherSwitch = false; //양문일 경우 내가 상호작용으로 열기를 눌렀는지 반대쪽 문때문에 자동으로 열린건지 구분하기 위함

    public bool delayCloseOn = true;
    private float delayClose = 0; //일정시간 후 문 자동닫힘

    public bool isLocked;

    // Start is called before the first frame update
    void Start()
    {

        doorAudio = gameObject.GetComponent<AudioSource>();

        x = gameObject.transform.rotation.eulerAngles.x;
        z = gameObject.transform.rotation.eulerAngles.z;
        //해당 값은 한번만 받으면 됨(문은 y축으로만 회전하기 때문)

    }

    public void ChangeDoorState()
    {
        open = !open;
        if(open)
        {
            doorAudio.clip = doorSound[1];
            doorAudio.Play();
            otherSwitch = true;
        }
        else
        {
            doorAudio.clip = doorSound[0];
            doorAudio.Play();
            otherSwitch = true;
        }

    }




    // Update is called once per frame
    void Update()
    {


        if (open)
        {
            

            Quaternion targetRotation = Quaternion.Euler(x, doorOpenAngle, z);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);
            //또는 transform.localRotation을 사용해도 됨.
            if (otherSide != null)
            {
                

                if (otherSwitch == true)
                {
                    otherSide.GetComponent<DoorScript>().open = true;
                    otherSwitch = false;

                }


            }

            if (delayCloseOn)
            {
                delayClose += Time.deltaTime;
                if (delayClose > 10.0f)
                {
                    GetComponent<DoorScript>().open = false;
                    if(otherSide != null)
                    {
                        otherSide.GetComponent<DoorScript>().open = false;
                    }
                    delayClose = 0;
                    doorAudio.clip = doorSound[0];
                    doorAudio.Play();
                }
            }

        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(x, doorCloseAngle, z);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smoot * Time.deltaTime);
            if (otherSide != null)
            {
                delayClose = 0;

                if (otherSwitch == true)
                {
                    otherSide.GetComponent<DoorScript>().open = false;
                    otherSwitch = false;
                }


            }
        }
        
    }

    

}
