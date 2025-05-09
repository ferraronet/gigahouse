﻿using GigaHouse.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GigaHouse.TaskList.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected Guid GetCurrentUserId() =>
                Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException("Usuário não autenticado."));

        protected string GetCurrentUserEmail() =>
            User.FindFirst(ClaimTypes.Email)?.Value ?? throw new UnauthorizedAccessException("Usuário não autenticado.");

        protected IActionResult Ok<T>(T data) =>
                base.Ok(new ApiResponseWithData<T> { Data = data, Success = true });

        protected IActionResult Created<T>(string routeName, object routeValues, T data) =>
            base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, Success = true });

        protected IActionResult BadRequest(string message) =>
            base.BadRequest(new ApiResponse { Message = message, Success = false });

        protected IActionResult NotFound(string message = "Resource not found") =>
            base.NotFound(new ApiResponse { Message = message, Success = false });

        //protected IActionResult OkPaginated<T>(PaginatedList<T> pagedList) =>
        //        Ok(new PaginatedResponse<T>
        //        {
        //            Data = pagedList,
        //            CurrentPage = pagedList.CurrentPage,
        //            TotalPages = pagedList.TotalPages,
        //            TotalCount = pagedList.TotalCount,
        //            Success = true
        //        });
    }

}
