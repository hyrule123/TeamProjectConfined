using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int index = 0; // 잠긴 문과 같은 index를 설정해줘야한다.






    private void OnDestroy()
    {
        //난이도 조절을 위해 키를 하나씩 습득할 때마다 제한시간을 0.8초씩 감소시킬 예정임.
        GhostManager.leftTime -= 1.0f;
    }


}
