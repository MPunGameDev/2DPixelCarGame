using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{

    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float turnFactor = 0.1f;
    public float speed = 0;
    public float minSpeed;

    private Rigidbody2D rb;
    private PlayerData playerData;

    // Local variables
    private float accelerationInput = 0;
    private float steeringInput = 0;
    private float rotationAngle = 0;
    private float velocityVsUp = 0;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        // Getting GameObject Components
        rb = GetComponent<Rigidbody2D>();
        playerData = Load.LoadPlayer();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        playerData.rb = rb;
        renderer.sprite = playerData.GetCar().Texture;
        speed = playerData.GetCar().Speed;
        minSpeed = playerData.GetCar().Speed;

        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.AddComponent<PowerUpHandler>();
    }

    // Update is called once per frame and is frame dependent
    void Update()
    {
        Vector2 inputVector = GetInput();

        // Send the input to the car controller.
        SetInputVector(inputVector);
    }

    Vector2 GetInput()
    {
        Vector2 inputVector = Vector2.zero;

        // Get input from Unity's input system.
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        return inputVector;
    }

    // Frame-rate independent for physics calculations.
    void FixedUpdate()
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }

    private void ApplyEngineForce()
    {
        //Apply drag if there is no accelerationInput so the car stops when the player lets go of the accelerator
        if (accelerationInput == 0)
            playerData.rb.drag = Mathf.Lerp(playerData.rb.drag, 3.0f, Time.fixedDeltaTime * 3);
        else playerData.rb.drag = 0;

        //Caculate how much "forward" we are going in terms of the direction of our velocity
        velocityVsUp = Vector2.Dot(transform.up, playerData.rb.velocity);

        //Limit so we cannot go faster than the max speed in the "forward" direction
        if (velocityVsUp > playerData.GetCar().MaxSpeed && accelerationInput > 0)
            return;

        //Limit so we cannot go faster than the 50% of max speed in the "reverse" direction
        if (accelerationInput < 0)
            return;

        //Limit so we cannot go faster in any direction while accelerating
        if (playerData.rb.velocity.sqrMagnitude > playerData.GetCar().MaxSpeed * playerData.GetCar().MaxSpeed && accelerationInput > 0)
            return;

        //Create a force for the engine
        Vector2 engineForceVector = accelerationInput * speed * transform.up;

        //Apply force and pushes the car forward
        playerData.rb.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void ApplySteering()
    {
        //Limit the cars ability to turn when moving slowly
        float minSpeedBeforeAllowTurningFactor = (playerData.rb.velocity.magnitude / 2);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        //Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        //Apply steering by rotating the car object
        playerData.rb.MoveRotation(rotationAngle);
    }

    private void KillOrthogonalVelocity()
    {
        //Get forward and right velocity of the car
        Vector2 forwardVelocity = transform.up * Vector2.Dot(playerData.rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(playerData.rb.velocity, transform.right);

        //Kill the orthogonal velocity (side velocity) based on how much the car should drift. 
        playerData.rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    private float GetLateralVelocity()
    {
        //Returns how how fast the car is moving sideways. 
        return Vector2.Dot(transform.right, playerData.rb.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        //Check if we are moving forward and if the player is hitting the brakes. In that case the tires should screech.
        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        //If we have a lot of side movement then the tires should be screeching
        if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
            return true;

        return false;

    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public float GetVelocityMagnitude()
    {
        return rb.velocity.magnitude;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public float GetSpeed()
    {
        return speed;
    }

}
