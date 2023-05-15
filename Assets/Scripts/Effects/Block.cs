using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BuildToHeaven.Cards
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Block : MonoBehaviour
    {
        Rigidbody2D body;
        

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        /*public override void Activate(Vector2 position)
        {
            base.Activate(position);

            Instantiate(gameObject, position, Quaternion.identity);
        }*/
    }
}