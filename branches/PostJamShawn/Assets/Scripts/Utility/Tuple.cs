﻿// --------------------------------------------------------------------------
// Functional Programming in .NET - Chapter 3
// --------------------------------------------------------------------------
// NOTE: This library contains several useful classes for functional 
// programming in C# that we implemented in chapter 3 and that we'll 
// extend and use later in the book. Each secion is marked with a reference
// to a code listing or section in the book where it was discussed.
// --------------------------------------------------------------------------

using System;
using System.Collections.Generic;

// Listing 3.8: Improved type inference for tuples in C#

/// <summary>
/// Utility class that simplifies cration of tuples by using 
/// method calls instead of constructor calls
/// copied from https://gist.github.com/michaelbartnett/5652076
/// </summary>
public static class Tuple
{
	/// <summary>
	/// Creates a new tuple value with the specified elements. The method 
	/// can be used without specifying the generic parameters, because C#
	/// compiler can usually infer the actual types.
	/// </summary>
	/// <param name="item1">First element of the tuple</param>
	/// <param name="second">Second element of the tuple</param>
	/// <returns>A newly created tuple</returns>
	public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 second)
	{
		return new Tuple<T1, T2>(item1, second);
	}
}

// --------------------------------------------------------------------------
// Section 3.2.2: Implementing tuple in C#

// Listing 3.7: Implementing the tuple type in C#

/// <summary>
/// Represents a functional tuple that can be used to store 
/// two values of different types inside one object.
/// </summary>
/// <typeparam name="T1">The type of the first element</typeparam>
/// <typeparam name="T2">The type of the second element</typeparam>
public sealed class Tuple<T1, T2>
{
	private T1 item1;
	private T2 item2;
	
	/// <summary>
	/// Returns the first element of the tuple
	/// </summary>
	public T1 Item1
	{
		get { return item1; }
		set { if (areEqual(value, item1)) return; item1 = value; }
	}
	
	/// <summary>
	/// Returns the second element of the tuple
	/// </summary>
	public T2 Item2
	{
		get { return item2; }
		set { if (areEqual(value, item2)) return; item2 = value; }
	}
	
	/// <summary>
	/// Create a new tuple value
	/// </summary>
	/// <param name="item1">First element of the tuple</param>
	/// <param name="second">Second element of the tuple</param>
	public Tuple(T1 item1, T2 item2)
	{
		this.item1 = item1;
		this.item2 = item2;
	}
	
	public override string ToString()
	{
		return string.Format("Tuple({0}, {1})", Item1, Item2);
	}
	
	public override int GetHashCode()
	{
		int hash = 17;
		hash = hash * 23 + item1.GetHashCode();
		hash = hash * 23 + item2.GetHashCode();
		return hash;
	}
	
	public override bool Equals(object o)
	{
		if (o.GetType() != typeof(Tuple<T1, T2>)) {
			return false;
		}
		
		var other = (Tuple<T1, T2>) o;
		
		return this == other;
	}

	//http://msdn.microsoft.com/en-us/library/ms173147(v=vs.80).aspx
	public static bool operator==(Tuple<T1, T2> a, Tuple<T1, T2> b)
	{
		// If both are null, or both are same instance, return true.
		if (System.Object.ReferenceEquals(a, b))
		{
			return true;
		}
		
		// If one is null, but not both, return false.
		if (((object)a == null) || ((object)b == null))
		{
			return false;
		}

		return a.item1.Equals(b.item1)
			&& a.item2.Equals(b.item2);
	}

	private static bool areEqual<T1orT2>(T1orT2 a, T1orT2 b)
	{
		return a.Equals(b);
	}
	
	public static bool operator!=(Tuple<T1, T2> a, Tuple<T1, T2> b)
	{
		return !(a == b);
	}
	
	public void Unpack(Action<T1, T2> unpackerDelegate)
	{
		unpackerDelegate(Item1, Item2);
	}
}