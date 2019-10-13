using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleState : MonoBehaviour
{
    public string FoodClass;

    internal void MakeNotEdible()
    {
        this.enabled = false;
        this.FoodClass = "notEdible";
    }
}
