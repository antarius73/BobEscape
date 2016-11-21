using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
    public float movetime = 1.5f;
    public LayerMask blockingLayer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
  

       protected virtual void Start()
    {
        this.boxCollider = GetComponent<BoxCollider2D>();
        this.rb2D = GetComponent<Rigidbody2D>();      
    }

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        this.boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, this.blockingLayer);
        this.boxCollider.enabled = true;

        if(hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPostiion = Vector3.MoveTowards(this.rb2D.position, end, 1.5f*Time.deltaTime);
            this.rb2D.MovePosition(newPostiion);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

    



}
