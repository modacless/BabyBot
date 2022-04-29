using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadVibration : MonoBehaviour
{
    public int indexGamepad;
    private Gamepad currentGamepad;

    private void Start()
    {
        currentGamepad = Gamepad.all[indexGamepad];
    }

    public void VibrationWithTime(float vibrationTime, float leftMotorForce, float rightMotorForce)
    {
        StartCoroutine(Vibration(vibrationTime, leftMotorForce, rightMotorForce));
    }

    public void StartVibration(float leftMotorForce, float rightMotorForce)
    {
        currentGamepad.SetMotorSpeeds(leftMotorForce, rightMotorForce);
    }

    public void StopVibration()
    {
        currentGamepad.ResetHaptics();
    }

    IEnumerator Vibration(float vibrationTime, float leftMotorForce, float rightMotorForce)
    {
        currentGamepad.SetMotorSpeeds(leftMotorForce, rightMotorForce);
        yield return new WaitForSeconds(vibrationTime);
        currentGamepad.ResetHaptics();
    }
}
