using System.Collections.Generic;
using System.Numerics;
using Impostor.Api.Innersloth;
using Impostor.Api.Innersloth.Maps;
using Impostor.Api.Net.Custom;
using Impostor.Api.Net.Inner.Objects.ShipStatus;
using Impostor.Server.Net.Inner.Objects.Systems;
using Impostor.Server.Net.Inner.Objects.Systems.ShipStatus;
using Impostor.Server.Net.State;

namespace Impostor.Server.Net.Inner.Objects.ShipStatus
{
    internal class InnerPolusShipStatus : InnerShipStatus, IInnerPolusShipStatus
    {
        public InnerPolusShipStatus(ICustomMessageManager<ICustomRpc> customMessageManager, Game game) : base(customMessageManager, game)
        {
        }

        public override IMapData Data => IMapData.Maps[MapTypes.Polus];

        public override Dictionary<int, bool> Doors { get; } = new Dictionary<int, bool>(12);

        public override float SpawnRadius => 1f;

        public override Vector2 InitialSpawnCenter { get; } = new Vector2(16.64f, -2.46f);

        public override Vector2 MeetingSpawnCenter { get; } = new Vector2(17.4f, -16.286f);

        public Vector2 MeetingSpawnCenter2 { get; } = new Vector2(17.4f, -17.515f);

        public Vector2 ElectricalSpawn { get; } = new Vector2(5.53f, -9.84f);
        public Vector2 O2Spawn { get; } = new Vector2(3.28f, -21.67f);
        public Vector2 SpecimenSpawn { get; } = new Vector2(36.54f, -20.84f);
        public Vector2 LaboSpawn { get; } = new Vector2(34.91f, -6.50f);

        public override Vector2 GetSpawnLocation(InnerPlayerControl player, int numPlayers, bool initialSpawn)
        {
            var rand = new System.Random();
            int randVal = rand.Next(0,5);
            switch(randVal){
                case 0:
                    return this.InitialSpawnCenter;
                        // return base.GetSpawnLocation(player, numPlayers, initialSpawn);
                case 1:
                    return this.MeetingSpawnCenter;
                    // var halfPlayers = numPlayers / 2; // floored intentionally
                    // var spawnId = player.PlayerId % 15;
                    // if (player.PlayerId < halfPlayers)
                    // {
                    //     return this.MeetingSpawnCenter + (new Vector2(0.6f, 0) * spawnId);
                    // }
                    // else
                    // {
                    //     return this.MeetingSpawnCenter2 + (new Vector2(0.6f, 0) * (spawnId - halfPlayers));
                    // }
                case 2:
                    return this.ElectricalSpawn;
                case 3:
                    return this.O2Spawn;
                case 4:
                    return this.SpecimenSpawn;
                case 5:
                    return this.LaboSpawn;

                default:
                        return base.GetSpawnLocation(player, numPlayers, initialSpawn);
            }
        }

        protected override void AddSystems(Dictionary<SystemTypes, ISystemType> systems)
        {
            base.AddSystems(systems);

            systems.Add(SystemTypes.Doors, new DoorsSystemType(Doors));
            systems.Add(SystemTypes.Comms, new HudOverrideSystemType());
            systems.Add(SystemTypes.Security, new SecurityCameraSystemType());
            systems.Add(SystemTypes.Laboratory, new ReactorSystemType());
        }
    }
}
