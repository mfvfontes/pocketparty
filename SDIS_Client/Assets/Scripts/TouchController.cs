using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TouchController : MonoBehaviour
{

    public float minSwipeDist = 50.0f;

    private Vector2 fingerStartPos;
    private float fingerStartTime = 0.0f;

    private bool isSwipe = false;

    private ControllerManager manager;
    private NetworkHandler netHandler;

    public Text console;
    GameObject touchPad;

    void Start()
    {
        netHandler = (NetworkHandler)gameObject.GetComponent<NetworkHandler>();
        manager = (ControllerManager)gameObject.GetComponent<ControllerManager>();

        console = GameObject.FindGameObjectWithTag("Console").GetComponent<Text>();
        touchPad = GameObject.FindGameObjectWithTag("TouchPad");

        resizeCollider();

    }

    void Update()
    {

#if UNITY_EDITOR

        // Testes para correr apenas no editor
        if (Input.GetButton("Fire1"))
        {

            Vector3 v = Input.mousePosition;
            Vector2 t = new Vector2(v.x, v.y);
            Collider2D hit = Physics2D.OverlapPoint(t);

            if (hit)
            {
                console.text = hit.transform.gameObject.name;
            }
            else
            {
                console.text = "Failed";
            }

        }
#endif

        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    /*
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.tag == "TouchPad")
                        {

                            isSwipe = true;
                            fingerStartPos = touch.position;
                            fingerStartTime = Time.time;

                            console.text = fingerStartPos.ToString();
                        }
                        else
                        {
                            isSwipe = false;
                        }
                    }*/


                    Vector3 v = touch.position;
                    Vector2 t = new Vector2(v.x, v.y);
                    Collider2D hit = Physics2D.OverlapPoint(t);


                    if (hit.tag == "TouchPad")
                    {
                        isSwipe = true;
                        fingerStartPos = touch.position;
                        fingerStartTime = Time.time;
                    }
                    else
                    {
                        console.text = "Clicked outside the TouchPad";
                    }

                    break;

                case TouchPhase.Canceled:
                    isSwipe = false;
                    break;

                case TouchPhase.Ended:
                    float gestureTime = Time.time - fingerStartTime;
                    Vector2 gesture = touch.position - fingerStartPos;

                    if (isSwipe && gesture.magnitude >= minSwipeDist)
                    {
                        manager.Swipe(gesture, gestureTime);
                    }
                    else if (gesture.magnitude < minSwipeDist)
                    {
                        manager.Tap(touch.position);
                    }

                    break;

                case TouchPhase.Moved:
                    console.text = ("Start = " + fingerStartPos.ToString() + " End = " + touch.position.ToString());


                    break;


            }
        }
    }

    private void resizeCollider()
    {


        BoxCollider2D collider = touchPad.GetComponent<BoxCollider2D>();
        RectTransform rect = touchPad.GetComponent<RectTransform>();


        collider.size = new Vector3(rect.rect.width, rect.rect.height, 1);
        //collider.offset = new Vector2(-rect.rect.width / 2, -rect.rect.height / 2);

        console.text = "Size = " + collider.size + " Offset = " + collider.offset;


    }
}
