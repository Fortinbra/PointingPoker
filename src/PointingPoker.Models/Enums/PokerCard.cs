// <copyright file="PokerCard.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointingPoker.Models.Enums
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Temp")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:Enumeration items should be documented", Justification = "Temp")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public enum PokerCard
	{
		None = 0,
		Zero = 1,
		One = 2,
		Two = 3,
		Three = 4,
		Five = 5,
		Eight = 6,
		Thirteen = 7,
		Twenty = 8,
		Forty = 9,
		Hundred = 10,
		Infinity = 11,
		QuestionMark = 12,
	}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
