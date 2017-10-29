using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.ModelBinding;

namespace Notes.API
{
    /// <summary>
    /// Валидатор состояния модели
    /// </summary>
    public static class ModelStateValidator
    {
        /// <summary>
        /// Проверяет валидность модели
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns>Возвращает строку, содержащую ошибки в модели. Если ошибок нет возвращает null.</returns>
        public static string Validate(ModelStateDictionary modelState)
        {
            if (modelState.IsValid) return null;
            StringBuilder stringBuilder = new StringBuilder("Не все обязательные поля заданы:");

            foreach (var value in modelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    stringBuilder.Append(error.ErrorMessage);
                }
            }
            return stringBuilder.ToString();
        }
    }
}