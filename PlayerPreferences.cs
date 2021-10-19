using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPreferences : MonoBehaviour
{
    public CameraController cameraControllerScript;
    public EnvironmentManager environmentManagerScript;
    public PlayerController playerControllerScript;

    public Vector3 cameraAngle;
    public Vector3 offsetFromRoot;

    public float forwardSpeed;
    public float leftRightSpeed;

    public int spawnDelay;
    public int intensityLevel;

    public InputField cameraAngleX, cameraAngleY, cameraAngleZ;
    public InputField offsetFromRootX, offsetFromRootY, offsetFromRootZ;

    public Slider forwardSpeedSlider, leftRightSpeedSlider, spawnDelaySlider, intensityLevelSlider;




    // Start is called before the first frame update
    void Start()
    {
        GetData();
        UpdateUI();
        //FirstTime();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData()
    {
        cameraAngle = new Vector3(float.Parse(cameraAngleX.text), float.Parse(cameraAngleY.text), float.Parse(cameraAngleZ.text));
        offsetFromRoot = new Vector3(float.Parse(offsetFromRootX.text), float.Parse(offsetFromRootY.text), float.Parse(offsetFromRootZ.text));
        forwardSpeed = forwardSpeedSlider.value;
        leftRightSpeed = leftRightSpeedSlider.value;
        spawnDelay = (int)spawnDelaySlider.value;
        intensityLevel = (int)intensityLevelSlider.value;

        PlayerPrefs.SetString("cameraAngle", cameraAngle.ToString());
        PlayerPrefs.SetString("offsetFromRoot", offsetFromRoot.ToString());
        PlayerPrefs.SetFloat("forwardSpeed", forwardSpeed);
        PlayerPrefs.SetFloat("leftRightSpeed", leftRightSpeed);
        PlayerPrefs.SetInt("spawnDelay", spawnDelay);
        PlayerPrefs.SetInt("intensityLevel", intensityLevel);

        cameraControllerScript.CameraAngles = cameraAngle;
        cameraControllerScript.offsetFromRoot = offsetFromRoot;
        playerControllerScript.forwardSpeed = forwardSpeed;
        playerControllerScript.leftRightSpeed = leftRightSpeed;
        environmentManagerScript.obstacleSpawnDiley = spawnDelay;
        environmentManagerScript.obstacleIntesityLevel = intensityLevel;
    }

    public void GetData()
    {
        cameraAngle = vector3ToString(PlayerPrefs.GetString("cameraAngle"));
        offsetFromRoot = vector3ToString(PlayerPrefs.GetString("offsetFromRoot"));
        forwardSpeed = PlayerPrefs.GetFloat("forwardSpeed");
        leftRightSpeed = PlayerPrefs.GetFloat("leftRightSpeed");
        spawnDelay = PlayerPrefs.GetInt("spawnDelay");
        intensityLevel = PlayerPrefs.GetInt("intensityLevel");

        cameraControllerScript.CameraAngles = cameraAngle;
        cameraControllerScript.offsetFromRoot = offsetFromRoot;
        playerControllerScript.forwardSpeed = forwardSpeed;
        playerControllerScript.leftRightSpeed = leftRightSpeed;
        environmentManagerScript.obstacleSpawnDiley = spawnDelay;
        environmentManagerScript.obstacleIntesityLevel = intensityLevel;
    }

    public Vector3 vector3ToString(string vector3String)
    {
        string[] axis=new string[3];
        vector3String = vector3String.Substring(1, vector3String.Length - 2);
        axis = vector3String.Split(',');
        return new Vector3(float.Parse(axis[0])/10, float.Parse(axis[1])/10, float.Parse(axis[2])/10);
    }

    public void UpdateUI()
    {
        cameraAngleX.text = cameraAngle.x.ToString();
        cameraAngleY.text = cameraAngle.y.ToString();
        cameraAngleZ.text = cameraAngle.z.ToString();

        offsetFromRootX.text = offsetFromRoot.x.ToString();
        offsetFromRootY.text = offsetFromRoot.y.ToString();
        offsetFromRootZ.text = offsetFromRoot.z.ToString();

        forwardSpeedSlider.value = forwardSpeed;
        leftRightSpeedSlider.value = leftRightSpeed;

        spawnDelaySlider.value = spawnDelay;
        intensityLevelSlider.value = intensityLevel;
    }

    public void FirstTime()
    {
        cameraAngle = cameraControllerScript.CameraAngles;
        offsetFromRoot = cameraControllerScript.offsetFromRoot;
        forwardSpeed = playerControllerScript.forwardSpeed;
        leftRightSpeed = playerControllerScript.leftRightSpeed;
        spawnDelay = environmentManagerScript.obstacleSpawnDiley;
        intensityLevel = environmentManagerScript.obstacleIntesityLevel;
        UpdateUI();
    }
}
