using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public GameObject gc;
    private Game_Controller master;


    [Range(1, 5)]
    private float playerMovementSpeed = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponentInChildren<Rigidbody>();
        master = gc.GetComponent<Game_Controller>();

    }



    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
            playerRb.AddForce(new Vector3(playerMovementSpeed, 0, 0));
        if (Input.GetKey(KeyCode.A))
            GetComponent<Rigidbody>().AddForce(new Vector3(-playerMovementSpeed, 0, 0));
        if (Input.GetKey(KeyCode.W))
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, playerMovementSpeed));
        if (Input.GetKey(KeyCode.S))
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -playerMovementSpeed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            master.HandleBasicCollision();
        }
        if (other.gameObject.CompareTag("Pickup2"))
        {
            master.HandleComplexCollision();
        }
    }
}
