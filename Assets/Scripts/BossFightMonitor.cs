using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossFightMonitor : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Debug.Log("Attack");
        player.GetComponent<Rigidbody>().AddForce(Vector3.forward * 500);
    }

}
