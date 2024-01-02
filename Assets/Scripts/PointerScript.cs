using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointerScript : MonoBehaviour
{
    public float radius = 5f;
    
    // holds values of the different inputs
    private float _horizontalValue;
    private float _verticalValue;
    private float _upValue;
    private float _downValue;
    private float _strengthValue;
    
    // holds horizontal and vertical values converted to spherical coordinates
    private float _theta;
    private float _phi;
    
    // holds Cartesian coordinates of theta and phi
    private float _x;
    private float _y;
    private float _z;

    private void Start()
    {
           
    }

    void Update()
    {
        // Get the InputHandler
        InputHandler inputHandler = GetComponent<InputHandler>();
        
        // Get the values for Horizontal, Up, Down and Strength from the InputHandler
        _horizontalValue = inputHandler.GetHorizontal();
        _upValue = inputHandler.GetUp();
        _downValue = inputHandler.GetDown();
        _strengthValue = inputHandler.GetStrength();
        
        // Calculate vertical value
        _verticalValue = 0f;
        
        
        /*
        // Get horizontal and vertical input values
        _horizontalValue = Input.GetAxis("Horizontal");
        _verticalValue = Input.GetAxis("Vertical");
        */
        
        // Convert input to spherical coordinates
        _theta = _horizontalValue * Mathf.PI * 2f;
        _phi = _verticalValue * Mathf.PI;

        // Convert spherical coordinates to Cartesian coordinates
        _x = radius * Mathf.Sin(_phi) * Mathf.Cos(_theta);
        _y = radius * Mathf.Cos(_phi);
        _z = radius * Mathf.Sin(_phi) * Mathf.Sin(_theta);

        // Update pointer object position
        transform.localPosition = new Vector3(_x, _y, _z);
        
        // Normalize position to stay on the sphere surface
        transform.localPosition = transform.localPosition.normalized * radius;
    }

    public override string ToString()
    {
        return $"Horizontal: {_horizontalValue}\n" +
               $"Up: {_upValue}\n" +
               $"Down: {_downValue}\n" +
               $"Vertical: {_verticalValue}\n" + 
               $"Strength: {_strengthValue}\n" +
               $"Theta: {_theta}, Phi: {_phi}\n" +
               $"X: {_x}, Y: {_y}, Z: {_z}";
    }
}