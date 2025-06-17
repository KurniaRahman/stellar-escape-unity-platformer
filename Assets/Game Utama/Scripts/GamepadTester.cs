using UnityEngine;

public class GamepadTester : MonoBehaviour
{
    // Jumlah joystick yang ingin dites (bisa lebih dari 2 jika perlu)
    int maxJoysticks = 2;
    int maxButtons = 20;

    void Update()
    {
        for (int joystick = 1; joystick <= maxJoysticks; joystick++)
        {
            for (int button = 0; button < maxButtons; button++)
            {
                string buttonName = "joystick " + joystick + " button " + button;

                if (Input.GetKeyDown(buttonName))
                {
                    Debug.Log($"Joystick {joystick} - Button {button} pressed");
                }
            }
        }
    }
}