using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct MovementSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        
    }

    public void OnUpdate(ref SystemState state)
    {
        state.Dependency.Complete();

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        var mgr = state.EntityManager;
        var j = new MoveJob
        {
            Time = Time.deltaTime,
            xInput = x,
            yInput = y,
            EntityManager = mgr
        };

        j.Run(); 
    }

    private partial struct MoveJob : IJobEntity
    {
        public float Time;
        public float yInput;
        public float xInput;

        public EntityManager EntityManager;
        
        private void Execute(Entity entity, ref LocalTransform transform, in MovementComponent movementComponent)
        {
            float3 dir = new float3(xInput, 0f, yInput) * transform.Forward();
            float acc = movementComponent.Speed * Time;

            float3 v = dir * new float3(acc, 0.0f, acc);

            transform.Position += v;
        }
    }
}
