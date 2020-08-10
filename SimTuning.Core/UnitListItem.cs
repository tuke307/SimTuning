// project=SimTuning.Core, file=UnitListItem.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using System;
using UnitsNet;

namespace SimTuning.Core
{
    /// <summary>
    ///     Represents an item in the from/to unit listboxes.
    ///     Provides a formatted <see cref="Text" /> property as well as holding on to the original unit enum value, in order
    ///     to perform the unit conversion.
    /// </summary>
    public sealed class UnitListItem
    {
        public UnitListItem(Enum val)
        {
            this.UnitEnumValue = val;
            this.UnitEnumValueInt = Convert.ToInt32(val);
            this.UnitEnumType = val.GetType();
            this.Abbreviation = UnitAbbreviationsCache.Default.GetDefaultAbbreviation(this.UnitEnumType, this.UnitEnumValueInt);

            this.Text = $"{val} [{this.Abbreviation}]";
        }

        public string Text { get; }
        public Enum UnitEnumValue { get; }
        public int UnitEnumValueInt { get; }
        public Type UnitEnumType { get; }
        public string Abbreviation { get; }
    }
}