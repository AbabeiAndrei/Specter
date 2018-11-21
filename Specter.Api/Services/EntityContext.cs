using System;
using System.Collections.Generic;

using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

using Specter.Api.Entities;

namespace Specter.Api.Services
{
    public class EntityContext : IEntitiyContext
    {
        private IFirebaseClient _client;

        public IEnumerable<User> Users => GetCollection<User>();

        public EntityContext()
        {
            var config = new FirebaseConfig
            {
                AuthSecret = "AIzaSyCEjdAJBTYPti6t8qHLNfpNq7KjdeRRB8Q",
                BasePath = "https://specter-ts.firebaseio.com"
            };

            _client = new FirebaseClient(config);
        }

        private IEnumerable<T> GetCollection<T>()
        {
            _client.GetAsync(typeof(T).Name)
        }
    }
}