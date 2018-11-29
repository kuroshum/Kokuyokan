using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
//Needed to make a class that is not MonoBehaviour to be serializable.
//Then we can serialize the bar, statMaxVal and statCurrentVal to allow us to modify them, and also to link our HP bar to our Player gameobject.
public class Stats
{
    [SerializeField]
    private BarScript bar;
    //We need to make a bridge to our bar script so that every stat has a bar linked to it (health = health bar)

    [SerializeField]
    private float statMaxVal;

    [SerializeField]
    private float statCurrentVal;
    //Those 2 values are basically the ones that will be referred to when updating the health bar.

    public float StatCurrentVal
    {
        get
        {
            return statCurrentVal;
        }

        set
        {
            this.statCurrentVal = Mathf.Clamp(value,0,StatMaxVal);
            bar.Value = statCurrentVal;
            // This property will allow to update our stat current value. First, we want this stat current value to be equal to our value, this last one being defined by our Value property.
            // Then, we want the bar to be updated according to our stat current value.
        }
    }

    public float StatMaxVal
    {
        get
        {
            return statMaxVal;
        }

        set
        {
            this.statMaxVal = value;
            bar.MaxValue = statMaxVal;
            // The first line allows us to update this specific stat Max Value to a new value (for example, set it to 110). 
            // The second line allows us to adjust our bar Max value to this new stat Max Value.
            // Everytime we set a new value, it will send us to the Value property from the Bar Script, and then update it accordingly. This is what allows us to update the bar.
            // Similarly, since we set the bar MaxValue, it will send us back to the MaxValue property from the bar script.
        }
    }

    public void Initialize()
    {
        this.StatMaxVal = statMaxVal;
        this.StatCurrentVal = statCurrentVal;
        //pp.MAXHP = (int)statMaxVal;
        //pp.Hp = (int)statCurrentVal;
    }

}
