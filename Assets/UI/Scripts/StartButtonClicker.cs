using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartButtonClicker : MonoBehaviour
{
    UIDocument buttonDocument;
    Button uiButton;
    // Setup variables for changing sort order of the screens on button press
    UIDocument[] screenUIDocuments;
    int[] initialSortOrders = { -1, 1, 0, 0 };
    int[] currentSortOrders = { -1, 1, 0, 0 };

    void OnEnable()
    {
        // Find the canvas component that is the parent to all of the screen
        // game objects and populate "screenUIDocuments" with its children
        Canvas parentCanvas = FindAnyObjectByType<Canvas>();
        if(parentCanvas == null)
        {
            Debug.LogError("Unable to find the parent canvas object!");
        }
        else
        {
            screenUIDocuments = parentCanvas.GetComponentsInChildren<UIDocument>();
            // Find order of UIDocuments
            for (int i = 0; i < screenUIDocuments.Length; i++)
            {
                Debug.Log(screenUIDocuments[i]);
            }
        }
        // Setup the initial sort orders so that the start screen is on top on
        // startup
        currentSortOrders = initialSortOrders;

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
        // Set all the sort orders of the screens
        for(int i = 0; i < screenUIDocuments.Length; i++)
        {
            screenUIDocuments[i].GetComponent<UIDocument>().sortingOrder = initialSortOrders[i];
        }
    }

    //Function for ClickEvent
    public void OnButtonClick(ClickEvent clkevnt)
    {
        Debug.Log("The UI Button has been clicked on!");
        // When button pressed, push Start screen to back and bring difficulty
        // screen forward
        for (int i = 0; i < screenUIDocuments.Length; i++)
        {
            // Flip the ordering of all screens (background is 0 so it will not
            // be affected by this)
            currentSortOrders[i] *= -1;
            // Set the orderings to each gamgeobject
            screenUIDocuments[i].GetComponent<UIDocument>().sortingOrder = currentSortOrders[i];
        }
    }
}
