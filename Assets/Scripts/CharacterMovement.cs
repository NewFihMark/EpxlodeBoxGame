using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Transform CameraTransform;
    public CharacterStatus CharacterStatus;
    public Animator anim;
    [Space(3)]

 
    public float JumpForce = 5;

    public float moveSpeed = 5f;
    public float rotationSpeed = 240f;
    [Space(3)]

    public Vector3 rotationDirection;
    public Vector3 moveDirection;



    void Update()
    {
        float move = Input.GetAxis("Vertical");   // W = 1, S = -1
        float turn = Input.GetAxis("Horizontal"); // A = -1, D = 1

        if (Mathf.Abs(move) > 0.1f)
        {
            transform.Translate(Vector3.forward * move * moveSpeed * Time.deltaTime);
        }

        if (Mathf.Abs(turn) > 0.1f)
        {
            float targetAngle = turn * rotationSpeed * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = transform.rotation * deltaRotation;
        }

        Jump();
    }

   

    public void RotationNormal()
    {
       
            rotationDirection = moveDirection;
        

        Vector3 targetDir = rotationDirection;
        targetDir.y = 0;
        
        if(targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }

        Quaternion lookDir = Quaternion.LookRotation(targetDir);
        Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookDir, Time.deltaTime * 2);
        transform.rotation = targetRot;
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CharacterStatus.isGround == true)
        {
            CharacterStatus.isGround = false;
            GetComponent<Rigidbody>().AddForce(Vector3.up*JumpForce);
        }
    }

    private void OnCollisionEnter()
    {
        CharacterStatus.isGround = true;
    }
}
