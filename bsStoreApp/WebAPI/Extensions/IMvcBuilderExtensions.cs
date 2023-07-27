using WebAPI.Utilities.Formatters;

namespace WebAPI.Extensions
{
    public static class IMvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomCvsFormatter(this IMvcBuilder builder) =>
            builder.AddMvcOptions(config =>
                config.OutputFormatters
            .Add(new CsvOutputFormatter())
            );
    }
}
