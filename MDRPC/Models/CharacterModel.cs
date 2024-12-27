using System.Xml.Linq;
using Il2CppAssets.Scripts.Database;
using Il2CppAssets.Scripts.PeroTools.Nice.Interface;

namespace MDRPC.Models;

internal class CharacterModel
{
    private static readonly List<string> CosCharacters = new()
    {
        { "Rin" },
        { "Buro" },
        { "Marija" }
    };

    public static string GetName()
    {
        var entity = GlobalDataBase
            .dbConfig
            .m_ConfigDic["character"]
            .Cast<DBConfigCharacter>()
            .GetLocal()
            .GetInfoByIndex(DataHelper.selectedRoleIndex);
        var roleSkinSettings = DataHelper.roleSkinSettings;
        if (entity == null)
            return Constants.Discord.UnknownCharacter;
        string skin = "";
        IData selectedChar = null;
        for (int i = 0; i < roleSkinSettings.Count; i++)
        {
            if (VariableUtils.GetResult<int>(DataHelper.roleSkinSettings[i].fields["index"]) == DataHelper.selectedRoleIndex)
            {
                selectedChar = roleSkinSettings[i];
                break;
            }
        }
        if (selectedChar != null && entity.cosName != "Sailor Suit")
        {
            int skinIndex = VariableUtils.GetResult<int>(selectedChar.fields["skinIndex"]);
            if (skinIndex == 1)
                skin = "(Nya form)";
            if (skinIndex == 2)
                skin = "(Muse Warrior)";
        }

        if (CosCharacters.Contains(entity.characterName, StringComparer.OrdinalIgnoreCase))
            return $"{entity.cosName} {entity.characterName} {skin}".Trim();
        return $"{entity.characterName} {skin}".Trim();
    }
}
