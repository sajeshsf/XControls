using System;

namespace XControls.Helpers.Extensions
{
    public static class ExceptionHelpers
    {
        public static string GetStringFromException(this Exception ex) => ex?.Message != null ? $"{ex?.Message}, {(ex?.InnerException)?.GetStringFromException()}" : ".";
    }
}
