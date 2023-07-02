using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class DifficultyScreenButtonClicker : MonoBehaviour
{
    // Setup variables for changing sort order of the screens on button press
    Canvas parentCanvas;
    UIDocument difficultyScreenUIDocument;
    Button easyUIButton, mediumUIButton, hardUIButton;
    
    void OnEnable()
    {
        // Find the canvas component that is the parent to all of the screen
        // game objects and populate "screenUIDocuments" with its children
        parentCanvas = FindAnyObjectByType<Canvas>();
        if (parentCanvas == null)
        {
            Debug.LogError("Unable to find the parent canvas object!");
        }
        else
        {
            Debug.Log("Found parent canvas!");
            parentCanvas.enabled = true;
        }

        difficultyScreenUIDocument = GetComponent<UIDocument>();

        // Check if UI Document was found
        if (difficultyScreenUIDocument == null)
        {
            Debug.LogError("No UI Document found for the Difficulty Screen!");
        }

        easyUIButton = difficultyScreenUIDocument.rootVisualElement.Q("ButtonContainer").Q("EasyChoice") as Button;
        mediumUIButton = difficultyScreenUIDocument.rootVisualElement.Q("ButtonContainer").Q("MediumChoice") as Button;
        hardUIButton = difficultyScreenUIDocument.rootVisualElement.Q("ButtonContainer").Q("HardChoice") as Button;

        // Check if all buttons where found
        if (easyUIButton != null)
        {
            Debug.Log("Easy Button found successfully!");
        }
        if (mediumUIButton != null)
        {
            Debug.Log("Medium Button found successfully!");
        }
        if (hardUIButton != null)
        {
            Debug.Log("Hard Button found successfully!");
        }

        easyUIButton.RegisterCallback<ClickEvent>(evnt => OnEasyButtonClick(evnt, parentCanvas));
        mediumUIButton.RegisterCallback<ClickEvent>(evnt => OnMediumButtonClick(evnt, parentCanvas));
        hardUIButton.RegisterCallback<ClickEvent>(evnt => OnHardButtonClick(evnt, parentCanvas));

    }

    //Function for easyClickEvent
    public void OnEasyButtonClick(ClickEvent clkevnt, Canvas canvasIN)
    {
        Debug.Log("The Easy UI Button has been clicked on!");
        canvasIN.gameObject.SetActive(false);
    }

    //Function for mediumClickEvent
    public void OnMediumButtonClick(ClickEvent clkevnt, Canvas canvasIN)
    {
        Debug.Log("The Medium UI Button has been clicked on!");
        canvasIN.gameObject.SetActive(false);
    }

    //Function for hardClickEvent
    public void OnHardButtonClick(ClickEvent clkevnt, Canvas canvasIN)
    {
        Debug.Log("The Hard UI Button has been clicked on!");
        canvasIN.gameObject.SetActive(false);
    }
}


