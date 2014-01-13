using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics;

namespace MigraineDiaryMVC.Helpers
{
	public sealed class EnumDescription<TEnum> where TEnum : struct, IConvertible
	{
		public IDictionary<TEnum, string> EnumToDescription { get; private set; }
		public IDictionary<string, TEnum> DescriptionToEnum { get; private set; }

		public EnumDescription()
		{
			this.EnumToDescription = EnumToDictionary.CreateEnumToDescriptionDictionary<TEnum>();
			this.DescriptionToEnum = EnumToDictionary.ReverseEnumToDescriptionDictionary(this.EnumToDescription);
		}
	}

	public class EnumToDictionary
	{
		public static Dictionary<TEnum, string> CreateEnumToDescriptionDictionary<TEnum>() where TEnum : struct, IConvertible
		{
			var translator = new DescriptionAttributes<TEnum>();
			return translator.EnumToDescriptions;
		}

		public static Dictionary<string, TEnum> ReverseEnumToDescriptionDictionary<TEnum>(IDictionary<TEnum,string> enumToDescriptionDictionary) where TEnum : struct, IConvertible
		{
			var descriptionToEnumDictionary = new Dictionary<string, TEnum>();
			foreach (var kvp in enumToDescriptionDictionary)
			{
				descriptionToEnumDictionary.Add(kvp.Value, kvp.Key);
			}

			return descriptionToEnumDictionary;
		}
	}

	public abstract class AbstractAttributes<TEnum, TAttribute>
		where TEnum : struct, IConvertible
		where TAttribute : Attribute
	{
		protected List<TAttribute> Attributes { get; private set; }
		protected Dictionary<TEnum,TAttribute> EnumToAttributes { get; private set; }

		public AbstractAttributes()
		{
			this.Attributes = new List<TAttribute>();
    
			var attributesToAdd = typeof(TEnum).GetMembers().SelectMany(m => m.GetCustomAttributes(typeof(TAttribute), false)).OfType<TAttribute>();
			this.Attributes.AddRange(attributesToAdd);
    
			var attributeKVPsToAdd = typeof(TEnum).GetEnumerations().Select
			(
				fi => new 
				{
					EnumValue = (TEnum)Enum.Parse(typeof(TEnum), fi.Name),
					Attribute = fi.GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().ToList().FirstOrDefault()
				}
			);

			this.EnumToAttributes = attributeKVPsToAdd.ToDictionary(kvp => kvp.EnumValue, kvp => kvp.Attribute);
		}
	}

	public class DescriptionAttributes<TEnum> : AbstractAttributes<TEnum, DescriptionAttribute> where TEnum : struct, IConvertible
	{
		public List<string> Descriptions { get; private set; }
		public Dictionary<TEnum, string> EnumToDescriptions { get; private set; }

		public DescriptionAttributes()
		{
			Descriptions = Attributes.Select(x => x.Description).ToList();
			EnumToDescriptions = new Dictionary<TEnum, string>
			(
				EnumToAttributes.Select(kvp => new { Key = kvp.Key, Value = kvp.Value.Description }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
			);
		}
	}

	public static class EnumUtilityMethods
	{
		public static IEnumerable<FieldInfo> GetEnumerations(this Type enumType)
		{
			Debug.Assert(enumType.IsEnum);

			var enumMembers = enumType.GetFields().Where(m => m.FieldType.IsEnum);
			return enumMembers;
		}
	}
}