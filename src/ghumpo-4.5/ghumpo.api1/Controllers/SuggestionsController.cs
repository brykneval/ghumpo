using System;
using System.Collections.Generic;
using System.Web.Http;
using ghumpo.model.Mobile;
using ghumpo.service.Query;

namespace ghumpo.api1.Controllers
{
    public class SuggestionsController : ApiController
    {
        private readonly ISuggestionsQueryService _suggestionsQueryService;

        public SuggestionsController(ISuggestionsQueryService suggestionsQueryService)
        {
            _suggestionsQueryService = suggestionsQueryService;
        }

        [HttpGet]
        public FormatListApi<SuggestionMobile> Get(string count)
        {
            var suggestions = _suggestionsQueryService.Suggestions();
            if (suggestions.Count == Convert.ToInt32(count))
            {
                return new FormatListApi<SuggestionMobile> {message = "", data = new List<SuggestionMobile>()};
            }
            return new FormatListApi<SuggestionMobile> {message = "", data = suggestions};
        }

        [HttpGet]
        public FormatListApi<LocationSuggestionMobile> Get(string query, string filter_city, string lat, string lon,
            string sort, string type, string offset)
        {
            var suggestions = _suggestionsQueryService.LocationSuggestions(query, filter_city, lat, lon,
                sort, type, offset);
            return new FormatListApi<LocationSuggestionMobile> {message = "", data = suggestions};
        }
    }
}