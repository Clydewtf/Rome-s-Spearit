using UnityEngine;
using UnityEngine.EventSystems;

public class MyButton : MonoBehaviour
{
    public bool isPressed;

    public void Down()
    {
        isPressed = true;
        Debug.Log("Held");
    }

    public void Up()
    {
        isPressed = false;
        Debug.Log("Released");
    }
}