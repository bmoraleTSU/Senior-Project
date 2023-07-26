using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject pathPrefab; // Prefab for the path
    public int levels = 5; // Number of levels
    public int stepsPerLevel = 10; // Number of steps per level
    public float minStepHeight = 1f; // Minimum step height
    public float maxStepHeight = 3f;
    public GameObject canvass;
    public GameObject canvass2;
    public GameObject canvas3;
    public Joystick joystick;
    

    public static NewBehaviourScript instance { get; set; }
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GenerateRandomPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GenerateRandomPath()
    {
        for (int level = 0; level < levels; level++)
        {
            for (int step = 0; step < stepsPerLevel; step++)
            {
                float stepHeight = Random.Range(minStepHeight, maxStepHeight);

                // Instantiate the path prefab at the desired position
                Vector3 position = new Vector3(step, level * stepHeight, 0f);
                Instantiate(pathPrefab, position, Quaternion.identity);
            }
        }
    }
    public void playgame() { 
        canvass.SetActive(false);
        canvass2.SetActive(false);
        canvas3.SetActive(true);
    }
    
}
