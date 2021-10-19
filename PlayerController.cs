using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStatus { Idle, Run, Jump, Dead, Finished }

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public InputController inputControllerScript;
    public EnvironmentManager environmentManagerScript;
    public LevelManager levelManagerScript;
    public PlayerStatus playerStatus=PlayerStatus.Idle;
    public Animator playerAnimator;
    [Range(0, 10)] public float forwardSpeed;
    [Range(0, 20)] public float leftRightSpeed;
    [Range(0, 5)] public float leftRightBoundaries;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManagerScript.levelStatus == LevelStatus.Play)
        {
            PlayerForwardMovement();
            PlayerLeftRightMovement();
            IsLevelFinished();
        }
    }

    public void IsLevelFinished()
    {
        if (player.transform.position.z > environmentManagerScript.finishDistance)
        {
            levelManagerScript.levelStatus = LevelStatus.Finish;
            playerStatus = PlayerStatus.Finished;
            playerAnimator.SetBool("Finish", true);
            levelManagerScript.SetPreviousCanvas(levelManagerScript.playCanvas);
            levelManagerScript.playCanvas.SetActive(false);
            levelManagerScript.finishCanvas.SetActive(true);
        }
    }

    public void PlayerForwardMovement()
    {
        player.transform.Translate(new Vector3(0, 0, forwardSpeed*Time.deltaTime));
    }

    public void PlayerLeftRightMovement()
    {
        if (inputControllerScript.inputType == InputType.keyboard)
        {
            if (inputControllerScript.IsLeftPressed() && -leftRightBoundaries < player.transform.position.x)
            {
                player.transform.Translate(new Vector3(-leftRightSpeed * Time.deltaTime, 0, 0));
            }
            if (inputControllerScript.IsRightPressed() && leftRightBoundaries > player.transform.position.x)
            {
                player.transform.Translate(new Vector3(leftRightSpeed * Time.deltaTime, 0, 0));
            }
            if (inputControllerScript.IsJumpPressed() && playerStatus==PlayerStatus.Run)
            {
                playerStatus = PlayerStatus.Jump;
                playerAnimator.SetBool("Run", false);
                playerAnimator.SetBool("Jump",true);
            }

        }
        if (inputControllerScript.inputType == InputType.touch)
        {
            if (inputControllerScript.IsSwiped() == -1 && -leftRightBoundaries < player.transform.position.x)
            {
                player.transform.Translate(new Vector3(-leftRightSpeed * Time.deltaTime, 0, 0));
            }
            if (inputControllerScript.IsSwiped() == 1 && leftRightBoundaries > player.transform.position.x)
            {
                player.transform.Translate(new Vector3(leftRightSpeed * Time.deltaTime, 0, 0));
            }
            if (inputControllerScript.IsDoubleTap() == true && leftRightBoundaries > player.transform.position.x)
            {
                playerStatus = PlayerStatus.Jump;
                playerAnimator.SetBool("Run", false);
                playerAnimator.SetBool("Jump", true);
            }
        }
        player.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x,-leftRightBoundaries,leftRightBoundaries),player.transform.position.y,player.transform.position.z);
    }

    public void JumpFinished()
    {
        playerAnimator.SetBool("Jump", false);
        playerStatus = PlayerStatus.Run;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if ((playerStatus != PlayerStatus.Jump && other.gameObject.layer != 9) || (playerStatus == PlayerStatus.Jump && other.gameObject.layer != 9) || (playerStatus != PlayerStatus.Jump && other.gameObject.layer == 9))
            {
                //Debug.Log("Isti");
                playerStatus = PlayerStatus.Dead;
                levelManagerScript.levelStatus = LevelStatus.Finish;
                playerAnimator.SetBool("Run", false);
                playerAnimator.SetBool("Dead", true);
                levelManagerScript.ChangeLevelStatusToFinish();
            }
            
        }
    }
}
