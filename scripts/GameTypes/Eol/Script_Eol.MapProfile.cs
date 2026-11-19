using System;

namespace InfServer.Script.GameType_Eol
{
    /// <summary>
    /// (2026-11-19, owner: "use this map [Eol Classic] instead of the eol pioneer ... but all the other stuff from eol pioneer
    /// like classes, bots etc. ... put that bug spawn i created on the map below the drop ship ... EXCEPT there is no toxic
    /// zones, the whole map is used")
    /// Everything the Eol scripts place on the map, per map. The map is picked from the zone's LvlFile at init: a level whose
    /// name has "classic" in it = Eol Classic, anything else = Pioneer Station (the values the scripts always had).
    /// Eol Classic positions: the dropship room is Classic's own (tiles 1249-1386 x 120-238); the bug cave was copied from
    /// Pioneer under it (shift -349,-98 tiles); everything placed on Pioneer's play map (tiles 0-1377 x 0-896) was scaled onto
    /// Classic's main map (0-1150 x 0-748, the same shape, x 0.835) and moved to the nearest open ground.
    /// Everything is a property (not a static field) so it is read after Init, never at type load.
    /// </summary>
    public static class EolMap
    {
        public static bool Classic { get; private set; }

        public static void Init(string lvlFile)
        {
            Classic = (lvlFile ?? "").IndexOf("classic", StringComparison.OrdinalIgnoreCase) >= 0;
            Log.write(TLog.Normal, "[Eol] Map profile: {0} (level {1}).", Classic ? "Eol Classic - whole map open, no toxic zones" : "Pioneer Station", lvlFile);
        }

        /// <summary>No toxic zones: every sector is always open, no daily radiation change, nobody sent back to the dropship.</summary>
        public static bool AllSectorsOpen { get { return Classic; } }

        /// <summary>Old fixed coordinate lists drawn for Pioneer (pylons): scaled onto Classic's main map.</summary>
        public static short P(int v) { return Classic ? (short)Math.Round(v * 0.835) : (short)v; }

        // ---- the dropship (the "MapPortal" / Classic "MapEnterA" portal, 80 x 80; bots appear 29 tiles south of it)
        public static short DropshipPortalX { get { return Classic ? (short)21160 : (short)27048; } }
        public static short DropshipPortalY { get { return Classic ? (short)2936 : (short)2904; } }
        public static short DeploySpawnX { get { return Classic ? (short)21160 : (short)27016; } }
        public static short DeploySpawnY { get { return Classic ? (short)3400 : (short)3368; } }
        /// <summary>Recall / respawn point in the dropship (Pioneer: 27008,3360).</summary>
        public static short RecallX { get { return Classic ? (short)21160 : (short)27008; } }
        public static short RecallY { get { return Classic ? (short)3400 : (short)3360; } }
        public static short SafeMinX { get { return Classic ? (short)19984 : (short)26048; } }
        public static short SafeMaxX { get { return Classic ? (short)22191 : (short)28080; } }
        public static short SafeMinY { get { return Classic ? (short)1920 : (short)2160; } }
        public static short SafeMaxY { get { return Classic ? (short)3823 : (short)3824; } }
        public static short HelmetTraderX { get { return (short)((Classic ? 1301 : 1667) * 16 + 8); } }
        public static short HelmetTraderY { get { return (short)((Classic ? 167 : 165) * 16 + 8); } }

        // ---- the play map / sector map
        public static int PlayMapMaxTileX { get { return Classic ? 1159 : 1377; } }
        public static int PlayMapMaxTileY { get { return Classic ? 748 : 896; } }
        /// <summary>A point known to be on the play map (tests whether the sector map is built).</summary>
        public static short MapProbeX { get { return Classic ? (short)5272 : (short)6305; } }
        public static short MapProbeY { get { return Classic ? (short)3576 : (short)4277; } }

        // ---- the bug cave + the swarm
        public static short CaveTileX { get { return Classic ? (short)1290 : (short)1639; } }
        public static short CaveTileY { get { return Classic ? (short)320 : (short)418; } }
        public static short CavePortalTileX { get { return Classic ? (short)1356 : (short)1705; } }
        public static short CavePortalTileY { get { return Classic ? (short)318 : (short)416; } }
        static readonly short[][] SitesP = { new short[] { 76, 178 }, new short[] { 55, 419 }, new short[] { 342, 260 } };
        static readonly short[][] SitesC = { new short[] { 57, 146 }, new short[] { 46, 355 }, new short[] { 286, 213 } };
        public static short[][] SwarmSites { get { return Classic ? SitesC : SitesP; } }
        static readonly short[][] ExtraP = { new short[] { 342, 260 }, new short[] { 448, 259 }, new short[] { 230, 246 }, new short[] { 629, 64 } };
        static readonly short[][] ExtraC = { new short[] { 286, 213 }, new short[] { 370, 216 }, new short[] { 195, 195 }, new short[] { 525, 76 } };
        public static short[][] SwarmExtraSpots { get { return Classic ? ExtraC : ExtraP; } }
        static readonly short[][] A3P = { new short[] { 96, 175 }, new short[] { 54, 156 }, new short[] { 17, 131 }, new short[] { 33, 213 } };
        static readonly short[][] A3C = { new short[] { 82, 147 }, new short[] { 45, 130 }, new short[] { 14, 109 }, new short[] { 33, 181 } };
        public static short[][] SwarmA3Spots { get { return Classic ? A3C : A3P; } }
        static readonly short[][] A6P = { new short[] { 21, 389 }, new short[] { 54, 375 }, new short[] { 52, 414 }, new short[] { 95, 416 } };
        static readonly short[][] A6C = { new short[] { 18, 325 }, new short[] { 45, 313 }, new short[] { 43, 343 }, new short[] { 81, 347 } };
        public static short[][] SwarmA6Spots { get { return Classic ? A6C : A6P; } }
        /// <summary>The area round the A6 spots (tiles) that the swarm and the bases share: min x, max x, min y, max y.</summary>
        public static short SwarmA6MinTX { get { return 0; } }
        public static short SwarmA6MaxTX { get { return Classic ? (short)99 : (short)119; } }
        public static short SwarmA6MinTY { get { return Classic ? (short)296 : (short)355; } }
        public static short SwarmA6MaxTY { get { return Classic ? (short)367 : (short)440; } }
        static readonly short[][] None = new short[0][];
        static readonly short[][] SwarmChokeP = { new short[] { 145, 410 } };
        public static short[][] SwarmChokeTiles { get { return Classic ? None : SwarmChokeP; } }

        // ---- other map spots
        static readonly short[][] BotChokeP = { new short[] { 146, 411 } };
        public static short[][] BotChokeTiles { get { return Classic ? None : BotChokeP; } }
        static readonly short[][] NoSpawnP = { new short[] { 361, 348 } };
        public static short[][] NoBotSpawnBuildingSeeds { get { return Classic ? None : NoSpawnP; } }
        static readonly short[][] VendorP = { new short[] { 88, 299 }, new short[] { 309, 80 }, new short[] { 159, 684 }, new short[] { 1250, 384 }, new short[] { 1231, 65 }, new short[] { 812, 93 }, new short[] { 660, 800 }, new short[] { 1125, 704 }, new short[] { 869, 432 } };
        static readonly short[][] VendorC = { new short[] { 85, 261 }, new short[] { 246, 67 }, new short[] { 133, 593 }, new short[] { 998, 304 }, new short[] { 1023, 54 }, new short[] { 678, 78 }, new short[] { 540, 657 }, new short[] { 939, 588 }, new short[] { 726, 361 } };
        public static short[][] VendorTiles { get { return Classic ? VendorC : VendorP; } }
    }
}
