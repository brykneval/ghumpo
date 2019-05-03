using System.Collections.Generic;
using ghumpo.model.Mobile;

namespace ghumpo.search
{
    public interface ILuceneSearchRestaurant<T, T1> : ILuceneSearch<T>
    {
        IList<RestaurantHomeMobile> SearchRestaurantHomeIndex(string searchQuery);

        IList<T1> SearchIndex(string searchQuery, string filterCity, string lat, string lon,
            string sort, string type, string offset);

        IList<LocationSuggestionMobile> SearchLocationIndex(string searchQuery, string filterCity, string lat,
            string lon,
            string sort, string type, string offset);
    }
}