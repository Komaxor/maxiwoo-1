using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.Place.Models;

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories.Place
{
    public class PlaceCache : IPlaceCache
    {
        private readonly ILogger<PlaceCache> _logger;
        private ConcurrentDictionary<Guid, List<PlaceDb>> _cache = new ConcurrentDictionary<Guid, List<PlaceDb>>();

        public PlaceCache(ILogger<PlaceCache> logger)
        {
            _logger = logger;
        }

        public void Invalidate()
        {
            try
            {
                _cache.Clear();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Can not invalidate cache of places");
                _cache = new ConcurrentDictionary<Guid, List<PlaceDb>>();
            }
        }
        
        public List<PlaceDb> Get(Guid institueId)
        {
            if (_cache.TryGetValue(institueId, out var places))
            {
                return places;
            }

            _logger.LogDebug($"Can not find places in cache by instuteId ({institueId})");
            
            return null;
        }

        public void Set(Guid institueId, List<PlaceDb> places)
        {
            try
            {
                _cache[institueId] = places;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Can not update cache of places for instituteId ({institueId})");
            }
        }
    }
}