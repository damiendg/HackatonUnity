using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Vector3 direction;
    private CharacterController controller;
    public float forwardSpeed = 5; // Vitesse de déplacement du joueur
    private int desiredLane = 1;
    public float laneDistance = 4;
    private float centerPosition;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        centerPosition = transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

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

        // transform.position = Vector3.Lerp(transform.position, targetPosition, 80*Time.deltaTime);

    }


    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }


}

