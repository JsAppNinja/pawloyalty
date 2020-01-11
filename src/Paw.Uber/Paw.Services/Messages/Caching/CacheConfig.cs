using Paw.Services.Common;
using Paw.Services.Messages.Web.Pets;
using Paw.Services.Messages.Web.Providers;
using Paw.Services.Messages.Web.ScheduleBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Caching
{
    public static class CacheConfig
    {
        static CacheConfig()
        {
            List<CacheBehavior> behaviorList = new List<CacheBehavior>();
            
            // Provider(Item)
            behaviorList.Add(new CacheBehavior()
            {
                Type = typeof(Provider),
                CacheKeyAccessor = item => GetProviderCacheKey(item, CacheTypes.Item),
                CreateCacheAction = (item,actionType) => new CacheAction()
                    .SetItem(item)
                    .SetCacheActionType(actionType)
                    .SetSourceType(typeof(Provider))
                    .AddProperty("Id", item.GetId())
                    .AddProperty("ProviderId", item.GetProviderId()),
                TriggerTypeList = new List<Type>() { typeof(Provider) },
                ProcessCacheAction = ProcessProviderAction,
                CreateCacheContainer = null
            });

            // Employee(List)
            behaviorList.Add(new CacheBehavior()
            {
                Type = typeof(Employee),
                CacheKeyAccessor = item => GetProviderCacheKey(item, CacheTypes.Item),
                CreateCacheAction = (item, actionType) => new CacheAction()
                    .SetItem(item)
                    .SetCacheActionType(actionType)
                    .SetSourceType(typeof(Employee))
                    .AddProperty("Id", item.GetId())
                    .AddProperty("ProviderId", item.GetProviderId()),
                TriggerTypeList = new List<Type>() { typeof(Employee) },
                ProcessCacheAction = ProcessEmployeeAction,
                CreateCacheContainer = null
            });

            // ScheduleBlock(List)
            behaviorList.Add(new CacheBehavior()
            {
                Type = typeof(ScheduleBlock),
                CacheKeyAccessor = item => GetProviderCacheKey(item, CacheTypes.Item),
                CreateCacheAction = (item, actionType) => new CacheAction()
                    .SetItem(item)
                    .SetCacheActionType(actionType)
                    .SetSourceType(typeof(ScheduleBlock))
                    .AddProperty("Id", item.GetId())
                    .AddProperty("ProviderId", item.GetProviderId()),
                TriggerTypeList = new List<Type>() { typeof(ScheduleBlock) },
                ProcessCacheAction = ProcessScheduleBlockAction,
                CreateCacheContainer = null
            });

            // ScheduleRule(List)
            behaviorList.Add(new CacheBehavior()
            {
                Type = typeof(ScheduleRule),
                CacheKeyAccessor = item => GetProviderCacheKey(item, CacheTypes.Item),
                CreateCacheAction = (item, actionType) => new CacheAction()
                    .SetItem(item)
                    .SetCacheActionType(actionType)
                    .SetSourceType(typeof(ScheduleRule))
                    .AddProperty("Id", item.GetId())
                    .AddProperty("ProviderId", item.GetProviderId()),
                TriggerTypeList = new List<Type>() { typeof(ScheduleRule) },
                ProcessCacheAction = ProcessScheduleRuleAction,
                CreateCacheContainer = null
            });

            // PetLink(List)[Pet]
            behaviorList.Add(new CacheBehavior()
            {
                Type = typeof(PetLink),
                CacheKeyAccessor = item => GetProviderCacheKey(item, CacheTypes.Item),
                CreateCacheAction = (item, actionType) => new CacheAction()
                    .SetItem(item)
                    .SetCacheActionType(actionType)
                    .SetSourceType(typeof(Pet))
                    .AddProperty("Id", item.GetId())
                    .AddProperty("ProviderGroupId", item.GetProviderGroupId()),
                TriggerTypeList = new List<Type>() { typeof(Pet), typeof(Owner) },
                ProcessCacheAction = ProcessPetLinkAction,
                CreateCacheContainer = null
            });

            CacheConfig.CacheBehaviorList = behaviorList;
        }

        public static List<CacheBehavior> CacheBehaviorList
        {
            get { return _CacheBehaviorList; }
            set { _CacheBehaviorList = value; }
        }
        private static List<CacheBehavior> _CacheBehaviorList = new List<CacheBehavior>();

        #region Get CacheKey ...

        public static string GetProviderCacheKey(this object item, CacheTypes cacheType)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            
            string result = string.Empty;
            switch (cacheType)
            {
                case CacheTypes.Item:
                    result = $"mc_{item.GetType().Name}_{item.GetProviderId()}_{item.GetId()}";
                    break;
                case CacheTypes.ItemList:
                    result = $"mc_{item.GetType().Name}_{item.GetProviderId()}";
                    break;
            }

            return result;

        }

        public static string GetProviderGroupCacheKey(this object item, CacheTypes cacheType)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            IProviderGroupId providerGroupIdItem = item as IProviderGroupId;

            if (providerGroupIdItem == null)
            {
                throw new InvalidOperationException($"Type [{item.GetType().Name}] does not implement IProviderGroupId");
            }

            string result = string.Empty;
            switch (cacheType)
            {
                case CacheTypes.Item:
                    result = $"mc_{item.GetType().Name}_{providerGroupIdItem.ProviderGroupId}_{providerGroupIdItem.Id}";
                    break;
                case CacheTypes.ItemList:
                    result = $"mc_{item.GetType().Name}_{providerGroupIdItem.ProviderGroupId}";
                    break;
            }

            return result;

        }

        #endregion

        #region Get Primary and Foreign Key ...

        public static Guid GetProviderId(this object item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            IProviderId providerIdItem = item as IProviderId;

            if (providerIdItem == null)
            {
                throw new InvalidOperationException($"Type [{item.GetType()}] does not implement IProviderId.");
            }

            return providerIdItem.ProviderId;
        }

        public static Guid GetProviderGroupId(this object item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            IProviderGroupId providerGroupIdItem = item as IProviderGroupId;

            if (providerGroupIdItem == null)
            {
                throw new InvalidOperationException($"Type [{item.GetType()}] does not implement IProviderGroupId.");
            }

            return providerGroupIdItem.ProviderGroupId;
        }

        public static Guid GetId(this object item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            IId idItem = item as IId;

            if (idItem == null)
            {
                throw new InvalidOperationException($"Type [{item.GetType()}] does not implement IId.");
            }

            return idItem.Id;
        }

        #endregion

        #region CacheAction Processor ...
        
        public static void ProcessProviderAction(CacheBehavior cacheBehavior, CacheAction cacheAction)
        {
            // Step 1. Get cache key
            string cacheKey = cacheBehavior.GetCacheKey(cacheAction.Item);

            // Step 2. Get cache value
            CacheItemContainer<Provider> providerContainer = CacheHelper2.GetCacheValue<CacheItemContainer<Provider>>(cacheKey);
            
            if (providerContainer == null)
            {
                return;
            }

            // Step 3. Refresh
            Provider provider = new GetProvider() { Id = providerContainer.Item.Id }.ExecuteItem(false);
            providerContainer.Item = provider;


        }

        
        public static void ProcessEmployeeAction(CacheBehavior cacheBehavior, CacheAction cacheAction)
        {
            // Step 1. Get cache key
            string cacheKey = cacheBehavior.GetCacheKey(cacheAction.Item);

            // Step 2. Get cache value
            CacheListContainer<Employee> employeeListContainer = CacheHelper2.GetCacheValue<CacheListContainer<Employee>>(cacheKey);

            if (employeeListContainer == null)
            {
                return;
            }

            // Step 3. Process
            if (cacheAction.CacheActionType == CacheActionTypes.Insert)
            {
                employeeListContainer.Put((Employee)cacheAction.Item);
            }
            else if (cacheAction.CacheActionType == CacheActionTypes.Update)
            {
                employeeListContainer.Put((Employee)cacheAction.Item);
            }
            else if (cacheAction.CacheActionType == CacheActionTypes.Delete)
            {
                employeeListContainer.Delete(((Employee)cacheAction.Item).Id);
            }
        }

        public static void ProcessScheduleBlockAction(CacheBehavior cacheBehavior, CacheAction cacheAction)
        {
            // Step 1. Get cache key
            string cacheKey = cacheBehavior.GetCacheKey(cacheAction.Item);

            // Step 2. Get cache value
            CacheListContainer<ScheduleBlock> scheduleBlockListContainer = CacheHelper2.GetCacheValue<CacheListContainer<ScheduleBlock>>(cacheKey);

            if (scheduleBlockListContainer == null)
            {
                return;
            }

            // Step 3. Process
            if (cacheAction.CacheActionType == CacheActionTypes.Insert)
            {
                scheduleBlockListContainer.Put((ScheduleBlock)cacheAction.Item);
            }
            else if (cacheAction.CacheActionType == CacheActionTypes.Update)
            {
                scheduleBlockListContainer.Put((ScheduleBlock)cacheAction.Item);
            }
            else if (cacheAction.CacheActionType == CacheActionTypes.Delete)
            {
                scheduleBlockListContainer.Delete(((ScheduleBlock)cacheAction.Item).Id);
            }
        }

        public static void ProcessScheduleRuleAction(CacheBehavior cacheBehavior, CacheAction cacheAction)
        {
            // Step 1. Get cache key
            string cacheKey = cacheBehavior.GetCacheKey(cacheAction.Item);

            // Step 2. Get cache value
            CacheListContainer<ScheduleRule> scheduleRuleListContainer = CacheHelper2.GetCacheValue<CacheListContainer<ScheduleRule>>(cacheKey);

            if (scheduleRuleListContainer == null)
            {
                return;
            }

            // Step 3. Process
            if (cacheAction.CacheActionType == CacheActionTypes.Insert)
            {
                scheduleRuleListContainer.Put((ScheduleRule)cacheAction.Item);
            }
            else if (cacheAction.CacheActionType == CacheActionTypes.Update)
            {
                scheduleRuleListContainer.Put((ScheduleRule)cacheAction.Item);
            }
            else if (cacheAction.CacheActionType == CacheActionTypes.Delete)
            {
                scheduleRuleListContainer.Delete(((ScheduleRule)cacheAction.Item).Id);
            }
        }

        public static void ProcessPetLinkAction(CacheBehavior cacheBehavior, CacheAction cacheAction)
        {

        }

        #endregion
    }

    
}
