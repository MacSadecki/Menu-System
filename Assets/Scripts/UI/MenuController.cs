using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//[RequireComponent(typeof(Canvas))]
[DisallowMultipleComponent]
public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Page initialPage;   

    [SerializeField] private Canvas rootCanvas;

    private Stack<Page> pageStack = new Stack<Page>();

    private void Awake() 
    {
        //rootCanvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        // Set the focus on the selected item of the initial page
        SetFocusItem(initialPage);

        // Push the inital page to the stack
        if(initialPage != null)
        {
            PushPage(initialPage);
        }
    }

    // Set a gameobject to be in focus
    private void SetFocusItem(Page page)
    {
        if(page.GetLastFocusItem() != null)
        {
            EventSystem.current.SetSelectedGameObject(page.GetLastFocusItem());
        }

        else if(page.GetFirstFocusItem() != null)
        {
            EventSystem.current.SetSelectedGameObject(page.GetFirstFocusItem());
        }
        // If page doesn't have a specified focus item, set selected as null
        else
        {
            EventSystem.current.SetSelectedGameObject(null);           
        }
    }

    // Save currently selected item as last selected before pushing another page on top of the stack
    private void SaveLastItemInFocus()
    {        
        // Check if the page on top of the stack exists
        if(pageStack.Count > 0)
        {
            Page page = pageStack.Peek();
            page.SetLastFocusItem(EventSystem.current.currentSelectedGameObject);
        }        
    }

    // Pop the page when player demands a cancel
    private void OnCancel()
    {
        if (rootCanvas.enabled && rootCanvas.gameObject.activeInHierarchy)
        {
            if(pageStack.Count != 0)
            {
                PopPage();
            }
        }
    }

    // Check if page is present in the stack
    public bool IsPageInStack(Page page)
    {
        return pageStack.Contains(page);
    }

    // Check if page is on top of the stack
    public bool IsPageOnTopOfStack(Page page)
    {
        return pageStack.Count > 0 && page == pageStack.Peek();
    }

    // Push the page on top of the stack
    public void PushPage(Page page)
    {
        // Play animations and audio
        page.Enter(true);

        // Save last focused item on previous page
        SaveLastItemInFocus();

        // Define the focus item on the new page
        SetFocusItem(page);

        // Check the page that is currently on top of the stack
        if(pageStack.Count > 0)
        {
            Page currentPage = pageStack.Peek();

            // If current top page need to exit if is on top, pop the page
            if(currentPage.exitOnNewPagePush)
            {
                currentPage.Exit(false);
            }
        }

        // Push the page on top of the stack
        pageStack.Push(page);
    }

    // Pop the top page out of the stack
    public void PopPage()
    {  
        // Check if stack has pages
        if(pageStack.Count > 1)
        {   
            // Pop the page on top of the stack
            Page currentPage = pageStack.Pop();
            currentPage.Exit(true);

            // Check if new top page has to exit immidietly
            Page newCurrentPage = pageStack.Peek();

            if(newCurrentPage.exitOnNewPagePush)
            {
                currentPage.Exit(false);
            }

            // Define the focus item on the page on top of the stack
            SetFocusItem(pageStack.Peek());
        }
        else
        {
            Debug.LogWarning("Trying to pop a page but only 1 page reamins in the stack!");
        }   
    }

    // Pop all pages present on the stack
    public void PopAllPages()
    {
        for(int i = 0; i <= pageStack.Count; i++)
        {
            PopPage();
        }
    }
}