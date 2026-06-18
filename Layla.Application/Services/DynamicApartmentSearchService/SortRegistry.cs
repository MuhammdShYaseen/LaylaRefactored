using Layla.Attributes;
using Layla.Models.DtosModels.MainDtos;
using System.Linq.Expressions;
using System.Reflection;

namespace Layla.Application.Services.DynamicApartmentSearchService
{
    internal static class SortRegistry
    {
        private static readonly Dictionary<string, LambdaExpression> _map;

        static SortRegistry()
        {
            _map = typeof(ApartmentSortMap)
           .GetProperties(BindingFlags.Public | BindingFlags.Static)
           .Where(p => p.IsDefined(typeof(SortableAttribute)))
           .Select(p =>
           {
               var value = p.GetValue(null);

               if (value is not LambdaExpression exp)
               {
                   throw new InvalidOperationException(
                       $"Property {p.Name} must be LambdaExpression");
               }

               return new
               {
                   Key = p.Name,
                   Expression = exp
               };
           })
           .ToDictionary(
               x => x.Key,
               x => x.Expression,
               StringComparer.OrdinalIgnoreCase
           );
        }

        public static bool TryGet(string key, out LambdaExpression expression)
        => _map.TryGetValue(key, out expression!);
    }
}
