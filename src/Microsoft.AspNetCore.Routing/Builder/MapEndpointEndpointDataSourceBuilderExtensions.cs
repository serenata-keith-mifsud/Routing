// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    public static class MapEndpointEndpointDataSourceBuilderExtensions
    {
        public static IApplyEndpointBuilder MapEndpoint(
            this EndpointDataSourcesBuilder builder,
            RequestDelegate requestDelegate,
            string pattern,
            string displayName)
        {
            return MapEndpoint(builder, requestDelegate, pattern, displayName, metadata: null);
        }

        public static IApplyEndpointBuilder MapEndpoint(
            this EndpointDataSourcesBuilder builder,
            RequestDelegate requestDelegate,
            RoutePattern pattern,
            string displayName)
        {
            return MapEndpoint(builder, requestDelegate, pattern, displayName, metadata: null);
        }

        public static IApplyEndpointBuilder MapEndpoint(
            this EndpointDataSourcesBuilder builder,
            RequestDelegate requestDelegate,
            string pattern,
            string displayName,
            params object[] metadata)
        {
            return MapEndpoint(builder, requestDelegate, RoutePatternFactory.Parse(pattern), displayName, metadata);
        }

        public static IApplyEndpointBuilder MapEndpoint(
            this EndpointDataSourcesBuilder builder,
            RequestDelegate requestDelegate,
            RoutePattern pattern,
            string displayName,
            params object[] metadata)
        {
            const int defaultOrder = 0;

            var endpointBuilder = new RouteEndpointBuilder(
               requestDelegate,
               pattern,
               defaultOrder);
            endpointBuilder.DisplayName = displayName;
            if (metadata != null)
            {
                foreach (var item in metadata)
                {
                    endpointBuilder.Metadata.Add(item);
                }
            }

            var builderEndpointDataSource = new BuilderEndpointDataSource(new[] { endpointBuilder });

            builder.EndpointDataSources.Add(builderEndpointDataSource);

            return builderEndpointDataSource;
        }
    }
}
