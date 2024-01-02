using UnityEditor;
using UnityEngine;

public class DebugPointer : MonoBehaviour
{
    [SerializeField] private Vector3 heightOffset = Vector3.up;
    [SerializeField] private Color color = Color.green;
    [SerializeField] private Color selectedColor = new Color(0.25f, 1f, 0.25f, 1f);
    [SerializeField] private GUIStyle style;

    private string _gizmoText;
    private string _gizmoSelectedText;

    bool IsSelected(GameObject obj)
    {
        // Check if the given GameObject is selected
        return UnityEditor.Selection.activeGameObject == obj;
    }
    
    void OnDrawGizmos()
    {
        if (!IsSelected(gameObject))
        {
            style.normal.textColor = color;
            style.alignment = TextAnchor.MiddleCenter;
        
            _gizmoText = transform.position.ToString();
        
            Handles.Label(transform.position + heightOffset, _gizmoText, style);   
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if(IsSelected(gameObject))
        {
            style.normal.textColor = selectedColor;
            style.alignment = TextAnchor.MiddleCenter;
        
            Component pointer = gameObject.GetComponent<PointerScript>();

            _gizmoSelectedText = "";
            _gizmoSelectedText = transform.position.ToString() + "\n" + pointer.ToString();
        
            Handles.Label(transform.position + heightOffset, _gizmoSelectedText, style);}
    }
    
}
