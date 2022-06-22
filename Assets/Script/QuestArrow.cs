using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestArrow : MonoBehaviour
{
    public Transform target;
    public float buffer;
    public Color FarColor;
    public Color CloseColor;
    public float MaxDistance;

    private SpriteRenderer Rend;

    public void Start()
    {
        Rend = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (target != null)
        {
            Vector2 difference = transform.position - target.position;
            float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(90, 0, angle + buffer);

            Rend.color = Color.Lerp(CloseColor, FarColor, DistanceToQuest());
        }

    }

   float DistanceToQuest()
    {
        return Mathf.Clamp01(Vector2.Distance(transform.position, target.position) / MaxDistance );
    }
}
