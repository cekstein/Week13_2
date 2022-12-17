using UnityEngine;
using UnityEngine.Events;


public class TeapotColorChange : MonoBehaviour
{
    //TurnInteractable turnInteractable;
    //[SerializeField] GameObject control;
    public UnityEvent controlAmount;

    //private float m_angleData;


    Color lerpedColor = Color.red;
    //MeshRenderer m_meshRenderer = null;
    //bool stopped = turnInfo.stopTurn;



    // Start is called before the first frame update
    void Start()
    {
        //turnInteractable = control.GetComponent<TurnInteractable>();
        //TurnInteractable turnInfo = GameObject.Find("Dial").GetComponent<TurnInteractable>();
        //m_angleData = turnInfo.turnAngle;
        //m_angleData = controlAmount;
        //Debug.Log(controlAmount);
        //lerpedColor = Color.Lerp(Color.red, Color.green, controlAmount);

        //gameObject.renderer.material.SetColor("_Color", lerpedColor);
    }

    private void FixedUpdate()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", lerpedColor);
    }
}
// public float turnAngle = 0.0f;