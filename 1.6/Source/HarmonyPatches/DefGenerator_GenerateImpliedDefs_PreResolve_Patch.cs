using HarmonyLib;
using RimWorld;
using Verse;

namespace PerishableCategory.HarmonyPatches;

[HarmonyPatch(typeof(DefGenerator), nameof(DefGenerator.GenerateImpliedDefs_PreResolve))]
internal static class DefGenerator_GenerateImpliedDefs_PreResolve_Patch
{
	internal static void Postfix(bool hotReload)
	{
		foreach (ThingCategoryDef thingCategoryDef in
		         ThingCategoryDefGenerator_Perishable.ImpliedThingCategoryDefs(hotReload))
		{
			DefGenerator.AddImpliedDef(thingCategoryDef, hotReload);
		}
	}
}