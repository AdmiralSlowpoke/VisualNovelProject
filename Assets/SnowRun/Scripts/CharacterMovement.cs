using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.8f;
    public float jumpHeight = 4f;
    public Transform groundcheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public AudioSource source;
    public List<AudioClip> clips;
    public GameObject hintText;
    private bool walkingPlaying = false;
    Vector3 velocity;
    bool isGrounded;
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance,groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move*speed*Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (move.x+move.z >= 0.7)
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(clips[Random.Range(0, clips.Count)]);
                source.pitch = 2;
                
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Invisible")) {
            hintText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Invisible"))
        {
            hintText.SetActive(false);
        }
    }
}
 