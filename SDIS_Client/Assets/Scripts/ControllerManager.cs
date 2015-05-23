using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    TouchController controller;
    private NetworkHandler netHandler;
    string identifier;

    public void Start()
    {
        controller = (TouchController)gameObject.GetComponent<TouchController>();
        netHandler = (NetworkHandler)gameObject.GetComponent<NetworkHandler>();

        identifier = SystemInfo.deviceUniqueIdentifier;
    }

    public void Swipe(Vector2 swipeVec, float swipeTime)
    {
        controller.console.text += (" Swipe " + swipeVec.ToString());
        
        REST_Play play = new REST_Play();
        
        Play pl = new Play();
        pl.move = swipeVec;
        pl.jump = false;

        play.play = pl;
        play.playerID = identifier;

        netHandler.sendObject(play);

    }

    public void Tap(Vector2 tapPos)
    {
        controller.console.text += (" Tap " + tapPos.ToString());

    }

}