using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SpiderMovement : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    public RectTransform gamePad;
    public float moveSpeed = 0.05f;


    static GameObject arObject;
    Vector3 move;

    bool walking;
    public AudioSource somPassos;
    public bool oneTime;
    public GameObject text;
    public GameObject button;

    public float time;
    void Start()
    {
         oneTime = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)gamePad.position, gamePad.rect.width * 0.5f);

        move = new Vector3(transform.localPosition.x, 0f, transform.localPosition.y).normalized; // no movement in y

        if(!walking)
        {
            walking = true;
            arObject.GetComponent<Animator>().SetBool("Walk", true); // on drag start the walk animation
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // do the movement when touched down
        StartCoroutine(PlayerMovement());


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero; // joystick returns to mean pos when not touched
        move = Vector3.zero;
        moveSpeed = moveSpeed*0.9145f;
        StopCoroutine(PlayerMovement());
        walking = false;
        arObject.GetComponent<Animator>().SetBool("Walk", false);


    }

  IEnumerator PlayerMovement()
    {
        while(true)
        {
            time+= Time.deltaTime;
            if(time > 30 && oneTime == true)
            {
                text.SetActive(true);
                button.SetActive(true);
                oneTime = false;
            }
            arObject = GameObject.FindGameObjectWithTag("Player");

            arObject.transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

            if(move != Vector3.zero)
                arObject.transform.rotation = Quaternion.Slerp(arObject.transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * 5.0f);
            
            yield return null;

                
        }
    }

    public void passo()
    {
        somPassos.Play();
    }
 
    
}


