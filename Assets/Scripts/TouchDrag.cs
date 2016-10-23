using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchDrag : Singleton<TouchDrag> 
{
    public GameObject TargetUp;
    public GameObject TargetDown;

	private float startPosition;
    private float currentPosition;
    private float dragLength;

    private const float DRAG_DELTA = 500f;
    private const float SPEED = 4000.0f;

    void Start()
    {
       // TargetUp = GameObject.Find("DialTargetUp").GetComponent<Transform>();
       // TargetDown = GameObject.Find("DialTargetDown").GetComponent<Transform>();
    }

	public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            startPosition = Input.mousePosition.y;

        if (Input.GetMouseButton(0))
            currentPosition = Input.mousePosition.y;

        dragLength = currentPosition - startPosition;
        //Debug.Log("position" + this.transform.position + " / DragLength : " + dragLength);

        if (dragLength < -DRAG_DELTA)
            MoveDown();

        else if (dragLength >= DRAG_DELTA)
            MoveUp();
    }

    public void MoveDown()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, TargetDown.GetComponent<Transform>().position, SPEED * Time.deltaTime);
    }

    public void MoveUp()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, TargetUp.GetComponent<Transform>().position, SPEED * Time.deltaTime);
    }

}
