using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //TODO:
    //1. get player input, move player object in th scene
    //2. beside consider moving position,rotation angle is indespensable
    //3. animation should be included

    //create a 3D vector to represent player movement
    Vector3 m_Movement;

    //get input in update() and represent in fixedUpdate() to flexible character moving
    //create variables to get input direction
    float horizontal;
    float vertical;

    //create a rigid object
    Rigidbody m_Rigidbody;
    //create an animator component object
    Animator m_Animator;

    //using Quaternion m_Rotation to represent the rotation
    //initialise Quaternion as no rotation
    Quaternion m_Rotation = Quaternion.identity;

    //rotation speed
    public float rotationSpeed = 20.0f;

    //walking audio
    AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        //after run the game, obtain rigid component, animator component and audio object
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        //combine player direction into 3D vector that movement need to use
        m_Movement.Set(horizontal, 0.0f, vertical);
        //set magnitude as 1, ignore length
        m_Movement.Normalize();

        //judge horizontal or vertical movement
        bool hasHorizontal = !Mathf.Approximately(horizontal, 0.0f);
        bool hasVertical = !Mathf.Approximately(vertical, 0.0f);
        bool isWalking = hasHorizontal || hasVertical;
        //passing the walking boolean value to animate controller
        m_Animator.SetBool("isWalking", isWalking);

        //using 3D vector to represent the player towards after rotation
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, rotationSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        //Audio play
        if (isWalking)
        {
            //ensure that aduio not repeat every single frame
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }
    }
    //when root moved in animation played, this function will be executed
    private void OnAnimatorMove()
    {
        //using m_Movement 3D vector as moving direction,using moving distance in animation every 0.02s which is fixed update frame as the object moving distance
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        //rotate player
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
