using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool[] keys = new bool[31]; // 문의 개수만큼 배열 설정하면 된다.
        
    //0 = 키 없음(못 열음 - 109-1번 문 못열음)
    //1~5 = 지하(5번 = 지하 입구 문)
    //11~19 = 1층(101호~109호)
    //21-20 = 2층(201호~209호)


    void Start()
    {
        // 열쇠 없이 열리는 방은 키와 방의 Index를 같게 설정하고 true로 설정하면 된다.
           
        keys[1] = false;
        keys[2] = false;
        keys[3] = false;
        keys[4] = true;
        keys[5] = true;
        keys[11] = true;
        keys[12] = true;
        keys[13] = true;
        keys[14] = false;
        keys[15] = false;
        keys[16] = false;
        keys[17] = false;
        keys[18] = false;
        keys[19] = false;
        keys[21] = false;
        keys[22] = true;
        keys[23] = true;
        keys[24] = false;
        keys[25] = false;
        keys[26] = false;
        keys[27] = false;
        keys[28] = false;
        keys[29] = false;
        keys[30] = false;
    }

}