using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildToHeaven.Cards;
using System.Linq;

namespace BuildToHeaven.GameManagement
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Camera cam;
        [SerializeField] float offset;
        [HideInInspector] public float currentHeight;

        private void Start()
        {
            currentHeight = GameManager.instance.platform.transform.position.y;
        }

        private void Update()
        {
            currentHeight = FindCamHeight(GameManager.instance.placedBlocks);
            cam.transform.position = new(cam.transform.position.x,currentHeight + offset, -10);
        }

        public float FindCamHeight(List<Block> blocks)
        {
            IEnumerable<Block> validBlocks = blocks.Where(block => block.transform.position.y >= currentHeight && block.resolved == BlockResolution.success);


            float max = currentHeight;
            foreach(var block in validBlocks)
            {
                if(block.transform.position.y > max)
                {
                    max = block.transform.position.y;
                }
            }

            return max;
        }
    }
}
