using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public string SceneName = "";
    public void LoadTargetScene()
    {
        SceneManager.LoadScene(SceneName);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadTargetScene();
        }

    }
}
