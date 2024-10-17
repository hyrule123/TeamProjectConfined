using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    private Inventory inventory;
    public GameObject Key109;
    public GameObject KeyStatue;
    public GameObject MemoStatue;
    public DoorScript Door109;
 

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        Door109 = GameObject.Find("Door109").GetComponent<DoorScript>();

        gameObject.transform.Find("KeyStatue").gameObject.SetActive(false);
    }

    void Update()
    {

        if ((Inventory.keys[2] == true || Door109.open == true) && Inventory.keys[14]==false)
        {
            gameObject.transform.Find("KeyStatue").gameObject.SetActive(true);
        }
        
    }
}
