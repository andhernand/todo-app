﻿using TodoApp.Server.Endpoints.Todos;

namespace TodoApp.Server.Endpoints;

public static class EndpointsMapper
{
    public static void MapApiEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapTodosEndpoints();
    }
}