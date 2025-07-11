using HarmonyLib;
using Verse;
namespace PerishableCategory;
public class PerishableCategoryMod : Mod
{
    public PerishableCategoryMod(ModContentPack content) : base(content)
    {
        Harmony harmony = new("xyz.nekogaming.rimworld.perishablecategory");
        harmony.PatchAll();
    }
}