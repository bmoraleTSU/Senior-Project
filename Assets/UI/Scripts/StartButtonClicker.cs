using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartButtonClicker : MonoBehaviour
{
    GameObject uiObject;
    Canvas parentCanvas;
    UIDocument buttonDocument;
    Button uiButton;
    // Setup variables for changing sort order of the screens on button press
    UIDocument[] screenUIDocuments;
    int[] initialSortOrders = { -1, 1, 0, 0 };
    int[] currentSortOrders = { -1, 1, 0, 0 };

    void OnEnable()
    {
        /*
        Find the canvas component that is the parent to all of the screen
        game objects and populate "screenUIDocuments" with its children
        */
        uiObject = GameObject.Find("UI-Screens");
        if(uiObject == null)
        {
            Debug.LogError("Unable to find the parent canvas object!");
        }
        else
        {
            // Find parent canvas from game object
            parentCanvas = uiObject.GetComponent<Canvas>();
            // Populate with all of the children UIDocument objects in parentCanvas
            screenUIDocuments = parentCanvas.GetComponentsInChildren<UIDocument>();
            // Find the order of the UIDocuments
            for (int i = 0; i < screenUIDocuments.Length; i++)
            {
                Debug.Log(screenUIDocuments[i]);
            }
        }
        /*
        Setup the current sort orders to be the initial sort orders 
        so that the start screen is always on top on startup
        */
        currentSortOrders = initialSortOrders;

        buttonDocument = GetComponent<UIDocument>();
        if(buttonDocument == null)
        {
            Debug.LogError("No button reference found!");
        }

        // Find the reference to the "START" button
        uiButton = buttonDocument.rootVisualElement.Q("StartButton") as Button;

        if(uiButton != null)
        {
            Debug.Log("Button found successfully!"); 
        }
        // Register a callback to call OnButtonClick() when the button is pressed
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
            // Set the orderings to each gameobject
            screenUIDocuments[i].GetComponent<UIDocument>().sortingOrder = currentSortOrders[i];
        }
    }
}
