using System.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class PlayerController : MonoBehaviour
{


    public int sensitivity = 1;
    public int jumpForce = 15;
    public int base_speed=7;
    private int speed;
    public bool dead = false;
    private Animator animator;
    private float distToGround = 1.1f;
    private LavaController lava;
    public GameObject gameOverUI;
    public GameObject youWinUI;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        lava = GameObject.Find("lava").GetComponent<LavaController>();
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            Move();
            Rotate();
            Drag();
            animate();
        }
        
    }

    bool IsGrounded()
    {
        return Physics.CapsuleCast(transform.position, transform.position, 0.49f, -Vector3.up, distToGround);
    }

private void Move()
    {
        if (!IsGrounded())
        {
            speed = base_speed/5;
        }
        else{
            speed = base_speed;
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(speed, 0, 0);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(-speed, 0, 0);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(0, 0, -speed/1.5f);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(0, 0, speed/1.5f);
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * Input.GetAxis("Mouse X"));
    }

    private void Drag()
    {
        if (IsGrounded())
            GetComponent<Rigidbody>().drag = 6f;
        else
            GetComponent<Rigidbody>().drag = 1.4f;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "lava":
                GameObject puppet = GameObject.Find("puppet_kid");
                // Turn the player all black
                foreach (Renderer ren in puppet.GetComponentsInChildren<Renderer>())
                {
                    ren.material.color = Color.black;
                }
                dead = true;
                puppet.GetComponent<Animator>().enabled = false;
                lava.lava_enabled = false;
                gameOverUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                break;
            case "stage1trigger":
                lava.current_stage = 1;
                break;
            case "stage2trigger":
                lava.current_stage = 2;
                break;
            case "stage3trigger":
                lava.current_stage = 3;
                break;
            case "GoldenWindow":
                youWinUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                break;
            default:
                break;

        }
    }

    private void animate()
    {
        if (IsGrounded())
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
