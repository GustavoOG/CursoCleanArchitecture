using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanArchitecture.Api.Documentation
{
    public sealed class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiDescriptionProvider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiDescriptionProvider)
        {
            _apiDescriptionProvider = apiDescriptionProvider;
        }

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var documentation in _apiDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(documentation.GroupName, CreateDocumentation(documentation));
            }
        }

        private static OpenApiInfo CreateDocumentation(ApiVersionDescription apiVersionDescription)
        {
            var openApiInfo = new OpenApiInfo()
            {
                Title = $"CleanArchitecture.Api v{apiVersionDescription.ApiVersion.ToString()}",
                Version = apiVersionDescription.ApiVersion.ToString(),
            };
            if (apiVersionDescription.IsDeprecated)
            {
                openApiInfo.Description += " This API version has been deprecated.";
            }
            return openApiInfo;
        }
    }
}
