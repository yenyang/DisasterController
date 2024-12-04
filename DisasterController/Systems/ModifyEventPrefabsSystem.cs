// <copyright file="ModifyEventPrefabsSystem.cs" company="Yenyangs Mods. MIT License">
// Copyright (c) Yenyangs Mods. MIT License. All rights reserved.
// </copyright>

namespace DisasterController.Systems
{
    using Colossal.Entities;
    using Colossal.Logging;
    using Colossal.Serialization.Entities;
    using DisasterController;
    using Game;
    using Game.Prefabs;
    using Unity.Entities;

    /// <summary>
    /// Modifies the probability of events.
    /// </summary>
    public partial class ModifyEventPrefabsSystem : GameSystemBase
    {
        public readonly PrefabID ForestFirePrefabID = new PrefabID("EventPrefab", "Forest Fire");
        public readonly PrefabID BuildingFirePrefabID = new PrefabID("EventPrefab", "Building Fire");
        public readonly PrefabID LoseControlAccidentPrefabID = new PrefabID("EventPrefab", "Lose Control Accident");
        public readonly PrefabID LightningStrikePrefabID = new PrefabID("EventPrefab", "Lightning Strike");

        private ILog m_Log;
        private PrefabSystem m_PrefabSystem;

        /// <summary>
        /// Sets the fire probability of event to 0.
        /// </summary>
        public void SetFireProbabilityToZero(PrefabID prefabID)
        {
            if (m_PrefabSystem.TryGetPrefab(prefabID, out PrefabBase prefab)
                && m_PrefabSystem.TryGetEntity(prefab, out Entity prefabEntity)
                && EntityManager.TryGetComponent(prefabEntity, out Game.Prefabs.FireData fireData))
            {
                fireData.m_StartProbability = 0;
                EntityManager.SetComponentData(prefabEntity, fireData);
            }            

            m_Log.Info($"{nameof(ModifyEventPrefabsSystem)}.{nameof(SetFireProbabilityToZero)} Complete.");
        }

        /// <summary>
        /// Sets the probability of fire event to vanilla.
        /// </summary>
        public void ResetFireProbability(PrefabID prefabID)
        {
            if (m_PrefabSystem.TryGetPrefab(prefabID, out PrefabBase prefab) 
                && prefab.TryGet(out Game.Prefabs.Fire fire)
                && m_PrefabSystem.TryGetEntity(prefab, out Entity prefabEntity)
                && EntityManager.TryGetComponent(prefabEntity, out Game.Prefabs.FireData fireData))
            {
                fireData.m_StartProbability = fire.m_StartProbability;
                EntityManager.SetComponentData(prefabEntity, fireData);
            }

            m_Log.Info($"{nameof(ModifyEventPrefabsSystem)}.{nameof(ResetFireProbability)} Complete.");
        }

        /// <inheritdoc/>
        protected override void OnCreate()
        {
            base.OnCreate();
            m_Log = Mod.log;
            m_Log.Info($"{nameof(ModifyEventPrefabsSystem)}.OnCreate");

            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();

            Enabled = false;
        }

        /// <inheritdoc/>
        protected override void OnUpdate()
        {
            return;
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);
            HandleToggle(Mod.m_Setting.BuildingFireToggle, BuildingFirePrefabID);

            HandleToggle(Mod.m_Setting.ForestFireToggle, ForestFirePrefabID);

            HandleToggle(Mod.m_Setting.LightningStrikeToggle, LightningStrikePrefabID);

            HandleToggle(Mod.m_Setting.LoseControlAccidentToggle, LoseControlAccidentPrefabID);


        }

        private void HandleToggle(bool toggle, PrefabID prefabID)
        {
            if (!toggle)
            {
                SetFireProbabilityToZero(prefabID);
            }
            else
            {
                ResetFireProbability(prefabID);
            }
        }
    }
}
