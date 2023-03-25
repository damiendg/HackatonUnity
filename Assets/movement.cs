using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    private Vector3 direction;
    private CharacterController controller;
    public float forwardSpeed; // Vitesse de déplacement du joueur
    private int desiredLane = 1;
    public float laneDistance = 4;
    private float centerPosition;

    public InputActionReference goLeft;
    public InputActionReference goRight;

    public bool isContinue = true;
    private bool oneTime = true;
    

    // Start is called before the first frame update
    void Start()
    {
        forwardSpeed = 5;
        //aButton = goLeft.FindAction("AButton");
        goLeft.action.started += OnAButtonStartedLeft;
        goRight.action.started += OnAButtonStartedRight;
        controller = GetComponent<CharacterController>();
        centerPosition = transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (isContinue)
        {
            direction.z = forwardSpeed;
            transform.position += transform.forward * forwardSpeed * Time.deltaTime;
        
            Vector3 targetPosition = transform.position;
          
            if (desiredLane == 0)
            {
                transform.position = new Vector3(centerPosition - laneDistance, transform.position.y, transform.position.z);
                //targetPosition += Vector3.left *laneDistance;
            }
            else if (desiredLane == 1)
            {
                transform.position = new Vector3(centerPosition, transform.position.y, transform.position.z);
                //targetPosition += Vector3.right * laneDistance;
            }
            else
            {
                transform.position = new Vector3(centerPosition + laneDistance, transform.position.y, transform.position.z);

            }
            // transform.position = Vector3.Lerp(transform.position, targetPosition, 80*Time.deltaTime);
        }
        else
        {
            if (oneTime)
            {
                Debug.Log("TERMINATION DE LA PARTIE");
                forwardSpeed = 0;
                direction.z = forwardSpeed;
                transform.position += transform.forward * forwardSpeed * Time.deltaTime;
        
                Vector3 targetPosition = transform.position;
                Debug.Log(desiredLane);
                if (desiredLane == 0)
                {
                    transform.position = new Vector3(centerPosition - laneDistance, transform.position.y, transform.position.z);
                    //targetPosition += Vector3.left *laneDistance;
                }
                else if (desiredLane == 1)
                {
                    transform.position = new Vector3(centerPosition, transform.position.y, transform.position.z);
                    //targetPosition += Vector3.right * laneDistance;
                }
                else
                {
                    transform.position = new Vector3(centerPosition + laneDistance, transform.position.y, transform.position.z);

                }

                oneTime = false;
            }
            
        }
    
       

    }


    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void OnAButtonStartedLeft(InputAction.CallbackContext context)
    {
        Debug.Log("OnButtonStartedLeft");

            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        

    }


    private void OnAButtonStartedRight(InputAction.CallbackContext context)
    {
        Debug.Log("OnButtonStartedRight");
        desiredLane++;
        if (desiredLane == 3)
        {
            desiredLane = 2;
        }
    }



}

