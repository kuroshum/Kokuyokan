using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {
    
    private float fillAmount;

    [SerializeField]
    private Image content;

    public float MaxValue { get; set; }
    // Property to determine what is the Max Health on the bar

    public float Value
    {
        set
        {
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
        // Property to determine what is the current health we want to show on the bar. So the fillAmount (current bar state) is equal to our Map method, 
        // taking into account the value of our health (value), and our MaxValue (Max Health) as showed in the MaxValue property. MaxValue needs then to be set too.
    }

    // Use this for initialization
    void Start()
{

}

// Update is called once per frame
void Update()
{
    HandleBar();
}

private void HandleBar()
{
        if(fillAmount != content.fillAmount)
            {
                content.fillAmount = fillAmount;
            }
        
        // fillAmount here is updated by our Value Property : Map(value,0,MaxValue,0,1)
        //if is here to say that our content.fillAmount (state of the bar) will be updated only if our fillAmount (the actual current value) is different from it.
}
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        //if min health is 0, same as value / inMax
    }

}