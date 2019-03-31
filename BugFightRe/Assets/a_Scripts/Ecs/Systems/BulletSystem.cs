using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

public class BulletSystem : ComponentSystem
{
    ComponentGroup m_Spawners;

    protected override void OnCreateManager()
    {
        m_Spawners = GetComponentGroup(typeof(UnitSpawner), typeof(Position));
    }
    
    protected override void OnUpdate()
    {
        Debug.Log("Created");
        // Get all the spawners in the scene.
        using (var spawners = m_Spawners.ToEntityArray(Allocator.TempJob))
        {
            foreach (var spawner in spawners)
            {
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        // Create an entity from the prefab set on the spawner component.
                        var prefab = EntityManager.GetSharedComponentData<UnitSpawner>(spawner).prefab;
                        var entity = EntityManager.Instantiate(prefab);

                        // Copy the position of the spawner to the new entity.
                        var position = EntityManager.GetComponentData<Position>(spawner);
                        position.Value.x = position.Value.x + 2 * i;
                        position.Value.z = position.Value.z + 2 * j;

                        EntityManager.SetComponentData(entity, position);

                        var aabb = new AABB
                        {
                            //0.5f will represent halfwidth for now
                            max = position.Value + 0.5f,
                            min = position.Value - 0.5f,

                        };
                        EntityManager.SetComponentData(entity, aabb);
                    }
                }

                // Destroy the spawner so this system only runs once.
                EntityManager.DestroyEntity(spawner);
            }
        }
    }
}
