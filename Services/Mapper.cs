namespace Stacks_rework.Services
{
    public static class Mapper
    {
        public static TDto AutoMap<T, TDto>(this T item)
        where TDto : class
        where T : class
        {
            var list = item.GetType().GetProperties();
            var inst = Activator.CreateInstance(typeof(TDto));
            foreach (var i in list)
            {
                if (((TDto)inst!).GetType().GetProperty(i.Name) == null)
                    continue;
                try
                {
                    var valor = i.GetValue(item, null);
                    ((TDto)inst).GetType().GetProperty(i.Name)?.SetValue((TDto)inst, valor);
                }
                catch { }
            }
            return (TDto)inst!;
        }
    }
}
