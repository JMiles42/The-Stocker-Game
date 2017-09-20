using System;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace JMiles42.Editor.PropertyDrawers
{
	public static class PropertyDrawerUtility
	{
		public static T GetActualObject<T>(FieldInfo fieldInfo, SerializedProperty property) where T: class
		{
			var obj = fieldInfo.GetValue(property.serializedObject.targetObject);
			if (obj == null)
			{
				return null;
			}

			T actualObject = null;
			if (obj.GetType().IsArray)
			{
				var index = Convert.ToInt32(new string(property.propertyPath.Where(char.IsDigit).ToArray()));
				actualObject = ((T[]) obj)[index];
			}
			else
			{
				actualObject = obj as T;
			}
			return actualObject;
		}

		public static T GetActualObject<T>(this PropertyDrawer drawer, SerializedProperty property) where T: class
		{
			var obj = drawer.fieldInfo.GetValue(property.serializedObject.targetObject);
			if (obj == null)
			{
				return null;
			}

			T actualObject = null;
			if (obj.GetType().IsArray)
			{
				var index = Convert.ToInt32(new string(property.propertyPath.Where(char.IsDigit).ToArray()));
				actualObject = ((T[]) obj)[index];
			}
			else
			{
				actualObject = obj as T;
			}
			return actualObject;
		}

		/// <summary>
		/// Gets the object the property represents.
		/// </summary>
		/// <param name="prop"></param>
		/// <returns></returns>
		public static object GetTargetObjectOfProperty(SerializedProperty prop)
		{
			var path = prop.propertyPath.Replace(".Array.data[", "[");
			object obj = prop.serializedObject.targetObject;
			var elements = path.Split('.');
			foreach (var element in elements)
			{
				if (element.Contains("["))
				{
					var elementName = element.Substring(0, element.IndexOf("["));
					var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
					obj = GetValue_Imp(obj, elementName, index);
				}
				else
				{
					obj = GetValue_Imp(obj, element);
				}
			}
			return obj;
		}

		public static T GetTargetObjectOfProperty<T>(this SerializedProperty prop)
		{
			var path = prop.propertyPath.Replace(".Array.data[", "[");
			object obj = prop.serializedObject.targetObject;
			var elements = path.Split('.');
			foreach (var element in elements)
			{
				if (element.Contains("["))
				{
					var elementName = element.Substring(0, element.IndexOf("["));
					var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
					obj = GetValue_Imp(obj, elementName, index);
				}
				else
				{
					obj = GetValue_Imp(obj, element);
				}
			}
			return (T) obj;
		}

		private static object GetValue_Imp(object source, string name)
		{
			if (source == null)
				return null;
			var type = source.GetType();

			while (type != null)
			{
				var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
				if (f != null)
					return f.GetValue(source);

				var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
				if (p != null)
					return p.GetValue(source, null);

				type = type.BaseType;
			}
			return null;
		}

		private static object GetValue_Imp(object source, string name, int index)
		{
			var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
			if (enumerable == null)
				return null;
			var enm = enumerable.GetEnumerator();
			//while (index-- >= 0)
			//    enm.MoveNext();
			//return enm.Current;

			for (int i = 0; i <= index; i++)
			{
				if (!enm.MoveNext())
					return null;
			}
			return enm.Current;
		}
	}
}