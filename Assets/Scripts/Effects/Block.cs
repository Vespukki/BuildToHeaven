using BuildToHeaven.GameManagement;
using UnityEngine;

namespace BuildToHeaven.Cards
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Block : MonoBehaviour
    {
        public delegate void BlockDelegate(Block block);
        public static event BlockDelegate OnFailedResolution;

        public BlockResolution resolved {get{ return CheckResolved(); } }

        [HideInInspector] public Rigidbody2D body;
        [HideInInspector] public BoxCollider2D coll;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            coll = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
         /*   if(body.IsAwake() && transform.position.y > GameManager.instance.LossHeight)
            {
                if (CheckResolved() == BlockResolution.failure)
                {
                    OnFailedResolution?.Invoke(this);
                }
            }*/
        }

        public void InvokeFailure()
        {
            OnFailedResolution?.Invoke(this);
        }

        BlockResolution CheckResolved()
        {
            if(transform.position.y < GameManager.instance.LossHeight)
            {
                OnFailedResolution?.Invoke(this);
                return BlockResolution.failure;
            }
            else if(body != null)
            {
                return body.IsSleeping() ? BlockResolution.success : BlockResolution.resolving;

            }
            return BlockResolution.failure;
        }

        public static Vector2 GetLowestPoint(SpriteRenderer spriter)
        {
            //return coll.ClosestPoint(new(transform.position.x, transform.position.y - 100));
            return spriter.bounds.min;
        }

        public static Vector2 GetHighestPoint(SpriteRenderer spriter)
        {
            //return coll.ClosestPoint(new(transform.position.x, transform.position.y + 100));
            return spriter.bounds.max;
        }
        public static Vector2 GetLowestPoint(Collider2D coll)
        {
            return coll.ClosestPoint(new(coll.transform.position.x, coll.transform.position.y - 100));
        }

        public static Vector2 GetHighestPoint(Collider2D coll)
        {
            return coll.ClosestPoint(new(coll.transform.position.x, coll.transform.position.y + 100));

        }

    }

   

    public enum BlockResolution
    {
        success, failure, resolving
    }
}