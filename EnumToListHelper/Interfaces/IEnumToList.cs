using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnumToListHelper.Interfaces
{
    public interface IEnumToList
    {
        SelectList EnumSelectList<TEnum>(bool indexed = false);
        string GetEnumDescription(Enum value);
        T GetValueFromDescription<T>(string description);
        List<int> GetEnumToListValue<TEnum>(bool indexed = false)
            where TEnum : struct;
    }
}
