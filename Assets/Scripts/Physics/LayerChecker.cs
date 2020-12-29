using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChecker : MonoBehaviour
{
    [SerializeField] LayerMask targetMask;
    [SerializeField] Vector2 direction;
    [SerializeField] float distance = 2;

    private bool _isTouching;
    public bool isTouching
    {
        get {
            _isTouching = Physics2D.Raycast(this.transform.position, direction, distance, targetMask);
            return _isTouching;
        }

       
    
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (isTouching)
        {
            Gizmos.color = Color.green;
        }
        else {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawRay(this.transform.position, direction * distance);
    }

#endif

}
