﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Framework.QueryParts
{
    public class OrderByDefinition
    {
        public enum SortDirection
        {
            Ascending,
            Descending
        }

        public SortDirection Order { get; private set; }
        public ColumnDefinition Column { get; private set; }

        public OrderByDefinition(ColumnDefinition column, SortDirection order = SortDirection.Ascending)
        {
            this.Order = order;
            this.Column = column;
        }
    }
}
