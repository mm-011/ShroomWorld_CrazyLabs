using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType { keyboard, touch }

public class Tap{
    public Touch touch;
    public Vector2 startPosition;
    public Vector2 endPosition;
    public float startTime;
    public float endTime;

    public Tap()
    {
        this.touch = new Touch();
        this.startPosition = new Vector2();
        this.endPosition = new Vector2();
        this.startTime = 0;
        this.endTime = 0;
    }
}

public class InputController : MonoBehaviour
{
    public InputType inputType;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    [Range(0, 2)] public float maxDeltaTimeForDoubleTap;
    [Range(0, 500)] public float maxDeltaPositionForDoubleTap;
    [Range(0, 1000)] public float minDeltaPositionForSwipe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsLeftPressed()
    {
        if (Input.GetKey(left))
            return true;
        else
            return false;
    }

    public bool IsRightPressed()
    {
        if (Input.GetKey(right))
            return true;
        else
            return false;
    }

    public bool IsJumpPressed()
    {
        if (Input.GetKey(jump))
            return true;
        else
            return false;
    }
    
    Touch touch;
    Vector2 beganPosition;
    Vector2 currentPosition;
    public int IsSwiped()
    {
        Input.simulateMouseWithTouches = true;
        int returnValue = 0;
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
        }
        if (touch.phase == TouchPhase.Began)
        {
            beganPosition = touch.position;
        }
        if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            currentPosition = touch.position;
            if (Vector2.Distance(beganPosition, currentPosition)>minDeltaPositionForSwipe)
            {
                if (beganPosition.x < currentPosition.x)
                {
                    returnValue = 1;
                }
                else if (beganPosition.x > currentPosition.x)
                {
                    returnValue = -1;
                }
                else
                {
                    returnValue = 0;
                }
            }
            
        }
        else
        {
            returnValue = 0;
        }
        return returnValue;
    }
    
    Tap lastTap = new Tap();
    Tap currentTap = new Tap();
    public bool IsDoubleTap()
    {
        bool isDoubleTap = false;
        if (FindCurrentTap() != null)
        {
            currentTap = FindCurrentTap();
            //Debug.Log("Currenttap:"+currentTap.endPosition);
            if (lastTap.endPosition == new Tap().endPosition)
            {
                lastTap.touch = currentTap.touch;
                lastTap.startPosition = currentTap.startPosition;
                lastTap.endPosition = currentTap.endPosition;
                lastTap.startTime = currentTap.startTime;
                lastTap.endTime = currentTap.endTime;
                //Debug.Log("Lasttap:" + lastTap.endPosition);
            }
            else
            {
                //Debug.Log("Lasttap:" + lastTap.endPosition);
                if (Vector2.Distance(currentTap.endPosition,lastTap.endPosition)<maxDeltaPositionForDoubleTap && Vector2.Distance(currentTap.endPosition, lastTap.endPosition)!=0 && currentTap.endTime - lastTap.endTime < maxDeltaTimeForDoubleTap)
                {
                    //Debug.LogError("1");
                    isDoubleTap = true;
                    lastTap.touch = currentTap.touch;
                    lastTap.startPosition = currentTap.startPosition;
                    lastTap.endPosition = currentTap.endPosition;
                    lastTap.startTime = currentTap.startTime;
                    lastTap.endTime = currentTap.endTime;
                    //Debug.Log("Lasttap:" + lastTap.endPosition);
                }
                else if(currentTap!=lastTap)
                {
                    //Debug.LogError("2");
                    lastTap.touch = currentTap.touch;
                    lastTap.startPosition = currentTap.startPosition;
                    lastTap.endPosition = currentTap.endPosition;
                    lastTap.startTime = currentTap.startTime;
                    lastTap.endTime = currentTap.endTime;
                    //Debug.Log("Lasttap:" + lastTap.endPosition);
                }
            }
            //Debug.LogError(isDoubleTap);
            //Debug.Log("--------------------------------");
        }
        return isDoubleTap;
    }
    


    Tap tap=new Tap();
    public Tap FindCurrentTap()
    {       
        if (Input.touchCount == 1)
        {
            tap.touch = Input.GetTouch(0);
            switch (tap.touch.phase)
            {
                case TouchPhase.Began:                    
                    tap.startPosition = tap.touch.position;
                    tap.startTime = Time.realtimeSinceStartup;

                    break;
                case TouchPhase.Ended:
                    tap.endPosition = tap.touch.position;
                    tap.endTime = Time.realtimeSinceStartup;                    
                    break;
            }
            if (Vector2.Distance(tap.startPosition, tap.endPosition) <= 35 && tap.startPosition != new Vector2(0, 0) && tap.endPosition != new Vector2(0, 0) && tap.endTime - tap.startTime < 0.25f)
            {
                //Debug.Log("Pocetak" + tap.startPosition);
                //Debug.Log("Kraj" + tap.endPosition);
                //Debug.Log("Distanca: " + Vector2.Distance(tap.startPosition, tap.endPosition));
                return tap;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
