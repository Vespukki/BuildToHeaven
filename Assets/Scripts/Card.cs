using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public abstract class Card : ScriptableObject
{
    public abstract void Use(Vector2 position);

    public abstract Sprite PreviewSprite { get; }
  
}
