using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 m_Movement;
    Animator m_Animator;
    Quaternion m_Rotation = Quaternion.identity;
    Rigidbody m_RigidBody;

    public AudioSource m_AudioSource;

    public float turnSpeed = 20F;
    float idleTime = 0.2F;
    float timer = 0.0F;


    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    //Where all physics should be calculated
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Sets movement vector based on input from horizontal (A, D) and vertical (W, S)
        m_Movement.Set(horizontal, 0F, vertical);
        //This normalizes the vector so diagonal movement isn't faster while maintaining direction (unit vector)
        m_Movement.Normalize();

        //Tests to see if there is current input through comparison
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0F);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0F);

        bool isWalking = hasHorizontalInput || hasVerticalInput;
        //To fix jitter when changing direction during continuous movement
        if (!isWalking)
        {
            timer += Time.deltaTime;
            if (timer >= idleTime)
            {
                m_Animator.SetBool("IsWalking", false);
                timer = 0F;
                m_AudioSource.Stop();
            }
        } else
        {
            m_Animator.SetBool("IsWalking", true);
            timer = 0F;
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }

        //Where to look
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0F);
        //Sets the game object to actually look at the object
        m_Rotation = Quaternion.LookRotation(desiredForward);


    }

    private void OnAnimatorMove()
    {
        m_RigidBody.MovePosition(m_RigidBody.position + (m_Movement * (float)1.37) * m_Animator.deltaPosition.magnitude);
        m_RigidBody.MoveRotation(m_Rotation);
    }

}
