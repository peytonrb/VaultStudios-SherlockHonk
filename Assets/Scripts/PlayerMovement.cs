using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; 
    public float speed;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask ground;
    public AudioSource honk;
    public AudioClip audioClip;
    private Vector3 velocity;
    private bool isGrounded;

    void Start() {
        honk = GetComponent<AudioSource>();
        honk.clip = audioClip;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground); // is player on ground?

        if (isGrounded && velocity.y < 0)  {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            // velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            honk.Play();
        }

        velocity.y += gravity * Time.deltaTime; 
        controller.Move(velocity * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Fox") {
            // trigger lose screen
            // Application.Quit();
        }
    }
}
