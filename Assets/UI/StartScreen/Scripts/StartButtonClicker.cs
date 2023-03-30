using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartButtonClicker : MonoBehaviour
{
    UIDocument buttonDocument;
    Button uiButton;

    void OnEnable()
    {
        buttonDocument = GetComponent<UIDocument>();
        if(buttonDocument == null)
        {
            Debug.LogError("No button reference found!");
        }

        uiButton = buttonDocument.rootVisualElement.Q("StartButton") as Button;

        if(uiButton != null)
        {
            Debug.Log("Button found successfully!"); 
        }

        uiButton.RegisterCallback<ClickEvent>(OnButtonClick);
    }

    //Function for ClickEvent
    public void OnButtonClick(ClickEvent clkevnt)
    {
        Debug.Log("The UI Button has been clicked on!");
    }
}
