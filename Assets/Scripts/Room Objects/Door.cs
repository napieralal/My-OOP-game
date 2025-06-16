using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour, IClickable
{
    public Transform leftDoor;
    public Transform rightDoor;
    public float openSpeed = 2.0f;
    private bool isOpen = false;

    private Quaternion leftDoorOpenRotation;
    private Quaternion rightDoorOpenRotation;
    private Quaternion leftDoorClosedRotation;
    private Quaternion rightDoorClosedRotation;
    
    void Start()
    {
        leftDoorClosedRotation = leftDoor.localRotation;
        rightDoorClosedRotation = rightDoor.localRotation;
        
        leftDoorOpenRotation = leftDoorClosedRotation * Quaternion.Euler(0, 90, 0);
        rightDoorOpenRotation = rightDoorClosedRotation * Quaternion.Euler(0, -90, 0);
    }

    public void OnClick()
    {
        StartCoroutine(ToggleDoor(isOpen ? false : true));
    }

    private IEnumerator ToggleDoor(bool opening)
    {
        float timeElapsed = 0f;

        Quaternion leftStartRotation = leftDoor.localRotation;
        Quaternion rightStartRotation = rightDoor.localRotation;

        Quaternion leftEndRotation = opening ? leftDoorOpenRotation : leftDoorClosedRotation;
        Quaternion rightEndRotation = opening ? rightDoorOpenRotation : rightDoorClosedRotation;

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * openSpeed;

            leftDoor.localRotation = Quaternion.Slerp(leftStartRotation, leftEndRotation, timeElapsed);
            rightDoor.localRotation = Quaternion.Slerp(rightStartRotation, rightEndRotation, timeElapsed);

            yield return null;
        }
        
        leftDoor.localRotation = leftEndRotation;
        rightDoor.localRotation = rightEndRotation;
        
        isOpen = opening;
    }
}
