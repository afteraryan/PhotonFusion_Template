using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{
    public Vector2 movementInput;
    //public float rotatingInput;
    public Vector3 aimForwardVector;
    public NetworkBool isJumpledPressed;
}
