﻿using System.Security.Claims;
using TodoApp.API.Dtos;
using TodoApp.API.Dtos.Requests;
using TodoApp.API.Dtos.Results;
using TodoApp.API.Mappers.Interfaces;
using TodoApp.DAL.Entities;

namespace TodoApp.API.Mappers
{
    public class TodoItemMapper: ITodoItemMapper
    {
        private readonly Guid accountId;
        public TodoItemMapper(IHttpContextAccessor httpContextAccessor)
        {
            accountId = new Guid(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
        public TodoItemResultDto Map(TodoItem entity)
        {
            return new TodoItemResultDto
            {
                Id = entity.Id,
                Type = entity.Type,
                Title = entity.Title,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                Due = entity.Due,
                CompletedAt = entity.CompletedAt,
                ImageIds = entity.Images.Any() ? entity.Images.Select(i => i.Id) : null,
            };
        }
        public List<TodoItemResultDto> Map(IEnumerable<TodoItem> entities)
        {
            return entities.Select(Map).ToList();
        }
        public TodoItem Map(TodoItemRequestDto dto)
        {
            return new TodoItem
            {
                Type = dto.Type!,
                Title = dto.Title!,
                Description = dto.Description,
                CreatedAt = DateTime.Now,
                Due = dto.Due,
                CompletedAt = dto.CompletedAt,
                AccountId = accountId
                
            };
        }
        public void ProjectTo(TodoItemRequestDto from, TodoItem to)
        {
            to.Type = from.Type!;
            to.Title = from.Title!;
            to.Description = from.Description;
            to.Due = from.Due;
            to.CompletedAt = from.CompletedAt;
        }
    }
}
