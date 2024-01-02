using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // This field will contain the actions wrapper instance
    private InputActions _inputActions;
    
    // This will hold the context
    private InputAction.CallbackContext _context;
    
    private float _horizontalValue;
    //private float _verticalValue;
    //private float _currentVerticalValue;
    private float _upValue;
    private float _downValue;
    private float _strengthValue;
    
    // Awake is called when the script is first loaded, or when an object it is attached to is instantiated.
    // Awake is called even if the script is disabled.
    void Awake()
    {
        // Instantiate the actions wrapper class
        _inputActions = new InputActions();
        
        // Adding callback methods for the performed state
        
        _inputActions.CueControl.Horizontal.performed += OnHorizontal;
        
        /*
        _inputActions.CueControl.Vertical.started += OnVertical;
        _inputActions.CueControl.Vertical.performed += OnVertical;
        _inputActions.CueControl.Vertical.canceled += OnVertical;
        */

        _inputActions.CueControl.Up.performed += OnUp;
        
        _inputActions.CueControl.Down.performed += OnDown;
        
        _inputActions.CueControl.Strength.performed += OnStrength;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Get input values for Horizontal, Up, Down and Strength
    
    public void OnHorizontal(InputAction.CallbackContext context)
    {
        _horizontalValue = _inputActions.CueControl.Horizontal.ReadValue<float>();
        //Debug.Log("Horizontal Input: " + _horizontalValue);
    }
    
    /* Input System 1D Axis seem bugged, using OnUp(), OnDown() instead
    public void OnVertical(InputAction.CallbackContext context)
    {

        _verticalValue = _inputActions.CueControl.Vertical.ReadValue<float>();
        _currentVerticalValue = Mathf.MoveTowards(_verticalValue, _currentVerticalValue, 1f * Time.deltaTime);
        Debug.Log("Vertical Input: " + _currentVerticalValue);
    }
    */

    public void OnUp(InputAction.CallbackContext context)
    {
        _upValue = _inputActions.CueControl.Up.ReadValue<float>();
        //Debug.Log("Up Input: " + _upValue);
    }
    
    public void OnDown(InputAction.CallbackContext context)
    {
        _downValue = _inputActions.CueControl.Down.ReadValue<float>();
        //Debug.Log("Down Input: " + _downValue);
    }
    
    public void OnStrength(InputAction.CallbackContext context)
    {
        _strengthValue = _inputActions.CueControl.Strength.ReadValue<float>();
        //Debug.Log("Strength Input: " + _strengthValue);
    }
    
    // Functions for returning Horizontal, Up, Down and Strength values
    public float GetHorizontal()
    {
        OnHorizontal(_context);
        return _horizontalValue;
    }
    
    public float GetUp()
    {
        OnUp(_context);
        return _upValue;
    }
    
    public float GetDown()
    {
        OnDown(_context);
        return _downValue;
    }
    
    public float GetStrength()
    {
        OnStrength(_context);
        return _strengthValue;
    }
    
    // The actions must be enabled and disabled when the GameObject is enabled or disabled
    public void OnEnable()
    {
        _inputActions.CueControl.Enable();
    }

    public void OnDisable()
    {
        _inputActions.CueControl.Disable();
    }
}
