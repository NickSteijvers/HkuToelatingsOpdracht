using UnityEngine;

public class MainDoor : MonoBehaviour
{
    public GameObject Door;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    public bool button1Pressed = false;
    public bool button2Pressed = false;
    public bool button3Pressed = false;

    private void Update()
    {
        if (button1Pressed && button2Pressed && button3Pressed)
        {
            Door.SetActive(false);
            Debug.Log("All buttons pressed! Door opened.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == button1)
        {
            button1Pressed = true;
        }

        if (other.gameObject == button2)
        {
            button2Pressed = true;
        }

        if (other.gameObject == button3)
        {
            button3Pressed = true;
        }
    }
}