using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;
    Vector2 viewInputVector = Vector2.zero;
    bool isJumpButtonPressed = false;
    private FixedJoystick joystick_move;
    private DynamicJoystick joystick_look;

    //Other components
    //CharacterMovementHandler characterMovementHandler;
    LocalCameraHandler localCameraHandler;

    private void Awake()
    {
        //characterMovementHandler = GetComponent<CharacterMovementHandler>();
        localCameraHandler = GetComponentInChildren<LocalCameraHandler>();
        joystick_move = FindObjectOfType<FixedJoystick>();
        joystick_look = FindObjectOfType<DynamicJoystick>();


    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        //View Input
        /*
        viewInputVector.x = Input.GetAxis("Mouse X");
        viewInputVector.y = Input.GetAxis("Mouse Y") * -1; // Invert the mouse look
        */
        viewInputVector.x = joystick_look.Horizontal;
        viewInputVector.y = joystick_look.Vertical * -1;


        //Set view
        localCameraHandler.SetViewInputVector(viewInputVector);

        //characterMovementHandler.SetViewInputVector(viewInputVector);

        //Move Input
        /* 
         moveInputVector.x = Input.GetAxis("Horizontal");
         moveInputVector.y = Input.GetAxis("Vertical");
        */
        moveInputVector.x = joystick_move.Horizontal;
        moveInputVector.y = joystick_move.Vertical;



        //Jump
        if (Input.GetButtonDown("Jump"))
            isJumpButtonPressed = true;
    }

    public NetworkInputData GetNetworkInput() 
    {
        NetworkInputData networkInputData = new NetworkInputData();

        //Aim Data
        networkInputData.aimForwardVector = localCameraHandler.transform.forward; //Only sending 'X' axis data because in our simple game,
                                                        //player looking up and down (y-axis) doesn't affect other players but x-axis does
        
        //Move Data
        networkInputData.movementInput = moveInputVector;

        //Jump Data
        networkInputData.isJumpledPressed = isJumpButtonPressed;

        //Reset variables now that we have read their states
        isJumpButtonPressed= false;

        return networkInputData;
    }
}
