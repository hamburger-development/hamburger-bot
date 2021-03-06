﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hamburger.Core.PersistentStorage
{
    public interface IDbStorage
    {
        Task<T> StoreOne<T>(T item, string path);
        Task<IEnumerable<T>> StoreMany<T>(IEnumerable<T> items, string path);
        Task<T> UpdateOne<T>(T item, Expression<Func<T, bool>> predicate, string path);
        void DeleteOne<T>(Expression<Func<T, bool>> predicate, string path);
        void DeleteMany<T>(Expression<Func<T, bool>> predicate, string path);
        Task<T> FindOne<T>(Expression<Func<T, bool>> predicate, string path);
        Task<IEnumerable<T>> FindMany<T>(Expression<Func<T, bool>> predicate, string path);
        bool Exists<T>(Expression<Func<T, bool>> predicate, string path);
        Task<bool> CollectionExistsAsync(string path);
        Task DeleteCollectionAsync(string path);
    }
}
