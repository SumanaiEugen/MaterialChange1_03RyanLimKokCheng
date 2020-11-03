using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    float gravityModifier = 3f;
    float jumpforce = 10f;
    float vlimit = 10f;
    float speed = 15f;
    bool IsonGround;
    Rigidbody playerRB;
    Renderer playerRBr;

    public Material[] playerMaterials;

    // Start is called before the first frame update
    void Start()
    {
        IsonGround = true;
        playerRB = GetComponent<Rigidbody>();
        playerRBr = GetComponent<Renderer>();
        Physics.gravity *= gravityModifier;
    }
    // Update is called once per frame
    void Update()
    {
        Jumpplayer();

        float VerticalInput = Input.GetAxis("Vertical");
        float HorizontalInput = Input.GetAxis("Horizontal");


        transform.Translate(Vector3.forward * Time.deltaTime * VerticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * HorizontalInput * speed);


        if (transform.position.z < -vlimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -vlimit);
            playerRBr.material.color = playerMaterials[2].color;
        }
        if (transform.position.z > vlimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, vlimit);
            playerRBr.material.color = playerMaterials[3].color;
        }
        if (transform.position.x < -vlimit)
        {
            transform.position = new Vector3(-vlimit, transform.position.y, transform.position.z);
            playerRBr.material.color = playerMaterials[4].color;
        }
        if (transform.position.x > vlimit)
        {
            transform.position = new Vector3(vlimit, transform.position.y, transform.position.z);
            playerRBr.material.color = playerMaterials[5].color;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GamePlane"))
        {
            IsonGround = true;

            playerRBr.material.color = playerMaterials[0].color;
        }
    }
    void Jumpplayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsonGround == true)
        {
            playerRB.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            IsonGround = false;

            playerRBr.material.color = playerMaterials[1].color;
        }
    }

}
