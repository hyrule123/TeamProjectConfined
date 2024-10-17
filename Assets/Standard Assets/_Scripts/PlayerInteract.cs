using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

//플레이어 상호작용 스크립트
public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 5f;//상호작용 가능 거리
    public bool isPlayingPuzzle = false;//퍼즐 플레이 여부
    private Image iaPanel;
    private Text iaText;//interactiontext, 상호작용 UI 텍스트를 표시해 주는UI 등록
    private GameObject flash;//플래시에 장애물과의 거리 인자 전달을 위함
    private Inventory inventory;
    private LightController LightController;//모든 조명 켜고 끄는 오브젝트와 연결
    public AudioClip memo_sfx;//상호작용 시 음향효과들
    public AudioClip key_sfx;
    public AudioClip Ldoor_sfx;
    public AudioClip ULdoor_sfx;
    AudioSource Psfx;






    // Start is called before the first frame update
    void Start()
    {
        Psfx = GetComponent<AudioSource>();
        iaText = GameObject.Find("Canvas").transform.Find("InteractionUI").GetComponent<Text>();
        //iaPanel = GameObject.Find("Canvas").transform.Find("InteractionPanel").GetComponent<Image>();
        flash = GameObject.Find("FlashLight");
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        LightController = GameObject.Find("LightController").GetComponent<LightController>();        

    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        //Debug.DrawRay(transform.position, transform.forward, Color.red, 0.05f, false);
        
        Ray ray = new Ray(transform.position, transform.forward);//플레이어가 보고 있는 방향으로 보이지 않는 광선 발사
        RaycastHit hit;





        if(Physics.Raycast(ray, out hit, interactDistance))//광선이 interacDistance 안에서 무언가와 충돌하면
        {
            

            flash.GetComponent<FlashLight>().getObstacleDist(hit.distance);


            if (hit.collider.CompareTag("Door"))//그 충돌한 오브젝트의 태그가 'Door'이면
            {

                DoorScript doorScript = hit.collider.transform.GetComponent<DoorScript>(); // DoorScript에 접근하는 변수

                if (doorScript.open == false)
                {
                    iaText.text = "<F>\n문 열기";
                    //iaPanel.gameObject.SetActive(true);
                }
                else
                {
                    iaText.text = "<F>\n문 닫기";
                    //iaPanel.gameObject.SetActive(true);
                }

                if (Input.GetKeyDown("f"))
                {

                    if (doorScript == null) return;

                    if (Inventory.keys[doorScript.index] == true)
                    {
                        Psfx.PlayOneShot(ULdoor_sfx);
                        doorScript.ChangeDoorState();//Door의 상태를 바꾼다
                    }
                    else Psfx.PlayOneShot(Ldoor_sfx);
                }
            }
            else if (hit.collider.CompareTag("Key")) // 광선이 'key' 태그에 충돌하면
            {
                iaText.text = "<F>\n열쇠 줍기";
                //iaPanel.gameObject.SetActive(true);

                if (Input.GetKeyDown("f"))
                {
                    Psfx.PlayOneShot(key_sfx);
                    Inventory.keys[hit.collider.GetComponent<Key>().index] = true; // 키인벤토리에 있는 키배열을 ture 변경하여 문이 열린다.

                        Destroy(hit.collider.gameObject);

                }
            }
            else if (hit.collider.CompareTag("Memo"))// 그 충돌한 오브젝트의 태그가 'Memo'이면
            {

                iaText.text = "<F>\n메모 읽기";
                //iaPanel.gameObject.SetActive(true);

                if (Input.GetKeyDown("f"))
                {
                    Psfx.PlayOneShot(memo_sfx);
                    MemoReader memoReader = hit.collider.transform.GetComponent<MemoReader>(); //MemoReader에 접근하는 변수
                    memoReader.read(); //읽기 메서드 실행
                }
            }
            else if (hit.collider.CompareTag("Gas") && gameObject.transform.Find("HeldJerryCan").gameObject.activeSelf == false)
            {
                iaText.text = "<F>\n기름통 들기";
               // iaPanel.gameObject.SetActive(true);

                if (Input.GetKeyDown("f"))
                {
                    gameObject.transform.Find("HeldJerryCan").gameObject.SetActive(true);

                }
            }
            else if (hit.collider.CompareTag("Generator") && gameObject.transform.Find("HeldJerryCan").gameObject.activeSelf)
            //발전기 앞에서 기름통을 가지고 있을 경우 활성화되는 상호작용 메뉴
            {
                iaText.text = "<F>\n연료 넣기";
              //  iaPanel.gameObject.SetActive(true);

                if (Input.GetKeyDown("f"))
                {
                    LightController.GeneratorOn();
                    gameObject.transform.Find("HeldJerryCan").gameObject.SetActive(false);//기름통 비활성화
                }
            }
            else if (hit.collider.CompareTag("Puzzle"))
            {
                if (GameObject.Find("Puzzle").transform.childCount == 0)//퍼즐이 없을떄만 알림이 뜸
                {
                    iaText.text = "<F>\n퍼즐 풀기";
                    //iaPanel.gameObject.SetActive(true);
                }
                else
                {
                    iaText.text = "";
                   // iaPanel.gameObject.SetActive(false);
                }

                if (Input.GetKeyDown("f"))
                {
                    GameObject.Find("Puzzle").GetComponent<PuzzleScript>().ChangePlaying(true);
                }
            }
        }
        else
        {
            iaText.text = "";
           // iaPanel.gameObject.SetActive(false);
            flash.GetComponent<FlashLight>().getObstacleDist(interactDistance);
            
        }
        
        
            
        
    }
}

