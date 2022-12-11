using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public enum HandType
{
    Left,
    Right
};

public class Hand : MonoBehaviour
{
    public HandType type = HandType.Left;
    public bool isHidden { get; private set; } = false;

    public InputAction trackedAction = null;

    bool m_isCurrentlyTracked = false;

    List<Renderer> m_currentRenderers = new List<Renderer>();

    Collider[] m_colliders = null;
    public bool isCollisionEnabled { get; private set; } = true;

    public XRBaseInteractor interactor = null;
    //public void OnGrab(SelectEnterEventArgs args)
    //{
       // HandControl ctl = args.interactableObject.transform.gameObject.GetComponent<HandControl>();
   // }


    private void Awake()
    {
        if(interactor == null)
        {
            interactor = GetComponentInParent<XRBaseInteractor>();
            Debug.Log("Awake");
        }
    }

    [Obsolete]
    private void OnEnable()
    {
        interactor.selectEntered.AddListener(OnGrab);
        //interactor.selectExited.AddListener(OnRelease);
        Debug.Log("Enabled");
    }

 

    //private void OnRelease(SelectExitEventArgs arg0)
    //{
    //throw new NotImplementedException();
    //}

    [System.Obsolete]
    private void OnDisable()
    {
        interactor?.selectEntered.RemoveListener(OnGrab);
        //interactor?.selectExited.RemoveListener(OnRelease);
        Debug.Log("Disabled");
    }

    //private void OnGrab(XRBaseInteractable arg0)
    //{
       // throw new NotImplementedException();
    //}

    // Start is called before the first frame update
    void Start()
    {
        m_colliders = GetComponentsInChildren<Collider>().Where(childCollider => !childCollider.isTrigger).ToArray();
        trackedAction.Enable();
        Hide();
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        float isTracked = trackedAction.ReadValue<float>();
        if(isTracked == 1.0f && !m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = true;
            Show();
            Debug.Log("Currently Tracked");
        }
        else if (isTracked == 0 && m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = false;
            Hide();
            Debug.Log("Not Currently Tracked");
        }
    }

    public void Show()
    {
        foreach(Renderer renderer in m_currentRenderers)
        {
            renderer.enabled = true;
            Debug.Log("Show Hands");
        }
        isHidden = false;
        EnableCollisions(true);
    }

    public void Hide()
    {
        m_currentRenderers.Clear();
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
            m_currentRenderers.Add(renderer);
            Debug.Log("Hide Hands");
        }
        isHidden = true;
        EnableCollisions(false);
    }

    public void EnableCollisions(bool enabled)
    {
        if (isCollisionEnabled == enabled) return;

        isCollisionEnabled = enabled;
        foreach(Collider collider in m_colliders)
        {
            collider.enabled = isCollisionEnabled;
        }
    }
    public void OnGrab(SelectEnterEventArgs args)
    {
        HandControl ctrl = args.interactableObject.transform.gameObject.GetComponent<HandControl>();
        if (ctrl != null)
        {
            if (ctrl.hideHand)
            {
                Hide();
                Debug.Log("Hide");
            }
        }
    }

    void OnRelease(XRBaseInteractable releasedObject)
    {
        HandControl ctrl = releasedObject.GetComponent<HandControl>();
       if (ctrl != null)
        {
           if (ctrl.hideHand)
        {
        Show();
         Debug.Log("Release and Show");
        }
        }
    }
}
