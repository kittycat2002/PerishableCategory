using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace PerishableCategory;

[DefOf]
public static class CategoryDefOf
{
	[UsedImplicitly] public static ThingCategoryDef? Perishables;

	static CategoryDefOf()
	{
		DefOfHelper.EnsureInitializedInCtor(typeof(ThingCategoryDefOf));
	}
}