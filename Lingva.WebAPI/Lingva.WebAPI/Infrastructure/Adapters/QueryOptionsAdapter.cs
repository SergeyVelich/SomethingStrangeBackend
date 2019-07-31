﻿using Lingva.WebAPI.Infrastructure.Models;
using QueryBuilder.Enums;
using QueryBuilder.QueryOptions;
using QueryBuilder.QueryOptions.Filter;
using QueryBuilder.QueryOptions.Includer;
using QueryBuilder.QueryOptions.Pagenator;
using QueryBuilder.QueryOptions.Sorter;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Infrastructure.Adapters
{
    [ExcludeFromCodeCoverage]
    public class QueryOptionsAdapter
    {
        public QueryOptionsAdapter()
        {

        }

        public virtual IQueryOptions Map(GroupsListOptionsModel optionsModel)
        {
            List<QueryFilter> filters = new List<QueryFilter>();
            if (optionsModel.Name != null)
            {
                filters.Add(new QueryFilterElement("Name", optionsModel.Name, FilterElementOperation.Contains, new QueryFilterAdditionalOptions() { IgnoreCase = true }));
            }                
            if (optionsModel.LanguageId != 0)
            {
                filters.Add(new QueryFilterElement("Language.Id", optionsModel.LanguageId, FilterElementOperation.Equal));
            }               
            if (optionsModel.DateFrom != DateTime.MinValue)
            {
                filters.Add(new QueryFilterElement("Date", optionsModel.DateFrom, FilterElementOperation.GreaterThanOrEqual));
            }
            if(optionsModel.DateTo != DateTime.MaxValue)
            {               
                filters.Add(new QueryFilterElement("Date", optionsModel.DateTo, FilterElementOperation.LessThanOrEqual));
            }              

            List<QuerySorter> sorters = new List<QuerySorter>();
            SortOrder sortOrder = Enum.Parse<SortOrder>(optionsModel.SortOrder, true);
            sorters.Add(new QuerySorter(optionsModel.SortProperty, sortOrder));

            List<QueryIncluder> includers = new List<QueryIncluder>();
            includers.Add(new QueryIncluder("Language"));

            int take = optionsModel.PageSize;
            int skip = optionsModel.PageSize * (Math.Max(optionsModel.PageIndex, 1) - 1);
            QueryPagenator pagenator = new QueryPagenator(take, skip);

            IQueryOptions queryOptions = new QueryOptions(
                filters: filters,
                sorters: sorters,
                includers: includers,
                pagenator: pagenator);

            return queryOptions;
        }
    }
}
