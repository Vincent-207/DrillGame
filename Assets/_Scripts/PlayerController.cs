using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    InputActionReference xMove, yMove;
    Rigidbody2D rb2D;
    [SerializeField] float moveSpeed, flySpeed, maxXSpeed, drillFuelScale, flyFuelScale;
    [SerializeField]
    float fuel;
    [SerializeField] AmountBar fuelBar;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        fuelBar.SetMaxAmount(fuel);
    }
    void Update()
    {

        HandleMovement();

        HandleMineable();
        
    }

    void HandleMineable()
    {
        Vector2 drillInput = xMove.action.ReadValue<Vector2>();
        if(IsVectorSingleValue(drillInput))
        {
        
            RaycastHit2D hit = Physics2D.Raycast(transform.position, drillInput.normalized);
            if(hit.collider != null)
            {
                Mineable mineable = hit.collider.GetComponent<Mineable>();
                if(mineable != null)
                {
                    // Handle mineable. 
                }
            }
        }
    }

    void HandleMovement()
    {
        // Handle general movement
        Vector2 drillInput = xMove.action.ReadValue<Vector2>();
        Vector2 yInput = yMove.action.ReadValue<Vector2>();
        Debug.Log("y: " + yInput);
        bool isFlying = false;
        if(yInput.y > 0) isFlying= true;
        else yInput.y = 0;
        
        rb2D.AddForce(drillInput * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
        rb2D.AddForce(yInput * flySpeed * Time.deltaTime, ForceMode2D.Impulse);

        float xMoveMagnitude = (drillInput * moveSpeed * Time.deltaTime).magnitude;
        float yMoveMagnitude = (yInput * flySpeed * Time.deltaTime).magnitude;
        UpdateFuel(xMoveMagnitude * drillFuelScale + yMoveMagnitude * flyFuelScale);
        
    }

    bool IsVectorSingleValue(Vector2 input)
    {
        input.Normalize();
        float[] inputs = {Mathf.Abs(input.x), Mathf.Abs(input.x), Mathf.Abs(input.x)};
        if(input.magnitude == 0) return false;

        foreach(float inputValue in inputs)
        {
            if(inputValue == 1) return true;
        }

        return false;
    }

    void UpdateFuel(float amountToRemove)
    {
        fuel -= amountToRemove;
        fuelBar.SetAmount(fuel);
    }
}
