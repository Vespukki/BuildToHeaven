using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BuildToHeaven.Cards
{
    public class CardPreview : MonoBehaviour
    {
        private static CardPreview _instance;

        public static CardPreview Instance
        {
            get { return _instance; }
        }


        private void Awake()
        {
            if(_instance != null)
            {
                Destroy(gameObject);
            }
            _instance = this;
        }

        private void Update()
        {
            Vector3 location = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            transform.position = new Vector3(location.x, location.y, 0);
        }
    }
}