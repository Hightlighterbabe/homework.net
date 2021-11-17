using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace homework7._1
{
    public static class HtmlContentExtensions
    {
        public static IHtmlContent MyEditorForModel(this IHtmlHelper helper)
        {
            FormContent a;
            if (helper.ViewData.Model != null)
                a = new FormContent(helper.ViewData.Model);
            else
                a = new FormContent(helper.ViewData.ModelMetadata.ModelType.GetConstructor(Type.EmptyTypes)?.
                    Invoke(Array.Empty<object>()));
            return a;
        }

        private class FormContent : IHtmlContent
        {
            private readonly string _resultContent;

            public FormContent(object model)
            {
                _resultContent = CreateContent(model.GetType().GetProperties(), model);
            }

            private static string CreateContent(PropertyInfo[] propertyInfos, object model)
            {
                List<string> b = new List<string>();
                foreach (var t in propertyInfos)
                {
                    b.Add(CreateHeaderForProperty(t) +
                          "<div class=\"editor-field\">" +
                          CreateBodyForProperty(t, model) +
                          "</div>");
                }

                string c ="";
                for (int i = 0; i < b.Count; i++)
                {
                    c += b[i];
                }
                return c;
            }

            private static string CreateHeaderForProperty(PropertyInfo prop)
            {
                var s = $"<div class=\"editor-label\"><label for=\"{prop.Name}\">" +
                        $"{((DisplayAttribute) prop.GetCustomAttribute(typeof(DisplayAttribute)))?.Name ?? FromCamelCase(prop.Name)}" +
                        $"</label></div>";
                return s;
            }

            private static string FromCamelCase(string str)
            {
                var e = str.Skip(1).Aggregate(str[0].ToString(),
                    (current, t) => current + (char.IsUpper(t) ? $" {char.ToLower(t)}" : t));
                return e;
            }

            private static string CreateBodyForProperty(PropertyInfo prop, object model)
            {
                var input = CreateInput(prop) + CreateSpan(prop, model);
                return input;
            }

            private static string CreateInput(PropertyInfo prop)
            {
                var selectClassFormGroup = prop.PropertyType.IsAssignableTo(typeof(Enum))
                    ? "<select class=\"form-group\">"
                      + $"<option selected>{prop.Name}</option>"
                      + prop.PropertyType
                          .GetFields()
                          .Where(m => m.Name != "value__")
                          .Select(field => $"<option value=\"{field.Name}\">{field.Name}</option>")
                          .Aggregate(string.Concat)
                      + "</select>"
                    : IsNumber(prop.PropertyType)
                        ? $"<input class=\"text-box single-line\" type=\"number\" name=\"{prop.Name}\">"
                        : $"<input class=\"text-box single-line\" type=\"text\" name=\"{prop.Name}\">";
                return selectClassFormGroup;
            }
            

            private static string CreateSpan(PropertyInfo prop, object model)
            {
                var res =
                    $"<span class=\"field-validation-error\" data-valmsg-for=\"{prop.Name}\" data-valmsg-replace=\"true\">";
                var attr = (ValidationAttribute) prop.GetCustomAttribute(typeof(ValidationAttribute));
                res += !attr?.IsValid(prop.GetValue(model))! ?? false
                    ? attr.ErrorMessage! ?? attr.FormatErrorMessage(prop.Name)
                    : string.Empty;
                res += $"</span>";
                return res;
            }

            private static readonly Type[] NumberTypesArray =
            {
                typeof(int), typeof(int?),
                typeof(uint), typeof(uint?),
                typeof(short), typeof(short?),
                typeof(ushort), typeof(ushort?),
                typeof(long), typeof(long?),
                typeof(ulong), typeof(ulong?),
                typeof(nint), typeof(nint?),
                typeof(byte), typeof(byte?),
                typeof(float), typeof(float?),
                typeof(double), typeof(double?),
                typeof(decimal), typeof(decimal?)
            };

            private static bool IsNumber(Type type)
            {
                var d = NumberTypesArray.Any(type.IsAssignableTo);
                return d;
            }

            void IHtmlContent.WriteTo(TextWriter writer, HtmlEncoder encoder)
            {
                writer.WriteLine(_resultContent);
            }
        }
    }
}