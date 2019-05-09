using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    static List<int> assignedControllers = new List<int>();
    public string[] controllers;

    public int controllerID = 0;

    public bool ConnectedController { get { return controllerID > 0; } }
    public string Horizontal { get { return "Controller" + controllerID + "Horizontal"; } }
    public string Vertical { get { return "Controller" + controllerID + "Vertical"; } }
    public string Jump { get { return "Controller" + controllerID + "Jump"; } }
    public string Action { get { return "Controller" + controllerID + "Action"; } }

    public static void FlushControllers()
    {
        assignedControllers.Clear();
    }

    void Start()
    {
        controllers = Input.GetJoystickNames();
        //Makes sure player1 (keyboard) always has control in the game
        if (!assignedControllers.Contains(1))
        {
            assignedControllers.Add(1);
            controllerID = 1;
        }
    }

    void Update()
    {
        if (controllerID <= 0)
        {
            //See if any controller activates
            for (int i = 1; i <= 3; i++)
            {
                if (!assignedControllers.Contains(i) && Input.GetButtonDown("Controller" + i + "Action"))
                {
                    controllerID = i;
                    assignedControllers.Add(i);
                }
            }
        }
    }
}