using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private float dt = 0;
    private float WalkingSpeed = 2.7f;
    private Vector3 mousePos;
    
    private Vector3 CharacterPos;
    //public Transform target;
    private Vector3 screenPoint;
    private Vector3 offset;
    private float angle;

    void Start()
    {
        mousePos = Vector3.zero;
        screenPoint = Vector3.zero;
        offset = Vector3.zero;
    }

    void Update()
    {
        dt = Time.deltaTime;
        
        MovementKeys();
        Aiming();
    }

    void MovementKeys()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //this.transform.Translate(new Vector3(0, WalkingSpeed, 0) * dt);
            CharacterPos.y  += WalkingSpeed * dt;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //this.transform.Translate(new Vector3(0, -WalkingSpeed, 0) * dt);
            CharacterPos.y += -WalkingSpeed * dt;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //this.transform.Translate(new Vector3(-WalkingSpeed, 0, 0) * dt);
            CharacterPos.x += -WalkingSpeed * dt;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //this.transform.Translate(new Vector3(WalkingSpeed, 0, 0) * dt);
            CharacterPos.x += WalkingSpeed * dt;
        }

        this.transform.position = CharacterPos;
    }

    void Aiming()
    {
        mousePos = Input.mousePosition;
        // translate the position of an object in world space into screen space
        screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        this.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
