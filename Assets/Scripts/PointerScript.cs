using UnityEngine;

public class PointerScript : MonoBehaviour
{
    public float radius = 5f;

    // Holds the InputHandler
    private InputHandler _inputHandler;
    
    // Hold values of the different inputs
    private float _horizontalValue;
    private float _verticalValue;
    // private float _upValue;
    // private float _downValue;
    private float _strengthValue;
    
    // Hold mapped values of the above inputs
    private float _horizontalMappedValue;
    private float _verticalMappedValue;
    private float _strengthMappedValue;
    
    // Hold horizontal and vertical values converted to spherical coordinates
    private float _theta;
    private float _phi;
    
    // Hold Cartesian coordinates of theta and phi
    private float _x;
    private float _y;
    private float _z;

    // Holds initial position (XYZ coordinates) of the pointer
    private Vector3 _position;
    
    // Rotation angles (Euler angles) for rotation around each axis
    [SerializeField] private Vector3 rotationAngles = new Vector3(0f, 0f, 0f);
    
    private void Start()
    {
        // Get the InputHandler
        _inputHandler = GetComponent<InputHandler>();
    }

    void Update()
    {
        // Get the values for Horizontal, Up, Down and Strength from the InputHandler
        _horizontalValue = _inputHandler.GetHorizontal();
        _verticalValue = _inputHandler.GetVertical();
        // _upValue = inputHandler.GetUp();
        // _downValue = inputHandler.GetDown();
        _strengthValue = _inputHandler.GetStrength();
        
        // Map _horizontalValue from (-1; 1) to (0;1)
        //_horizontalMappedValue = Map(_horizontalValue, -1f, 1f, 0f, 1f);
        
        // Map _verticalValue from (1; -1) to (0;1)
        _verticalMappedValue = Map(_verticalValue, -1f, 1f, 1f, 0f);
        
        // Map _strengthValue from (1; -1) to (0;1)
        _strengthMappedValue = Map(_strengthValue, -1f, 1f, 1f, 0f);
        
        // Convert input to spherical coordinates
        _theta = _horizontalValue * Mathf.PI;
        _phi = _verticalMappedValue * Mathf.PI;
        
        // Convert spherical coordinates to Cartesian coordinates
        _x = radius * Mathf.Sin(_phi) * Mathf.Cos(_theta);
        _y = radius * Mathf.Cos(_phi);
        _z = radius * Mathf.Sin(_phi) * Mathf.Sin(_theta);

        // Initial position (XYZ coordinates) of the pointer
        _position = new Vector3(_x, _y, _z);
        
        // Quaternion for rotation
        Quaternion rotationQuaternion = Quaternion.Euler(rotationAngles);
        
        // Rotate the position vector using the quaternion
        Vector3 rotatedPosition = rotationQuaternion * _position;
        
        // Update pointer object position
        transform.localPosition = rotatedPosition;
        
        // Normalize position to stay on the sphere surface
        transform.localPosition = transform.localPosition.normalized * radius;
    }
    
    // Map function to map values from one range to another
    float Map(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return Mathf.Clamp01((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin);
    }

    public override string ToString()
    {
        return $"Horizontal: {_horizontalValue}\n" +
               $"Horizontal Mapped: {_horizontalMappedValue}\n" +
               // $"Up: {_upValue}\n" +
               // $"Down: {_downValue}\n" +
               $"Vertical: {_verticalValue}\n" +
               $"Vertical Mapped: {_verticalMappedValue}\n" +
               $"Strength: {_strengthValue}\n" +
               $"Strength Mapped: {_strengthMappedValue}\n" +
               $"Theta: {_theta}, Phi: {_phi}\n" +
               $"X: {_x}, Y: {_y}, Z: {_z}";
    }
}