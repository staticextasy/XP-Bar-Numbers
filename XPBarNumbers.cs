using BepInEx;
using HarmonyLib;
using UnityEngine;
using TMPro;

namespace XPBarNumbers
{
    [BepInPlugin("com.staticextasy.xpbarnumbers", "XP Bar Numbers", "1.0.0")]
    public class BetterXPDisplayPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("XP Bar Numbers plugin loaded!");
            Harmony harmony = new Harmony("com.staticextasy.xpbarnumbers");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(LiveStatUpdate), "FixedUpdate")]
    public static class LiveStatUpdatePatch
    {
        public static void Postfix(LiveStatUpdate __instance)
        {
            if (__instance?.PlayerStats != null && __instance.XP != null)
            {
                int currentXP = __instance.PlayerStats.CurrentExperience;
                int requiredXP = __instance.PlayerStats.ExperienceToLevelUp;
                int xpPercent = Mathf.RoundToInt((float)currentXP / (float)requiredXP * 100f);

                // Optional: Hide XP if max level reached (35)
                if (__instance.PlayerStats.Level >= 35)
                {
                    __instance.XP.text = "Max Level";
                }
                else
                {
                    __instance.XP.text = $"{xpPercent}% ({currentXP}/{requiredXP})";
                }
            }
        }
    }
}