using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using Paw.Services.Util;

namespace Paw.Services.Messages
{
    public static class MessageExtensions
    {
        #region Add ...

        public static int ExecuteNonQuery<T>(this IAdd<T> message) where T : class, new()
        {
            using (var context = DataContext.CreateForMessage(message))
            {
                return ExecuteNonQuery<T>(message, context);
            }
        }

        public static int ExecuteNonQuery<T>(this IAdd<T> message, DataContext context) where T : class, new()
        {
            // Step 1. Create new entity
            T entity = new T();

            // Step 2. Map from message
            entity.InjectFrom<CommonValueInjection>(message);

            // Step 3. Add entity
            context.Set<T>().Add(entity);

            return context.SaveChanges();
        }

        #endregion

        #region Update ...

        public static int ExecuteNonQuery<T>(this IUpdate<T> message) where T : class, IId
        {
            using (var context = DataContext.CreateForMessage(message))
            {
                return ExecuteNonQuery<T>(message, context);
            }
        }

        public static int ExecuteNonQuery<T>(this IUpdate<T> message, DataContext context) where T : class, IId
        {
            // Step 1. Get entity
            T entity = context.Set<T>().Where(x => x.Id == message.Id).SingleOrDefault();

            if (entity == null) return 0;

            // Step 2. Map from message
            entity.InjectFrom<CommonValueInjection>(message);

            return context.SaveChanges();
        }

        public static int ExecuteNonQuery<T>(this IUpdateProvider<T> message, DataContext context) where T : class, IId, IProviderId
        {
            // Step 1. Get entity
            T entity = context.Set<T>().Where(x => x.Id == message.Id && x.ProviderId == message.ProviderId).SingleOrDefault();

            if (entity == null) return 0;

            // Step 2. Map from message
            entity.InjectFrom<ExcludeReadOnly>(message);

            return context.SaveChanges();
        }

        public static int ExecuteNonQuery<T>(this IUpdateProviderGroup<T> message, DataContext context) where T : class, IProviderGroupId
        {
            // Step 1. Get entity
            T entity = context.Set<T>().Where(x => x.Id == message.Id && x.ProviderGroupId == message.ProviderGroupId).SingleOrDefault();

            if (entity == null) return 0;

            // Step 2. Map from message
            entity.InjectFrom<CommonValueInjection>(message);

            return context.SaveChanges();
        }

        #endregion

        #region Delete ...

        public static int ExecuteNonQuery<T>(this IDelete<T> message) where T : class, IId, new()
        {
            using (var context = DataContext.CreateForMessage(message))
            {
                T entity = context.Set<T>().Where(x => x.Id == message.Id).SingleOrDefault();

                if (entity == null) return 0;

                context.Set<T>().Remove(entity);

                return context.SaveChanges();
            }
        }

        public static int ExecuteNonQuery<T>(this IDeleteProvider<T> message) where T : class, IId, IProviderId, new()
        {
            using (var context = DataContext.CreateForMessage(message))
            {
                T entity = context.Set<T>().Where(x => x.Id == message.Id && x.ProviderId == message.ProviderId).SingleOrDefault();

                if (entity == null) return 0;

                context.Set<T>().Remove(entity);

                return context.SaveChanges();
            }
        }

        public static int ExecuteNonQuery<T>(this IDeleteProviderGroup<T> message) where T : class, IId, IProviderGroupId, new()
        {
            using (var context = DataContext.CreateForMessage(message))
            {
                T entity = context.Set<T>().Where(x => x.Id == message.Id && x.ProviderGroupId == message.ProviderGroupId).SingleOrDefault();

                if (entity == null) return 0;

                context.Set<T>().Remove(entity);

                return context.SaveChanges();
            }
        }

        #endregion

        #region Get ...

        public static T ExecuteItem<T>(this IGet<T> message) where T : class, IId, new()
        {
            using (var context = DataContext.CreateForMessage(message))
            {
                T entity = context.Set<T>().Where(x => x.Id == message.Id).SingleOrDefault();

                return entity;
            }
        }

        public static T ExecuteItem<T>(this IGetProvider<T> message) where T : class, IId, IProviderId, new()
        {
            using (var context = DataContext.CreateForMessage(message))
            {
                T entity = context.Set<T>().Where(x => x.Id == message.Id && x.ProviderId == message.ProviderId).SingleOrDefault();

                return entity;
            }
        }

        public static T ExecuteItem<T>(this IGetProviderGroup<T> message) where T : class, IId, IProviderGroupId, new()
        {
            using (var context = DataContext.CreateForMessage(message))
            {
                T entity = context.Set<T>().Where(x => x.Id == message.Id && x.ProviderGroupId == message.ProviderGroupId).SingleOrDefault();

                return entity;
            }
        }

        public static R ExecuteItem<T, R>(this IGet<T, R> message)
            where T : class, IId
            where R : class, new()
        {
            using (var context = DataContext.CreateForMessage(message))
            {
                // Step 1. Get the entity
                T entity = context.Set<T>().Where(x => x.Id == message.Id).SingleOrDefault();

                // Step 2. Return null if not found
                if (entity == null)
                {
                    return null;
                }

                // Step 3. Create result
                R result = new R();

                // Step 4. Set values
                result.InjectFrom<CommonValueInjection>(entity);

                return result;
            }

        }

        public static R ExecuteItem<T, R>(this IGetProvider<T, R> message)
            where T : class, IId, IProviderId
            where R : class, new()
        {
            using (var context = DataContext.CreateForMessage(message))
            {
                // Step 1. Get the entity
                T entity = context.Set<T>().Where(x => x.Id == message.Id && x.ProviderId == message.ProviderId).SingleOrDefault();

                // Step 2. Return null if not found
                if (entity == null)
                {
                    return null;
                }

                // Step 3. Create result
                R result = new R();

                // Step 4. Set values
                result.InjectFrom<CommonValueInjection>(entity);

                return result;
            }

        }

        public static R ExecuteItem<T, R>(this IGetProviderGroup<T, R> message)
    where T : class, IId, IProviderGroupId
    where R : class, new()
        {
            using (var context = DataContext.CreateForMessage(message))
            {
                // Step 1. Get the entity
                T entity = context.Set<T>().Where(x => x.Id == message.Id && x.ProviderGroupId == message.ProviderGroupId).SingleOrDefault();

                // Step 2. Return null if not found
                if (entity == null)
                {
                    return null;
                }

                // Step 3. Create result
                R result = new R();

                // Step 4. Set values
                result.InjectFrom<CommonValueInjection>(entity);

                return result;
            }

        }

        public static R ExecuteItem<T, R>(this IGet<T, R> message, Expression<Func<T, bool>> predicate)
            where T : class, IId
            where R : class, new()
        {
            using (var context = DataContext.CreateForMessage(message))
            {
                // Step 1. Get the entity
                T entity = context.Set<T>().Where(predicate).SingleOrDefault();

                // Step 2. Return null if not found
                if (entity == null)
                {
                    return null;
                }

                // Step 3. Create result
                R result = new R();

                // Step 4. Set values
                result.InjectFrom<CommonValueInjection>(entity);

                return result;
            }

        }

        #endregion
        
        #region Timezone ...

        public static DateTime ToPST(this DateTime dateTime)
        {
            DateTime result = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTimeFromUtc(result, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));
        }

        public static DateTime ToUTC(this DateTime dateTime)
        {
            DateTime result = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));
        }

        public static DateTime GetLocalTime(this DateTime dateTime)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

            if (timeZoneInfo.IsDaylightSavingTime(dateTime))
            {
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Pacific Daylight Time");
            }
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZoneInfo);
        }

        #endregion
    }
}
