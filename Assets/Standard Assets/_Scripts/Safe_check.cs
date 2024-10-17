using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe_check : MonoBehaviour
{
    public bool _safeCheck = true;
    public bool _inEndZone = false;
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {
            _safeCheck = true;
        }
        if (other.gameObject.CompareTag("EndZone"))
        {
            _inEndZone = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {
            _safeCheck = true;
        }
        if (other.gameObject.CompareTag("EndZone"))
        {
            _inEndZone = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {
            _safeCheck = false;
        }
        if (other.gameObject.CompareTag("EndZone"))
        {
            _inEndZone = false;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
