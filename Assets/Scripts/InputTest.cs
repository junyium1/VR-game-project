using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    public InputActionProperty testAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float value = testAction.action.ReadValue<float>();
        Debug.Log("value : " + value);
    }
}
