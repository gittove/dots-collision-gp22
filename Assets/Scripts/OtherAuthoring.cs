using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class OtherAuthoring : MonoBehaviour
{
    public class OtherBaker : Baker<OtherAuthoring>
    {
        public override void Bake(OtherAuthoring authoring)
        {
            var e = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<OtherTag>(e);
        }
    }
}
