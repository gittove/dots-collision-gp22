using Unity.Entities;
using Unity.Collections;
using Unity.Physics;
using UnityEngine;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct TriggerSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {

    }

    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        var j = new ProcessTriggerEventsJob {
            OtherTag = SystemAPI.GetComponentLookup<OtherTag>(isReadOnly: true),
            PlayerTag = SystemAPI.GetComponentLookup<PlayerTag>(isReadOnly: true),
            Ecb = ecb
        };

        state.Dependency = j.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        state.Dependency.Complete(); 
        
        ecb.Playback(state.EntityManager);
    }

    public partial struct ProcessTriggerEventsJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<OtherTag> OtherTag;
        [ReadOnly] public ComponentLookup<PlayerTag> PlayerTag;
        public EntityCommandBuffer Ecb;

        public void Execute(Unity.Physics.TriggerEvent ce)
        {
            var entityA = ce.EntityA;
            var entityB = ce.EntityB;

            if (OtherTag.HasComponent(entityA) && PlayerTag.HasComponent(entityB)
            || OtherTag.HasComponent(entityB) && PlayerTag.HasComponent(entityA))
            {
                Debug.LogError("I'M COLLIDING AAAAA IT HURTS");
            }
        }
    }
}
