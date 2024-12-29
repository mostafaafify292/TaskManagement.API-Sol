using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Dtos;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repository.Contract;
using TaskManagement.Repository;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public TasksController(ITaskRepository repository , IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("GetTasks")]
        public async Task<ActionResult<IReadOnlyList<TaskModelDto>>> GetTasks(string? status = null)
        {
            
            if (!string.IsNullOrEmpty(status))
            {
                var tasksByStatus =await _repository.GetTasksByStatusAsync(status);
                var data = _mapper.Map<IReadOnlyList<TaskModel>, IReadOnlyList<TaskModelDto>>(tasksByStatus);
                return Ok(data);
            }
            var tasks = await _repository.GetAllAsync();
            var data1 = _mapper.Map<IReadOnlyList<TaskModel>, IReadOnlyList<TaskModelDto>>(tasks);
            return Ok(data1);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModelDto>> GetTask(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound("Task not found .");
            }
            var data1 = _mapper.Map<TaskModel , TaskModelDto>(task);

            return Ok(task);
        }
        [HttpPost("CreateTask")]
        public async Task<ActionResult<TaskModelDto>> CreateTask(TaskModelDto task)
        {

            var data = _mapper.Map< TaskModelDto , TaskModel>(task);
            await _repository.AddAsync(data);
            var data0 = _mapper.Map< TaskModel , TaskModelDto>(data);
            return Ok(data0);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskModelDto>> UpdateTask(int id, TaskModelDto task)
        {
            if (id != task.Id)
            {
                return BadRequest("Task ID mismatch.");
            }

            var existingTask = await _repository.GetByIdAsync(id);
            if (existingTask == null)
            {
                return NotFound("Task not found.");
            }
            _mapper.Map(task, existingTask);

            await _repository.UpdateAsync(existingTask);
            var data = _mapper.Map< TaskModel , TaskModelDto>(existingTask);
            return Ok(data);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            await _repository.DeleteAsync(id);
            return Ok("Task deleted successfully.");
        }
    }
}
