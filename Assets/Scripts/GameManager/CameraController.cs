using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildToHeaven.Cards;
using System.Linq;
using BuildToHeaven.Effects;

namespace BuildToHeaven.GameManagement
{
    public class CameraController : MonoBehaviour
    {
        //[SerializeField] Camera cam;
        public Transform cameraTarget;
        [SerializeField] float offset;
        [HideInInspector] public float currentHeight;

        private void Start()
        {
            currentHeight = GameManager.instance.platform.transform.position.y;
        }

        public void MoveCamera()
        {
            currentHeight = FindCamHeight(GameManager.instance.placedBlocks);
            cameraTarget.transform.position = new(cameraTarget.transform.position.x, currentHeight + offset, -10);
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
