using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class MovementComponentAuthoring : MonoBehaviour
{
    public int speed;
    
}

public class MovementComponentBaker : Baker<MovementComponentAuthoring>
{
    public override void Bake(MovementComponentAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new MovementComponent { Speed = authoring.speed });
    }
}