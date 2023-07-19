using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AIAgent : Agent {
    // Init to eventually hold reference to 3D object's ri
    Rigidbody rBody;
    // Init array of GameObjects to hold platforms
    GameObject[] platforms;
    Dictionary<string, bool> platformsTouched = new Dictionary<string, bool> {};
    // Init colliding bools
    bool isCurrentlyColliding = false;
    bool lastPlatformTouched = false;
    // Start is called before the first frame update
    void Start()
    {
        // Get reference to 3D object rigid body
        rBody = GetComponent<Rigidbody>();
        // Get GameObjects that are platforms
        platforms = GameObject.FindGameObjectsWithTag("platform");
        Debug.Log("The first platform found is " + platforms[0].name);
        // Get all platform game objects
        foreach(GameObject platform in platforms) {
            Debug.Log("Platform found: " + platform.name);
            // Add platform entry into traversal tracking dictionary
            platformsTouched.Add(platform.name, false);
        }

    }

    // Make collision checker function
    private bool colliderChecker(Collision collision)
    {
        if (collision.gameObject.tag == "platform" && !platformsTouched[collision.gameObject.name])
        {
            //Debug.Log("Key being used: " + collision.gameObject.name);
            platformsTouched[collision.gameObject.name] = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    // Turn collision bool on if colliding
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected!");
        if (colliderChecker(collision))
        {
            Debug.Log("Collided with " + collision.gameObject.name);
            isCurrentlyColliding = true;
            // Check if last platform
            if (collision.gameObject.name == platforms[0].name)
            {
                lastPlatformTouched = true;
            }
        }
    }

    // Turn collision bool off when not colliding
    private void OnCollisionExit(Collision collision)
    {
        isCurrentlyColliding = false;
    }

    public Transform Target;
    // Extend episode start function
    public override void OnEpisodeBegin()
    {
        // If the Agent fell, zero its momentum
        if (this.transform.localPosition.y < 0)
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            // Reset ball's position to top most platform
            this.transform.localPosition = new Vector3(-51.48f, 12.327f, 0.16f);
        }

        // Move the target to a new spot
        /*Target.localPosition = new Vector3(Random.value * 8 - 4,
                                           0.5f,
                                           Random.value * 8 - 4);*/
        // Reset the platformsTouched dictionary to indicate that no platform has been traversed
        foreach (GameObject platform in platforms)
        {
            platformsTouched[platform.name] = false;
        }
    }

    // Extend observation function
    public override void CollectObservations(VectorSensor sensor)
    {
        // Platform and Agent positions
        foreach(GameObject platform in platforms)
        {
            sensor.AddObservation(platform.transform.localPosition);
        }
        // sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }

    public float forceMultiplier = 10;
    // Extend action reciever and reward assigner function
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * forceMultiplier);

        // Rewards
        // Get distance to last platform
        //float distanceToTarget = Vector3.Distance(this.transform.localPosition, platforms[0].transform.localPosition);

        // Close to bottom platform
        if (lastPlatformTouched)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        // Fell off platform
        else if (this.transform.localPosition.y < 0)
        {
            // Adding punishment so we don't punish to heavily
            // on falling per step
            AddReward(-0.4f); // Consider removing this penalty to avoid fear of jumping
            EndEpisode();
        }

        // Punishment for staying on the same platform for too long
        if (isCurrentlyColliding && this.rBody.velocity != Vector3.zero)
        {
            AddReward(0.4f); // Small reward for staying on a platform and moving
        }
        else
        {
            AddReward(-0.1f); // Small punishment for staying still or not on a platform
        }
    }

    // Allow keyboard control of capsule
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

}
