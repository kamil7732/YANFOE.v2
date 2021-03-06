﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <license>
//   This software is licensed under a Creative Commons License
//   Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0) 
//   http://creativecommons.org/licenses/by-nc-sa/3.0/
//   See this page: http://www.yanfoe.com/license
//   For any reuse or distribution, you must make clear to others the 
//   license terms of this work.  
// </license>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Tools.Extentions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text.RegularExpressions;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.Tools.Clean;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Models;

    /// <summary>
    /// C# Language extentions methods used within Y.Framework
    /// </summary>
   public static class Extensions
    {
        #region Public Methods

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="bindingList">The binding list.</param>
        /// <param name="collection">The collection.</param>
        public static void AddRange<T>(this BindingList<T> bindingList, IEnumerable<T> collection)
        {
            if (bindingList == null)
            {
                bindingList = new BindingList<T>();
            }

            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            foreach (T item in collection)
            {
                bindingList.Add(item);
            }
        }

        /// <summary>
        /// Clean a string
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The cleaned string
        /// </returns>
        public static string Clean(this string value)
        {
            return Text.FullClean(value).Replace("...", string.Empty).Trim();
        }

        /// <summary>
        /// Clones the specified source.
        /// </summary>
        /// <typeparam name="T">
        /// Generic type
        /// </typeparam>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// Cloned object
        /// </returns>
        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException(@"The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Determines whether [contains] [any value within the Enumerable collection].
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="values">
        /// The values to check against.
        /// </param>
        /// <returns>
        /// <c>true</c> if [contains] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this string value, IEnumerable<string> values)
        {
            return values.Any(s => s.Contains(value));
        }

        /// <summary>
        /// Convert a string into most common types
        /// </summary>
        /// <param name="value">
        /// The string to convert.
        /// </param>
        /// <returns>
        /// The return value.
        /// </returns>
        public static dynamic ConvertStringToType(this string value)
        {
            string logCatagory = "String > ConvertStringToType > " + value;

            try
            {
                // Check if Bool
                bool outBool;
                bool tryBool = bool.TryParse(value, out outBool);
                if (tryBool)
                {
                    return tryBool; // Return double
                }

                // Check if Double
                double outDouble;
                bool tryDouble = double.TryParse(value, out outDouble);
                if (tryDouble)
                {
                    return outDouble; // Return double
                }

                // Check if Int
                int outInt;
                bool tryInt = int.TryParse(value, out outInt);
                if (tryInt)
                {
                    return outInt; // Return int
                }

                // Check if DateTime
                DateTime outDate;
                bool tryDate = DateTime.TryParse(value, out outDate);
                if (tryDate)
                {
                    return outDate; // Return datetime
                }

                // Must be string, process...

                // Return plain string.
                return value;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, logCatagory, ex.Message);
            }

            return null;
        }

        /// <summary>
        /// The get number from a string
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The first found number
        /// </returns>
        public static int GetNumber(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return -1;
            }

            string v = Regex.Match(value, @"\d{1,2}").Groups[0].Value;
            return Convert.ToInt16(v);
        }

        /// <summary>
        /// Get numbers from string
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>All found numbers</returns>
        public static List<int> GetNumbers(this string value)
        {
            var output = new List<int>();

            if (string.IsNullOrEmpty(value))
            {
                return output;
            }

            MatchCollection matches = Regex.Matches(value, @"\d{1,2}");

            foreach (Match number in matches)
            {
                output.Add(Convert.ToInt16(number.Groups[0].Value));
            }

            return output;
        }

        /// <summary>
        /// Check if value is filled
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The value is filled.
        /// </returns>
        public static bool IsFilled(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Check if value is filled
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The value is filled.
        /// </returns>
        public static bool IsFilled(this int value)
        {
            return value > -1;
        }

        /// <summary>
        /// Check if value is filled
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The value is filled.
        /// </returns>
        public static bool IsFilled(this double value)
        {
            return value > -1;
        }

        /// <summary>
        /// Check if value is filled
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The value is filled.
        /// </returns>
        public static bool IsFilled(this BindingList<string> value)
        {
            return value.Count > 0;
        }

        /// <summary>
        /// Check if value is filled
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The value is filled.
        /// </returns>
        public static bool IsFilled(this BindingList<PersonModel> value)
        {
            return value.Count > 0;
        }

        /// <summary>
        /// Check if value is filled
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The value is filled.
        /// </returns>
        public static bool IsFilled(this BindingList<ImageDetailsModel> value)
        {
            return value.Count > 0;
        }

        /// <summary>
        /// Check if value is filled
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The value is filled.
        /// </returns>
        public static bool IsFilled(this BindingList<TrailerDetailsModel> value)
        {
            return value.Count > 0;
        }

        /// <summary>
        /// Check if value is filled
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The value is filled.
        /// </returns>
        public static bool IsFilled(this Dictionary<ImageSizeType, BindingList<ImageDetailsModel>> value)
        {
            return value.Count > 0;
        }

        /// <summary>
        /// Check if value is filled
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The is filled.
        /// </returns>
        public static bool IsFilled(this DateTime value)
        {
            return value.Year > 1800;
        }

        /// <summary>
        /// Remove character returns from string
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The remove character return.
        /// </returns>
        public static string RemoveCharacterReturn(this string value)
        {
            value = Regex.Replace(value, "\n", string.Empty);
            value = Regex.Replace(value, "\r", string.Empty);

            return value;
        }

        /// <summary>
        /// Removes any instances of 2 or more spaces in a row.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The remove extra white space.
        /// </returns>
        public static string RemoveExtraWhiteSpace(this string value)
        {
            return Regex.Replace(value, @"\s{2,}", " ");
        }

        /// <summary>
        /// Replaces the specified value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="replaceWith">
        /// The replace with.
        /// </param>
        /// <returns>
        /// The replace.
        /// </returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", 
            "SA1616:ElementReturnValueDocumentationMustHaveText", Justification = "Reviewed. Suppression is OK here.")]
        public static string Replace(this string value, string[] values, string replaceWith)
        {
            return values.Aggregate(value, (current, s) => current.Replace(s, replaceWith));
        }

        /// <summary>
        /// Replaces values found in the string array with string.empty.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="strings">
        /// The strings to replace with nothing.
        /// </param>
        /// <returns>
        /// A processed string.
        /// </returns>
        public static string ReplaceWithStringEmpty(this string value, IEnumerable<string> strings)
        {
            return strings.Aggregate(value, (current, s) => current.Replace(s, string.Empty));
        }

        /// <summary>
        /// The to binding list.
        /// </summary>
        /// <param name="enumerableList">
        /// The enumerable list.
        /// </param>
        /// <typeparam name="T">
        /// Generic type
        /// </typeparam>
        /// <returns>
        /// Generic bindinglist
        /// </returns>
        public static BindingList<T> ToBindingList<T>(this IEnumerable<T> enumerableList)
        {
            if (enumerableList != null)
            {
                // create an emtpy observable collection object
                var bindingList = new BindingList<T>();

                // loop through all the records and add to observable collection object
                foreach (T item in enumerableList)
                {
                    bindingList.Add(item);
                }

                // return the populated observable collection
                return bindingList;
            }

            return null;
        }

        /// <summary>
        /// Converts a string array to a List{string}
        /// </summary>
        /// <param name="value">
        /// The string array
        /// </param>
        /// <returns>
        /// The resulting List{string}
        /// </returns>
        public static BindingList<string> ToBindingStringList(this IEnumerable<string> value)
        {
            return value.Select(s => s.Trim()).ToBindingList();
        }

        /// <summary>
        /// To binding string list.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="delimiter">
        /// The delimiter.
        /// </param>
        /// <returns>
        /// BindingList string
        /// </returns>
        public static BindingList<string> ToBindingStringList(this string value, char delimiter = ',')
        {
            var f = from s in value.Split(delimiter) where s != string.Empty select s;
            return f.ToBindingStringList();
        }

        /// <summary>
        /// To comma list.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="delimited">
        /// The delimited.
        /// </param>
        /// <returns>
        /// Comma list.
        /// </returns>
        public static string ToCommaList(this BindingList<string> value, char delimited = ',')
        {
            return string.Join(delimited.ToString(), value.ToList());
        }

        /// <summary>
        /// Will try and convert a string to a region specific double.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The to double.
        /// </returns>
        public static double ToDouble(this string value)
        {
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            return Convert.ToDouble(value.Replace(new[] { ",", ".", }, decimalSeparator));
        }

        /// <summary>
        /// Will try and convert a string to an Int. If fails will return -1
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// Parsed string file
        /// </returns>
        public static int ToInt(this string value)
        {
            int i;
            int.TryParse(value, out i);
            return i;
        }

        /// <summary>
        /// Convert to long.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// Long value
        /// </returns>
        public static long ToLong(this string value)
        {
            long i;
            long.TryParse(value, out i);
            return i;
        }

        /// <summary>
        /// The to person list.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// Person list
        /// </returns>
        public static BindingList<PersonModel> ToPersonList(this BindingList<string> value)
        {
            return value.Select(p => new PersonModel(p.Trim())).ToBindingList();
        }

        /// <summary>
        /// Convert to person list.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="delimiter">
        /// The delimiter.
        /// </param>
        /// <param name="thumbPreUrl">
        /// The thumb pre url.
        /// </param>
        /// <returns>
        /// Person List
        /// </returns>
        public static BindingList<PersonModel> ToPersonList(
            this string value, char delimiter = ',', string thumbPreUrl = null)
        {
            string[] p = (from v in value.Split(delimiter) where v != string.Empty select v).ToArray();

            return p.ToPersonList(thumbPreUrl);
        }

        /// <summary>
        /// Convert string to PersonList
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="thumbPreUrl">
        /// The thumb pre url.
        /// </param>
        /// <returns>
        /// PersonModel Bindinglist
        /// </returns>
        public static BindingList<PersonModel> ToPersonList(this string[] value, string thumbPreUrl = null)
        {
            var list = new BindingList<PersonModel>();

            foreach (string l in value)
            {
                list.Add(new PersonModel(l));
            }

            return list;
        }

        /// <summary>
        /// Converts PersonModel BindingList to string
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="delimited">
        /// The delimited.
        /// </param>
        /// <returns>
        /// The to string.
        /// </returns>
        public static string ToString(this BindingList<PersonModel> value, char delimited = ',')
        {
            return string.Join(delimited.ToString(), (from v in value select v.Name).ToList());
        }

        #endregion
    }
}