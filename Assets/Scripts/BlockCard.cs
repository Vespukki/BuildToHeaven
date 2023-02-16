using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu()]
public class BlockCard : Card
{
    [SerializeField] Block _block;
    public Block HeldBlock => _block;

    [SerializeField] string _cardName;
    public string CardName => _cardName;

    public override Sprite PreviewSprite
    {
        get { return HeldBlock.GetComponent<SpriteRenderer>().sprite; }
    }

    public override void Use(Vector2 position)
    {
        Instantiate(HeldBlock, position, Quaternion.identity);
    }
}
