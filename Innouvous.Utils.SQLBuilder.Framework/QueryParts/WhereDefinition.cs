﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Framework.QueryParts
{
    public enum WhereType
    {
        NA,
        And,
        Or
    }

    public class WhereDefinition
    {
        public IColumn Column { get; private set; }

        public object Value { get; private set; }

        public string Operator { get; private set; }

        
        public WhereType Joiner { get; private set; }

        public WhereDefinition(IColumn column, string op, object value)
            : this(WhereType.NA, column, op, value)
        {
        }

        public WhereDefinition(WhereType type, IColumn column, string op, object value)
        {
            this.Column = column;
            this.Value = value;
            this.Joiner = type;
            this.Operator = op;
        }
    }
}
