using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessScript : MonoBehaviour
{
    public PostProcessVolume volume;//인스펙터에서 등록했음
    private Vignette _vignette;

    //유령이 가까워짐에 따라 비네트 효과를 강하게 하기 위함
    public GhostManager ghostmanager;
    public float maxIntensity = 1f;//최대 intensity 값
    private float intensity;

    private float deltatime;

    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGetSettings(out _vignette);//vignette 컴포넌트의 값이 _vignette에 저장됨.
        ghostmanager = GameObject.Find("Ghost").GetComponent<GhostManager>();

      
    }

    // Update is called once per frame
    void Update()
    {

        if(ghostmanager._caught == true)
        {
            _vignette.intensity.value = maxIntensity;
        }
        else
        {
            intensity = (GhostManager.leftTime - ghostmanager.ghost_timer) / GhostManager.leftTime;
            _vignette.intensity.value = maxIntensity * intensity;
        }
    

    }
}
