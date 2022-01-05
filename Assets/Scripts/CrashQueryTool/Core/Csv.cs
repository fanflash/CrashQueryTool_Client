using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using CrashQuery.Core;
using IGG.Framework.Config;

namespace IGG.Framework.Utils
{
    /// <summary>
    /// Author  gaofan
    /// Date    2021.8.21
    /// Desc    简单的创建CSV表
    ///         主要用于测试收集数据。
    /// </summary>
    public partial class Csv<TRow>: IList<TRow> where TRow : new()
    {
        private List<TRow> m_list;

        public int Count => m_list.Count;
        public bool IsReadOnly => false;

        public Csv(int capacity = 0)
        {
            m_list = new List<TRow>(capacity);
        }

        public void SetDataByCsv(string csvStr)
        {
            var csv = CsvHelper.CsvStrToList(csvStr);
            SetData(csv);
        }

        public void SetData(List<string[]> table)
        {
            for (int i = 1; i < table.Count; i++)
            {
                object row = new TRow();
                var csvRow = table[i];
                if (csvRow == null)
                {
                    csvRow = Array.Empty<string>();
                }

                for (int j = 0; j < g_headers.Length; j++)
                {
                    var h = g_headers[j];
                    if (h.Attribute.Index < csvRow.Length)
                    {
                        var colStr = csvRow[h.Attribute.Index];
                        var headType = h.Type;
                        if (headType == typeof(string))
                        {
                            h.SetValue(row, colStr);
                        }
                        else
                        {
                            var converter = Csv.GetDecoder(headType);
                            if (converter != null)
                            {
                                try
                                {
                                    //var value = converter.Convert(colStr);
                                    //还可以这样
                                    //var reference = __makererf(row);
                                    //fi.SetValueDirect(reference, value);
                                    //h.SetValue(row, value);
                                }
                                catch (Exception e)
                                {
                                    var message = $"{e.Message} type={headType.Name}, value={colStr}";
                                    throw new Exception(message);
                                }
                            }
                            else
                            {
                                h.SetDefaultValue(row);
                            }
                        }
                    }
                    else
                    {
                        h.SetDefaultValue(row);
                    }
                }
                Add((TRow)row);
            }
        }

        public void Add(TRow row)
        {
            m_list.Add(row);
        }

        public void Clear()
        {
            m_list.Clear();
        }

        public bool Contains(TRow item)
        {
            return m_list.Contains(item);
        }

        public void CopyTo(TRow[] array, int arrayIndex)
        {
            m_list.CopyTo(array, arrayIndex);
        }

        public bool Remove(TRow item)
        {
            return m_list.Remove(item);
        }
        
        public IEnumerator<TRow> GetEnumerator()
        {
            return m_list.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_list.GetEnumerator();
        }
        
        public int IndexOf(TRow item)
        {
            return m_list.IndexOf(item);
        }

        public void Insert(int index, TRow item)
        {
            m_list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            m_list.RemoveAt(index);
        }

        public TRow this[int index]
        {
            get => m_list[index];
            set => m_list[index] = value;
        }

        public override string ToString()
        {
            var sb = SbPool.Get();
            sb.Append(g_headerRow);
            for (int i = 0; i < m_list.Count; i++)
            {
                var row = m_list[i];
                sb.AppendLine();
                for (int j = 0; j < g_headers.Length; j++)
                {
                    ref var header = ref g_headers[j];
                    var value = header.GetValue(row);
                    if (value == null)
                    {
                        value = header.Attribute.Default;
                    }
                    sb.Append(CsvHelper.ConvStrToCsvFormat(value.ToString())).Append(',');
                }

                sb.Remove(sb.Length - 1, 1);
            }

            return SbPool.PutAndToStr(sb);
        }
    }

    public partial class Csv<TRow> where TRow : new()
    {
        private static HeaderVo[] g_headers;
        private static string g_headerRow;
        
        static Csv()
        {
            List<HeaderVo> headers = new List<HeaderVo>();
            var rowType = typeof(TRow);
            var bindingFlags= BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var fields = rowType.GetFields(bindingFlags);
            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var attribute = field.GetCustomAttribute<CsvHeaderAttribute>();
                if (attribute == null)
                {
                    continue;
                }

                var header = new HeaderVo();
                header.Attribute = attribute;
                header.Field = field;
                headers.Add(header);
            }

            var props = rowType.GetProperties(bindingFlags);
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                var attribute = prop.GetCustomAttribute<CsvHeaderAttribute>();
                if (attribute == null)
                {
                    continue;
                }
                var header = new HeaderVo();
                header.Attribute = attribute;
                header.Property = prop;
                headers.Add(header);
            }

            headers.Sort((a, b) => a.Attribute.Index - b.Attribute.Index);
            g_headers = headers.ToArray();
            if (g_headers.Length > 0)
            {
                var sb = SbPool.Get();
                sb.Append(CsvHelper.ConvStrToCsvFormat(g_headers[0].Title));
                for (int i = 1; i < g_headers.Length; i++)
                {
                    ref var header = ref g_headers[i];
                    sb.Append(',').Append( CsvHelper.ConvStrToCsvFormat(header.Title));
                }
                g_headerRow = SbPool.PutAndToStr(sb);
            }
            else
            {
                g_headerRow = "";
            }
        }


        
        struct HeaderVo
        {
            public CsvHeaderAttribute Attribute;
            public FieldInfo Field;
            public PropertyInfo Property;

            public string Title
            {
                get
                {
                    if (string.IsNullOrEmpty(Attribute.Title))
                    {
                        return Field == null ? Property.Name : Field.Name;
                    }
                    else
                    {
                        return Attribute.Title;
                    }
                }
            }

            public Type Type
            {
                get
                {
                    if (Field == null)
                    {
                        return Property.PropertyType;
                    }

                    return Field.FieldType;
                }
            }

            public object GetValue(object obj)
            {
                return Field == null ? Property.GetValue(obj) : Field.GetValue(obj);
            }

            public void SetValue(object obj, object value)
            {
                if (Field == null)
                {
                    Property.SetValue(obj,value);
                }
                else
                {
                    Field.SetValue(obj, value);
                }
            }

            public void SetDefaultValue(object obj)
            {
                SetValue(obj,Attribute.Default);
            }
        }
    }

    public static class Csv
    {
        private static Dictionary<Type, IStringConverter> g_converterMap = new Dictionary<Type, IStringConverter>();
        
        static Csv()
        {
            SetDecoder<ulong>(new GeneralStrConv<ulong>(Convert.ToUInt64));
            SetDecoder<int>(new GeneralStrConv<int>(Convert.ToInt32));
            SetDecoder<uint>(new GeneralStrConv<uint>(Convert.ToUInt32));
        }

        public static void SetDecoder(Type type, IStringConverter converter)
        {
            g_converterMap[type] = converter;
        }

        public static void SetDecoder<T>(IStringConverter converter)
        {
            SetDecoder(typeof(T), converter);
        }

        public static IStringConverter GetDecoder(Type type)
        {
            IStringConverter converter;
            g_converterMap.TryGetValue(type, out converter);
            return converter;
        }
        
        public static Csv<TRow> New<TRow>(string csvPath, Encoding encoding = null) where TRow:new()
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            string csvStr = File.ReadAllText(csvPath, encoding);
            var csv = new Csv<TRow>();
            csv.SetDataByCsv(csvStr);
            return csv;
        }
    }

    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class CsvHeaderAttribute:Attribute
    {
        public readonly int Index;
        public readonly string Title;
        public readonly object Default;

        public CsvHeaderAttribute(string title, int index = int.MaxValue, object defaultValue = null)
        {
            Title = title;
            Index = index;
            Default = defaultValue == null ? "" : defaultValue;
        }

        public CsvHeaderAttribute(int index = int.MaxValue, object defaultValue = default)
        {
            Index = index;
            Default = defaultValue;
        }
    }

    public interface IStringConverter
    {
        //public object Convert(string value);
    }

    public class GeneralStrConv<T>:IStringConverter
    {
        private Func<string, T> m_handler;
        public GeneralStrConv(Func<string,T> handler)
        {
            m_handler = handler;
        }

        public object Convert(string value)
        {
            return m_handler(value);
        }
    }
}