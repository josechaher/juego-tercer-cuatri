using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    public PlayerMovement _playerMovement;
   
    private void OnTriggerStay(Collider other)
    {
        _playerMovement.canJump = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _playerMovement.canJump = false;
    }
}
