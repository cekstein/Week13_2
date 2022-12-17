using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[Serializable]
public class TurnEvent : UnityEvent<float> { }

public class TurnInteractable : XRBaseInteractable
{
    XRBaseInteractor m_interactor = null;

    Coroutine m_turn = null;

    [HideInInspector]
    public float turnAngle = 0.0f;

    Vector3 m_startingRotation = Vector3.zero;

    public UnityEvent onTurnStart = new();
    public UnityEvent onTurnStop = new();
    public TurnEvent onTurnUpdate = new();
    public bool stopTurn = true;

    public GameObject _object;

    private Transform m_transform = null;
    private Transform _objectTransform = null;

    Quaternion GetLocalRotation(Quaternion targetWorld)
    {
        return Quaternion.Inverse(targetWorld) * transform.rotation;
    }

    private void Start()
    {
        _objectTransform = _object.GetComponentInChildren<Transform>();
        m_transform = GetComponent<Transform>();
    }
    void StartTurn()
    {
        if (m_turn != null)
        {
            StopCoroutine(m_turn);
        }
        Quaternion localRotation = GetLocalRotation(m_interactor.transform.rotation);
        m_startingRotation = localRotation.eulerAngles;
        onTurnStart?.Invoke();
        stopTurn = false;
        m_turn = StartCoroutine(UpdateTurn());
    }

    void StopTurn()
    {
        if (m_turn != null)
        {
            StopCoroutine(m_turn);
            onTurnStop?.Invoke();
            stopTurn = true;
            m_turn = null;
        }
    }

    IEnumerator UpdateTurn()
    {
        while (m_interactor != null)
        {
            Quaternion localRotation = GetLocalRotation(m_interactor.transform.rotation);
            turnAngle = m_startingRotation.z - localRotation.eulerAngles.z;
            onTurnUpdate?.Invoke(turnAngle);
            yield return null;
        }

    }

    [Obsolete]
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        m_interactor = interactor;
        StartTurn();
        base.OnSelectEntered(interactor);


    }

    [Obsolete]
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        StopTurn();
        m_interactor = null;
        base.OnSelectExited(interactor);
    }

    private void FixedUpdate()
    {
        float newFactor = 0.0f;

        //y_amount = z_amount/2;
        //m_rotation = Vector3.forward * 2;
        //m_rotation = Quaternion.Euler(x: 0, y: 8, z: 0);

        if (m_interactor != null && _objectTransform != null)
        {
            newFactor = (turnAngle) * 0.0046875f;

            if(turnAngle > 200.0f)
                { 
                _object.transform.Rotate(Vector3.up, -newFactor);
                //Debug.Log("Clockwise");
                }
            else
            {
                _object.transform.Rotate(Vector3.up, newFactor * 1.8f);
            }

        }
 
    }

}
