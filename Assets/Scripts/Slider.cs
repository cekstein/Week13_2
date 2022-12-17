using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slider : MonoBehaviour
{
    public Transform startPosition = null;
    public Transform endPosition = null;
    public GameObject _object;
    Rigidbody rb = null;

    MeshRenderer meshRenderer = null;
    private float upMovement;
    
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rb = _object.GetComponent<Rigidbody>();
    }

    public void OnSlideStart()
    {
        meshRenderer.material.SetColor("_Color", Color.red);
        //down = true;
    }

    public void OnSlideStop()
    {
        meshRenderer.material.SetColor("_Color", Color.white);
        //down = false;
    }

    public void UpdateSlider(float percent)
    {
        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, percent);
        upMovement = percent;
    }

    private void FixedUpdate()
    {  
        if(upMovement>0.4)
        rb.AddForce(new Vector3(0f, upMovement, 0f), ForceMode.Force);
        else
            rb.AddForce(new Vector3(0f, -upMovement, 0f), ForceMode.Force);

    }

}

