using BuildToHeaven.Cards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildToHeaven.Effects;


namespace BuildToHeaven.GameManagement
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LoseBar : MonoBehaviour
    {

        [SerializeField] float failSpeed;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<Block>(out Block block))
            {
                if(block.body.velocity.y < failSpeed)
                {
                    block.InvokeFailure();
                }
            }
        }
    }
}
