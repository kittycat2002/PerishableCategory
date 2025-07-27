using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;

namespace PerishableCategory;

public static class ThingCategoryDefGenerator_Perishable
{
	private static readonly Dictionary<ThingCategoryDef, ThingCategoryDef> createdDefs = [];

	public static IEnumerable<ThingCategoryDef> ImpliedThingCategoryDefs(bool hotReload = false)
	{
		createdDefs.Clear();
		foreach (ThingDef thingDef in DefDatabase<ThingDef>.AllDefs)
		{
			if (!thingDef.HasComp<CompRottable>() || thingDef.thingCategories.Count <= 0) continue;
			foreach (ThingCategoryDef thingDefCategory in thingDef.thingCategories.Where(thingCategoryDef =>
				         !thingCategoryDef.defName.EndsWith("_perishable") &&
				         thingCategoryDef.Parents.Contains(ThingCategoryDefOf.Root)).ToList())
			{
				thingDef.thingCategories.Add(BaseThingCategoryDef(thingDefCategory, hotReload));
			}
		}

		return DefDatabase<ThingCategoryDef>.AllDefs
			.Intersect(createdDefs.Keys)
			.Select(def => createdDefs[def]).ToList(); // Making sure the elements are ordered correctly
	}

	private static ThingCategoryDef BaseThingCategoryDef(ThingCategoryDef baseDef, bool hotReload = false)
	{
		if (createdDefs.TryGetValue(baseDef, out ThingCategoryDef existingDef))
		{
			return existingDef;
		}

		string defName = $"{baseDef.defName}_perishable";
		ThingCategoryDef thingCategoryDef = hotReload
			? DefDatabase<ThingCategoryDef>.GetNamed(defName, false) ?? new ThingCategoryDef()
			: new ThingCategoryDef();
		thingCategoryDef.defName = defName;
		thingCategoryDef.label = baseDef.label;
		thingCategoryDef.childSpecialFilters = baseDef.childSpecialFilters;
		thingCategoryDef.parent = baseDef.parent == ThingCategoryDefOf.Root || baseDef.parent is null
			? CategoryDefOf.Perishables
			: BaseThingCategoryDef(baseDef.parent);
		createdDefs.Add(baseDef, thingCategoryDef);
		return thingCategoryDef;
	}
}