using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjmng.Models;
using prjmng.ViewModels;

namespace prjmng.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController: ControllerBase
    {
        ApplicationDbContext _ctx;
        public ProjectTaskController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
         // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> Get(int projectId, int? parentTaskId)
        {
            var projectTasks = await _ctx.ProjectTasks.Where(pt=>pt.TaskProject.Id==projectId && 
                                                             parentTaskId==pt.ParentTask.Id)
                                                .Select(pt=> new {Id = pt.Id,
                                                                    name = pt.Name,
                                                                    startDate = pt.StartDate,
                                                                    endDate = pt.EndDate,
                                                                    state = pt.State,
                                                                    subtasks = pt.SubtaskAmount})
                                                .ToListAsync();
            return projectTasks;
        }

        // GET api/values/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<object>> Details(int id)
        {
            var pt = await _ctx.ProjectTasks.FindAsync(id);
            if(pt!=null)
                return Ok(new {name = pt.Name, startDate = pt.StartDate, endDate = pt.EndDate, state =pt.State });
            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProjectTaskViewModel projectTaskViewModel)
        {
            if(ModelState.IsValid)
            {
                ProjectTask pt = new ProjectTask(){
                    State = projectTaskViewModel.State??TaskState.NotCompleted,
                    Name = projectTaskViewModel.Name,
                    StartDate = projectTaskViewModel.StartDate,
                    EndDate = projectTaskViewModel.EndDate
                };
                if(projectTaskViewModel.ParentTaskId.HasValue)
                {
                    pt.ParentTask = await _ctx.ProjectTasks.FindAsync(projectTaskViewModel.ParentTaskId);
                }
                if(projectTaskViewModel.TaskAssigneeId.HasValue)
                {
                    pt.TaskAssignee = await _ctx.Assignees.FindAsync(projectTaskViewModel.TaskAssigneeId);
                }
                pt.TaskProject = await _ctx.Projects.FindAsync(projectTaskViewModel.ProjectId);
                await _ctx.ProjectTasks.AddAsync(pt);
                await _ctx.SaveChangesAsync();
                return Ok(new {id = pt.Id,name = pt.Name, startDate = pt.StartDate, endDate = pt.EndDate, state =pt.State });
            }
            else 
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProjectTaskViewModel projectTaskViewModel)
        {
            if(ModelState.IsValid)
            {
                ProjectTask pt = new ProjectTask(){
                    Id = id,
                    State = projectTaskViewModel.State??TaskState.NotCompleted,
                    Name = projectTaskViewModel.Name,
                    StartDate = projectTaskViewModel.StartDate,
                    EndDate = projectTaskViewModel.EndDate
                };
                if(projectTaskViewModel.ParentTaskId.HasValue)
                {
                    pt.ParentTask = await _ctx.ProjectTasks.FindAsync(projectTaskViewModel.ParentTaskId);
                }
                if(projectTaskViewModel.TaskAssigneeId.HasValue)
                {
                    pt.TaskAssignee = await _ctx.Assignees.FindAsync(projectTaskViewModel.TaskAssigneeId);
                }
                pt.TaskProject = await _ctx.Projects.FindAsync(projectTaskViewModel.ProjectId);
                _ctx.ProjectTasks.Update(pt);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            ProjectTask pt = await _ctx.ProjectTasks.FindAsync(id);
            if(pt!=null)
            {
                _ctx.ProjectTasks.Remove(pt);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}