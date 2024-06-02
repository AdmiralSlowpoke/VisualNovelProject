using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBG : MonoBehaviour
{
    Vector2 StartPos;
    [SerializeField] int moveModifier;
    void Start()
    {
        StartPos = transform.position;
    }

    void Update()
    {
        Vector2 position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        
        float positionX = Mathf.Lerp(transform.position.x, StartPos.x+(position.x* moveModifier), 2f * Time.deltaTime);
        float positionY = Mathf.Lerp(transform.position.y, StartPos.y + (position.y * moveModifier), 2f * Time.deltaTime);

        transform.position = new Vector3(positionX, positionY, 0);
    }
}
