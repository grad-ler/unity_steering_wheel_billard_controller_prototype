using UnityEngine;
using UnityEngine.InputSystem;

public class BallScript : MonoBehaviour
{
    // These embedded actions are configurable in the inspector:
    public InputAction horizontalAction;
    public InputAction verticalAction;
    public InputAction strengthAction;
    
    private float _horizontalValue;
    private float _verticalValue;
    private float _strengthValue;
    
    // Awake is called when the script is first loaded, or when an object it is attached to is instantiated.
    // Awake is called even if the script is disabled.
    void Awake()
    {
        
        /* not needed so far
        horizontalAction.performed += ctx => { OnHorizontal(ctx); };
        verticalAction.performed += ctx => { OnVertical(ctx); };
        strengthAction.performed += ctx => { OnStrength(ctx); };
        */    
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalValue = horizontalAction.ReadValue<float>();
        _verticalValue = verticalAction.ReadValue<float>();
        _strengthValue = strengthAction.ReadValue<float>();
        
        Debug.Log("hV: " + _horizontalValue);
        //Debug.Log("vV: " + _verticalValue);
        //Debug.Log("sV: " + _strengthValue);
    }
    
    /* not needed so far
    public void OnHorizontal(InputAction.CallbackContext context)
    {
        
    }
    
    public void OnVertical(InputAction.CallbackContext context)
    {
        
    }
    
    public void OnStrength(InputAction.CallbackContext context)
    {
        
    }
    */
    
    // The actions must be enabled and disabled when the GameObject is enabled or disabled
    public void OnEnable()
    {
        horizontalAction.Enable();
        verticalAction.Enable();
        strengthAction.Enable();
    }

    public void OnDisable()
    {
        horizontalAction.Disable();
        verticalAction.Disable();
        strengthAction.Disable();
    }
}
